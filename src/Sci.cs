﻿#region SearchAThing.UnitTests, Copyright(C) 2016 Lorenzo Delana, License under MIT
/*
* The MIT License(MIT)
* Copyright(c) 2016 Lorenzo Delana, https://searchathing.com
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
* DEALINGS IN THE SOFTWARE.
*/
#endregion

using SearchAThing.Core;
using SearchAThing.Sci;
using System;
using System.Collections.Generic;
using Xunit;
using static System.Math;

namespace SearchAThing.UnitTests
{

    public class Sci
    {
        IModel model = new SampleModel();

        [Fact]
        public void Vector3DTest()
        {
            var tolLen = model.MUDomain.Length.Value;
            var tolRad = model.MUDomain.PlaneAngle.Value;

            // length
            Assert.True(new Vector3D(1, 5.9, 4).Length.EqualsTol(tolLen, 7.198));

            // normalized
            Assert.True(new Vector3D(1, 5.9, 4).Normalized().EqualsTol(Constants.NormalizedLengthTolerance, new Vector3D(0.13893, 0.81968, 0.55572)));

            // distance
            Assert.True(new Vector3D(1, 5.9, 4).Distance(new Vector3D(3, 4.3, 1.03)).EqualsTol(tolLen, 3.922));

            // dot product
            Assert.True(new Vector3D(5, 1, 3).DotProduct(new Vector3D(5, 4, 6)).EqualsTol(tolLen, 47));

            // cross product
            Assert.True(new Vector3D(2, 4, 12).CrossProduct(new Vector3D(3, 6, 1)).EqualsTol(tolLen, new Vector3D(-68, 34, 0)));

            // angle rad
            Assert.True(new Vector3D(3.48412, 2.06577, 0).AngleRad(tolLen, new Vector3D(1.4325, 2.70248, 0)).EqualsTol(tolRad, 0.548));

            // angle rad
            Assert.True(new Vector3D(3.48412, 2.06577, 0).AngleRad(tolLen, new Vector3D(-3.48412, -2.066, 0)).EqualsTol(tolRad, PI));

            // vector projection
            Assert.True(new Vector3D(101.546, 25.186, 1.3).Project(new Vector3D(48.362, 46.564, 5))
                .EqualsTol(tolLen, new Vector3D(64.9889, 62.5728, 6.719)));

            // vector vers
            Assert.True(new Vector3D(101.546, 25.186, 1.3).Concordant(tolLen, new Vector3D(50.773, 12.593, .65)));
            Assert.False(new Vector3D(101.546, 25.186, 1.3).Concordant(tolLen, new Vector3D(-50.773, -12.593, .65)));

            // angle toward
            Assert.True(new Vector3D(120.317, 42.914, 0).AngleToward(tolLen, new Vector3D(28.549, 63.771, 0), Vector3D.ZAxis)
                .EqualsTol(0.80726, MUCollection.PlaneAngle.rad.Tolerance(model)));

            Assert.False(new Vector3D(120.317, 42.914, 0).AngleToward(tolLen, new Vector3D(28.549, 63.771, 0), -Vector3D.ZAxis)
                .EqualsTol(0.80726, MUCollection.PlaneAngle.rad.Tolerance(model)));

            Assert.True(new Vector3D(120.317, 42.914, 0).AngleToward(tolLen, new Vector3D(28.549, 63.771, 0), -Vector3D.ZAxis)
                .EqualsTol(2 * PI - 0.80726, MUCollection.PlaneAngle.rad.Tolerance(model)));

            // z-axis rotation
            Assert.True(new Vector3D(109.452, 38.712, 0).RotateAboutZAxis((50.0).ToRad())
                .EqualsTol(tolLen, new Vector3D(40.699, 108.728, 0)));

            // arbitrary axis rotation
            Assert.True(new Vector3D(747.5675, 259.8335, 0).RotateAboutAxis(new Vector3D(123.151, 353.8977, 25.6), (50.0).ToRad())
                .EqualsTol(tolLen, new Vector3D(524.3462, 370.9603, -462.4069)));

            // rotate relative
            Assert.True(
                new Vector3D(69.1831, 157.1155, 300).RotateAs(tolLen,
                    new Vector3D(443.6913, 107.8843, 0), new Vector3D(342.7154, 239.6307, 0))
                .EqualsTol(tolLen, new Vector3D(7.3989, 171.5134, 300)));

            // vector parallel (1-d)
            Assert.True(new Vector3D(1, 0, 0).IsParallelTo(tolLen, new Vector3D(-1, 0, 0)));
            Assert.True(new Vector3D(0, 1, 0).IsParallelTo(tolLen, new Vector3D(0, -2, 0)));
            Assert.True(new Vector3D(0, 0, 1).IsParallelTo(tolLen, new Vector3D(0, 0, -3)));

            // vector parallel (2-d)
            Assert.True(new Vector3D(1, 1, 0).IsParallelTo(tolLen, new Vector3D(-2, -2, 0)));
            Assert.False(new Vector3D(1, 1, 0).IsParallelTo(tolLen, new Vector3D(-2, 2, 0)));

            Assert.True(new Vector3D(1, 0, 1).IsParallelTo(tolLen, new Vector3D(-2, 0, -2)));
            Assert.False(new Vector3D(1, 0, 1).IsParallelTo(tolLen, new Vector3D(-2, 0, 2)));

            Assert.True(new Vector3D(0, 1, 1).IsParallelTo(tolLen, new Vector3D(0, -2, -2)));
            Assert.False(new Vector3D(0, 1, 1).IsParallelTo(tolLen, new Vector3D(0, -2, 2)));

            // vector parallel (3-d)
            Assert.True(new Vector3D(1, 1, 1).IsParallelTo(tolLen, new Vector3D(-2, -2, -2)));
            Assert.True(new Vector3D(253.6625, 162.6347, 150).IsParallelTo(tolLen, new Vector3D(380.4937, 243.952, 225)));

            // vector parallel ( tolerance checks )
            {
                // ensure mm tolerance 1e-1
                var tmpModel = new SampleModel();

                var tmpTolLen = 1e-1;

                // Z component of first vector will be considered zero cause < 1e-1 model tolerance     
                Assert.True(new Vector3D(10, 0, 0.05).IsParallelTo(tmpTolLen, new Vector3D(-2, 0, 0)));

                // Z component of first vector will be considered not-zero cause > 1e-1 model tolerance
                Assert.False(new Vector3D(10, 0, 0.11).IsParallelTo(tmpTolLen, new Vector3D(-2, 0, 0)));

                // X, Z components of first vector force internal usage of normalized tolerance 1e-4
                // cause the length of the shortest vector here is < 1.5 and may represents a normalized vector
                // or a result of such type of operation
                Assert.True(new Vector3D(0.09, 0, 0.09).IsParallelTo(tmpTolLen, new Vector3D(-20, 0, -20)));
            }
        }

