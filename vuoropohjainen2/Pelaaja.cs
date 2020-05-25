using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    public class Pelaaja : Hahmo
    {
        static public int Exp;
        static public int Taso = 1;
        static public int voitetutTaistelut;
        public static List<Tavara> Tavaralista = new List<Tavara>();

        public Pelaaja(string nimi, int hp, int str, int dex, int def, int maxHp) : base(nimi, hp, str, dex, def, maxHp) {}

        static public void SaaTavara(string tavara)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Löysit {0}n.", tavara);
            if (tavara == "Pommi")
                Tavaralista.Add(new Tavara("Pommi"));
            //Console.ReadKey(true);
        }

        static public void SaaKokemusta(int exp)
        {
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Sait {0} kokemuspistettä.", exp);
            Exp = Exp + exp;
            Console.ReadKey(true);
            if (Exp >= 5 && Taso < 2)
            {
                Console.Clear();
                Console.Write("Nousit tasolle 2.");
                Taso++;
                Console.ReadKey(true);
                Console.ResetColor();
                pelaaja.MaxHp += 5;
                Taidonnosto();
            }
            else if (Exp >= 10 && Taso < 3)
            {
                Console.Clear();
                Console.Write("Nousit tasolle 3.");
                Taso++;
                Console.ReadKey(true);
                Console.ResetColor();
                pelaaja.MaxHp += 5;
                Taidonnosto();
            }
            Console.ResetColor();
        }

        static public void Taidonnosto()
        {
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            Console.Clear();
            ConsoleKeyInfo nappiInfo;
            do
            {
                Console.Clear();

                Console.WriteLine("\nValitse nostettava taito: \n1) STR {0} \n2) DEX {1} \n3) DEF {2}", pelaaja.Str, pelaaja.Dex, pelaaja.Def);
                nappiInfo = Console.ReadKey(true);
                if (nappiInfo.Key == ConsoleKey.D2)
                    break;
                if (nappiInfo.Key == ConsoleKey.D3)
                    break;
            } while (nappiInfo.Key != ConsoleKey.D1);
            if (nappiInfo.Key == ConsoleKey.D1)
            {
                Console.Clear();
                pelaaja.Str++;
                Console.WriteLine("\nVoimaa nostettu.");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   STR {0}", pelaaja.Str);
                Console.ResetColor();
                Console.WriteLine("   DEX {0}\n   DEF {1}", pelaaja.Dex, pelaaja.Def);

            }
            if (nappiInfo.Key == ConsoleKey.D2)
            {
                Console.Clear();
                pelaaja.Dex++;
                Console.WriteLine("\nNopeutta nostettu");

                Console.WriteLine("   STR {0}", pelaaja.Str);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   DEX {0}", pelaaja.Dex);
                Console.ResetColor();
                Console.WriteLine("   DEF {0}", pelaaja.Def);

            }
            if (nappiInfo.Key == ConsoleKey.D3)
            {
                Console.Clear();
                pelaaja.Def++;
                pelaaja.LisääHptä(pelaaja.Def, pelaaja.Hp, pelaaja.MaxHp);
                Console.WriteLine("\nPuolustusta nostettu");
                Console.WriteLine("   STR {0}\n   DEX {1}", pelaaja.Str, pelaaja.Dex);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("   DEF {0}", pelaaja.Def);
                Console.ResetColor();
            }
            Console.ReadKey(true);
        }

        static public void Hahmonluonti(int taitopisteet)
        {
            do
            {
                Console.Clear();
                if (taitopisteet == 1)
                    Console.WriteLine("Sinulla on {0} taitopiste.", taitopisteet);
                else
                    Console.WriteLine("Sinulla on {0} taitopistettä.", taitopisteet);
                Console.ReadKey(true);
                Taidonnosto();
                taitopisteet--;
            } while (taitopisteet > 0);
        }
    }
}
