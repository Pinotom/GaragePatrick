using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPatrick
{
    class Auto : IKanParkeren
    {
        public string Nummerplaat { get; set; }

        public int[] Locatie { get; set; }

        public Auto()
        {
            Nummerplaat = GenereerRandomNummerplaat();
        }

        public Auto(string nummerplaat)
        {
            Nummerplaat = nummerplaat;
        }

        public string GenereerRandomNummerplaat()
        {
            string nummerplaat = "1-";
            Random r = new Random();
            for (int i = 0; i < 3; i++)
            {
                nummerplaat += Convert.ToChar((byte)r.Next(65, 91));
            }
            nummerplaat += "-";
            int cijfers = (r.Next(1, 10) * 100) + (r.Next(1, 10) * 10) + r.Next(1, 10);
            nummerplaat += cijfers;
            return nummerplaat;
        }

        public void Parkeer(IParking garage)
        {
            garage.ParkeerVoertuig(this);
        }

        public override string ToString()
        {
            return Nummerplaat;
        }
    }
}
