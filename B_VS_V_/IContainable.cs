using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_VS_V_
{
    interface IContainable
    {
        void ReadFromFile(string filename);

        object this[int i]
        {
            get;
        }

        int Count { get; }
    }
}
