using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuoropohjainen2
{
    class Areena
    {
        public static List<Hahmo> LuurankoLista = new List<Hahmo>();
        public static List<Hahmo> Vampyyrilista = new List<Hahmo>();
        public static List<Hahmo> Areenalista = new List<Hahmo>();

        public static async void LuoVihollisia(int kpl, string vihollistyyppi)
        {
            for (int i = 0; i < kpl; i++)
            {
                int listaNro = i + 1;

                //Arvotaan heikko/vahva ja lisätään areenalistalle
                Random arvonta = new Random();
                arvonta.Next(1, 10);
                if (arvonta.Next(1, 10) > 5)
                {
                    if(vihollistyyppi=="Luuranko")
                        Areenalista.Add(new Hahmo("Vahva Luuranko " + listaNro, 15, 4, 4, 4, 15));
                    if(vihollistyyppi=="Vampyyri")
                        Areenalista.Add(new Hahmo("Vahva Vampyyri " + listaNro, 40, 3, 2, 6, 40));
                }
                else
                {
                    if (vihollistyyppi == "Luuranko")
                        Areenalista.Add(new Hahmo("Heikko Luuranko " + listaNro, 10, 2, 4, 2, 10));
                    if (vihollistyyppi == "Vampyyri")
                        Areenalista.Add(new Hahmo("Heikko Vampyyri " + listaNro, 25, 3, 2, 3, 25));
                }
                await Task.Delay(100); //odotetaan jotta saadaan uusi randomnro
            }
        }  

        public static void LuoUusiPelaaja()
        {
            Areenalista.Add(new Pelaaja("Pelaaja", 5, 1, 1, 1, 5));
            Hahmo pelaaja = Areenalista.Find(item => item.Nimi == "Pelaaja");
            pelaaja.LisääHptä(pelaaja.Def, pelaaja.Hp, pelaaja.MaxHp);
        }

        public static void PoistaKuolleet()
        {
            for (int i = 0; i < LuurankoLista.Count; i++)
            {
                if (LuurankoLista[i].Hp <= 0)
                    LuurankoLista.Remove(LuurankoLista[i]);
            }
            for (int i = 0; i < Areenalista.Count; i++)
            {
                if (Areenalista[i].Hp <= 0)
                    Areenalista.Remove(Areenalista[i]);
            }
        }

        public static int VihollisMääränArvonta(int minMäärä, int maxMäärä)
        {
            Random arvonta = new Random();
            int vihollisMäärä = arvonta.Next(minMäärä, minMäärä + 1);
            return vihollisMäärä;

        }
    }
}
