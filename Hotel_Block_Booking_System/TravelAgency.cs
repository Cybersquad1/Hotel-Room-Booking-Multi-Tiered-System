using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{

    class TravelAgency
    {
        int travelAgencyid;
        int hotelSupplierId;
        int discountPrice;
        Boolean triggerred = false;
        public static Boolean hotelSupplierActive = true;

        //List of Credit Cards available for all Travel agencies
        static long[] creditCards =
        {
            6234567890123456,
            5678901234567890,
            6890123456958741,
            9990085764789212

        };

        static Random rng = new Random();

        public TravelAgency(int id)
        {
            this.travelAgencyid = id;
        }

        //Subscribe the Pricecut event and link it to the callback function IssueOrder
        public void subscribe(HotelSupplier h)
        {

            h.pricecutvent += issueOrder;
        }

        //Event Triggered whenever there is a PriceCut
        public void issueOrder(int supplierId, int room_price)
        {

            this.hotelSupplierId = supplierId;
            this.discountPrice = room_price;
            triggerred = true;

        }

        //Start the travel Agency thread and wait till the callback function is triggered
        public void startTravelAgency()
        {
            while (hotelSupplierActive)
            {
                if (triggerred)
                {
                    string encodedObject = processOrder();
                    //Put the String in Multicell buffer after getting the Encoded string from the Encoder
                    Program.container.addElement(encodedObject);
                    //triggerred = false;
                    Thread.Sleep(1000);
                }

            }
            Console.WriteLine("Travel Agency {0} done executing", travelAgencyid);
        }

        public string processOrder()
        {
            //Generate random no of rooms to order
            int noOfRooms = rng.Next(1, 10);
            int totalAmt = noOfRooms * discountPrice;

            //Select random credit card from the available List
            int creditCardNo = rng.Next(0, 3);
            Order obj = new Order(travelAgencyid, hotelSupplierId, creditCards[creditCardNo], noOfRooms, totalAmt);

            Console.WriteLine("New order Placed :\r\n Travel Agency ID = {0} \r\n Hotel Supplier ID = {1} \r\n credit card no = {2} \r\n Total Rooms = {3} \r\n Total Amt = {4} \r\n", obj.TravelAgencyID, obj.HotelSupplierID, obj.CardNo, obj.NoOfRooms, obj.TotalAmt);
            string encodedObject = Encoder.convertToString(obj);
            return encodedObject;
        }

        //Confirmation of Order receieved from The Hotel Supplier
        internal static void gotConfirmation(Order o)
        {
            Console.WriteLine("Travel Agency got Confirmation from Hotel Supplier {0} : order details : \r\n Total Rooms = {1} \r\n Total Amt = {2} \r\n", o.HotelSupplierID, o.NoOfRooms, o.TotalAmt);
        }
    }
}
