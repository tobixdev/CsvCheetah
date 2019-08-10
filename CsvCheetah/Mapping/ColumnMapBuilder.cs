using System;
using System.Linq.Expressions;
using System.Reflection;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class ColumnMapBuilder<T> : IColumnMapBuilder<T> where T : class
    {
        private readonly IMutableMap<T> _map = new SimpleMap<T>();

        public IColumnMapBuilder<T> WithColumn(int columnIndex, Expression<Func<T, string>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("The given Expression is not a member expression.");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            
            if(propertyInfo == null)
                throw new ArgumentException("The given Expression is not a property.");

            _map.AddMapping(columnIndex, propertyInfo.Name);

            return this;
        }

        public IMap<T> Build()
        {
            return _map;
        }
    }
}