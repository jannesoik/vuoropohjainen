using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace vuoropohjainen2
{
    class UI
    {
        static public void VahinkoVäri(int vahinko)
        {
            if (vahinko >= 10)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(vahinko);
                Console.ResetColor();
            }
            else if (vahinko > 5)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(vahinko);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(vahinko);
                Console.ResetColor();
            }
        }

        public static void HpVäri(int hp, int maxHp)
        {
            if (hp * 10 >= maxHp * 7)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(hp + "/" + maxHp);
                Console.ResetColor();
            }
            else if (hp * 10 > maxHp * 3)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(hp + "/" + maxHp);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(hp + "/" + maxHp);
                Console.ResetColor();
            }
        }

        static public Hahmo ValitseVihollinen()
        {
            //Käydään areenalista läpi ja tallennetaan viholliset omaan listaansa
            List<Hahmo> vihollislista = new List<Hahmo>();
            for (int i = 0; i < Areena.Areenalista.Count(); i++)
            {
                if (Areena.Areenalista[i].Nimi != "Pelaaja")
                    vihollislista.Add(Areena.Areenalista[i]);
            }

            ConsoleKeyInfo nappiInfo;
            do
            {
                Console.Clear();

                Console.WriteLine("Valitse vihollinen");

                for (int i = 0; i < vihollislista.Count(); i++)
                {
                    Console.Write("\n{1}) {0}, HP: ", vihollislista[i].Nimi, i + 1);
                    HpVäri(vihollislista[i].Hp, vihollislista[i].MaxHp);
                }

                nappiInfo = Console.ReadKey(true);

                if (vihollislista.Count() > 1 && nappiInfo.Key == ConsoleKey.D2)
                    break;
                if (vihollislista.Count() > 2 && nappiInfo.Key == ConsoleKey.D3)
                    break;
                if (vihollislista.Count() > 3 && nappiInfo.Key == ConsoleKey.D4)
                    break;

            } while (nappiInfo.Key != ConsoleKey.D1);
            Console.Clear();
            if (nappiInfo.Key == ConsoleKey.D3)
                return vihollislista[2];
            if (nappiInfo.Key == ConsoleKey.D2)
                return vihollislista[1];
            if (nappiInfo.Key == ConsoleKey.D4)
                return vihollislista[3];
            else
                return vihollislista[0];
        }

        static public Tavara ValitseTavara()
        {
            ConsoleKeyInfo nappiInfo;
            do
            {
                Console.Clear();

                Console.WriteLine("Valitse tavara");

                for (int i = 0; i < Pelaaja.Tavaralista.Count(); i++)
                {
                    Console.WriteLine("{0}) {1}", (i + 1), Pelaaja.Tavaralista[i].Nimi);
                }

                nappiInfo = Console.ReadKey(true);

                if (Pelaaja.Tavaralista.Count() > 1 && nappiInfo.Key == ConsoleKey.D2)
                    break;
                if (Pelaaja.Tavaralista.Count() > 2 && nappiInfo.Key == ConsoleKey.D3)
                    break;

            } while (nappiInfo.Key != ConsoleKey.D1);
            Console.Clear();
            if (nappiInfo.Key == ConsoleKey.D3)
                return Pelaaja.Tavaralista[2];
            if (nappiInfo.Key == ConsoleKey.D2)
                return Pelaaja.Tavaralista[1];
            else
                return Pelaaja.Tavaralista[0];
        }

        public static void Ohje()
        {
            Console.WriteLine("\nTAIDOT");
            Console.WriteLine("STR - Hyökkäysvahinko");
            Console.WriteLine("DEX - Vuorojärjestys, väistömahdollisuus, tavaroiden löytömahdollisuus");
            Console.WriteLine("DEF - Vahingonvastustus, kestopisteet");
            Console.WriteLine("\nKOMENNOT");
            Console.WriteLine("Hyökkää - STR-riippuvainen hyökkäys.");
            Console.WriteLine("Puolusta - DEF&DEX-riippuvainen puolustus, mahdollisuus extravuoroon. ");
            Console.WriteLine("Tavara - Käytä löytämääsi tavaraa.");
            Console.WriteLine("\nTAVARAT");
            Console.WriteLine("Pommi - Heitä pommi, osuu kaikkiin vihollisiin.");
            Console.WriteLine("Juoma - Palautta DEF:stä riippuvaisen määrän kestopisteitä.");
            Console.WriteLine("\n\n...");
            Console.ReadKey(true);
            Päävalikko();
        }

        public static void Päävalikko()
        {
            ConsoleKeyInfo nappiInfo;
            do
            {
                Console.Clear();
                Console.WriteLine("Päävalikko\nValitse komento: \n1) Aloita taistelu \n2) Ohje");
                nappiInfo = Console.ReadKey(true);
                if (nappiInfo.Key == ConsoleKey.D2)
                    break;
            } while (nappiInfo.Key != ConsoleKey.D1);

            if (nappiInfo.Key == ConsoleKey.D1 || nappiInfo.Key == ConsoleKey.NumPad1)
                Console.Clear();

            if (nappiInfo.Key == ConsoleKey.D2 || nappiInfo.Key == ConsoleKey.NumPad2)
            {
                Console.Clear();
                Ohje();
            }
        }

        public static List<Hahmo> ValitseToissijaisetViholliset(Hahmo ensisijainenVihollinen)
        {
            List<Hahmo> excludeLista = new List<Hahmo>();            
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            excludeLista.Add(ensisijainenVihollinen);
            excludeLista.Add(pelaaja);

            var tulos = Areena.Areenalista.Except(excludeLista);
           
            List<Hahmo> toissijaisetViholliset = tulos.ToList();

            return toissijaisetViholliset;
        }        
    }
}
