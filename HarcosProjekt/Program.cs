﻿using System;
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
        static int menuPont;
        static int menu()
        {

            do
            {
                Console.Clear();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                string s = "Harcos Játék";
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.WriteLine(s);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tMenü");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t1 - Megküzdeni egy harcossal");
                Console.WriteLine("\t2 - Gyógyulni");
                Console.WriteLine("\t3 - Kilépés");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tKérem válasszon menüpontot: ");
                Console.ResetColor();
                try
                {
                    while (!int.TryParse(Console.ReadLine(), out menuPont) || menuPont < 1 || menuPont > 6)
                    {

                        MessageBox.Show("Hiba, nem létező menüpontot választott!");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine();
            }
            while (menuPont < 1 || menuPont > 6);

            return menuPont;
        }

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

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            string s = "Harcos Játék";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\nKérem adja meg milyen néven szeretné létrehozni harcosát: ");
            Console.ResetColor();
            string bekertHarcosNev = Console.ReadLine();
            int bekertStatuszSablon = 0;
            bool bevitel;
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\nKérem adja meg milyen státusz sablonnal szeretné létrehozni harcosát(1,2 v. 3): ");
                    Console.ResetColor();
                    bevitel = int.TryParse(Console.ReadLine(), out bekertStatuszSablon);
                    while (!bevitel)
                    {
                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                    if (bekertStatuszSablon != 1 && bekertStatuszSablon != 2 && bekertStatuszSablon != 3 && bevitel)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nHiba! Az érték 1, 2 vagy 3 lehet csak! Kérem adja meg újra!" +
                            "\n\nNyomjon egy ENTER-t a folytatáshoz!");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine();
                    Console.WriteLine(s);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            while (bekertStatuszSablon != 1 && bekertStatuszSablon != 2 && bekertStatuszSablon != 3);

            harcosLista.Add(new Harcos(bekertHarcosNev, bekertStatuszSablon));
            int korSzamlalo = 0;
            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.WriteLine(s);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\nA többi harcos: \n");
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int i = 0; i < harcosLista.Count - 1; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ". " + harcosLista[i]);
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Az Ön harcosa:");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" \n\t" + (harcosLista.Count) + ". " + harcosLista[harcosLista.Count - 1]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNyomjon egy ENTER-t a menü megjelenítéséhez!");
                if (harcosLista[harcosLista.Count - 1].Eletero <= 0)
                {
                    var yesNO = MessageBox.Show("Szeretne új játékot kezdeni?", "Az Ön harcosa meghalt!", MessageBoxButtons.YesNo);
                    if (yesNO == DialogResult.Yes)
                    {
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                }
                int hanyHalott = 0;
                foreach (var item in harcosLista)
                {
                    if (item.Eletero <= 0)
                        hanyHalott++;
                }
                if (hanyHalott >= harcosLista.Count - 1)
                {
                    var yesNO = MessageBox.Show("Szeretne új játékot kezdeni?", "Gratulálunk Ön nyert!", MessageBoxButtons.YesNo);
                    if (yesNO == DialogResult.Yes)
                    {
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                }
                Console.ReadKey();

                int menuPont;
                do
                {
                    menuPont = menu();

                    switch (menuPont)
                    {
                        case 1:
                            int sorszam = 0;
                            bool beker;
                            do
                            {
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write("\tMelyik harcossal szeretne megküzdeni? Írja ide a sorszámát: ");
                                    Console.ResetColor();
                                    beker = int.TryParse(Console.ReadLine(), out sorszam);
                                    while (!beker)
                                    {
                                        MessageBox.Show("Hiba, csak számot adhat meg!");
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        Console.Write(new string(' ', Console.BufferWidth));
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        break;
                                    }
                                    if ((sorszam < 1 || sorszam > harcosLista.Count) && beker)
                                    {
                                        MessageBox.Show("Hiba! Nem létező sorszámú harcost választott!");
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        Console.Write(new string(' ', Console.BufferWidth));
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            while (sorszam < 1 || sorszam > harcosLista.Count);
                            harcosLista[harcosLista.Count - 1].Megkuzd(harcosLista[sorszam - 1]);
                            break;
                        case 2:
                            harcosLista[harcosLista.Count - 1].Gyogyul();
                            break;
                        case 3:
                            MessageBox.Show("Köszönjük, hogy részt vett a játékban");
                            Environment.Exit(0); break;
                    }
                }
                while (menuPont == 3);
                korSzamlalo++;
                if (korSzamlalo % 3 == 0)
                {
                    Random r = new Random();

                    int kivelKuzdMeg = r.Next(harcosLista.Count - 1);

                    harcosLista[harcosLista.Count - 1].Megkuzd(harcosLista[kivelKuzdMeg]);

                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                    Console.WriteLine(s);
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nEbben a körben az Ön harcosa küzd meg egy véletlenül kiválasztott harcossal");
                    Console.WriteLine("\nAz ellenfél: \n\n\t" + harcosLista[kivelKuzdMeg]);

                    Console.WriteLine("\nA csata után minden harcos gyógyul.");
                    foreach (var item in harcosLista)
                    {
                        item.Gyogyul();
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNyomjon egy ENTER-t a menü megjelenítéséhez!");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
            while (menuPont != 3);


            Console.ReadKey();
        }
    }
}
