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

            //Lisätään pelaaja areenalistaan
            Areena.LuoUusiPelaaja();

            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");
           
            Pelaaja.Hahmonluonti(7);

            Pelaaja.SaaTavara("Juoma");

            int taisteluita = 3;
            int luurankoMin = 1, luurankoMax = 2;
            for (int i = 0; i < taisteluita; i++)
            {
                Areena.LuoLuuranko(Areena.VihollisMääränArvonta(luurankoMin, luurankoMax));
                if (pelaaja.Kuollut==false)
                {
                    AloitaTaistelu();
                }
                luurankoMin++;
                luurankoMax++;

                if (pelaaja.Kuollut)
                    break;
            }

            if (pelaaja.Kuollut == false)
            {
                Console.WriteLine("VOITIT");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("Hävisit.");
                Console.ReadKey(true);
            }
        }

        static public void AloitaTaistelu()
        {
            Console.Clear();
            Console.WriteLine("Taistelu {0}", Pelaaja.voitetutTaistelut+1);
            Console.ReadKey(true);
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            for (int j = 0; pelaaja.Hp > 0; j++) //taistelu jatkuu, kunnes pelaajan hp loppuu
            {
                if (Areena.LuurankoLista.Count < 1) //Taistelun voitto
                {
                    Console.Clear();
                    Pelaaja.voitetutTaistelut++;
                    Console.WriteLine("Taisteluja voitettu {0}/3", Pelaaja.voitetutTaistelut);
                    Console.ReadKey(true);
                    Console.ResetColor();
                    break;
                }
                Console.Clear(); Console.ResetColor();
                Console.Write("KIERROS {0}\n\nAreenalla:", (j + 1));
                for (int k = 0; k < Areena.Areenalista.Count(); k++) //Käydään areenalista läpi
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
                }
            }
        }
    }
}
