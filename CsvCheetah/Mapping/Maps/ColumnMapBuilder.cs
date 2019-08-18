using System;
using System.Linq.Expressions;
using System.Reflection;

namespace tobixdev.github.io.CsvCheetah.Mapping.Maps
{
    public class ColumnMapBuilder<TData> : IColumnMapBuilder<TData> where TData : class
    {
        private readonly IMutableMap<TData> _map = new SimpleMap<TData>();

        public IColumnMapBuilder<TData> WithColumn<TTarget>(int columnIndex, Expression<Func<TData, TTarget>> propertyExpression)
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

        public IMap<TData> Build()
        {
            return _map;
        }
    }
}