using Android.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageEncoder
{
    public class BitmapToJpeg
    {
        public const int DefaultJpegQualityLevel = 75;
        public const int DefaultInputPixelLimit = 1280 * 720;

        private static int _jpegQualityLevel = DefaultJpegQualityLevel;
        private static int _inputPixelLimit = DefaultInputPixelLimit;

        public static int JpegQualityLevel
        {
            get
            {
                return _jpegQualityLevel;
            }
            set
            {
                if (0 < value && value < 100)
                {
                    _jpegQualityLevel = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public static int InputPixelLimit
        {
            get
            {
                return _inputPixelLimit;
            }
            set
            {
                if (value > 1)
                {
                    _inputPixelLimit = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public static byte[] ConvertToJpeg(byte[] bitmapBytes)
        {
            // Create bitmap from byte array
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapBytes, 0, bitmapBytes.Length);
            if (bitmap == null)
            {
                // Invalid byte array
                throw new FormatException();
            }

            // Downscale image and compress bitmap to jpeg
            MemoryStream outStream = new MemoryStream();
            DownscaleImage(bitmap).Compress(Bitmap.CompressFormat.Jpeg, JpegQualityLevel, outStream);

            return outStream.ToArray();
        }

        private static Bitmap DownscaleImage(Bitmap image)
        {
            int imageSize = image.Height * image.Width;
            if (imageSize > InputPixelLimit)
            {
                // Downsize image to the specified limit
                double scaleFactor = Math.Sqrt(image.Width) * Math.Sqrt(image.Height) / Math.Sqrt(InputPixelLimit);
                return Bitmap.CreateScaledBitmap(image, (int)(image.Width / scaleFactor), (int)(image.Height / scaleFactor), true);
            }
            else
            {
                // Don't bother scaling if image is small enough
                return image;
            }
        }
    }
}
