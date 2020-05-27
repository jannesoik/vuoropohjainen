using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    class Vuoro
    {
        static public List<Hahmo> SuurinDex(List<Hahmo> areenalista)
        {
            //järjestä lista dex:n mukaan
            int pelaajanDex = areenalista[0].Dex;
            List<Hahmo> järjestettyLista = areenalista.OrderByDescending(o => o.Dex).ToList();            
            return järjestettyLista;
        }

        public static void VihollisenVuoro(Hahmo vihollinen)
        {            
            Hahmo pelaaja = Areena.Areenalista.Find(item => item.Nimi == "Pelaaja");

            try
            {
                if (pelaaja.Kuollut == false)
                {
                    Console.Clear();

                    if (Areena.Areenalista.Contains(vihollinen))
                    {
                        //Console.WriteLine("Vuorossa {0}", vihollinen.Nimi);

                        if (pelaaja.Väistä() == false)
                        {
                            pelaaja.MenetäHPtä(vihollinen.Hyökkää(pelaaja, vihollinen));
                            Console.WriteLine("\n\n...");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Console.WriteLine("{0} hyökkäsi, pelaaja väisti.", vihollinen.Nimi);
                            Console.WriteLine("\n\n...");
                            Console.ReadKey(true);
                        }
                    }
                }
            }
            catch (Exception)
            {

                
            }
                                    
        }

        public static void PelaajanVuoro(Hahmo pelaaja)
        {
            if (pelaaja.Kuollut==false)
            {
                Hahmo vihollinen;

                if (pelaaja.Puolustautunut && pelaaja.ExtraVuoro == false)
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
                        vihollinen.MenetäHPtä(pelaaja.Hyökkää(vihollinen, pelaaja));
                        Console.WriteLine("\n\n...");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.WriteLine("{0} hyökkäsi, {1} väisti.", pelaaja.Nimi, vihollinen.Nimi);
                        Console.WriteLine("\n\n...");
                        Console.ReadKey(true);
                    }
                }
                //Puolustus
                if (nappiInfo.Key == ConsoleKey.D3 || nappiInfo.Key == ConsoleKey.NumPad3)
                {
                    pelaaja.Puolusta();
                    Console.Clear();
                    Console.WriteLine("Pelaaja puolustautuu");
                    System.Threading.Thread.Sleep(600);
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
                            Pelaaja.Tavaralista.Remove(valittuTavara);
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
                        System.Threading.Thread.Sleep(600);
                        PelaajanVuoro(pelaaja);
                    }
                }

                if (pelaaja.ExtraVuoro == true) //uusi vuoro            
                    PelaajanVuoro(pelaaja);
            }          

        }

        public static bool TaistelunVoittoehdotTäytetty()
        {
            //Käydään areenalista läpi ja tallennetaan viholliset omaan listaansa
            List<Hahmo> vihollislista = new List<Hahmo>();
            for (int i = 0; i < Areena.Areenalista.Count(); i++)
            {
                if (Areena.Areenalista[i].Nimi != "Pelaaja")
                    vihollislista.Add(Areena.Areenalista[i]);
            }

            if (vihollislista.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       
    }
}
