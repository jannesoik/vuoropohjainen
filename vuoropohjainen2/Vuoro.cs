using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    class Vuoro
    {
        static public Hahmo SuurinDex(List<Hahmo> areenalista)
        {
            //järjestä lista dex:n mukaan
            int pelaajanDex = areenalista[0].Dex;
            List<Hahmo> järjestettyLista = areenalista.OrderByDescending(o => o.Dex).ToList();
            for (int i = 0; i < järjestettyLista.Count; i++)
            {
                if (järjestettyLista[i] != null)
                    Console.WriteLine((i + 1) + ". vuorossa " + järjestettyLista[i].Nimi + ", Dex: " + järjestettyLista[i].Dex);
            }
            return järjestettyLista[0];
        }

        public static void LuurankojenVuoro(List<Hahmo> luurankolista, Hahmo pelaaja)
        {
            Console.Clear();
            if (luurankolista.Count() > 1)
                Console.WriteLine("\nLuurankojen vuoro");
            else
                Console.WriteLine("\nLuurangon vuoro");

            for (int i = 0; i < luurankolista.Count(); i++) //käydään luurankolista läpi
            {
                if (pelaaja.Kuollut)
                    break;

                if (pelaaja.Väistä() == false)
                {
                    pelaaja.MenetäHPtä(luurankolista[i].Hyökkää(pelaaja));
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("{0} hyökkäsi, pelaaja väisti.", luurankolista[i].Nimi);
                    Console.ReadKey(true);
                }
            }
        }


        public static void PelaajanVuoro(Hahmo pelaaja, Hahmo vihollinen)
        {
            Console.WriteLine("extravuoro "+pelaaja.ExtraVuoro);
            Console.ReadKey(true);

            
            if(pelaaja.Puolustautunut && pelaaja.ExtraVuoro==false)
                pelaaja.LaskePuolustus();

            ConsoleKeyInfo nappiInfo;
            do
            {
                Console.Clear();
                if (pelaaja.ExtraVuoro == false)
                {
                    Console.WriteLine("\nPelaajan vuoro\nValitse komento: \n1) Hyökkää \n2) Tavara \n3) Puolusta");
                    nappiInfo = Console.ReadKey(true);
                    if (nappiInfo.Key == ConsoleKey.D2)
                        break;
                    if (nappiInfo.Key == ConsoleKey.D3)
                        break;
                }
                else
                {
                    Console.WriteLine("\nEXTRAVUORO\nValitse komento: \n1) Hyökkää \n2) Tavara");
                    nappiInfo = Console.ReadKey(true);
                    pelaaja.ExtraVuoro = false;
                    if (nappiInfo.Key == ConsoleKey.D2)
                        break;
                }

            } while (nappiInfo.Key != ConsoleKey.D1);

            //Hyökkäys
            if (nappiInfo.Key == ConsoleKey.D1 || nappiInfo.Key == ConsoleKey.NumPad1)
            {
                vihollinen = UI.ValitseVihollinen();

                if (vihollinen.Väistä() == false)
                {
                    vihollinen.MenetäHPtä(pelaaja.Hyökkää(vihollinen));
                }
                else
                    Console.WriteLine("{0} hyökkäsi, {1} väisti.", pelaaja.Nimi, vihollinen.Nimi);
            }
            //Puolustus
            if (nappiInfo.Key == ConsoleKey.D3 || nappiInfo.Key == ConsoleKey.NumPad3)
            {
                pelaaja.Puolusta();
                Console.WriteLine("Pelaaja puolustautuu");
                Console.ReadKey(true);
            }
            //Tavara
            if (nappiInfo.Key == ConsoleKey.D2 || nappiInfo.Key == ConsoleKey.NumPad2)
            {
                if (Pelaaja.Tavaralista.Count() > 0)
                {
                    Tavara valittuTavara = UI.ValitseTavara();
                    if (valittuTavara.Nimi == "Juoma")
                    {
                        Tavara.JuoJuoma(pelaaja);
                    }
                    else if (valittuTavara.Nimi == "Pommi")
                    {
                        vihollinen = UI.ValitseVihollinen();
                        Tavara.HeitäPommi(pelaaja, vihollinen, UI.ValitseToissijaisetViholliset(vihollinen));
                        Pelaaja.Tavaralista.Remove(valittuTavara);
                    }
                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ei tavaroita.");
                    Console.ReadKey(true);
                    PelaajanVuoro(pelaaja, vihollinen);
                }
            }

            if (pelaaja.ExtraVuoro == true) //uusi vuoro            
                PelaajanVuoro(pelaaja, vihollinen);            

        }

    }
}
