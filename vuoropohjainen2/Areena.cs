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
        public static List<Hahmo> Areenalista = new List<Hahmo>();

        public static async void LuoLuuranko(int kpl)
        {
            int listaNro = LuurankoLista.Count();
            for (int i = 0; i < kpl; i++)
            {
                Random arvonta = new Random();
                int taso = arvonta.Next(1, 10);
                if (taso > 5)
                {
                    LuurankoLista.Add(new Hahmo("Vahva Luuranko " + i + 1, 15, 3, 6, 2, 15));
                    Areenalista.Add(LuurankoLista[i]);
                }
                else
                {
                    LuurankoLista.Add(new Hahmo("Heikko Luuranko " + i + 1, 10, 2, 4, 2, 10));
                    Areenalista.Add(LuurankoLista[i]);
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
