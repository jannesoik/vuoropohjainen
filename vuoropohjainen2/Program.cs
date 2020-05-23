using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.Päävalikko();

            //Luodaan vihollisia
            Random arvonta = new Random();
            int luurankolukumäärä = arvonta.Next(1, 4);
            Areena.LuoLuuranko(luurankolukumäärä);

            Areena.LuoUusiPelaaja();

            //Pelaaja ja viholliset lisätään areenalistaan

            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");
           
            Pelaaja.Hahmonluonti(5);

            Pelaaja.SaaTavara("Pommi");

            AloitaTaistelu(pelaaja);
        }

        static public void AloitaTaistelu(Hahmo pelaaja)
        {
            for (int j = 0; pelaaja.Hp > 0; j++) //taistelu jatkuu, kunnes pelaajan hp loppuu
            {
                if (Areena.LuurankoLista.Count < 1)
                {
                    Console.WriteLine("VOITIT");
                    break;
                }
                Console.Clear();
                Console.Write("KIERROS {0}\n\nAreenalla:", (j + 1));
                for (int k = 0; k < Areena.Areenalista.Count(); k++)
                {
                    if (Areena.Areenalista[k] != null)
                    {
                        Console.Write("\n" + Areena.Areenalista[k].Nimi + ", HP: ");
                        UI.HpVäri(Areena.Areenalista[k].Hp, Areena.Areenalista[k].MaxHp);
                        if (Areena.Areenalista[k].Puolustautunut)
                            Console.Write(" [puolustautuu]");
                    }
                }
                Console.WriteLine("\n");

                //kierroksen alussa valitaan hahmo jolla suurin dex:
                Hahmo vuorossa = Vuoro.SuurinDex(Areena.Areenalista);

                Console.ReadKey(true);

                if (vuorossa.Nimi.Contains("Luuranko")) //Luurangot aloittavat
                {
                    if (Areena.LuurankoLista.Count > 0)
                        Vuoro.LuurankojenVuoro(Areena.LuurankoLista, pelaaja);
                    if (pelaaja.Kuollut == false)
                    {
                        Vuoro.PelaajanVuoro(pelaaja, Areena.LuurankoLista[0]);
                        Console.ReadKey(true);
                    }
                }
                else // pelaaja aloittaa
                {
                    Vuoro.PelaajanVuoro(pelaaja, Areena.LuurankoLista[0]);
                    Console.ReadKey();
                    if (Areena.LuurankoLista.Count > 0)//luurankoja vielä elossa                    
                        Vuoro.LuurankojenVuoro(Areena.LuurankoLista, pelaaja);

                    Console.WriteLine("\n***\n");
                }
            }
        }
    }
}
