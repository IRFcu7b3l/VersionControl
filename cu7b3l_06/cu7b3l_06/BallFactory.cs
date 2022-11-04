﻿using cu7b3l_06.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cu7b3l_06
{
    public class BallFactory: IToyFactory
    {

        public Toy CreateNew() 
        {
            return new Ball();        
        }

        
    }
}
