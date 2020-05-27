using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonLib;

namespace NewProject
{
    /// <summary>
    /// WinLopgin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WinLogin : Window
    {
        private MSSqlConnection dbCon = new MSSqlConnection();
        private RegistoryControl regCtl = new RegistoryControl();
        public WinLogin()
        {
            InitializeComponent();
        }

        private void MouseDrag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                txtUserID.Text = regCtl.readRegistry("USER_ID");

                if (txtUserID.Text.Equals(""))
                    txtUserID.Focus();
                else
                    txtPWD.Focus();

                this.Activate();
                //this.Visibility = Visibility.Visible;
            }
            catch
            {
                txtUserID.Focus();
            }
        }

        private void Setting_Clicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void PasswordChange_Clicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }

        private void SelectAllPassword(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            passwordBox.Dispatcher.BeginInvoke(new Action(() => passwordBox.SelectAll()));
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DoLogin();
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DoLogin()
        {
            if (txtUserID.Text.Length == 0)
            {
                MessageBox.Show("사용자 ID 를 입력하여 주십시오. ", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUserID.Focus();
                return;
            }

            if (txtUserID.Text.Equals(Constants.loginID))
            {
                MessageBox.Show("이미 " + Constants.loginUserName + " 님으로 로그인 되어 있읍니다. ", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPWD.Focus();
                return;
            }

            if (txtPWD.Password.Length == 0)
            {
                MessageBox.Show("비밀번호를 입력하여 주십시오. ", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPWD.Focus();
                return;
            }

            try
            {
                string sSql = "";
                sSql += " SELECT USER_ID, USER_T2, USER_DUTY, USER_TRIBE";
                sSql += "   FROM tb_com_user";
                sSql += "  WHERE USER_ID = '" + txtUserID.Text.Trim() + "'";
                sSql += "    AND USER_PW = '" + txtPWD.Password.Trim() + "'";

                DataTable returnDT = dbCon.getQueryResult(sSql);

                //for (int iLoop = 0; iLoop < returnDT.Rows.Count; ++iLoop)
                if (returnDT.Rows.Count > 0)
                {
                    Constants.loginID = returnDT.Rows[0]["USER_ID"].ToString();
                    Constants.loginUserName = returnDT.Rows[0]["USER_T2"].ToString();
                    Constants.loginUserDuty = returnDT.Rows[0]["USER_DUTY"].ToString();
                    Constants.UserTribe = returnDT.Rows[0]["USER_TRIBE"].ToString();

                    regCtl.writeRegistry("USER_ID", Constants.loginID);
                    this.DialogResult = true;

                    return;
                }
                else
                {
                    string msg = "아이디 혹은 암호가 틀렸습니다. \r\n\r\n동일 증상이 반복될 경우 스탭에게 문의하세요. ";

                    MessageBox.Show(msg, "Login 실패", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtUserID.SelectAll();
                    txtUserID.Focus();
                }
                returnDT.Dispose();

            }
            catch (Exception ex)
            {
                string msg = "DB 접속에 문제가 있습니다. \r\n\r\n동일 증상이 반복될 경우 전산 담당자에게 문의하세요. \r\n\r\n" + ex.Message.ToString();

                MessageBox.Show(msg, "Login 실패", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUserID.SelectAll();
                txtUserID.Focus();
            }
            finally
            {
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinSignUp fPopup = new WinSignUp();

            fPopup.ShowDialog();

        }
    }
}
