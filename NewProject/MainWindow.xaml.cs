using BasicAssembly;
using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using System.IO;
using DevExpress.Xpf.Docking;
using System.Reflection;
using System.Data;
using DevExpress.Mvvm;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Threading;
using System.IO.Ports;
using System.Threading;

namespace NewProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : BaseWindow, INotifyPropertyChanged
    {

        private MSSqlConnection dbCon = new MSSqlConnection();

        public IList<MenuItemVM> MenuItems { get; set; }
        public MenuItemVM SelectedMenuItem { get; set; }

        private string lsProgramTitle = "Clant2";
        private string lsVersion = "Ver-200526-01";
        private bool bMenuSelect = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            initializeMainPage();

        }

        private void initializeMainPage()
        {

            WinLogin fPopup = new WinLogin();

            if (fPopup.ShowDialog() != true)
            {
                this.Close();
                return;
            }


            GetMenuInfo();

            this.Title = lsProgramTitle + " [" + lsVersion + "]";
            BsiLoginUser.Content = " Login : " + Constants.loginUserName + "님";
            BsiLoginTime.Content = " " + Constants.loginTime + " ";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void GetMenuInfo()
        {
            string sSql = "";

            sSql += " with mnu as (";
           sSql += "    SELECT 1 level_code,";
            sSql += "             rtrim(dbo.rpad(' ', 1 * 4, '.') + '1') level_text,";
            sSql += "             menu_category,";
            sSql += "			PAGE_DUTY,";
            sSql += "             menu_id,";
            sSql += "             menu_name,";
            sSql += "             upper_menu_id,";
            sSql += "             page_type,";
            sSql += "             page_id,";
            sSql += "             menu_level,";
            sSql += "             remarks,";
            sSql += "             use_yn,";
            sSql += "             order_seq original_seq,";
            sSql += "             order_seq,";
            sSql += "             cast(order_seq as varchar) order_text,";
            sSql += "             update_date,";
            sSql += "             update_id";
            sSql += "        FROM tb_com_menu cmn";
            sSql += "       WHERE upper_menu_id = ''";
            sSql += "      UNION ALL";
            sSql += "      SELECT mnu.level_code + 1 level_code,";
            sSql += "             rtrim(dbo.rpad(' ', (mnu.level_code + 1) * 4, '.') + cast((mnu.level_code + 1) as varchar)) level_text,";
            sSql += "             xmn.menu_category,";
            sSql += "			xmn.PAGE_DUTY,";
            sSql += "             xmn.menu_id,";
            sSql += "             xmn.menu_name,";
            sSql += "             xmn.upper_menu_id,";
            sSql += "             xmn.page_type,";
            sSql += "             xmn.page_id,";
            sSql += "             xmn.menu_level,";
            sSql += "             xmn.remarks,";
            sSql += "             xmn.use_yn,";
            sSql += "             xmn.order_seq original_seq,";
            sSql += "             mnu.order_seq * 100 + xmn.order_seq  order_seq,";
           sSql += "             cast((mnu.order_seq * 100 + xmn.order_seq) as varchar) order_text,";
            sSql += "             xmn.update_date,";
            sSql += "             xmn.update_id";
            sSql += "        FROM tb_com_menu xmn,";
            sSql += "             mnu";
            sSql += "       WHERE xmn.upper_menu_id = mnu.menu_id";
            sSql += "         AND xmn.use_yn = 'Y'";
            sSql += "      )";
            sSql += "      SELECT*";
            sSql += "        FROM(";
            sSql += "      SELECT mnu.menu_category  Category,";
            sSql += "             mnu.menu_category  Module,";
            sSql += "             mnu.page_duty page_duty,"; 
            sSql += "             mnu.level_code     MenuLevel,";
            sSql += "             mnu.menu_id        MenuCode,";
            sSql += "             mnu.menu_name      MenuName,";
            sSql += "             mnu.page_type,";
            sSql += "             mnu.page_id        PageCode,";
            sSql += "             order_text";
            sSql += "        FROM mnu";
            sSql += "       WHERE mnu.level_code != 3";
            sSql += "      UNION ALL";
            sSql += "      SELECT mnu.menu_category  Category,";
            sSql += "             mnu.menu_category  Module,";
            sSql += "             mnu.page_duty page_duty,";
            sSql += "             mnu.level_code     MenuLevel,";
            sSql += "             mnu.menu_id        MenuCode,";
            sSql += "             mnu.menu_name      MenuName,";
            sSql += "             mnu.page_type,";
            sSql += "             mnu.page_id        PageCode,";
            sSql += "             order_text";
            sSql += "        FROM mnu";
            sSql += "       WHERE mnu.level_code = 3";
            sSql += "            ) xmn";
            sSql += "         where page_duty <= '" + Constants.loginUserDuty + "'";
            sSql += "        order by order_text;";
            
            DataTable dtMenu = dbCon.getQueryResult(sSql);

            if (dtMenu.Rows.Count == 0)
            {
                return;
            }
            else
            {
                int iRow = 0;
                int jRow = 0;
                int kRow = 0;

                MenuItems = new List<MenuItemVM>();
                string[] linkedPageText = new string[dtMenu.Rows.Count];
                linkedPageText[0] = "";

                while (iRow < dtMenu.Rows.Count)
                {
                    if (int.Parse(dtMenu.Rows[iRow]["MenuLevel"].ToString()) == 1)
                    {
                        jRow = iRow + 1;

                        List<MenuItemVM> childItems = new List<MenuItemVM>();

                        while (jRow < dtMenu.Rows.Count)
                        {
                            if (int.Parse(dtMenu.Rows[jRow]["MenuLevel"].ToString()) == 1)
                                break;

                            if (jRow < dtMenu.Rows.Count && int.Parse(dtMenu.Rows[jRow]["MenuLevel"].ToString()) == 2)
                            {
                                if (int.Parse(dtMenu.Rows[jRow]["MenuLevel"].ToString()) < 2)
                                    break;

                                kRow = jRow + 1;

                                List<MenuItemVM> grandchildItems = new List<MenuItemVM>();

                                while (kRow < dtMenu.Rows.Count)
                                {
                                    if (kRow < dtMenu.Rows.Count && int.Parse(dtMenu.Rows[kRow]["MenuLevel"].ToString()) <= 3)
                                    {
                                        if (int.Parse(dtMenu.Rows[kRow]["MenuLevel"].ToString()) < 3)
                                            break;

                                        linkedPageText[kRow] = dtMenu.Rows[kRow]["PageCode"].ToString() + "";

                                        grandchildItems.Add(new MenuItemVM("" + dtMenu.Rows[kRow]["MenuName"].ToString(),
                                         "" + linkedPageText[kRow],
                                         "" + dtMenu.Rows[kRow]["Module"].ToString(),
                                         () => { }));
                                        kRow++;
                                    }
                                }

                                linkedPageText[jRow] = dtMenu.Rows[jRow]["PageCode"].ToString() + "";

                                childItems.Add(new MenuItemVM("" + dtMenu.Rows[jRow]["MenuName"].ToString(),
                                "" + linkedPageText[jRow],
                                "" + dtMenu.Rows[jRow]["Module"].ToString(),
                                () => { },
                                grandchildItems));

                                jRow = kRow;
                            }
                        }

                        linkedPageText[iRow] = dtMenu.Rows[iRow]["PageCode"].ToString() + "";

                        MenuItems.Add(new MenuItemVM(dtMenu.Rows[iRow]["MenuName"].ToString(),
                       "",
                       dtMenu.Rows[iRow]["Module"].ToString(),
                       () => { },
                       childItems));

                        iRow = jRow;
                    }
                }
                jRow--;
                iRow = MenuItems.Count;
                DataContext = this;
                MainMenu.ItemsSource = MenuItems;

                if (MainDocGroup.Items.Count == 0)
                {
                    for (int i = 1; i < MenuItems.Count; i++)
                    {
                        MainMenu.CollapseItem(MenuItems[i]);
                    }
                }
                else
                {
                    int iCheck = 0;
                    for (int i = 1; i < MenuItems.Count; i++)
                    {
                        iCheck = 0;
                        for (int jloopCount = 0; jloopCount < MenuItems[i].ChildItems.Count; jloopCount++)
                        {

                            for (int iloopCount = 0; iloopCount < MainDocGroup.Items.Count; iloopCount++)
                            {
                                if (MenuItems[i].ChildItems[jloopCount].Caption.ToString() == MainDocGroup.Items[iloopCount].Caption.ToString())
                                {
                                    iCheck++;
                                    continue;
                                }

                                for (int kloopCount = 0; kloopCount < MenuItems[i].ChildItems[jloopCount].ChildItems.Count; kloopCount++)
                                {
                                    if (MenuItems[i].ChildItems[jloopCount].ChildItems[kloopCount].Caption.ToString() == MainDocGroup.Items[iloopCount].Caption.ToString())
                                    {
                                        iCheck++;
                                        continue;
                                    }
                                }
                            }
                        }
                        if (iCheck == 0)
                        {
                            MainMenu.CollapseItem(MenuItems[i]);
                        }
                    }
                }
            }
        }

        private void ExecuteLogOut(object sender, EventArgs e)
        {

        }

        private void ImmediateClose(object sender, EventArgs e)
        {

        }

        private BasePage GetActivePage()
        {
            BasePage basePage = null;

            try
            {
                basePage = (MainDockManager.DockController.ActiveItem as DocumentPanel)?.Control as BasePage;

                if (basePage == null && MainDocGroup.SelectedItem != null)
                {
                    basePage = (MainDocGroup.SelectedItem as DocumentPanel).Control as BasePage;
                }
            }
            catch (Exception ex)
            {
                basePage = null;
                //message += ex.Message.ToString();
            }

            return basePage;
        }

        private void ExecuteNew(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoNew();
        }

        private void ExecuteSelect(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoSelect();
        }

        private void ExecuteSave(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoSave();
        }

        private void ExecuteDelete(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoDelete();
        }

        private void ExecuteExcelDownload(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoDownloadExcel();
        }

        private void ExecutePdfDownload(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoDownloadPDF();
        }

        private void ExecutePrint(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoPrint();
        }

        private void ExecutePreview(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoPreview();
        }

        private void ExecuteFixedRows(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            GetActivePage()?.DoFixedRows();
        }

        private void ExecuteClose(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                if (MainDocGroup.SelectedItem == null)
                {
                    if (MessageBoxResult.Yes ==
                    DXMessageBox.Show(" [" + lsProgramTitle + "] 프로그램을 종료하시겠습니까? ",
                   "확인", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes))
                    {
                        try { this.Close(); } catch { }
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MainDockManager.DockController.RemovePanel(MainDocGroup.SelectedItem as DocumentPanel);

                    if (MainDocGroup.SelectedItem == null)
                    {
                        MainMenu.SelectedItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                DXMessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainMenu_SelectedItemChanged(object sender, DevExpress.Xpf.Accordion.AccordionSelectedItemChangedEventArgs e)
        {
            bMenuSelect = false;
            if (!bMenuSelect)
            {
                bMenuSelect = true;

                MenuItemVM selectedItem = e.NewItem as
                 NewProject.MenuItemVM;
                MenuItemVM deselectedItem = e.OldItem as NewProject.MenuItemVM;

                if (selectedItem == null)
                    return;

                AddNewPage(selectedItem.PageCode, selectedItem.Caption.ToString(), selectedItem.Module);
            }
            else
                bMenuSelect = false;
        }

        private void AddNewPage(string pageCode, string pageName, string pageModule)
        {
            string moduleName = "Module" + pageModule;

            ////전체 주석처리
            try
            {
                if (pageCode.Equals(""))
                    return;

                //Assembly assy = Assembly.GetExecutingAssembly();

                //string Format 의 따옴표와 마침표 주의!!
                //BasePage basePage = assy.CreateInstance(string.Format("{0}.{1}", nameSpace, pageCode)) as BasePage;

                DocumentGroup documentGroup = MainDockManager.GetItem("MainDocGroup") as DocumentGroup;

                // searh a group
                if (documentGroup == null)
                {
                    documentGroup =
                    MainDockManager.DockController.AddDocumentGroup(DevExpress.Xpf.Layout.Core.DockType.None);

                    // Create the if necessary
                    documentGroup.Name = "MainDocGroup";
                }

                DocumentPanel documentPanel = MainDockManager.GetItem(pageCode) as DocumentPanel;

                if (documentPanel == null)
                {
                    documentPanel = new DocumentPanel();

                    MainDockManager.DockController.Insert(documentGroup, documentPanel, 999);


                    //documentPanel = MainDockManager.DockController.AddDocumentPanel(documentGroup);
                    ////documentPanel = MainDockManager.DockController.AddPanel(DockType.Right);
                    documentPanel.Caption = pageName;
                    documentPanel.Name = pageCode;

                    string xamlFile = @"/" + moduleName + ";component/" + pageCode + ".xaml";
                    documentPanel.Content = new Uri(xamlFile, UriKind.Relative);

                    BasePage basePage = documentPanel.Control as BasePage;

                    basePage.Program_ID = pageCode;
                    basePage.Program_Name = pageName;
                    basePage.Program_Module = pageModule;

                    basePage.MessageHandler += new Action<string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult>(ShowMessage);
                    basePage.HideHandler += new Action(HideMessage);

                    documentPanel.IsActive = true;
                    documentPanel.ShowControlBox = true;
                }
                else if (documentPanel.Closed)
                {
                    documentPanel.Closed = false;
                    documentPanel.Visibility = Visibility.Visible;
                    documentPanel.SetZIndex(0);
                    documentPanel.IsActive = true;
                    documentPanel.ShowControlBox = true;
                }
                else
                {
                    documentPanel.Visibility = Visibility.Visible;
                    documentPanel.SetZIndex(0);
                    documentPanel.IsActive = true;
                    documentPanel.ShowControlBox = true;
                }

                documentPanel.Focus();
                this.MainDockManager.DockController.Activate(documentPanel);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainDocGroup_SelectedItemChanged(object sender, DevExpress.Xpf.Docking.Base.SelectedItemChangedEventArgs e)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // 새로추가
                GetActivePage()?.DoNew();
            }
            else if (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // 조회하기
                GetActivePage()?.DoSelect();
            }
            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // 저장하기
                GetActivePage()?.DoSave();
            }
            else if (e.Key == Key.D && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // 삭제하기
                GetActivePage()?.DoDelete();
            }
            else if (e.Key == Key.E && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Excel 다운로드
                GetActivePage()?.DoDownloadExcel();
            }
            else if (e.Key == Key.F && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // Pdf 다운로드
                GetActivePage()?.DoDownloadPDF();
            }
            else if (e.Key == Key.L && Keyboard.Modifiers == ModifierKeys.Control)
            {
                // 종료하기
                ExecuteClose(null, null);
            }
        }

        #region MessageBox 처리 관리 => ShowMessage, HideMessage
        // 향후 Panel 을 이용한 별도의 MessageBox 로 처리
        // delayTime (mili second 만큼 대기 후 자동으로 메시지 감춤)

        /*******************/
        public void ShowMessage()
        {
            if (fgMessages.Visibility == Visibility.Hidden)
            {
                fgMessages.Visibility = Visibility.Visible;
            }
        }

        public override void ShowMessage(string message, string caption,
                                         MessageBoxButton msgButton = MessageBoxButton.OK,
                                         MessageBoxImage msgImage = MessageBoxImage.Information,
                                         MessageBoxResult msgDefault = MessageBoxResult.OK)
        {
            base.ShowMessage(message, caption, msgButton, msgImage, msgDefault);


            pnlMessages.Caption = caption;
            msgContent.Text = message;

            if (msgButton == MessageBoxButton.OK)
            {
                messageOK.Visibility = Visibility.Visible;
                messageYes.Visibility = Visibility.Hidden;
                messageNo.Visibility = Visibility.Hidden;
            }
            else
            {
                messageOK.Visibility = Visibility.Hidden;
                messageYes.Visibility = Visibility.Visible;
                messageNo.Visibility = Visibility.Visible;
            }

            string _ImagePath = "";

            if (msgImage == MessageBoxImage.None)
            {
                _ImagePath = "IconOK_48x48";
            }
            else if (msgImage == MessageBoxImage.Information || msgImage == MessageBoxImage.Question)
            {
                _ImagePath = "IconInfo_48x48";
            }
            else if (msgImage == MessageBoxImage.Warning || msgImage == MessageBoxImage.Exclamation)
            {
                _ImagePath = "IconWarning_48x48";
            }
            else if (msgImage == MessageBoxImage.Error)
            {
                _ImagePath = "IconError_48x48";
            }
            else
            {
                _ImagePath = "IconInfo_48x48";
            }

            BitmapImage bitmap = new BitmapImage(new Uri($"pack://application:,,,/NeoMES;component/Images/{_ImagePath}.png"));

            if (bitmap != null)
                msgIcon.Source = bitmap;
            else
                msgIcon.Source = null;

            //if (msgDefault == MessageBoxResult.OK)
            //{
            //    messageOK.Focus();
            //}
            //else if (msgDefault == MessageBoxResult.Yes)
            //{
            //    messageYes.Focus();
            //}
            //else if (msgDefault == MessageBoxResult.No)
            //{
            //    messageNo.Focus();
            //}


        }

        public override void HideMessage()
        {
            base.HideMessage();

            if (fgMessages.Visibility == Visibility.Visible)
            {
                fgMessages.Visibility = Visibility.Hidden;
            }

            MainDocGroup.Focus();
        }

        private void MessageOK_Clicked(object sender, RoutedEventArgs e)
        {
            // OK 클릭시 처리로직 기술
            // 지금은 처리 안됨. OK 버튼에 대해서만 사용. 향후 Yes / No 에 대해서 개발해 보삼
            //GetActivePage()?.MessageOK_Clicked();
            HideMessage();
        }

        private void MessageYes_Clicked(object sender, RoutedEventArgs e)
        {
            // Yes 클릭시 처리로직 기술
            // 지금은 처리 안됨. OK 버튼에 대해서만 사용. 향후 Yes / No 에 대해서 개발해 보삼
            //GetActivePage()?.MessageYes_Clicked();
            HideMessage();
        }

        private void MessageNo_Clicked(object sender, RoutedEventArgs e)
        {
            // No 클릭시 처리로직 기술
            // 지금은 처리 안됨. OK 버튼에 대해서만 사용. 향후 Yes / No 에 대해서 개발해 보삼
            //GetActivePage()?.MessageNo_Clicked();
            HideMessage();
        }
        #endregion MessageBox 처리 관리 => ShowMessage, HideMessage

        private void ExecuteReviewMessage(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void MessagesPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MessagesPanel_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
