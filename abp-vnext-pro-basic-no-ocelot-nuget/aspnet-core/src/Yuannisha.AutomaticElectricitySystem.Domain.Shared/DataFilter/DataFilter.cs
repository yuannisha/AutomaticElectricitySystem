using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuannisha.AutomaticElectricitySystem
{
    public class DataFilter : DataFilter<object>
    {
    }

    public class DataFilter<T>
    {
        //[{"operator":"like","value":"91520","property":"code"},
        //{"operator":"like","value":"aaa","property":"name"},
        //{"operator":"==","value":true,"property":"status"},
        //{"operator":"lt","value":1,"property":"money"},
        //{"operator":"gt","value":1,"property":"money"},
        //{"operator":"eq","value":2,"property":"taxRate"},
        //{"operator":"lt","value":"07/01/2018","property":"moneyEndDate"},
        //{"operator":"gt","value":"06/01/2018","property":"moneyEndDate"},
        //{"operator":"eq","value":"07/17/2018","property":"paymentDate"}]

        /// <summary>
        /// 操作符
        /// </summary>
        public string Operator { get; set; } = "like";

        /// <summary>
        /// 属性名
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }
    }
}
