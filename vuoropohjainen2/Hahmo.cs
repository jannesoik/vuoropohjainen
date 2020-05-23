using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    public class Hahmo
    {
        public string Nimi { get; set; }
        public int Hp;
        public int MaxHp;
        public int Str;
        public int Dex;
        public int Def;
        public bool Kuollut;
        public bool Puolustautunut;
        int puolustusDef;

        public Hahmo(string nimi, int hp, int str, int dex, int def, int maxHp)
        {
            this.Nimi = nimi;
            this.Hp = hp;
            this.Str = str;
            this.Dex = dex;
            this.Def = def;
            this.MaxHp = maxHp;
        }

        public void MenetäHPtä(int vahinko)
        {
            Hp = Hp - vahinko;

            if (Hp <= 0) //<-kuolema
            {
                if (Nimi.Contains("elaaja"))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.WriteLine("\n" + Nimi + " kuoli.");
                Console.ResetColor();
                Kuollut = true;

                if (Nimi.Contains("Vahva Luuranko"))
                {
                    Pelaaja.SaaKokemusta(8);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 25)
                        Pelaaja.SaaTavara("Pommi");
                }
                else if (Nimi.Contains("Heikko Luuranko"))
                {
                    Pelaaja.SaaKokemusta(5);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 25)
                        Pelaaja.SaaTavara("Pommi");
                }

                Areena.PoistaKuolleet();
            }
        }

        public void LisääHptä(int def, int hp, int maxHp)
        {
            int maxHplisäys = def * 2;
            int hplisäys = def * 2;
            MaxHp += maxHplisäys;
            Hp += hplisäys;
        }

        public int Hyökkää(Hahmo puolustaja)
        {
            Random arvonta = new Random();
            int vahinkoKerroin = arvonta.Next(1, 3);
            int vahinko = (Str * vahinkoKerroin) - puolustaja.Def;
            if (vahinko < 1)
            {
                int torjuttuVahinko = (Str * vahinkoKerroin) - 1;
                vahinko = 1;
                Console.Write("{0} hyökkäsi, {1} menetti ", Nimi, puolustaja.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(":n kestopisteen (" + torjuttuVahinko + " vastustettu)\n");
            }
            else
            {
                Console.Write("{0} hyökkäsi, {1} menetti ", Nimi, puolustaja.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(" kestopistettä (" + puolustaja.Def + " vastustettu)\n");
            }

            return vahinko;
        }

        public bool Väistä()
        {
            Random arvonta = new Random();
            int arpaNro = arvonta.Next(1, 100);
            int väistöprosentti = Dex * 5;
            //Console.WriteLine(Nimi+"n väistömahdollisuus: " + väistöprosentti+"%");
            if (arpaNro <= 5 * Dex)
                return true;
            else
                return false;
        }

        public void Puolusta()
        {
            Random arvonta = new Random();
            int arpaNro = arvonta.Next(1, 3);
            puolustusDef = arpaNro + Def;
            Def = Def + puolustusDef;
            Dex = Dex + 1;
            Puolustautunut = true;
        }
        public void LaskePuolustus()
        {
            Def = Def - puolustusDef;
            Dex = Dex - 1;
            Puolustautunut = false;
        }    


    }
}
