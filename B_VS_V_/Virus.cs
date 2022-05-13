using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_VS_V_
{
    class Virus : Microbe, IIlfectable
    {
        private string[] hosts;

        private ulong[] victimCounts;

        private bool isInfecting;

        public bool Infect(string host)
        {
            for (int i = 0; i < hosts.Length; i++)
            {
                if (!hosts.Contains(host))
                {
                    isInfecting = true;
                    victimCounts[i] = Reproduce(victimCounts[i]);
                    isInfecting = false;
                    return true;
                }
            }
            return false;
        }

        public Virus(string name, float reproductionRate, string[] hosts) : base(name, reproductionRate)
        {
            if (hosts.Length < 1) throw new FewHostsException();
            this.hosts = hosts;
            victimCounts = new ulong[hosts.Length];
            for (int i = 0; i < victimCounts.Length; i++)
            {
                victimCounts[i] = 1;
            }
        }

        public override ulong Reproduce(ulong victimCount)
        {
            if (!isInfecting) return victimCount;
            else
            {
                return (ulong)(victimCount * ReproductionRate);
            }
        }

        public override int CompareTo(Object obj)
        {
            if (obj == null) throw new Exception("...");
            if (!(obj is Virus)) throw new Exception("Nem vírus.");
            Virus egyik = obj as Virus;

            ulong sumOfVictimCount = 0;
            for (int i = 0; i < this.victimCounts.Length; i++)
            {
                sumOfVictimCount += this.victimCounts[i];
            }
            ulong sumOfIncomingVictimCount = 0;
            for (int i = 0; i < egyik.victimCounts.Length; i++)
            {
                sumOfIncomingVictimCount += egyik.victimCounts[i];
            }

            if (sumOfVictimCount > sumOfIncomingVictimCount)
                    return 1;
            if (sumOfVictimCount < sumOfIncomingVictimCount)
                    return -1;
            return 0;
        }

        public override string ToString()
        {
            string rest = "";
            for (int i = 0; i < hosts.Length; i++)
            {
                rest += victimCounts[i] + "(" + hosts[i] + ")";
            }
            return string.Format(Name + " : " + rest);
        }
    }
}
