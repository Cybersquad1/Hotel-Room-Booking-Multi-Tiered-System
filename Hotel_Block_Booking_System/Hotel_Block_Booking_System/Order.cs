using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class Order
    {
        // The identity of the sender (i.e. TravelAgency)
        int travelAgencyID;
        // The identity of the receiver (i.e. HotelSupplier)
        int hotelSupplierID;
        // An integer that represents a credit card number
        long cardNo;
        //No of rooms to Order
        int noOfRooms;
        int totalAmt;

        public Order(int travelAgencyID, int hotelSupplierID, long creditCardNumber, int numRooms, int totalAmt)
        {
            this.cardNo = creditCardNumber;
            this.travelAgencyID = travelAgencyID;
            this.hotelSupplierID = hotelSupplierID;
            this.noOfRooms = numRooms;
            this.totalAmt = totalAmt;
        }

        public int TravelAgencyID
        {
            get
            {
                return travelAgencyID;
            }

            set
            {
                travelAgencyID = value;
            }
        }

        public int HotelSupplierID
        {
            get
            {
                return hotelSupplierID;
            }

            set
            {
                hotelSupplierID = value;
            }
        }

        public long CardNo
        {
            get
            {
                return cardNo;
            }

            set
            {
                cardNo = value;
            }
        }

        public int NoOfRooms
        {
            get
            {
                return noOfRooms;
            }

            set
            {
                noOfRooms = value;
            }
        }

        public int TotalAmt
        {
            get
            {
                return totalAmt;
            }

            set
            {
                totalAmt = value;
            }
        }



    }
}
