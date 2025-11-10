using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
   public static class AppDpContext<T>
    {
        public static List<T> datas;
        static AppDpContext()
        {
            datas = new List<T>();
        }
    }
}
