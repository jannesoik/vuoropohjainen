using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    public class Tavara
    {
        public string Nimi;

        public Tavara(string nimi)
        {
            Nimi = nimi;
        }

        public static void HeitäPommi(Hahmo heittäjä, Hahmo vihollinen, List<Hahmo> toissijaisetViholliset)
        {
            Random arvonta = new Random();

            int vahinkoKerroin = arvonta.Next(2, 4);
            int toissijainenKerroin = arvonta.Next(3, 7);

            int vahinko = (4 * vahinkoKerroin) - vihollinen.Def;

            if (vahinko < 1)
            {
                int torjuttuVahinko = (4 * vahinkoKerroin) - 1;
                vahinko = 1;
                Console.Write("{0} heitti pommin, {1} menetti ", heittäjä.Nimi, vihollinen.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(":n kestopisteen (" + torjuttuVahinko + " vastustettu)\n");
            }
            else
            {
                Console.Write("{0} heitti pommin, {1} menetti ", heittäjä.Nimi, vihollinen.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(" kestopistettä (" + vihollinen.Def + " vastustettu)\n");                
            }

            //toissijainen vahinko
            for (int i = 0; i < toissijaisetViholliset.Count; i++)
            {
                int toissijainenVahinko = toissijainenKerroin - toissijaisetViholliset[i].Def;

                if (toissijainenVahinko<1)
                {
                    int torjuttuVahinko = toissijainenKerroin - 1;
                    toissijainenVahinko = 1;
                    toissijaisetViholliset[i].Hp -= toissijainenVahinko;
                    Console.Write("\nMyös{0} menetti ", toissijaisetViholliset[i].Nimi);
                    UI.VahinkoVäri(toissijainenVahinko);
                    Console.Write(" kestopistettä (" + torjuttuVahinko + " vastustettu)\n");
                }
                else
                {
                    toissijaisetViholliset[i].Hp -= toissijainenVahinko;
                    Console.Write("\nMyös {0} menetti ", toissijaisetViholliset[i].Nimi);
                    UI.VahinkoVäri(toissijainenVahinko);
                    Console.Write(" kestopistettä (" + toissijaisetViholliset[i].Def + " vastustettu)\n");
                }
                
            }

            vihollinen.MenetäHPtä(vahinko);
        }

        public static void JuoJuoma(Hahmo juoja)
        {
            int parannusMäärä = juoja.Def * 2 + 5;
            juoja.Hp += parannusMäärä;

            if (juoja.Hp > juoja.MaxHp)
                juoja.Hp = juoja.MaxHp;
            
            Console.Write("Juoma paransi ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(parannusMäärä); 
            Console.ResetColor();
            Console.Write(" kestopistettä");
            Console.ReadKey(true);
        }

    }
}
