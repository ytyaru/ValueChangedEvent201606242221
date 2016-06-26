using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValueChangedEvent201606242221
{
    public static class HsvRgbConverter
    {
        public enum SaturationModelType
        {
            Cone,       // 円錐モデル
            Cylinder,   // 円柱モデル
        }

        public static dynamic ToRgb(float h, float s, float v, SaturationModelType saturationModel = SaturationModelType.Cylinder)
        {
            // ※HSV/RGBを0.0～1.0の浮動小数点数で表現した円柱モデルの場合
            float r = v;
            float g = v;
            float b = v;
            if (s > 0.0f)
            {
                //h *= 6.0f;
                float h2 = h / 60.0f;
                int i = (int)h2;
                float f = h2 - (float)i;
                switch (i)
                {
                    default:
                    case 0:
                        g *= 1 - s * (1 - f);
                        b *= 1 - s;
                        break;
                    case 1:
                        r *= 1 - s * f;
                        b *= 1 - s;
                        break;
                    case 2:
                        r *= 1 - s;
                        b *= 1 - s * (1 - f);
                        break;
                    case 3:
                        r *= 1 - s;
                        g *= 1 - s * f;
                        break;
                    case 4:
                        r *= 1 - s * (1 - f);
                        g *= 1 - s;
                        break;
                    case 5:
                        g *= 1 - s;
                        b *= 1 - s * f;
                        break;
                }
                if (SaturationModelType.Cone == saturationModel)
                {
                    r /= v;
                    g /= v;
                    b /= v;
                }
            }

            return new { R = (int)(r * 255), G = (int)(g * 255), B = (int)(b * 255) };
        }

        public static dynamic ToHsv(int R, int G, int B, SaturationModelType saturationModel = SaturationModelType.Cylinder)
        {
            if (R < 0 || G < 0 || B < 0) { throw new ArgumentException("RGB値は0～255の範囲です。"); }
            if (255 < R || 255 < G || 255 < B) { throw new ArgumentException("RGB値は0～255の範囲です。"); }
            //if (((R < 0 || G < 0 || B < 0) || (255 < R || 255 < G || 255 < B))) { throw new ArgumentException("RGB値は0～255の範囲です。"); }

            //object hsv = new { H = 0, S = 0, V = 0 };
            //dynamic hsv = new { H = 0, S = 0, V = 0 };

            dynamic hsv = new System.Dynamic.ExpandoObject();

            float r = (float)R / 255.0f;
            float g = (float)G / 255.0f;
            float b = (float)B / 255.0f;

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);

            hsv.V = max;

            if (0 == max) { hsv.S = 0; }
            else
            {
                if (SaturationModelType.Cone == saturationModel) { hsv.S = max - min; }
                else { hsv.S = (max - min) / max; }
            }

            if (min == max) { hsv.H = 0; }
            else
            {
                if (min == b)
                {
                    hsv.H = 60 * ((g - r) / (max - min)) + 60;
                }
                else if (min == r)
                {
                    hsv.H = 60 * ((b - g) / (max - min)) + 180;
                }
                else // if (min == g)
                {
                    hsv.H = 60 * ((r - b) / (max - min)) + 300;
                }

                if (hsv.H < 0 || 360 < hsv.H) { hsv.H %= 360; }
                //if (hsv.H < MinH || MaxH < hsv.H) { hsv.H %= MaxH; }
            }

            return hsv;
        }

    }
}
