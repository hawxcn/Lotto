using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace WPFGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            //Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            System.Diagnostics.Process.Start(link.NavigateUri.AbsoluteUri);  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                FilePathLabel.Content = openFileDialog.FileName;
            }
        }
        private int getSelectedType()
        {
            int type = 0;
            if (StudentCheckBox.IsChecked == true) type += 4;
            if (TeacherCheckBox.IsChecked == true) type += 1;
            if (AssistentCheckBox.IsChecked == true) type += 2;
            return type;
        }
        private void LuckyDrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilePathLabel.Content.Equals("NULL"))
            {
                MessageBox.Show("文件为空，请选择文件！");
                return;//放弃
            }
            if (Key.Text.Equals(""))
            {
                MessageBox.Show("活动关键字为空，请输入关键字！");
                return;//放弃
            }
            if (getSelectedType() == 0)
            {
                MessageBox.Show("请选择抽奖人员类型！");
                return;//放弃
            }

            var frequence = FrequenceNum.Text ?? "1";
            var startDate = StartDate.SelectedDate ?? DateTime.Now;
            var endDate = StartDate.SelectedDate ?? DateTime.Now.AddDays(7);//默认七天
            var firstPrizeNum = FirstPrizeNum.Text.Equals("") ? 1 : Int32.Parse(FirstPrizeNum.Text);
            var secondPrizeNum = SecondPrizeNum.Text.Equals("") ? 2 : Int32.Parse(SecondPrizeNum.Text);
            var thirdPrizeNum = ThirdPrizeNum.Text.Equals("") ? 3 : Int32.Parse(ThirdPrizeNum.Text);
            string activity = Key.Text;

            //Console.WriteLine("FrequenceNum "+ frequence);
            //Console.WriteLine("startDate " + startDate);
            //Console.WriteLine("endDate " + endDate);
            //Console.WriteLine("firstPrizeNum " + firstPrizeNum);
            //Console.WriteLine("secondPrizeNum " + secondPrizeNum);
            //Console.WriteLine("thirdPrizeNum " + thirdPrizeNum);



        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Back && (e.Key< System.Windows.Input.Key.D0|| e.Key > System.Windows.Input.Key.D9))
            {
                e.Handled = true;
            }
        }
        
    }
}
