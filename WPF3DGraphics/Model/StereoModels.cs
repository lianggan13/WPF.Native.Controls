using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WPF3DGraphics.Model
{
    public static class StereoModels
    {
        private const int N = 24;  // 圆的等份
        public static Model3D DrawCircle(Vector3D pos, Vector3D normalVec, double radius, Brush foreColor, Brush backColor)
        {
            List<Point3D> plist = GetCirclePlist(N, pos, radius);
            List<int> nlist = GetCiclePIndex(N);
            MeshGeometry3D mgd = new MeshGeometry3D()
            {
                Positions = new Point3DCollection(plist),
                TriangleIndices = new Int32Collection(nlist),
            };
            GeometryModel3D gmd = new GeometryModel3D();
            gmd.Geometry = mgd;
            gmd.Material = new DiffuseMaterial(foreColor);
            gmd.BackMaterial = new DiffuseMaterial(backColor);

            // 法向量  倾斜角度
            Vector3 vRaw = new Vector3(0, 0, 1);
            Vector3 vNormal = new Vector3((float)normalVec.X, (float)normalVec.Y, (float)normalVec.Z);
            Vector3 vRotate = Vector3.Cross(vRaw, vNormal);
            float cosTheta = Vector3.Dot(vRaw, vNormal) / (vRaw.Length() * vNormal.Length());
            double theta = Math.Acos(cosTheta) * (180 / Math.PI);

            AxisAngleRotation3D ard = new AxisAngleRotation3D()
            {
                Axis = new Vector3D(vRotate.X, vRotate.Y, vRotate.Z),
                Angle = theta,

            };
            RotateTransform3D rt = new RotateTransform3D()
            {
                CenterX = pos.X,
                CenterY = pos.Y,
                CenterZ = pos.Z,
                Rotation = ard,
            };

            gmd.Transform = rt;

            return gmd;
        }

        public static Model3D DrawCone(Vector3D pos, Vector3D normalVec, double radius, double height, Brush foreColor, Brush backColor)
        {
            // 底面圆
            var circle = DrawCircle(pos, normalVec, radius, foreColor, backColor) as GeometryModel3D;

            // 侧外围
            var plist = GetCirclePoints(circle);
            var indexes = GetCircleTriangleIndices(circle);
            Point3D top = new Point3D(pos.X, pos.Y, pos.Z + height);
            plist[0] = top;

            GeometryModel3D ngmd = new GeometryModel3D();
            ngmd.Geometry = new MeshGeometry3D()
            {
                Positions = new Point3DCollection(plist),
                TriangleIndices = new Int32Collection(indexes),
            };
            ngmd.Material = new DiffuseMaterial(foreColor);
            ngmd.BackMaterial = new DiffuseMaterial(backColor);

            Model3DGroup group3dm = new Model3DGroup();
            group3dm.Children.Add(circle);
            group3dm.Children.Add(ngmd);

            return group3dm;
        }

        public static List<Point3D> GetCirclePoints(GeometryModel3D circle)
        {
            var mgd = circle.Geometry as MeshGeometry3D;
            return mgd.Positions.ToList();
        }

        public static List<int> GetCircleTriangleIndices(GeometryModel3D circle)
        {
            var mgd = circle.Geometry as MeshGeometry3D;
            return mgd.TriangleIndices.OfType<int>().ToList();
        }

        public static Model3D DrawCylinder(Vector3D pos, Vector3D normalVec, double radius, double height, Brush foreColor, Brush backColor)
        {
            // 顶面圆
            Vector3D top = new Vector3D(pos.X, pos.Y, pos.Z + height);
            var circle1 = DrawCircle(top, normalVec, radius, foreColor, backColor) as GeometryModel3D;

            // 底面圆
            var circle2 = DrawCircle(pos, normalVec, radius, foreColor, backColor) as GeometryModel3D;

            // 侧外围
            var plist1 = GetCirclePoints(circle1);
            var plist2 = GetCirclePoints(circle2);
            plist1.RemoveAt(0);
            plist2.RemoveAt(0);

            var plist = new List<Point3D>();
            plist.AddRange(plist1);
            plist.AddRange(plist2);

            int n = Math.Min(plist1.Count, plist2.Count);
            List<int> indexes = new List<int>();
            for (int i = 0; i < n - 1; i++)
            {
                indexes.Add(i);
                indexes.Add(i + 1);
                indexes.Add(i + n);

                indexes.Add(i + 1);
                indexes.Add(i + n);
                indexes.Add(i + n + 1);
            }

            indexes.Add(n - 1);
            indexes.Add(0);
            indexes.Add(2 * n - 1);

            indexes.Add(0);
            indexes.Add(2 * n - 1);
            indexes.Add(n);

            GeometryModel3D ngmd = new GeometryModel3D();
            ngmd.Geometry = new MeshGeometry3D()
            {
                Positions = new Point3DCollection(plist),
                TriangleIndices = new Int32Collection(indexes),
            };
            ngmd.Material = new DiffuseMaterial(foreColor);
            ngmd.BackMaterial = new DiffuseMaterial(backColor);

            Model3DGroup group3dm = new Model3DGroup();
            group3dm.Children.Add(circle2);
            group3dm.Children.Add(circle1);
            group3dm.Children.Add(ngmd);

            return group3dm;
        }


        static List<Point3D> GetCirclePlist(int n, Vector3D center, double radius)
        {
            double a = 2 * Math.PI / n;
            List<Point3D> points = new List<Point3D>();
            points.Add(new Point3D(center.X, center.Y, center.Z)); // 圆心点坐标
            for (int i = 0; i < n; i++)
            {
                points.Add(new Point3D()
                {
                    X = center.X + radius * Math.Cos(a * i),
                    Y = center.Y + radius * Math.Sin(a * i),
                    Z = center.Z,
                });
            }
            return points;
        }

        static List<int> GetCiclePIndex(int n)
        {
            // n = 8
            List<int> indexes = new List<int>();
            for (int i = 1; i < n; i++)
            {
                // i == 7
                indexes.Add(i);
                indexes.Add(i + 1);
                indexes.Add(0);
            }

            indexes.Add(n);
            indexes.Add(1);
            indexes.Add(0);

            return indexes;
        }

    }
}
