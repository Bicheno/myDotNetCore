using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Media;

namespace myWPF.Converter
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var path = value.ToString();
                if (!string.IsNullOrWhiteSpace(path))
                {
                    ImageSource Source;
                    FileStream imgStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    Source = GetBitmapImage(imgStream);
                    imgStream.Close();
                    imgStream.Dispose();
                    return Source;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static BitmapSource GetBitmapImage(Stream fromFileStream, double templateWidth = 250, double templateHeight = 250)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息 
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            //缩略图宽、高 
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图 
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放 
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图 
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放 
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }
            //取得图片大小 
            System.Drawing.Size mySize = new System.Drawing.Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片 
            Bitmap bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板 
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布 
            g.Clear(System.Drawing.Color.White);
            //在指定位置画图 
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height), GraphicsUnit.Pixel);

            var bmpSource = ChangeBitmapToBitmapSource(bitmap);
            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();

            return bmpSource;
        }

        /// <summary>
        /// 从Bitmap转换成BitmapSource
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static BitmapSource ChangeBitmapToBitmapSource(Bitmap bmp)
        {

            BitmapSource returnSource;
            try
            {
                returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                returnSource = null;
            }
            return returnSource;
        }
    }
}
