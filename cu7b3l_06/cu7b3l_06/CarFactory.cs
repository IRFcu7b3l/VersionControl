using cu7b3l_06.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cu7b3l_06
{
    internal class CarFactory: IToyFactory
    {
        public Toy CreateNew() 
        {
            return new Car();
        }
    }
}
