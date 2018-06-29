using System;
using System.Collections.Generic;
using System.IO;

namespace ParkingPatrick
{
    class Garagebediende : Parkeerbediende
    {
        private bool inDienst = false;
        public Garage Garage { get; set; }

        public Garagebediende(Garage garage)
        {
            Garage = garage;
        }

        public void BeginTeWerken()
        {
            inDienst = true;
            while (inDienst)
            {
                if (Garage.Wachtrij.Count != 0)
                {
                    GaVoertuigParkeren();
                }
            }
        }

        public void StopMetWerken()
        {
            inDienst = false;
        }

        public override void GaVoertuigParkeren()
        {
            if (Garage.Wachtrij.Count == 0)
            {
                return;
            }
            IKanParkeren teParkerenVoertuig = Garage.Wachtrij.Dequeue();
            for (int i = 0; i < Garage.AantalVerdiepingen; i++)
            {
                int aantalPlaatsen = Garage.Verdiepingen[i].Parkeerplaatsen.Length;
                for (int j = 0; j < aantalPlaatsen; j++)
                {
                    if (Garage.Verdiepingen[i].Parkeerplaatsen[j] == null)
                    {
                        ParkeerVoertuig(teParkerenVoertuig, i, j);
                        return;
                    }
                }
            }
            Garage.NieuwVerdiep();
            ParkeerVoertuig(teParkerenVoertuig, (Garage.AantalVerdiepingen - 1), 0);
        }

        private void ParkeerVoertuig(IKanParkeren teParkerenVoertuig, int verdiep, int plaats)
        {
            Garage.Verdiepingen[verdiep].Parkeerplaatsen[plaats] = teParkerenVoertuig;
            using (TextWriter writer = File.AppendText("Verdieping" + verdiep + ".txt"))
            {
                writer.WriteLine(teParkerenVoertuig.ToString());
            }
            System.Threading.Thread.Sleep(1000);
            teParkerenVoertuig.Locatie = new int[] { verdiep, plaats };
            Console.WriteLine($"Voertuig {teParkerenVoertuig.ToString()} is geparkeerd op plaats {verdiep} - {plaats + 1}.");
        }

        public override void VerwijderVoertuig()
        {
            Random r = new Random();
            bool verdiepLeeg = true;
            while (verdiepLeeg)
            {
                int verdiep = r.Next(0, Garage.AantalVerdiepingen);
                List<IKanParkeren> gevondenVoertuigen = new List<IKanParkeren>();
                int aantalPlaatsen = Garage.Verdiepingen[verdiep].Parkeerplaatsen.Length;
                for (int i = 0; i < aantalPlaatsen; i++)
                {
                    if (Garage.Verdiepingen[verdiep].Parkeerplaatsen[i] != null)
                    {
                        verdiepLeeg = false;
                        gevondenVoertuigen.Add(Garage.Verdiepingen[verdiep].Parkeerplaatsen[i]);
                    }
                }
                if (!verdiepLeeg)
                {
                    int keuzeUitLijst = r.Next(0, gevondenVoertuigen.Count);
                    int locatieRandomVoertuig = gevondenVoertuigen[keuzeUitLijst].Locatie[1];
                    Garage.Verdiepingen[verdiep].Parkeerplaatsen[locatieRandomVoertuig] = null;
                    gevondenVoertuigen.Remove(gevondenVoertuigen[keuzeUitLijst]);
                    using (TextWriter writer = File.CreateText("Verdieping" + verdiep + ".txt"))
                    {
                        foreach (IKanParkeren resterendVoertuig in gevondenVoertuigen)
                        {
                            writer.WriteLine(resterendVoertuig.ToString());
                        }
                    }
                    Console.WriteLine($"Voertuig op plaats {verdiep} - {locatieRandomVoertuig + 1} verwijderd.");
                }
            }
        }
    }
}
