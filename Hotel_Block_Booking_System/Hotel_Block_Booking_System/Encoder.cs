using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class Encoder
    {
        //Encoding the Order object to a String with "-" separated Delimiter
        public static string convertToString(Order o)
        {
            string output = o.TravelAgencyID.ToString() + "-" + o.HotelSupplierID.ToString() + "-" + o.CardNo.ToString() + "-" + o.NoOfRooms.ToString() + "-" + o.TotalAmt.ToString();
            return output;
        }
    }
}