        [Fact]
        void Line3DTest()
        {
            var tolLen = model.MUDomain.Length.Value;            

            // line contains point
            {
                // line (0,0,0)-(1,0,0)
                var l = new Line3D(Vector3D.Zero, new Vector3D(1, 0, 0), Line3DConstructMode.PointAndVector);
                Assert.True(l.LineContainsPoint(tolLen, 2, 0, 0));
                Assert.False(l.LineContainsPoint(tolLen, 2, 1, 0));
                Assert.False(l.LineContainsPoint(tolLen, 2, 0, 1));

                Assert.True(l.SegmentContainsPoint(tolLen, 1, 0, 0));
                Assert.True(l.SegmentContainsPoint(tolLen, 0, 0, 0));
                Assert.False(l.SegmentContainsPoint(tolLen, -.1, 0, 0));
            }

            // line 3d intersection
            {
                {
                    var l1 = new Line3D(new Vector3D(0, 0, 0), new Vector3D(1, 0, 0));
                    var l2 = new Line3D(new Vector3D(2, 0, 0), new Vector3D(2, 0, 2));

                    Assert.True(l1.Intersect(tolLen, l2).EqualsTol(tolLen, 2, 0, 0));
                }

                {
                    var l1 = new Line3D(new Vector3D(0, 0, 0), new Vector3D(0, 1, 0));
                    var l2 = new Line3D(new Vector3D(0, 2, 0), new Vector3D(2, 2, 0));

                    Assert.True(l1.Intersect(tolLen, l2).EqualsTol(tolLen, 0, 2, 0));
                }

                {
                    var l1 = new Line3D(new Vector3D(0, 0, 0), new Vector3D(0, 0, 1));
                    var l2 = new Line3D(new Vector3D(0, 0, 2), new Vector3D(2, 0, 2));

                    Assert.True(l1.Intersect(tolLen, l2).EqualsTol(tolLen, 0, 0, 2));
                }

                {
                    var l1 = new Line3D(new Vector3D(0, 0, 0), new Vector3D(1.6206, 2, -1.4882));
                    var l2 = new Line3D(new Vector3D(1.2, .7, 2), new Vector3D(.6338, .3917, .969));

                    Assert.True(l1.Intersect(tolLen, l2).EqualsTol(tolLen, 0.0675, 0.0833, -0.062));
                }
            }

            // project point on a line
            {
                var p = new Vector3D(1, 1, 0);
                var perpLine = Line3D.XAxisLine.Perpendicular(tolLen, p);
                Assert.True(perpLine.From.EqualsTol(tolLen, p) && perpLine.To.EqualsTol(tolLen, 1, 0, 0));
            }

            // check two lines are colinear
            {
                Assert.True(new Line3D(new Vector3D(0, 0, 0), new Vector3D(1, 1, 1))
                    .Colinear(tolLen, new Line3D(new Vector3D(2, 2, 2), new Vector3D(3, 3, 3))));

                Assert.False(new Line3D(new Vector3D(0, 0, 0), new Vector3D(1, 1, 1))
                    .Colinear(tolLen, new Line3D(new Vector3D(0, 0, 0), new Vector3D(1, 1, 1.11))));
            }
        }

