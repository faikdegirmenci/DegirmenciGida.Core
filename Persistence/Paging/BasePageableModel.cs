﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Paging
{
    public class BasePageableModel
    {
        public int Size { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
