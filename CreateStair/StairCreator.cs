using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace CreateStair
{
    public class StairCreator
    {
        public static Line CreateCenterLine(double height)
        {
            Point3d origin = new Point3d(0, 0, 0);
            Point3d top = new Point3d(0, 0, height);
            Line centerLine = new Line(origin, top);
            return centerLine;
        }
        public static Point3d[] DivideCenterLine(Line centerLine, int numberOfStairs)
        {
            Point3d from = centerLine.From;
            Point3d to = centerLine.To;
            Point3d[] dividedPts = new Point3d[numberOfStairs + 1];
            for (int i = 0; i < numberOfStairs + 1; i++)
            {
                dividedPts[i] = from + (double)i / (double)numberOfStairs * (to - from);
            }
            return dividedPts;
        }
        public static Line[] CreateSpiralLines(Point3d[] dividedPoints, double length, double totalRotation)
        {
            int numberOfStairs = dividedPoints.Length - 1;
            Line[] spiralLines = new Line[numberOfStairs];
            for(int i = 0; i < numberOfStairs; i++)
            {
                Point3d pts = dividedPoints[i + 1] + new Vector3d(length, 0, 0);
                double angle = totalRotation * (double)i / (double)numberOfStairs;
                var rotation = Transform.Rotation(angle, Vector3d.ZAxis, Point3d.Origin);
                spiralLines[i] = new Line(dividedPoints[i + 1],  rotation * pts);
            }
            return spiralLines;
        }
        public static Tuple<Line, Line[]> CreateStair(double height, double length, double totalRotation, int numberOfStairs)
        {
            Line centerLine = CreateCenterLine(height);
            Point3d[] dividedPts = DivideCenterLine(centerLine, numberOfStairs);
            Line[] spiralLines = CreateSpiralLines(dividedPts, length, totalRotation);
            return new Tuple<Line, Line[]>(centerLine, spiralLines);
        }
    }
}
