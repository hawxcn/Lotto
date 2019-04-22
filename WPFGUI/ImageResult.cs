using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WPFGUI
{
    public class ImageResult
    {
        public static void OutputResultAsImage(WinnerGroup winnerGroup,string sImgPath)
        {           
            using (Image image = (Image)Properties.Resources.ResourceManager.GetObject("winner"))
            {
                try
                {
                    Bitmap bitmap = new Bitmap(image);

                    int width = bitmap.Width, height = bitmap.Height;
                    //测试文字
                    string text = "一等奖 巴扎黑";
                    //string[] texts = {"一等奖 巴扎黑", "一等奖 巴扎黑", "一等奖 巴扎黑" , "一等奖 巴扎黑" , "一等奖 巴扎黑" , "一等奖 巴扎黑" , "一等奖 巴扎黑" , "一等奖 巴扎黑" };

                    Graphics g = Graphics.FromImage(bitmap);

                    g.DrawImage(bitmap, 0, 0);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                    Font crFont = new Font("微软雅黑", 48, FontStyle.Bold);
                    SizeF sizef = g.MeasureString(text, crFont);//得到文本的宽高

                    //背景位置(去掉了. 如果想用可以自己调一调 位置.)
                    //graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)), (width - crSize.Width) / 2, (height - crSize.Height) / 2, crSize.Width, crSize.Height);

                    SolidBrush semiTransBrush = new SolidBrush(Color.White); //new SolidBrush(Color.FromArgb(120, 177, 171, 171));

                    //将原点移动 到图片开始输出部分
                    float startY = height / 20 * 9;
                    g.TranslateTransform(0, startY);
                    //以原点为中心 转 -45度
                    //g.RotateTransform(-45);
                    float currentY = 0;
                    for (int i=0; startY+currentY < image.Width && i < winnerGroup.winnerGroup.Count; currentY += sizef.Height + 10,i++)
                    {
                        string tempLine;
                        if (i < winnerGroup.WinnerCondition.firstPrizeNumber)
                        {
                            tempLine = $"一等奖：{winnerGroup.winnerGroup[i].name}({winnerGroup.winnerGroup[i].ID})";//输出的字符串
                        }
                        else if (i >= winnerGroup.WinnerCondition.firstPrizeNumber && i < winnerGroup.WinnerCondition.firstPrizeNumber + winnerGroup.WinnerCondition.secondPrizeNumber)
                        {
                            tempLine = $"二等奖：{winnerGroup.winnerGroup[i].name}({winnerGroup.winnerGroup[i].ID})";//输出的字符串
                        }
                        else
                        {
                            tempLine = $"三等奖：{winnerGroup.winnerGroup[i].name}({winnerGroup.winnerGroup[i].ID})";//输出的字符串
                        }
                        sizef = g.MeasureString(tempLine, crFont);//得到文本的宽高
                        g.DrawString(tempLine, crFont, semiTransBrush, new PointF((image.Width - sizef.Width) / 2, currentY));
                    }
                    
                    //保存文件
                    bitmap.Save(sImgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                catch (Exception e)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
