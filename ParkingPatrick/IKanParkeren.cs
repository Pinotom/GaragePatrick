using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPatrick
{
    interface IKanParkeren
    {
        int[] Locatie { get; set; }
        void Parkeer(IParking parking);
    }
}
