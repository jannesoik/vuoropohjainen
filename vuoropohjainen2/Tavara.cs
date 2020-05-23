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

        public static void HeitäPommi(Hahmo heittäjä, Hahmo vihollinen)
        {

            Random arvonta = new Random();
            int vahinkoKerroin = arvonta.Next(2, 4);
            int vahinko = (4 * vahinkoKerroin) - vihollinen.Def;
            if (vahinko < 1)
            {
                int torjuttuVahinko = (4 * vahinkoKerroin) - 1;
                vahinko = 1;
                Console.Write("{0} heitti pommin, {1} otti vain ", heittäjä.Nimi, vihollinen.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(":n vanhinkopisteen (" + torjuttuVahinko + " vastustettu)\n");
            }
            else
            {
                Console.Write("{0} heitti pommin, {1} otti ", heittäjä.Nimi, vihollinen.Nimi);
                UI.VahinkoVäri(vahinko);
                Console.Write(" pistettä vahinkoa (" + vihollinen.Def + " vastustettu)\n");
            }

            vihollinen.MenetäHPtä(vahinko);
        }
    }
}
