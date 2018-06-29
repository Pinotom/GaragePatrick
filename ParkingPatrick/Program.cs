using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPatrick
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage garage1 = new Garage(5, 5);
            Task task = Task.Run((Action)garage1.StartAutomatischParkeren);
            Console.WriteLine("P = Parkeer nieuwe wagen, V = Verwijder wagen, S = Stop");
            ConsoleKeyInfo input = Console.ReadKey(true);
            do
            {
                switch (input.KeyChar)
                {
                    case 'p':
                        Auto auto = new Auto();
                        auto.Parkeer(garage1);
                        break;
                    case 'v':
                        garage1.Bediende.VerwijderVoertuig();
                        break;
                    case 's':

                        continue;
                    default:
                        break;
                }
                Console.WriteLine("P = Parkeer wagen, V = Verwijder wagen, S = Stop");
                input = Console.ReadKey(true);
            } while (input.KeyChar != 's');

        }
    }
}
