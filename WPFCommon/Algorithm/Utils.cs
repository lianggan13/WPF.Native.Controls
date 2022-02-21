using System;
using System.Windows;

namespace WPFCommon.Algorithm
{
    public static class Utils
    {
        public static double LinearEquation(double a, double b, double x)
        {
            return a * x + b;
        }

        /// <summary>
        /// 对一组点通过最小二乘法进行线性回归
        /// </summary>
        /// <param name="array"></param>
        public static void LinearRegression(System.Windows.Point[] array, out double a, out double b)
        {
            a = double.NaN;
            b = double.NaN;
            //点数不能小于2
            if (array.Length < 2)
            {
                string msg = $"Linear regression exception: points count [{array.Length}]";
                throw new Exception(msg);
            }
            //求出横纵坐标的平均值
            double averagex = 0, averagey = 0;
            foreach (Point p in array)
            {
                averagex += p.X;
                averagey += p.Y;
            }
            averagex /= array.Length;
            averagey /= array.Length;
            //经验回归系数的分子与分母
            double numerator = 0;
            double denominator = 0;
            foreach (Point p in array)
            {
                numerator += (p.X - averagex) * (p.Y - averagey);
                denominator += (p.X - averagex) * (p.X - averagex);
            }
            //回归系数b（Regression Coefficient）
            double RCB = numerator / denominator;
            //回归系数a
            double RCA = averagey - RCB * averagex;

            //Console.WriteLine("回归系数A： " + RCA.ToString("0.0000"));
            //Console.WriteLine("回归系数B： " + RCB.ToString("0.0000"));
            //Console.WriteLine(string.Format("方程为： y = {0} + {1} * x",
            //RCA.ToString("0.0000"), RCB.ToString("0.0000")));

            b = RCA;
            a = RCB;
        }
    }
}
