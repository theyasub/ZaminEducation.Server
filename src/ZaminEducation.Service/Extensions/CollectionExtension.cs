using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaminEducation.Domain.Configurations;
using ZaminEducation.Service.Helpers;

namespace ZaminEducation.Service.Extensions
{
    public static class CollectionExtension
    {
        public static IEnumerable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
        {
            var metaData = new PaginationMetaData(source.Count(), @params);

            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);

            return @params.PageSize > 0 && @params.PageIndex >= 0
                ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : source;
        }
    }
}
