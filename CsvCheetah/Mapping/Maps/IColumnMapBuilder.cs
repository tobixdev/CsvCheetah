using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping.Maps
{
    public interface IColumnMapBuilder<TData>
    {
        IColumnMapBuilder<TData> WithColumn<TTarget>(int columnIndex, Expression<Func<TData, TTarget>> propertyExpression);
        IMap<TData> Build();
    }
}