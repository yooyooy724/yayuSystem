using System;
using UnityEngine;

namespace yayu
{
    public class MyMath
    {
        public static double Sigmoid(double x, double steep) => (x / steep) / (1 + Math.Abs(x / steep));

        public static double Remap(double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        //input 0-1, output 0-1
        public static double UpDown(double rate)
        {
            double value = 0;
            if (0.0 <= rate && rate < 0.5) value = 0.5 * Math.Sin(MyMath.Remap(rate, 0.0, 0.5, -Math.PI/4, Math.PI / 4)) + 0.5;
            if (0.5 <= rate && rate < 1.0) value = 0.5 * (-Math.Sin(MyMath.Remap(rate, 0.5, 1.0, -Math.PI / 4, Math.PI / 4))) + 0.5;
            return value;
        }
        public static float SmoothZeroToOne(float start, float goal, float val, bool isOneToZero = false)
        {
            float theta = Remap(val, start, goal, 0, Mathf.PI);
            return isOneToZero ?
                Remap(Mathf.Cos(theta), 1, -1, 1, 0) :
                Remap(Mathf.Cos(theta), 1, -1, 0, 1);
        }

        public static double Tween_Exponential(double rate, double exponent)
        {
            if (rate < 0) return 0;
            else if (rate < 1) return Math.Pow(rate, exponent);
            else return 1;
        }
        public static float Tweenf_Exponential(float rate, float exponent)
        {
            if (rate < 0) return 0;
            else if (rate < 1) return Mathf.Pow(rate, exponent);
            else return 1;
        }

        /// x=0‚Å‚ÌŒX‚«‚ª2‚Å‚ ‚èAx=1‚Å‚Ìy‚Ì’l‚ª1‚ÅAŒX‚«‚ª0‚É‚È‚é
        public static float Tweenf_linearFirst(float rate)
        {
            var x = rate;
            float y = 2f * x - x * x;
            return Mathf.Clamp(y, 0f, 1f); // Clamp the output to the range [0, 1]
        }

        public static float SquaredDistance(Vector3 pos1, Vector3 pos2)
        {
            return Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2) + Mathf.Pow(pos1.z - pos2.z, 2);
        }
        public static float Distance(Vector3 pos1, Vector3 pos2)
        {
            return Mathf.Sqrt(SquaredDistance(pos1, pos2));
        }

        public static float PerlinNoise3D(float x, float y, float z)
        {
            float xy = Mathf.PerlinNoise(x, y);
            float xz = Mathf.PerlinNoise(x, z);
            float yz = Mathf.PerlinNoise(y, z);
            float yx = Mathf.PerlinNoise(y, x);
            float zx = Mathf.PerlinNoise(z, x);
            float zy = Mathf.PerlinNoise(z, y);
            return (xy + xz + yz + yx + zx + zy) / 6;
        }

        /// <param name="theta">radian value</param>
        /// <returns></returns>
        public static Vector2 RThetaToXY(float r, float theta)
        {
            Vector2 vec;
            vec.x = r * Mathf.Cos(theta);
            vec.y = r * Mathf.Sin(theta);
            return vec;
        }
        public static Vector2 RThetaToXY_XYReverse(float r, float theta)
        {
            Vector2 vec;
            vec.x = r * Mathf.Sin(theta);
            vec.y = r * Mathf.Cos(theta);
            return vec;
        }

        public class ConversionTo1
        {
            public ConversionTo1(float a)
            {
                this.a = a;
                r = ((maxValue / a) - 1) / (maxValue / a);
            }
            float maxValue = 1;
            float a;
            float r;
            public float Value(float _value) => (float)(a * (Mathf.Pow(r, _value) - 1) / (r - 1));
        }

        public static float ConvertDoubleToFloat(double value)
        {
            // Check for infinity or NaN
            if (double.IsInfinity(value) || double.IsNaN(value))
            {
                throw new ArgumentException("Value cannot be converted to float because it is infinity or NaN.");
            }

            // Check if the value is within the range of float
            if (value > float.MaxValue)
            {
                return float.MaxValue;
            }
            else if (value < float.MinValue)
            {
                return float.MinValue;
            }
            else if (value > 0 && value < float.Epsilon)
            {
                return float.Epsilon;
            }
            else if (value < 0 && value > -float.Epsilon)
            {
                return -float.Epsilon;
            }
            else
            {
                return (float)value;
            }
        }

        public static float UnlimitedLerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float LimitedLerp(float a, float b, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return UnlimitedLerp(a, b, t);
        }

        public static Color UnlimitedLerp(Color colA, Color colB, float t)
        {
            float r = colA.r + (colB.r - colA.r) * t;
            float g = colA.g + (colB.g - colA.g) * t;
            float b = colA.b + (colB.b - colA.b) * t;
            float alpha = colA.a + (colB.a - colA.a) * t;

            return new Color(r, g, b, alpha);
        }

        public static Vector3 RandomVector3(float min, float max)
        {
            return new Vector3(
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max)
            );
        }

        public static Vector3 RandomVector2AsVector3(float minX, float maxX, float minY, float maxY, float z = 0)
        {
            return new Vector3(
                UnityEngine.Random.Range(minX, maxX),
                UnityEngine.Random.Range(minY, maxY),
                z
            );
        }
    }
}
