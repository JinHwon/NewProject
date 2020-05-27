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
            BsiLoginUser.Content = " Login : " + Constants.loginUserName + " " + Constants.loginUserDuty + "님  ";
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
            sSql += " SELECT MENU_LEVEL MenuLevel, MENU_ID, MENU_NAME, PAGE_ID PageCode, menu_name MenuName, menu_category Module ";
            sSql += "   FROM tb_com_menu";
            sSql += "  WHERE USE_YN = 'Y'";
            sSql += "  ORDER BY ORDER_SEQ";

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

        private void ExecuteNew(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecuteSelect(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecuteSave(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecuteDelete(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecuteExcelDownload(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecutePdfDownload(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void ExecuteReviewMessage(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

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
    }
}
