using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarcosProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var harcosLista = new List<Harcos>();

            try
            {
                StreamReader r = new StreamReader("harcosok 1.csv", Encoding.UTF8);
                while (!r.EndOfStream)
                {
                    string[] st = r.ReadLine().Split(';');

                    harcosLista.Add(new Harcos(st[0],int.Parse(st[1])));
                }
                r.Close();

                foreach (var item in harcosLista)
                {
                    Console.WriteLine(item);
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A fájl nem található!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex);
            }


            Console.ReadKey();
        }
    }
}
