using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 *NAME: ABHISHEK RAO
 *ASU ID: 1210425135
 *ENVIRONMENT USED : VISUAL STUDIO 2015 
 */
namespace Hotel_Block_Booking_System
{
    class Program
    {
        //Single  Multicell Buffer used for Processing orders
        public static MultiCellBuffer container = new MultiCellBuffer();
        static void Main(string[] args)
        {
            int NO_OF_HOTEL_SUPPLIERS = 2;
            int NO_OF_TRAVEL_AGENCIES = 5;
            ArrayList hotelSuppliers = new ArrayList();

            //HotelSupplier threads are created and started 
            Thread[] hotelSuppliersThreads = new Thread[NO_OF_HOTEL_SUPPLIERS];
            for (int i = 0; i < NO_OF_HOTEL_SUPPLIERS; i++)
            {
                HotelSupplier newSupplier = new HotelSupplier(i + 1);
                hotelSuppliers.Add(newSupplier);
                hotelSuppliersThreads[i] = new Thread(newSupplier.startHotelSupplier);
                hotelSuppliersThreads[i].Name = (i + 1).ToString();
                hotelSuppliersThreads[i].Start();
                Console.WriteLine("Hotel Supplier {0} started", i + 1);
            }

            ArrayList agencies = new ArrayList();
            Thread[] agenciesThreads = new Thread[NO_OF_TRAVEL_AGENCIES];

            //All Travel agencies are subscribed to all Hotel Suppliers for Price cut events
            for (int i = 0; i < NO_OF_TRAVEL_AGENCIES; i++)
            {
                TravelAgency newAgency = new TravelAgency(i + 1);
                agencies.Add(newAgency);
                for (int j = 0; j < NO_OF_HOTEL_SUPPLIERS; j++)
                {
                    newAgency.subscribe((HotelSupplier)hotelSuppliers[j]);
                    Console.WriteLine("Travel Agency {0} Subscribed to Hotel Supplier {1}", i + 1, j + 1);
                }
                agenciesThreads[i] = new Thread(newAgency.startTravelAgency);
                agenciesThreads[i].Start();
                Console.WriteLine("After Subscription Travel Agency {0} started", i + 1);

            }

            //Once the TravelAgency threads are created start producing pricecuts for Hotel Suppliers
            HotelSupplier.cannotProducePriceCuts = false;

            //Join each HotelSupplier thread
            for (int i = 0; i < NO_OF_HOTEL_SUPPLIERS; i++)
            {
                hotelSuppliersThreads[i].Join();
            }

            Console.WriteLine("All Hotel Suppliers are done executing");

            //Notify all Travel agencies once all the HotelSupplier threads stops.
            TravelAgency.hotelSupplierActive = false;

            //Join each TravelAgency threads once all HotelSupplier threads stops
            for (int i = 0; i < NO_OF_TRAVEL_AGENCIES; i++)
            {
                agenciesThreads[i].Join();
            }
            Console.WriteLine("Press Enter To exit");
            Console.ReadLine();

        }
    }
}
