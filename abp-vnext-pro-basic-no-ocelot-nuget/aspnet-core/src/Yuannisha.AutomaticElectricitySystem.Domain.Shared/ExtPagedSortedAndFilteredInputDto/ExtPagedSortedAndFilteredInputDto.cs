using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Yuannisha.AutomaticElectricitySystem
{
    /// <summary>
    /// 分页、排序及过滤查询
    /// </summary>
    /// <seealso cref="Abp.Application.Services.Dto.PagedAndSortedInputDto" />
    public class ExtPagedSortedAndFilteredInputDto :  IPagedResultRequest, ISortedResultRequest
    {

        /// <summary>
        /// 排序.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// </summary>
        /// <example>
        /// Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </example>
        public string Sorting { get; set; }


        [Range(1, 1000)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public ExtPagedSortedAndFilteredInputDto()
        {
            MaxResultCount = 25;
        }


        private string prefix;
        private Type type;

        /// <summary>
        /// join查询时默认表名（需要带点号）
        /// </summary>
        /// <example>
        ///  from auditLog in _auditLogRepository.GetAll()
        ///  join user in _userRepository.GetAll() on auditLog.UserId equals user.Id into userJoin
        ///   from joinedUser in userJoin.DefaultIfEmpty()
        ///   select new AuditLogAndUser { AuditLog = auditLog, User = joinedUser }
        /// //此时假设默认表是AuditLog，则设置prefix为"AuditLog."
        /// </example>
        /// <param name="value"></param>
        public void SetPrefix(string value, Type type)
        {
            this.type = type;
            this.prefix = value;
        }

        /// <summary>
        /// 获取前缀
        /// </summary>
        /// <returns></returns>
        public string GetPrefix()
        {
            return this.prefix;
        }

        //private string sorting;

        //public override string Sorting
        //{
        //    get { return sorting ?? GetSorter(); }
        //    set { sorting = value; }
        //}

        /// <summary>
        /// 排序字段 
        /// </summary>
        /// <remarks>
        /// json格式
        /// </remarks>
        /// <value>
        /// The sort.
        /// </value>
        public string Sort { get; set; }

        /// <summary>
        /// 分组字段
        /// </summary>
        /// <remarks>
        /// json格式
        /// </remarks>
        /// <value>
        /// The group.
        /// </value>
        public string Group { get; set; }

        //public string Dir { get; set; }

        /// <summary>
        /// 查询字段.
        /// </summary>
        /// <remarks>
        /// json格式
        /// </remarks>
        /// <value>
        /// The fields.
        /// </value>
        public virtual string Fields { get; set; }

        /// <summary>
        /// 查询值.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        /// <remarks>
        /// json格式
        /// </remarks>
        /// <value>
        /// The filters.
        /// </value>
        public virtual string Filters { get; set; }

        /// <summary>
        /// 获取查询字段列表.
        /// </summary>
        /// <returns></returns>
        public virtual List<string> GetFields()
        {
            if (!string.IsNullOrEmpty(Fields))
            {
                var fields = JsonConvert.DeserializeObject<List<string>>(Fields);
                var newFields = new List<string>();
                foreach (var item in fields)
                {
                    newFields.Add(this.GetPrefixProperty(item));
                }
                return newFields;
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取排序字段.
        /// </summary>
        /// <returns></returns>
        public virtual string GetSorter()
        {
            var group = GetGrouper();
            var list = new List<DataSorter>();
            if (!string.IsNullOrEmpty(Group))
            {
                var g = JsonConvert.DeserializeObject<DataSorter>(Group);
                g.Property = this.GetPrefixProperty(g.Property);
                list.Add(g);
            }
            if (!string.IsNullOrEmpty(Sort))
            {
                //var grouped = string.Empty;
                //if (list.Count > 0)
                //{
                //    grouped = list[0].Property;
                //}
                var sorts = JsonConvert.DeserializeObject<List<DataSorter>>(Sort);
                foreach (var item in sorts)
                {
                    item.Property = this.GetPrefixProperty(item.Property);
                    list.Add(item);
                }
                //if (!string.IsNullOrEmpty(grouped))
                //{
                //    list[0].Direction = list.Last(x => x.Property == grouped).Direction;
                //}
            }
            if (list.Count == 0)
            {
                list.Add(new DataSorter { Property = this.GetPrefixProperty("Id"), Direction = SortDirection.DESC });
            }
            var result = string.Join(",", list.Select(x => x.Property + " " + x.Direction));
            return result;
        }

        /// <summary>
        /// 获取分组字段.
        /// </summary>
        /// <returns></returns>
        public virtual string GetGrouper()
        {
            if (!string.IsNullOrEmpty(Group))
            {
                var g = JsonConvert.DeserializeObject<DataSorter>(Group);
                var result = this.GetPrefixProperty(g.Property) + " " + g.Direction;
                return result;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取前缀属性.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public string GetPrefixProperty(string property)
        {
            var newP = new List<string>();
            var p = property.Split('.');
            foreach (var item in p)
            {
                newP.Add(item.Substring(0, 1).ToUpper() + item.Substring(1));
            }
            if (type != null && type.GetProperty(newP[0]) == null)
            {
                newP[0] = prefix + newP[0];
            }
            if (newP.Count > 1)
            {
                return string.Join(".", newP);
            }
            return newP[0];
        }

        /// <summary>
        /// 获取过滤条件列表.
        /// </summary>
        /// <returns></returns>
        public virtual List<DataFilter> GetFilters()
        {
            if (!string.IsNullOrEmpty(Filters))
            {
                var filters = JsonConvert.DeserializeObject<List<DataFilter>>(Filters);
                foreach (var item in filters)
                {
                    item.Property = this.GetPrefixProperty(item.Property);
                }
                return filters;
            }
            var result = new List<DataFilter>();
            return result;
        }

        /// <summary>
        /// 设置过滤条件.
        /// </summary>
        /// <remarks>
        /// 过滤条件为覆盖原条件，非追加
        /// </remarks>
        /// <param name="filter">The filter.</param>
        public virtual void SetFilter(List<DataFilter> filter)
        {
            this.Filters = JsonConvert.SerializeObject(filter);
        }

        public virtual void Normalize()
        {
            if (string.IsNullOrEmpty(Sort))
            {
                Sort = @"[{""property"":""Id"",""direction"":""DESC""}]";
            }
        }


        ///// <summary>
        ///// 检查属性是否存在，不存在自动加上前缀，为复杂类型适配
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="propertyNames"></param>
        ///// <param name="prefix"></param>
        ///// <returns></returns>
        //static List<string> CheckProperty<TSource>(List<string> propertyNames, string prefix)
        //    where TSource : class
        //{
        //    for (int i = 0; i < propertyNames.Count; i++)
        //    {
        //        propertyNames[i] = CheckProperty<TSource>(propertyNames[i], prefix);
        //    }
        //    return propertyNames;
        //}

        //static string CheckProperty<TSource>(string propertyName, string prefix)
        //    where TSource : class
        //{
        //    if (typeof(TSource).GetProperty(propertyName) == null)
        //    {
        //        propertyName = prefix + propertyName;
        //    }
        //    return propertyName;
        //}
    }
}
