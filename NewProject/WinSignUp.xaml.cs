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
    /// WinSignUp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WinSignUp : Window
    {
        private MSSqlConnection dbCon = new MSSqlConnection();

        bool bCheck = false;
        bool bCheck2 = false;

        public WinSignUp()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxUserID.Text.Trim() == "")
                {
                    MessageBox.Show("아이디를 입력해주세요.", "ID 미입력", MessageBoxButton.OK, MessageBoxImage.Error);
                    bCheck = false;
                    return;
                }

                string sSql = "";
                sSql = "";
                sSql += "   SELECT USER_ID";
                sSql += "     FROM tb_com_user";
                sSql += "    WHERE USER_ID = '" + tbxUserID.Text + "'";

                DataTable returnDT = dbCon.getQueryResult(sSql);

                if (returnDT.Rows.Count == 0)
                {
                    bCheck = true;
                    MessageBox.Show("사용가능한 ID 입니다.", "사용가능", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    bCheck = false;
                    MessageBox.Show("이미 존재하는 ID 입니다.", "사용불가", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                initTribe();
            }
            catch (Exception ex)
            {
            }
        }

        private void initTribe()
        {
            try
            {
                string sSql = "";
                sSql = " SELECT rtrim(code) code, rtrim(code_name) code_name"
                + "   FROM tb_common"
                + "  WHERE CODE_GROUP  = 'TRIBE'"
                + "  ORDER BY DISP_SEQ";

                DataTable dtData = dbCon.getQueryResult(sSql);

                WindowUtil.SetComboBoxWithDataTable(cboTribe, dtData, false, false);

            }
            catch (Exception ex)
            {
            }
        }

        private void tbxUserID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (bCheck == true)
                {
                    bCheck = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void tbxPasswordCheck_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxPasswordCheck.Password == "")
                {
                    tbCheck.Text = "";
                }
                else
                {
                    if (tbxPassword.Password == tbxPasswordCheck.Password)
                    {
                        tbCheck.Foreground = Brushes.Black;
                        tbCheck.Text = "일치";
                    }
                    else
                    {
                        tbCheck.Foreground = Brushes.Red;
                        tbCheck.Text = "미일치";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bCheck == false)
                {
                    MessageBox.Show("아이디 중복체크를 다시 해주세요", "아이디 중복체크", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (tbCheck.Text != "일치")
                {
                    MessageBox.Show("패스워드를 확인 해주세요", "패스워드 체크", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (tbxUserName.Text == "")
                {
                    MessageBox.Show("이름을 입력해주세요", "이름 체크", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (bCheck2 == false)
                {
                    MessageBox.Show("게임 아이디 중복체크를 다시 해주세요", "아이디 중복체크", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string sTribe = cboTribe.SelectedValue.ToString();

                string sSql = "";
                sSql = "";
                sSql += " INSERT INTO tb_com_user(USER_ID, USER_PW, USER_NAME, USER_TRIBE, USER_EMAIL, USER_MOBILE, USER_DUTY, ACTIVATED, CREATE_DATE, USER_T2)";
                sSql += "   VALUES( '" + tbxUserID.Text.Trim() + "'";
                sSql += "           ,'" + tbxPassword.Password.Trim() + "'";
                sSql += "           ,'" + tbxUserName.Text.Trim() + "'";
                sSql += "           ,'" + sTribe + "'";
                sSql += "           ,'" + tbxUserEmail.Text.Trim() + "'";
                sSql += "           ,'" + tbxUserMobile.Text.Trim() + "'";
                sSql += "           ,'0'";
                sSql += "           ,'Y'";
                sSql += "           ,GETDATE()";
                sSql += "           ,'" + tbxUserT2.Text.Trim() + "'";
                sSql += "           )";

                string sResult = dbCon.execQueryUpdate(sSql);

                if (sResult != "")
                {
                    MessageBox.Show(sResult);
                    return;
                }
                else
                {
                    MessageBox.Show("회원가입완료! 로그인 해주세요", "회원가입완료", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void MouseDrag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbxUserT2.Text.Trim() == "")
                {
                    MessageBox.Show("사용하길 희망하는 게임아이디를 입력해주세요.", "ID 미입력", MessageBoxButton.OK, MessageBoxImage.Error);
                    bCheck2 = false;
                    return;
                }

                string sSql = "";
                sSql = "";
                sSql += "   SELECT USER_T2";
                sSql += "     FROM tb_com_user";
                sSql += "    WHERE USER_T2 = '" + tbxUserT2.Text + "'";

                DataTable returnDT = dbCon.getQueryResult(sSql);

                if (returnDT.Rows.Count == 0)
                {
                    bCheck2 = true;
                    MessageBox.Show("사용가능한 ID 입니다.", "사용가능", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    bCheck2 = false;
                    MessageBox.Show("이미 존재하는 ID 입니다.", "사용불가", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void tbxUserT2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (bCheck2 == true)
                {
                    bCheck2 = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
