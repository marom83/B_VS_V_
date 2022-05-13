using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_VS_V_
{
    abstract class Microbe: IReproducable
    {
        private string name;

        public string Name
        {
            get { return name; }
            private set {
                if (value.Length == 0)
                    throw new NullReferenceException();
                if (value.Length < 5)
                    throw new TooShortException();
                name = value; }
        }

        private float reproductionRate;

        public float ReproductionRate
        {
            get { return reproductionRate; }
            private set {
                if (value <= 0)
                    throw new NotNegativException();
                reproductionRate = value; }
        }

        protected Microbe(string name, float reproductionRate)
        {
            Name = name;
            ReproductionRate = reproductionRate;
        }

        public abstract ulong Reproduce(ulong victimCount);

        public bool Analyze(List<IAnalyzable> analyzables)
        {
            foreach (IAnalyzable analyzable in analyzables)
            {
                if (analyzable.CompareTo(this) >= 0) return false;
            }return true;
        }

        public virtual int CompareTo(object obj)
        {
            if (obj == null) throw new Exception("Nem jó.");
            if (!(obj is Microbe)) throw new Exception("Nem microbe.");
            Microbe masik = obj as Microbe;

            if(this.reproductionRate > masik.reproductionRate)
                return 1;
            if (this.reproductionRate < masik.reproductionRate)
                return -1;
            return 0;
        }
    }
}