        [Fact]
        void VectorStringifyTest()
        {
            Assert.True((0.5049).Stringify(3) == (0.5051).Stringify(3));
            Assert.True(new Vector3D(0.5049, 1, 2).Stringify(3) == "0.505_1_2");
        }

        [Fact]
        void Matrix3DTest()
        {
            var m = new Matrix3D(new double[] {
                1, .5, 6,
                .1, 2, .05,
                .7, 11, .55
            });

            // det
            Assert.True(m.Determinant().EqualsTol(1e-6, -1.260));

            // inv
            Assert.True(m.Inverse().EqualsTol(1e-3, new Matrix3D(new double[] {
                -0.437, -52.163, 9.504,
                0.016, 2.897, -0.437,
                0.238, 8.452, -1.548
            })));

            // solve
            Assert.True(m.Solve(1.1, 2.2, 3.3).EqualsTol(1e-3, new Vector3D(-83.875, 4.95, 13.75)));
        }

        [Fact]
        void CoordinateSystemTest()
        {
            var p = new Vector3D(53.0147, 34.5182, 20.1);

            var o = new Vector3D(15.3106, 22.97, 0);
            var v1 = new Vector3D(10.3859, 3.3294, 30);
            var v2 = new Vector3D(2.3515, 14.101, 0);

            var cs = new CoordinateSystem(o, v1, v2);

            var u = p.ToUCS(cs);
            Assert.True(u.EqualsTol(1e-4, 32.3623, 12.6875, -27.3984));
            Assert.True(u.ToWCS(cs).EqualsTol(1e-4, p));
        }

        [Fact]
        void PolygonTest()
        {
            var tolLen = model.MUDomain.Length.Value;

            var B = 700; var b = 50;
            var H = 900; var h = 50;

            var pts = new List<Vector3D>()
                {
                    new Vector3D(0, 0, 0), new Vector3D(B, 0, 0),
                    new Vector3D(B, h, 0),new Vector3D(b, h, 0),
                    new Vector3D(b, H+h, 0), new Vector3D(B, H+h, 0),
                    new Vector3D(B, H +2*h, 0), new Vector3D(0, H+2*h, 0)
                };

            // area
            var area = pts.Area(tolLen);
            Assert.True(area.EqualsTol(tolLen, 2 * B * h + H * b));

            // centroid
            Assert.True(pts.Centroid(tolLen, area).EqualsTol(tolLen, (2 * h * B * B / 2 + b * b * H / 2) / area, H / 2 + h, 0));

        }

        [Fact]
        void BBox3DTest()
        {
            var tolLen = model.MUDomain.Length.Value;

            var pts = new List<Vector3D>()
                {
                    new Vector3D(1,5,8),
                    new Vector3D(-2,-.5,9),
                    new Vector3D(10,.5,.9)
                };

            var bbox = pts.BBox();

            Assert.True(bbox.Min.EqualsTol(tolLen, -2, -.5, .9));
            Assert.True(bbox.Max.EqualsTol(tolLen, 10, 5, 9));

            var bbox2 = new BBox3D().Union(new Vector3D(1, 2, 3));
            Assert.True(bbox.EqualsTol(tolLen, bbox.Union(bbox2)));

            Assert.True(bbox.Contains(tolLen, bbox2));
        }

        [Fact]
        void MeasureUnitTest()
        {
            var mud = new MUDomain();

            // Length
            {
                var tol = mud.Length.Value;

                var mm = MUCollection.Length.mm;
                var m = MUCollection.Length.m;
                var km = MUCollection.Length.km;

                var a = (212356.435 * mm).ConvertTo(mud).Value;
                var b = (a / 1e3 * m).ConvertTo(mud).Value;
                var c = (b / 1e3 * km).ConvertTo(mud).Value;

                Assert.True(a.EqualsTol(tol, b));
                Assert.True(c.EqualsTol(tol, c));
            }

        }

    }

}
