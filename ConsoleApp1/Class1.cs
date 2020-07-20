using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ConsoleApp1
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

    public static class QueryableExt
    {
        public static Expression<Func<TElement, TKey>> BuildExpression<TElement, TKey>(this IQueryable<TElement> self, TKey model, params string[] propNames)
        {

            var modelType = model.GetType();
            var props = modelType.GetProperties();
     
            var modelCtor = modelType.GetConstructor(props.Select(t => t.PropertyType).ToArray());

            return self.BuildExpression(model, modelCtor, props, propNames);
        }

        public static Expression<Func<TElement, TKey>> BuildExpression<TElement, TKey>(this IQueryable<TElement> self, TKey model, ConstructorInfo modelCtor, PropertyInfo[] props, params string[] propNames)
        {
            var parameter = Expression.Parameter(typeof(TElement), "x");
            var propExpressions = props
                .Select(p =>
                {
                    Expression value;

                    if (propNames.Contains(p.Name))
                        value = Expression.PropertyOrField(parameter, p.Name);
                    else
                        value = Expression.Convert(Expression.Constant(null, typeof(object)), p.PropertyType);

                    return value;
                })
                .ToArray();

            var n = Expression.New(
                modelCtor,
                propExpressions,
                props
            );

            var expr = Expression.Lambda<Func<TElement, TKey>>(n, parameter);
            return expr;
        }
    }
}
