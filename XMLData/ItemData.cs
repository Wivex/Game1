﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public abstract class ItemData : EntityData
    {
        public uint Cost { get; set; }
    }
}