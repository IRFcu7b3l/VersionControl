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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get {
                return string.Format("{0} {1}", FirstName, LastName);
                    } 
        }
    }
}
