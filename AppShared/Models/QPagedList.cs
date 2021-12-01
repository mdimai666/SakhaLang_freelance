using BlastCore.Extensions;
using Remote.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppShared.Models
{
    [Serializable]
    public class QPagedList<T>
    {
        public int totalCount { get; set; }
        public IEnumerable<T> data { get; set; }
        public int page { get; set; }
        public int perpage { get; set; }
        public int totalPages
        {
            get
            {
                int pagesCount = totalCount / perpage;
                if (perpage < ((float)totalCount / (float)perpage)) pagesCount++;
                return pagesCount;
            }
        }
    }

    public class QueryFilter
    {
        public string Search { get; set; } = "";
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
        //public string SortField { get; set; } = "";
        //public bool SortDesc { get; set; } = true;
        /// <summary>
        /// example: "Id, -Created"
        /// </summary>
        public string OrderByStringParam { get; set; }

        public string JsonExpression { get; set; }

        [Obsolete]
        public IEnumerable<TableFilter> Filters { get; set; }

        public QueryFilter()
        {

        }

        public QueryFilter(int page, int pageSize)
        {
            Skip = --page * pageSize;
            Take = pageSize;
        }

        public QueryFilter AddQuery<T>(Expression<Func<T, bool>> expression)
        {
            string serialized = RemoteLinq.SerialiseRemoteExpression(expression);

            //JsonExpression = RemoteLinq.SerialiseRemoteExpression(expression);
            //var bytes = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(serialized));
            //var str = Encoding.UTF8.GetString(bytes);
            //JsonExpression = /*Uri.EscapeDataString*/(/*RemoteLinq.Compress*/(str));
            //JsonExpression = RemoteLinq.Compress(str));
            //JsonExpression = RemoteLinq.Compress(serialized);
            JsonExpression = (serialized);

            return this;
        }

        public QueryFilter(QueryFilter f)
        {
            Search = f.Search;
            Skip = f.Skip;
            Take = f.Take;
            OrderByStringParam = f.OrderByStringParam;
            Filters = f.Filters;
            JsonExpression = f.JsonExpression;
        }

    }


    public interface ITableFilter
    {
        public string FieldName { get; set; }
        public IEnumerable<string> SelectedValues { get; set; }
        //public IList<TableFilter> Filters { get; set; }
    }

    public class TableFilter : ITableFilter
    {
        public string FieldName { get; set; }
        public IEnumerable<string> SelectedValues { get; set; }
    }

    public class TotalResponse<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalCount { get; set; }
        public ETotalResponeResult Result { get; set; }
        public string Message { get; set; }

        public bool Ok => Result == ETotalResponeResult.OK;
        public int TotalPages
        {
            get
            {
                if (Records == null) return 0;
                int perpage = Records.Count();

                if (perpage == 0) return 0;

                int pagesCount = TotalCount / perpage;
                if (perpage < ((float)TotalCount / (float)perpage)) pagesCount++;
                return pagesCount;
            }
        }
    }

    public enum ETotalResponeResult
    {
        OK,
        ERROR
    }
}
