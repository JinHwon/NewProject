using System;
using System.Collections.Generic;
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
using BasicAssembly;
using System.Data;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;

namespace ModuleCOM
{
    /// <summary>
    /// PageComUserTable.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageComUserTable : BasePage
    {
        MSSqlConnection dbCon = new MSSqlConnection();

        // ComboBox 에 활용할 DataTable 사전 정의

        // UserControl 에 사용할 DataTable 사전 정의
        private DataTable dtData;
        private DataTable dtTribe;
        // UserControl 에 삽입된 GridControl & TableView
        private GridControl gcData;
        private TableView tvData;

        // 조회시 데이타 변경여부를 Check 하기 위한 변수 
        private bool isChanged = false;

        private bool isColorChanged = false;
        public PageComUserTable()
        {
            PreviousJob();

            InitializeComponent();

            DoInitialize();
        }

        private void PreviousJob()
        {
            //dtPbProcess = dbCon.getCodeList("PROCESS");
            //dtDisuse = dbCon.getCodeList("DISUSE");
            string sSql = "";
            sSql = " SELECT rtrim(code) code, rtrim(code_name) code_name"
            + "   FROM tb_common"
            + "  WHERE CODE_GROUP  = 'TRIBE'"
            + "  ORDER BY DISP_SEQ";

            dtTribe = dbCon.getQueryResult(sSql);

        }

        public override void DoInitialize()
        {
            string sSql = "";

            base.DoInitialize();


            // 그리드 컨트롤과 테이블뷰 DataTable 과 연동(?)
            // ucData 컨트롤은 BaseUserControl01에 구현된 유저컨트롤 그리드
            gcData = ucData.FindName("gcData") as GridControl;
            tvData = ucData.FindName("tvData") as TableView;

            tvData.ShowGroupPanel = false;
            // 첫번째 검색 row 제거
            tvData.ShowAutoFilterRow = false;
            // 마지막 합계 row 제거
            tvData.ShowTotalSummary = false;

            SolidColorBrush color = new SolidColorBrush(Color.FromRgb(241, 241, 241));

            tvData.AlternateRowBackground = color;

            WindowUtil.SetComboBoxWithDataTable(cboTribe, dtTribe, true, false);

            InitializeGridDataColumn();
        }

        private void InitializeGridDataColumn()
        {
            ucData.InitializeGridDataColumn();

            // UserControl 의 칼럼을 정의
            SetDataColumn();

        }

        private void SetDataColumn()
        {
            // 칼럼 추가

            //gcData

            // Text / Number 칼럼 추가
            BasicMethod.AddGridColumn(gcData, "USER_DUTY", "등급", HorizontalAlignment.Center, 80);
            BasicMethod.AddGridColumn(gcData, "USER_T2", "닉네임", HorizontalAlignment.Center, 80);
            BasicMethod.AddComboBoxColumn(gcData, "USER_TRIBE", "종족", dtTribe, HorizontalAlignment.Center, 100);
            BasicMethod.AddGridColumn(gcData, "USER_NAME", "이름", HorizontalAlignment.Center, 100);

            BasicMethod.AddGridColumn(gcData, "CREATE_DATE", "가입일자", HorizontalAlignment.Center, 110);
            BasicMethod.AddGridColumn(gcData, "LAST_LOGIN", "마지막 로그인시간", HorizontalAlignment.Center, 160);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 이미 Loading 된 경우 return;
            if (isPageLoaded)
                return;

            // UserControl 이 어느 프로그램 소속인지 지정
            ucData.Program_ID = this.Program_ID;         // PageComCode
            ucData.Program_Name = this.Program_Name;     // 거래처 관리
            ucData.Program_Module = this.Program_Module; // COM

            // Check initial GridControl layout
            // layout 초기화를 위하여 DefaultLayout 이 없으면 저장. 개발 후 화면별로 최초 로딩시 1회만 수행
            ucData.InitializeLayout();
            // Load GridControl layout
            // 페이지 로딩시 GridControl layout 을 저장된 layout으로 초기화
            ucData.LoadLayout();

            // UserControl gridControl 의 SelectionChanged Event 와 CellValueChanged Event 호출을 위한 Delegate
            //ucData.DataSelectionChanged += new GridSelectionChangedHandler(GcData_SelectionChaned);
            //ucData.DataFocusedRowChanged += new TableViewFocusedRowChangedHandler(TvData_FocusedRowChanged);
            // 화면 조회시 Component 변경 여부 체크
            // false : 변경되지 않음, true : 변경 완료됨
            isChanged = true;

            // Page Loading 완료 여부 관리
            isPageLoaded = true;
        }

        bool isShown = false;
        private void Page_Shown(object sender, EventArgs e)
        {
            if (isPageLoaded && !isShown && (this.ActualHeight > 0 || this.ActualWidth > 0))
            {
                isChanged = false;

                DoSelect();

                isShown = true;
                isChanged = true;
            }
        }


        public override bool DoActuallySelect()
        {
            string sSql = "";
            //string searchID = tbxSearchID.Text;
            //string searchName = tbxSearchName.Text;

            base.DoActuallySelect();

            try
            {
                sSql = "";
                sSql += "   SELECT c1.code_name USER_DUTY, CU1.USER_T2, CU1.USER_TRIBE, CONVERT(CHAR(10), CU1.CREATE_DATE, 23) CREATE_DATE, CU1.USER_NAME, CONVERT(CHAR(19), CU1.LAST_LOGIN, 21) LAST_LOGIN";
                sSql += "     FROM tb_com_user cu1";
                sSql += "     inner join tb_common c1 on cu1.USER_DUTY = c1.code and c1.code_group = 'user_duty'";
                if (cboTribe.SelectedIndex > 0)
                {
                    sSql += "   WHERE CU1.USER_TRIBE = '" + cboTribe.SelectedValue + "'";
                }
                sSql += "     ORDER BY CU1.USER_DUTY, CREATE_DATE DESC";

                //this.Cursor = Cursors.Wait;

                dtData = dbCon.getQueryResult(sSql);

                if (dtData.Rows.Count > 0)
                {
                    isChanged = false;

                    isColorChanged = false;
                    if (!isColorChanged)
                    {
                        isColorChanged = true;
                        ChangeCellColor();
                    }

                    gcData.ItemsSource = dtData;

                    isChanged = true;



                    // Column Width Auto fit .. autosize
                    if (Constants.COLUMN_FIT)
                        (tvData as TableView)?.BestFitColumns();

                    //홀수/짝수 row 색깔 차이 필요시..하지만 Theme 사용으로 불필요
                    //Brush brush = new SolidColorBrush(Color.FromRgb(100, 255, 25));
                    //tvData.AlternateRowBackground = brush;
                    //tvData.EvenRowBackground = brush;

                }
                else
                {
                    gcData.ItemsSource = null;
                    DXMessageBox.Show("조회된 데이타가 없습니다.", "확인");
                }
            }
            catch (Exception ex)
            {
                DXMessageBox.Show("오류 발생." + ex.Message.ToString(), "오류");
            }

            return true;
        }

        private void ChangeCellColor()
        {

            var CellColor01 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '999'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Red,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor01);

            var CellColor02 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '5'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Blue,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor02);

            var CellColor03 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '4'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Green,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor03);

            var CellColor04 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '3'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Yellow,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor04);

            var CellColor05 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '2'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Orange,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor05);

            var CellColor06 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '1'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.LightPink,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor06);

            var CellColor07 = new FormatCondition()
            {
                // process_name 칼럼의 값이 SMT 인 경우
                Expression = "[USER_DUTY] = '0'",
                FieldName = "USER_DUTY",
                Format = new Format() // using DevExpress.Xpf.Core.ConditionalFormatting;
                {
                    Background = Brushes.Gray,
                    Foreground = Brushes.Black
                }
            };
            tvData.FormatConditions.Add(CellColor07);

        }

        private void cboTribe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (isShown == true)
                {
                    DoSelect();
                }
            }
            catch (Exception EX)
            {
            }
        }
    }
}
