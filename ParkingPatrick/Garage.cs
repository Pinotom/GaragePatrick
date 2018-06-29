using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParkingPatrick
{
    class Garage : IParking
    {
        public List<Verdieping> Verdiepingen { get; set; }

        public Queue<IKanParkeren> Wachtrij { get; set; }

        public Garagebediende Bediende { get; set; }

        public int AantalVerdiepingen { get; set; }

        public int PlaatsenPerVerdiep { get; set; }

        public Garage(int aantalVerdiepingen, int plaatsenPerVerdiep)
        {
            AantalVerdiepingen = aantalVerdiepingen;
            PlaatsenPerVerdiep = plaatsenPerVerdiep;
            Verdiepingen = new List<Verdieping>();
            Wachtrij = new Queue<IKanParkeren>();
            Bediende = new Garagebediende(this);
            MaakVerdiepingenAan();    
        }

        private void MaakVerdiepingenAan()
        {
            for (int i = 0; i < AantalVerdiepingen; i++)
            {
                Verdiepingen.Add(new Verdieping(PlaatsenPerVerdiep));
                FileStream fileCreator = File.Create("Verdieping" + i + ".txt");
                fileCreator.Close();
            }
        }

        public void StartAutomatischParkeren()
        {
            Bediende.BeginTeWerken();
        }

        public void ParkeerVoertuig(IKanParkeren voertuig)
        {
            Wachtrij.Enqueue(voertuig);
        }

        public void NieuwVerdiep()
        {
            AantalVerdiepingen++;
            Verdiepingen.Add(new Verdieping(PlaatsenPerVerdiep));
            Console.WriteLine("Nieuw verdiep bijbouwen...");
            Random r = new Random();
            for (int i = 0; i < 100; i += r.Next(5,15))
            {
                Console.WriteLine(i + "%");
                System.Threading.Thread.Sleep(1000);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.WriteLine("100%");
            Console.WriteLine("Verdiep succesvol bijgebouwd.");
            FileStream fileCreator = File.Create("Verdieping" + (AantalVerdiepingen - 1) + ".txt");
            fileCreator.Close();
        }



    }
}
