using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace B_VS_V_
{
    class MicrobiologyLab : IContainable
    {
        private List<Microbe> microbes = new List<Microbe>();

        private string labName;

        public string LabName
        {
            get { return labName; }
        }

        private bool isVirusLab;

        private const string logFileName = "microbes.txt";

        public MicrobiologyLab(string labName, bool isVirusLab)
        {
            this.labName = labName;
            this.isVirusLab = isVirusLab;
        }

        public void Add(Microbe m)
        {
            if ((!isVirusLab) && m is Virus)
            {
                throw new NotVirusLaborException();
            }
            microbes.Add(m);
        }
        public object this[int i]
        {
            get
            {
                return this.microbes[i];
            }
        }

        public int Count
        {
            get
            {
                return this.microbes.Count();
            }
        }

        public void ReadFromFile(string filename)
        {
            StreamReader reader = null;
            string[] tokens;

            try
            {
                reader = new StreamReader(filename);
                while (!reader.EndOfStream)
                {
                    tokens = reader.ReadLine().Split(';');

                    switch (tokens.Length)
                    {
                        case 1:
                            throw new ArgumentException("A txt fájl sora nem megfelelő hosszú.");
                        case 2:
                            Add(new Bacterium(tokens[0], float.Parse(tokens[1])));
                            break;
                        default:
                            Add(new Virus(tokens[0], float.Parse(tokens[1]), tokens.Skip(2).ToArray()));
                            break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nem sikerült megnyitni a fájt.");
            }
            catch (IOException e)
            {
                Console.WriteLine("Valamilyen I/O hiba történt: {0}", e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Nem sikerült  a parsolás: {0}", e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch(TooShortException e)
            {
                Console.WriteLine("Nem elég hosszú.", e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("A név nem lehet nulla.");
            }
            catch (NotNegativException e)
            {
                Console.WriteLine("A reprodukciós ráta nem lehet negatív.", e.Message);
            }
            catch (FewHostsException e)
            {
                Console.WriteLine("Kevés a hostok száma.", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Vmilyen hiba történt.", e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void AnalyzeRandomSamples(byte number)
        {
            List<Microbe> selectedMicrobes = new List<Microbe>();
            Random rnd = new Random();
            StreamWriter writer = null;
            writer = new StreamWriter(logFileName);

            for (int i = 0; i < number; i++)
            {
                int rndNumber = rnd.Next(microbes.Count);
                Microbe m = microbes[rndNumber];

                rndNumber = rnd.Next(microbes.Count);
                if (!selectedMicrobes.Contains(microbes[rndNumber]) && microbes[rndNumber] != m)
                    selectedMicrobes.Add(microbes[rndNumber]);

                List<IAnalyzable> selectedAnalyzables = selectedMicrobes.ToList<IAnalyzable>();
                m.Analyze(selectedAnalyzables);

                writer.WriteLine("Az alábbi mikróbát analizálom: " + m.ToString());

            }
        }
    }
}
