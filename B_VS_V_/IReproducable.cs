﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_VS_V_
{
    interface IReproducable: IAnalyzable
    {
        ulong Reproduce(ulong victimCount);
    }
}
