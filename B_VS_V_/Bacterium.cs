using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_VS_V_
{
    class Bacterium : Microbe
    {

        private ulong victimCount = 1;

        public override ulong Reproduce(ulong victimCount)
        {
            this.victimCount += (ulong)(this.victimCount * ReproductionRate);
            return this.victimCount;
        }

        public Bacterium(string name, float reproductionRate): base(name, reproductionRate)
        {
        }
        public override int CompareTo(Object obj)
        {
            if (obj == null) throw new Exception("Nem jó.");
            if (!(obj is Bacterium)) throw new Exception("Nem baktérium.");
            Bacterium masik = obj as Bacterium;

            if (this.victimCount > masik.victimCount)
                return 1;
            if (this.victimCount < masik.victimCount)
                return -1;
            return 0;
        }

        public override string ToString()
        {
            return string.Format(Name + " : " + victimCount);
        }
    }
}
