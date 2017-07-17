using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class HotelSupplier
    {
        public static bool cannotProducePriceCuts = true;
        int id;
        int pricecuts = 0;
        const int total_price_cuts = 3;
        static Random rng = new Random();
        int room_price;
        public delegate void PriceCut(int id, int room_price);
        public event PriceCut pricecutvent;
        ArrayList processingThreads = new ArrayList();

        public HotelSupplier(int id)
        {
            this.id = id;
            room_price = 100;
        }

        public void startHotelSupplier()
        {
            /*All Travel Agencies have not yet Subscribed to the Hotel Supplier.
            So wait till all agencies subscribe to the hotel Supplier and then
            Produce PriceCuts  */
            while (cannotProducePriceCuts)
            {
                Thread.Sleep(100);
            }
            while (pricecuts < total_price_cuts)
            {
                int price = rng.Next(5, 100);
                changePrice(price);

            }
            //Wait till all Processing threads are active
            /*foreach (Thread item in processingThreads)
            {
                while (item.IsAlive) ;
            }*/
            Console.WriteLine("Hotel Supplier {0} is done with producing price cuts", id);

        }
        public void changePrice(int price)
        {

            if (price < room_price)
            {

                room_price = price;
               // emit event to subscriberer
                triggerPriceCut();
               
               
            }
            string order = Program.container.getElement();
            Order orderToProcess = Decoder.convertToOrder(order);

            //If the Order to process is for Particular Hotel Supplier then only process the order
            if (orderToProcess.HotelSupplierID.ToString().Equals(Thread.CurrentThread.Name) && orderToProcess.HotelSupplierID.ToString() != null)
                {
                    processOrder(orderToProcess);
                }


        }
        public void triggerPriceCut()
        {
            // there is at least a subscriber   
            if (pricecutvent != null)
            {
                pricecuts++;
                //Console.WriteLine("Hotel Supplier {0} produced {1} pricecut", id, pricecuts);
                pricecutvent(id, room_price);
            }
            else
                Console.WriteLine("No PriceCut subscriberts");
        }

        public void processOrder(Order orderToProcess)
        {
            //Create a OrderProcessing object and add it to a Threadpool
            OrderProcessing order = new OrderProcessing(orderToProcess);
            Thread orderProcessingThread = new Thread(order.confirmOrder);
            processingThreads.Add(orderProcessingThread);
            orderProcessingThread.Start();

            //Subscribe the Order Confirmation with The Travel Agency
            order.orderConfirm += new OrderProcessing.OrderConfirmed(TravelAgency.gotConfirmation);
            foreach (Thread thread in processingThreads)
                thread.Join();
        }
    }
}
