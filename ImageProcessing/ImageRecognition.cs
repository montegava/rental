using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using tesseract;
using System.Reflection;

namespace ImageProcessing
{


    public static class ImageRecognition
    {
        #region Image resizing
        /// <summary>
        /// For slando we have to fill alfa chennel
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image FillTransporate(Image image)
        {
            Bitmap result = (Bitmap)image;
            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                    if (result.GetPixel(x, y).A == 255)
                        FloodFill(result, x, y, Color.White);
            return (Image)result;
        }

        /// <summary>
        /// Resize image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="mul"></param>
        /// <returns></returns>
        private static Image ResizeImage(Image image, int mul = 4)
        {
            Bitmap result = new Bitmap((int)(image.Width * mul), (int)(image.Height * mul));
            using (var newGraph = Graphics.FromImage(result))
            {
                newGraph.CompositingQuality = CompositingQuality.HighQuality;
                newGraph.SmoothingMode = SmoothingMode.HighQuality;
                newGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, (int)(image.Width * mul), (int)(image.Height * mul));
                newGraph.DrawImage(image, imageRectangle);
            }
            return (Image)result;
        }

        /// <summary>
        /// Change pixel color in bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        private static void FloodFill(Bitmap bitmap, int x, int y, Color color)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int[] bits = new int[data.Stride / 4 * data.Height];
            Marshal.Copy(data.Scan0, bits, 0, bits.Length);

            LinkedList<Point> check = new LinkedList<Point>();
            int floodTo = color.ToArgb();
            int floodFrom = bits[x + y * data.Stride / 4];
            bits[x + y * data.Stride / 4] = floodTo;

            if (floodFrom != floodTo)
            {
                check.AddLast(new Point(x, y));
                while (check.Count > 0)
                {
                    Point cur = check.First.Value;
                    check.RemoveFirst();

                    foreach (Point off in new Point[] {
                new Point(0, -1), new Point(0, 1), 
                new Point(-1, 0), new Point(1, 0)})
                    {
                        Point next = new Point(cur.X + off.X, cur.Y + off.Y);
                        if (next.X >= 0 && next.Y >= 0 &&
                            next.X < data.Width &&
                            next.Y < data.Height)
                        {
                            if (bits[next.X + next.Y * data.Stride / 4] == floodFrom)
                            {
                                check.AddLast(next);
                                bits[next.X + next.Y * data.Stride / 4] = floodTo;
                            }
                        }
                    }
                }
            }

            Marshal.Copy(bits, 0, data.Scan0, bits.Length);
            bitmap.UnlockBits(data);
        }
        #endregion

        #region Image Recognition

        public static string RecognizeImage(Image image, bool isSlando)
        {
            try
            {
                var ocr = new TesseractProcessor();
                string _tessData = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\tessdata\\"; ;
                string _lang = "eng";
                int _ocrEngineMode = (int)eOcrEngineMode.DEFAULT;

                bool status = ocr.Init(_tessData, _lang, _ocrEngineMode);
                ocr.SetVariable("tessedit_char_whitelist", "0123456789-+(),;"); // If digit only
                if (isSlando)
                    image = FillTransporate(image);
                image = ResizeImage(image);

                return ocr.Apply(image);
            }
            catch (Exception e)
            {
                Log.Append("\tError on rec " + e.Message);
                return String.Empty;
            }

        }

        public static string RecognizeImage(string fileName, bool isSlando)
        {

            string result = String.Empty;
            if (File.Exists(fileName))
            {
                Image image = Image.FromFile(fileName);
                result = RecognizeImage(image, isSlando);
            }
            return result;
        }


        #endregion
    }

    static class Log
    {
        private static string m_logpath = "rental.log";

        public static void Append(string apath, string amsg)
        {
            if (apath == null)
                return;
            string msg = DateTime.Now + "\t" + amsg;
            try
            {
                if (!File.Exists(apath))
                {
                    using (StreamWriter sw = File.CreateText(apath))
                    {
                        sw.WriteLine(msg);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(apath))
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }


        public static void Append(string amsg)
        {
            Append(m_logpath, amsg);
        }



    }
}
