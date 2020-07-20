using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using NUnit.Framework;

namespace Tests
{
    public class ColorSerial
    {
        public string ColorGroup1Desc { get; set; }
        public string ColorGroup3Desc { get; set; }
        public string ColorGroup2Desc { get; set; }
        public string ColorGroup5Desc { get; set; }
        public string ColorGroup4Desc { get; set; }
        public string ColorType3Desc { get; set; }

    }
    public class Tests
    {
        private IQueryable<ColorSerial> queryable = null;
        [SetUp]
        public void Setup()
        {
            //data
            var list = new List<ColorSerial>();
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "purple", ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "orange", ColorGroup2Desc = "purple", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "orange", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "red", ColorGroup2Desc = "orange", ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "blue", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "red", ColorGroup2Desc = "orange", ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            queryable = list.AsQueryable();
        }

        [Test]
        public void Test1()
        {

            var gpy = queryable.BuildExpression(new
            {
                PartGroup1Desc = (string)null
            }, nameof(ConsoleApp1.ColorSerial.ColorGroup1Desc));


            //group by different expression(Func)(group by PartGroup1Desc/PartGroup2Desc/PartGroup3Desc...)
            var result = queryable.GroupBy(gpy.Compile());
            Assert.AreEqual(result.Count(), 3);
        }
        [Test]
        public void Test2()
        {

            var gpy = queryable.BuildExpression(new ColorSerial(), nameof(ColorSerial.ColorGroup1Desc));

            //group by different expression(Func)(group by PartGroup1Desc/PartGroup2Desc/PartGroup3Desc...)
            var result = queryable.GroupBy(gpy.Compile());
            Assert.AreEqual(result.Count(), 3);
        }


    }
}