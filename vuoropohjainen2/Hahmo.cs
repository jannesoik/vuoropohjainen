﻿using System;
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
        public string Tagi;
        public bool ExtraVuoro;
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
                System.Threading.Thread.Sleep(500);

                if (Nimi.Contains("Vahva Luuranko"))
                {
                    Pelaaja.SaaKokemusta(12);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 1)
                        Pelaaja.SaaTavara("Pommi");
                    if (arvonta.Next(1, 101) > 15)
                        Pelaaja.SaaTavara("Juoma");
                }
                else if (Nimi.Contains("Heikko Luuranko"))
                {
                    Pelaaja.SaaKokemusta(8);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 25)
                        Pelaaja.SaaTavara("Pommi");
                    if (arvonta.Next(1, 101) > 50)
                        Pelaaja.SaaTavara("Juoma");
                }
                else if (Nimi.Contains("Heikko Vampyyri"))
                {
                    Pelaaja.SaaKokemusta(10);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 50)
                        Pelaaja.SaaTavara("Pommi");
                    if (arvonta.Next(1, 101) > 25)
                        Pelaaja.SaaTavara("Juoma");
                }
                else if (Nimi.Contains("Vahva Vampyyri"))
                {
                    Pelaaja.SaaKokemusta(20);
                    Random arvonta = new Random();
                    if (arvonta.Next(1, 101) > 50)
                        Pelaaja.SaaTavara("Pommi");
                    if (arvonta.Next(1, 101) > 15)
                        Pelaaja.SaaTavara("Juoma");
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

        public int Hyökkää(Hahmo puolustaja, Hahmo hyökkääjä)
        {
            Random arvonta = new Random();
            int vahinkoKerroin = arvonta.Next(1, 4);
            int kriittinenArvonta = arvonta.Next(1, 101);
            int vahinko = (Str * vahinkoKerroin) - puolustaja.Def;

            if (hyökkääjä.Dex >= kriittinenArvonta)//kriittinen osuma
            {
                vahinko = (vahinko+1) * 2;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Kriittinen osuma");
                Console.ResetColor();
            }

            if (vahinko < 1)
            {
                int torjuttuVahinko = (Str * vahinkoKerroin) - 1;
                vahinko = 1;
                Console.Write("{0} hyökkäsi, {1}{2} menetti ", Nimi, puolustaja.Nimi, puolustaja.Tagi);
                UI.VahinkoVäri(vahinko);
                Console.Write(":n kestopisteen (" + torjuttuVahinko + " vastustettu)\n");
            }
            else
            {
                Console.Write("{0} hyökkäsi, {1}{2} menetti ", Nimi, puolustaja.Nimi, puolustaja.Tagi);
                UI.VahinkoVäri(vahinko);
                Console.Write(" kestopistettä (" + puolustaja.Def + " vastustettu)\n");
            }

            if (hyökkääjä.Nimi.Contains("Vampyyri"))
            {
                if (hyökkääjä.Hp<hyökkääjä.MaxHp)
                {
                    hyökkääjä.Hp += vahinko;
                    Console.Write("\n{0} sai takaisin ", hyökkääjä.Nimi);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(vahinko);
                    Console.ResetColor();
                    Console.Write(" kestopistettä.");
                    if (hyökkääjä.Hp > hyökkääjä.MaxHp)
                        hyökkääjä.Hp = hyökkääjä.MaxHp;
                }
                
            }

            return vahinko;
        }

        public bool Väistä()
        {
            Random arvonta = new Random();
            int arpaNro = arvonta.Next(1, 101);
            int väistöprosentti = Dex * 7;
            //Console.WriteLine(Nimi+"n väistömahdollisuus: " + väistöprosentti+"%");
            if (arpaNro <= 5 * Dex)
                return true;
            else
                return false;
        }

        public void Puolusta()
        {
            Random arvonta = new Random();
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            ExtraVuoronArvonta();

            int arpaNro = arvonta.Next(1, 4);
            puolustusDef = arpaNro + Def;
            Def = Def + puolustusDef;
            Dex = Dex + 1;
            Puolustautunut = true;
            Tagi = "[puolustautunut]";
        }

        public void LaskePuolustus()
        {
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            Def = Def - puolustusDef;
            Dex = Dex - 1;
           Puolustautunut = false;
            Tagi = "";
        }

        public void ExtraVuoronArvonta()
        {
            Random arvonta = new Random();

            int extraArpa = arvonta.Next(1, 101);
            if (extraArpa < Def *5)
            {
                Console.Clear();
                Console.WriteLine("Sait extravuoron! ({0} vs {1})", Def*9, extraArpa);
                System.Threading.Thread.Sleep(700);
                ExtraVuoro = true;
            }
        }
    }
}
