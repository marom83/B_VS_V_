using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace B_VS_V_
{
    class Program
    {
        static void Main(string[] args)
        {
            MicrobiologyLab lab = new MicrobiologyLab("Pesti laboratórium", true);
            lab.ReadFromFile("microbes.txt");
            lab.AnalyzeRandomSamples(3);

            Console.WriteLine(lab);

            Console.ReadLine();
        }
    }
}
