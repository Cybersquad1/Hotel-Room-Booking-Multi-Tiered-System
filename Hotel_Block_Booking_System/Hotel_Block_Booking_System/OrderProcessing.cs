using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class OrderProcessing
    {

        public delegate void OrderConfirmed(Order o);
        public event OrderConfirmed orderConfirm;
        Order orderReceived;
        int tax = 45;
        int locationCharge = 23;
        public OrderProcessing(Order order)
        {
            orderReceived = order;
        }

        //Confirm the Order if it has the Valid Credit Card
        public void confirmOrder()
        {
            //Validity of Credit Card is done by checking the first 4 digits of a Credit Card and if it lies in the range 5000 to 7000
            string creditCardNo = orderReceived.CardNo.ToString();
            int cardNo = Int32.Parse(creditCardNo.Substring(0, 4));
            if (cardNo >= 5000 && cardNo <= 7000)
            {
                int finalAmt = orderReceived.TotalAmt + tax + locationCharge;
                Console.WriteLine("Order from Travel Agency {0} of {1} rooms has been confirmed by HotelSupplier {2}", orderReceived.TravelAgencyID, orderReceived.NoOfRooms, orderReceived.HotelSupplierID);
                
                //Emit an event once the Order is successfully Confrimed
                orderConfirm(orderReceived);
            }

        }
    }
}
