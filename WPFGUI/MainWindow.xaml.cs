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
    ///
    public partial class MainWindow : Window
    {
        private WinnerGroup lastResult;
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

            Condition condition = new Condition();
            condition.type = getSelectedType();
            condition.frequency = FrequenceNum.Text.Equals("") ? 1 : Int32.Parse(FrequenceNum.Text);
            condition.starTime = StartDate.SelectedDate ?? DateTime.Now;
            condition.endTime = EndDate.SelectedDate ?? DateTime.Now.AddDays(7);//默认七天
            condition.firstPrizeNumber = FirstPrizeNum.Text.Equals("") ? 1 : Int32.Parse(FirstPrizeNum.Text);
            condition.secondPrizeNumber = SecondPrizeNum.Text.Equals("") ? 2 : Int32.Parse(SecondPrizeNum.Text);
            condition.thirdPrizeNumber = ThirdPrizeNum.Text.Equals("") ? 3 : Int32.Parse(ThirdPrizeNum.Text);
            StringBuilder keyText = new StringBuilder("#");
            keyText.Append(Key.Text.ToString());
            keyText.Append("#");
            condition.key = keyText.ToString();

            //Console.WriteLine("FrequenceNum "+ frequence);
            //Console.WriteLine("startDate " + startDate);
            //Console.WriteLine("endDate " + endDate);
            //Console.WriteLine("firstPrizeNum " + firstPrizeNum);
            //Console.WriteLine("secondPrizeNum " + secondPrizeNum);
            //Console.WriteLine("thirdPrizeNum " + thirdPrizeNum);

            Channel a = new Channel(FileProcess.readFile(FilePathLabel.Content.ToString()));//构造群
            a.InitializeGroupMember();//构造成员
            lastResult = a.GetLuckyGuys(condition);

            MessageBox.Show("抽奖程序执行完毕,请跳转结果页进行查看.");
            StringBuilder tempString = new StringBuilder(lastResult.theme + "\n");

            for (int i = 0; i < lastResult.winnerGroup.Count; i++)
            {
                if (i < lastResult.WinnerCondition.firstPrizeNumber)
                {
                    tempString.Append("一等奖：" + lastResult.winnerGroup[i].name + "(" + lastResult.winnerGroup[i].ID + ")\n");
                }
                else if (i >= lastResult.WinnerCondition.firstPrizeNumber && i < lastResult.WinnerCondition.firstPrizeNumber + lastResult.WinnerCondition.secondPrizeNumber)
                {
                    tempString.Append("二等奖：" + lastResult.winnerGroup[i].name + "(" + lastResult.winnerGroup[i].ID + ")\n");
                }
                else
                {
                    tempString.Append("三等奖：" + lastResult.winnerGroup[i].name + "(" + lastResult.winnerGroup[i].ID + ")\n");
                }
            }
            ResultBox.Text = tempString.ToString();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Back && (e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9))
            {
                e.Handled = true;
            }
        }

        private void Write_button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathLabel.Content.ToString();
            string outputPath = filePath.Substring(0, filePath.LastIndexOf("\\")) + "\\result.txt";
            Console.WriteLine(outputPath);
            //FileProcess.writeFile(, lastResult);
            FileProcess.writeFile(outputPath, lastResult);
            MessageBox.Show("已输出至" + outputPath);
        }
    }
}
