using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rhino.Geometry;
using CreateStair;

namespace CreateStairTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateCenterLineTest()
        {
            // Prepair test case
            Point3d origin = new Point3d(0, 0, 0);
            Point3d top = new Point3d(0, 0, 70);
            Line centerLineExpected = new Line(origin, top);

            // create center line
            Line centerLine = StairCreator.CreateCenterLine(70);

            double distFrom = (centerLineExpected.From - centerLine.From).Length;
            double distTo = (centerLineExpected.To - centerLine.To).Length;

            Assert.AreEqual(0.0, distFrom);
            Assert.AreEqual(0.0, distTo);
        }
        
        [TestMethod]
        public void DivideCenterLineTest()
        {
            // Prepair test case
            Point3d[] dividedPtsExpected = new Point3d[11];
            for(int i = 0; i < 11; i++)
            {
                dividedPtsExpected[i] = new Point3d(0, 0, i * 70.0 / 10.0);
            }

            Line centerLine = StairCreator.CreateCenterLine(70);
            Point3d[] dividedPts = StairCreator.DivideCenterLine(centerLine, 10);

            for(int i = 0; i < 11; i++)
            {
                double dist = (dividedPtsExpected[i] - dividedPts[i]).Length;
                Assert.AreEqual(0.0, dist);
            }
        }

        [TestMethod]
        public void CreateSpiralLinesTest()
        {
            // Prepair test case
            Point3d[] expectedSpiralPts = new Point3d[10];
            for(int i = 0; i < 10; i++)
            {
                Point3d pts = new Point3d(70.0, 0, 70.0 * (double)(i + 1) / 10.0);
                double angle = 2.0 * Math.PI * (double)i / 10.0;
                expectedSpiralPts[i] = Transform.Rotation(angle, Vector3d.ZAxis, Point3d.Origin) * pts;
            }
            Line centerLine = StairCreator.CreateCenterLine(70);
            Point3d[] dividedPts = StairCreator.DivideCenterLine(centerLine, 10);
            Line[] spiralLines = StairCreator.CreateSpiralLines(dividedPts, 70, 2.0 * Math.PI);
            for(int i = 0; i < 10; i++)
            {
                double dist = (expectedSpiralPts[i] - spiralLines[i].To).Length;
                Assert.AreEqual(0.0, dist);
            }
        }
    }
}
