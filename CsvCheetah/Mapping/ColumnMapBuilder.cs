using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class ColumnMapBuilder<T> : IColumnMapBuilder<T>
    {
        private readonly IMutableMap<T> _map = new SimpleMap<T>();
        
        public IColumnMapBuilder<T> WithColumn(int columnIndex, Expression<Func<T, string>> propertyExpression)
        {
            _map.AddMapping(columnIndex, propertyExpression);
            
            return this;
        }

        public IMap<T> Build()
        {
            return _map;
        }
    }
}