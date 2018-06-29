using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPatrick
{
    class Verdieping
    {
        public IKanParkeren[] Parkeerplaatsen { get; set; }

        public Verdieping(int aantalPlaatsen)
        {
            Parkeerplaatsen = new IKanParkeren[aantalPlaatsen];
            
            
        }

    }
}
