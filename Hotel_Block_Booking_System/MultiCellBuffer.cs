using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel_Block_Booking_System
{
    class MultiCellBuffer
    {
        //Size of the MultiBuffer is set to 3
        const int SIZE = 3;
        int head = 0, tail = 0, nElements = 0;
        string[] buffer = new string[SIZE];

        Semaphore write = new Semaphore(3, 3);
        Semaphore read = new Semaphore(2, 2);

        //Adding the Order in the Buffer
        public void addElement(string n)
        {
            write.WaitOne();
            //  Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entred Write");
            lock (this)
            {
                while (nElements == SIZE)
                {
                    Monitor.Wait(this);
                }
                buffer[tail] = n;
                tail = (tail + 1) % SIZE;
                // Console.WriteLine("Write to the buffer: {0}, {1}, {2}", n, DateTime.Now, nElements);
                nElements++;
                //   Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Leaving Write");
                write.Release();
                Monitor.Pulse(this);
            }
        }

        //Retrieving the Order from the Buffer
        public string getElement()
        {
            read.WaitOne();
            //Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entred Read");
            lock (this)
            {
                string element;
                while (nElements == 0)
                {
                    Monitor.Wait(this);
                }
                element = buffer[head];
                head = (head + 1) % SIZE;
                nElements--;
                //  Console.WriteLine("Read from the buffer: {0} , {1}, {2}", element, DateTime.Now, nElements);
                //  Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "leaving Read");
                read.Release();
                Monitor.Pulse(this);
                return element;
            }

        }
    }
}
