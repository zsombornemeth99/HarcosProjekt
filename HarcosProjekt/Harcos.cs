using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarcosProjekt
{
    class Harcos
    {
        string nev;
        int szint;
        int tapasztalat;
        int eletero;
        int alapEletero;
        int alapSebzes;

        public Harcos(string nev, int statuszSablon)
        {
            this.nev = nev;
            this.szint = 1;
            this.tapasztalat = 0;
            if (statuszSablon == 1)
            {
                this.alapEletero = 15;
                this.alapSebzes = 3;
                this.eletero = MaxEletero;
            }
            else if (statuszSablon == 2)
            {
                this.alapEletero = 12;
                this.alapSebzes = 4;
                this.eletero = MaxEletero;
            }
            else if (statuszSablon == 3)
            {
                this.alapEletero = 8;
                this.alapSebzes = 5;
                this.eletero = MaxEletero;
            }

        }

        public string Nev { get => nev; set => nev = value; }
        public int Szint { get => szint; set => szint = value; }
        public int Tapasztalat{ get => tapasztalat; set => tapasztalat = value; }

        public int Eletero
        {
            get => eletero;
            set
            {
                if (Eletero <= 0)
                {
                    this.tapasztalat = 0;
                    this.eletero = 0;
                }
                else if (eletero >= MaxEletero)
                    eletero = MaxEletero;
                else
                    eletero = value;

                if (tapasztalat >= SzintLepeshez)
                {
                    tapasztalat -= SzintLepeshez;
                    szint++;
                    eletero = MaxEletero;
                }
            }
        }
        public int AlapEletero { get => alapEletero; }
        public int AlapSebzes { get => alapSebzes; }

        public int Sebzes
        {
            get => AlapSebzes + Szint;
        }

        public int SzintLepeshez
        {
            get => 10 + szint * 5;
        }

        public int MaxEletero
        {
            get => AlapEletero + (szint * 3);
        }

        public void Megkuzd(Harcos masikHarcos)
        {
            if (this == masikHarcos)
                Console.WriteLine("Hiba!");
            else if (this.eletero == 0)
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
            else if (masikHarcos.eletero == 0)
                MessageBox.Show("A másik harcos halott.");
            else
            {
                masikHarcos.eletero -= this.Sebzes;
                if (masikHarcos.eletero > 0)
                {
                    this.eletero -= masikHarcos.Sebzes;
                }

                if (this.eletero > 0)
                    this.tapasztalat += 5;
                if (masikHarcos.eletero > 0)
                    masikHarcos.tapasztalat += 5;
                if (this.eletero <= 0)
                    masikHarcos.tapasztalat += 10;
                if (masikHarcos.eletero <= 0)
                    this.tapasztalat += 10;
            }
        }

        public void Gyogyul()
        {
            if (this.eletero == 0)
                Eletero = MaxEletero;
            else
                Eletero += (3 + this.szint);

        }

        public override string ToString()
        {
            return string.Format("{0} - LVL:{1} - EXP:{2}/{3} - HP:{4}/{5} - DMG:{6}",
                Nev, Szint, Tapasztalat, SzintLepeshez, Eletero, MaxEletero, Sebzes);
        }
    }
}
