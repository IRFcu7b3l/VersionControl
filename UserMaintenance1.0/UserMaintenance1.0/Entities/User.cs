using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintenance1._0.Entities
{
    internal class User
    {
        public Guid id { get; set; } = Guid.NewGuid();


        public string FullName { get; set; }
    }
}
