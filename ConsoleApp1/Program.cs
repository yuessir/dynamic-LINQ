using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp1
{
    static class Program
    {
        static void Main(string[] args)
        {
            //data
            var list = new List<ColorSerial>();
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "purple", ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "orange", ColorGroup2Desc = "purple", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "orange", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "red", ColorGroup2Desc = "orange", ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "green", ColorGroup2Desc = "blue", ColorGroup3Desc = "green", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            list.Add(new ColorSerial() { ColorGroup1Desc = "red", ColorGroup2Desc = "orange",ColorGroup3Desc = "red", ColorGroup4Desc = "blue", ColorGroup5Desc = "yellow", ColorType3Desc = "mix" });
            var queryable = list.AsQueryable();
            

            var gpy = queryable.BuildExpression(new
            {
                PartGroup1Desc  = (string)null,
                PartGroup2Desc = (string)null
            }, nameof(ColorSerial.ColorGroup1Desc));


            //group by different expression(Func)(group by PartGroup1Desc/PartGroup2Desc/PartGroup3Desc...)
            var result = list.GroupBy(gpy.Compile());



        }
     
    }
}
