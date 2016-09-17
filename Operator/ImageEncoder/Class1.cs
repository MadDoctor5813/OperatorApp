using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageEncoder
{
    public class BitmapToJpeg
    {
        public const long DefaultJpegQualityLevel = 75L;
        public const int DefaultInputPixelLimit = 1280 * 720;

        private long _jpegQualityLevel = DefaultJpegQualityLevel;
        private int _inputPixelLimit = DefaultInputPixelLimit;

        public long JpegQualityLevel
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

        public int InputPixelLimit
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

        public static byte[] ConvertToJpeg(byte[] bitmap)
        {
            return new byte[0];
        }
    }
}
