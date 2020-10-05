using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                    harcosLista.Add(new Harcos(st[0], int.Parse(st[1])));
                }
                r.Close();

                //foreach (var item in harcosLista)
                //{
                //    Console.WriteLine(item);
                //    Console.WriteLine();
                //}
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A fájl nem található!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex);
            }

            Console.Write("Kérem adja meg milyen néven szeretné létrehozni harcosát: ");
            string bekertHarcosNev = Console.ReadLine();
            int bekertStatuszSablon = 0;
            bool bevitel;
            do
            {
                try
                {
                    Console.Write("Kérem adja meg milyen státusz sablonnal szeretné létrehozni harcosát(1,2 v. 3): ");
                    bevitel = int.TryParse(Console.ReadLine(), out bekertStatuszSablon);
                    while (!bevitel)
                    {
                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                    if (bekertStatuszSablon != 1 && bekertStatuszSablon != 2 && bekertStatuszSablon != 3 && bevitel)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hiba! Az érték 1, 2 vagy 3 lehet csak! Kérem adja meg újra!" +
                            "\nNyomjon egy ENTER-t a folytatáshoz!");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            while (bekertStatuszSablon != 1 && bekertStatuszSablon != 2 && bekertStatuszSablon != 3);

            harcosLista.Add(new Harcos(bekertHarcosNev, bekertStatuszSablon));

            do
            {
                Console.Clear();
                Console.WriteLine("\nA többi harcos: \n");
                for (int i = 0; i < harcosLista.Count - 1; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ". " + harcosLista[i]);
                    Console.WriteLine();
                }
                Console.WriteLine("Az Ön harcosa: \n\n\t" + (harcosLista.Count) + ". " + harcosLista[harcosLista.Count - 1]);


            }
            while (false);


            Console.ReadKey();
        }
    }
}
