using AntDesign;
using AntDesign.TableModels;
using AppShared.Models;
using BlastCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppFront.Models
{
    public class TableFilterModel2 : ITableFilterModel
    {
        public string FieldName { get; set; }
        public IEnumerable<string> SelectedValues { get; set; }
        public IList<AntDesign.TableFilter> Filters { get; set; }

        public IQueryable<TItem> FilterList<TItem>(IQueryable<TItem> source)
        {
            throw new NotImplementedException();
        }
    }

    public static class QueryModelExtensions
    {
        public static QueryFilter AsQueryFilter<T>(this QueryModel<T> queryModel, string search = "")
        {
            string sort = queryModel.SortModel?
                .Where(s => s.Sort != null).Select(s =>
                {
                    string descend = ((s.Sort == "descend") ? "-" : "");
                    string sort = $"{descend}{s.FieldName}";
                    return sort;
                }).JoinStr(",");

            QueryFilter filter = new QueryFilter(queryModel.PageIndex, queryModel.PageSize)
            {
                OrderByStringParam = sort,
                Search = search,
                Filters = queryModel?.FilterModel.Select(f => new AppShared.Models.TableFilter
                {
                    FieldName = f.FieldName,
                    SelectedValues = f.SelectedValues
                })
            };

            return filter;
        }
    }

}