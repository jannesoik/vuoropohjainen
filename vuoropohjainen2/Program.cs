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
           
            Pelaaja.Hahmonluonti(9);

            Pelaaja.SaaTavara("Juoma");
            Pelaaja.SaaTavara("Pommi");


            //Taistelun asetukset
            int taisteluita = 3, luurankoMin = 1, luurankoMax = 1, vampyyriMin=1, vampyyriMax=1;
            for (int i = 0; i < taisteluita; i++)
            {
                Areena.LuoVihollisia(Areena.VihollisMääränArvonta(luurankoMin, luurankoMax), "Luuranko");
                if(i>0)
                    Areena.LuoVihollisia(Areena.VihollisMääränArvonta(vampyyriMin, vampyyriMax), "Vampyyri");


                if (pelaaja.Kuollut==false)
                {
                    AloitaTaistelu();
                }
                luurankoMin++;
                luurankoMax++;

                if (pelaaja.Kuollut)
                    break;
            }

            //Pelin loppu
            if (pelaaja.Kuollut == false)
            {
                Console.Clear();
                Console.WriteLine("VOITIT");
                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Hävisit.");
                Console.ReadKey(true);
            }
        }

        static public void AloitaTaistelu()
        {
            #region Taistelun alku
            Console.Clear();
            Console.WriteLine("Taistelu {0}", Pelaaja.voitetutTaistelut + 1);
            System.Threading.Thread.Sleep(1100);

            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja"); 
            #endregion

            for (int j = 0; pelaaja.Hp > 0; j++) //taistelu jatkuu, kunnes pelaajan hp loppuu
            {

                #region Taistelun voittoehdot
                bool voittoehdotTäytetty = Vuoro.TaistelunVoittoehdotTäytetty();
                if (voittoehdotTäytetty) //Taistelun voitto
                {
                    Console.Clear();
                    Pelaaja.voitetutTaistelut++;
                    Console.WriteLine("Taisteluja voitettu {0}/3", Pelaaja.voitetutTaistelut);
                    System.Threading.Thread.Sleep(950);
                    Console.ResetColor();
                    break;
                }
                #endregion

                #region Kierroksen alku

                //kierroksen alussa järjestetään areenalista dex:n mukaan:
                List<Hahmo> järjestettyLista = Vuoro.SuurinDex(Areena.Areenalista);

                Console.Clear(); Console.ResetColor();
                Console.Write("Kierros {0}\n\nVuorojärjestys:", (j + 1));
                for (int k = 0; k < järjestettyLista.Count(); k++) //Käydään areenalista läpi
                {
                    if (järjestettyLista[k] != null)
                    {
                        int vuoroNro = k + 1;
                        Console.Write("\n "+vuoroNro+". "  + järjestettyLista[k].Nimi + ", HP: ");
                        UI.HpVäri(järjestettyLista[k].Hp, järjestettyLista[k].MaxHp);
                        if (järjestettyLista[k].Puolustautunut)
                            Console.Write(" [puolustautuu]");
                    }
                }
                Console.WriteLine("\n\n...");
                Console.ReadKey(true);
                #endregion
                
                
                for (int i = 0; i < Areena.Areenalista.Count(); i++)
                {
                    Hahmo vuorossa = järjestettyLista[i];

                    if (vuorossa == pelaaja)
                    {
                        Vuoro.PelaajanVuoro(pelaaja);
                        
                    }
                    else
                    {
                        Vuoro.VihollisenVuoro(vuorossa);
                    }                    
                }              
            }
        }
    }
}
