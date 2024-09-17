using Constract;
using SolutionData;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolveData data;
        public MainWindow()
        {
            data = new SolveData();
            InitializeComponent();
        }

        private void HideAllBlocks()
        {
            Grid1.Visibility = Visibility.Collapsed;
            Grid2.Visibility = Visibility.Collapsed;
            Grid3.Visibility = Visibility.Collapsed;
            Grid4.Visibility = Visibility.Collapsed;
        }

        private void q1_Click(object sender, RoutedEventArgs e)
        {
            HideAllBlocks();
            var (top5, bottom5) =  data.QuestionOne();
            Grid1.Visibility = Visibility.Visible;
            Grid1_Top5DataGrid.ItemsSource = top5;
            Grid1_Bottom5DataGrid.ItemsSource = bottom5;
        }

        private void q2_Click(object sender, RoutedEventArgs e)
        {
            HideAllBlocks();
            StudentScores student = data.QuestionTwo();
            Grid2.Visibility = Visibility.Visible;
            Grid2_Sbd.Text = student.SBD.ToString();
            Grid2_Total.Text = student.TongDiemKhoiA.ToString();
        }

        private void q3_Click(object sender, RoutedEventArgs e)
        {
            HideAllBlocks();
            SubjectParticipantCounts subjectCounts = data.QuestionThree();
            Grid3.Visibility=Visibility.Visible;
            Grid3_ToanCount.Text = subjectCounts.Toan.ToString();
            Grid3_VatLiCount.Text = subjectCounts.VatLi.ToString();
            Grid3_HoaHocCount.Text = subjectCounts.HoaHoc.ToString();
            Grid3_NguVanCount.Text = subjectCounts.NguVan.ToString();
            Grid3_SinhHocCount.Text = subjectCounts.SinhHoc.ToString();
            Grid3_LichSuCount.Text = subjectCounts.LichSu.ToString();
            Grid3_DiaLiCount.Text = subjectCounts.DiaLi.ToString();
            Grid3_GDCDCount.Text = subjectCounts.GDCD.ToString();
            Grid3_NgoaiNguCount.Text = subjectCounts.NgoaiNgu.ToString();
        }

        private void q4_Click(object sender, RoutedEventArgs e)
        {
            HideAllBlocks();
            var (khoaHocTuNhienCount, khoaHocXaHoiCount) = data.QuestionFour();
            Grid4.Visibility=Visibility.Visible;
            Grid4_Khtn.Text = khoaHocTuNhienCount.ToString();
            Grid4_Khxh.Text = khoaHocXaHoiCount.ToString();
        }
    }
}