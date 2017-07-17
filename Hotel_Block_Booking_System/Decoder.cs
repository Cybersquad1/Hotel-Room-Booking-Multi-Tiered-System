using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class Decoder
    {
        //Decoding the Delimited String to an Order Object
        public static Order convertToOrder(string o)
        {
            string[] outputOrder = o.Split('-');
            int travelAgencyId = Int32.Parse(outputOrder[0]);
            int hotelSupplierId = Int32.Parse(outputOrder[1]);
            long cardNo = Convert.ToInt64(outputOrder[2]);
            int noOfRooms = Int32.Parse(outputOrder[3]);
            int totalAmt = Int32.Parse(outputOrder[4]);

            Order order = new Order(travelAgencyId, hotelSupplierId, cardNo, noOfRooms, totalAmt);
            return order;


        }

    }
}
