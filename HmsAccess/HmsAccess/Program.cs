using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new HotelManagementServerEntities();
          
            //ADD GUESTS
            Console.WriteLine("Enter the Name of the Guest you want to add : ");
            string name = Console.ReadLine();
            var g1 = new Guest() { Name = name };
            ctx.Guests.Add(g1);

            //REMOVE GUESTS
            Console.WriteLine("Enter the GuesID you want to delete? : ");
            int x = Convert.ToInt16(Console.ReadLine());
            var remove = from id in ctx.Guests
                         where id.GuestId == x
                         select id;
            foreach (var value in remove)
                ctx.Guests.Remove(value);
                


            //INSERT BOOKING
            Console.WriteLine("Enter the GuestID:");
            int gid = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the Check In Date:");
            DateTime cin = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter the Check Out Date:");
            DateTime cout = Convert.ToDateTime(Console.ReadLine());

            var insert = new Booking() { GuestID = gid, CheckInDate = cin, CheckOutDate = cout };
            ctx.Bookings.Add(insert);
            

            //Availability of Rooms for a month
            

            var month = from rooms in ctx.Bookings
                        where rooms.StatusID != 1
                        select rooms;

            var check = from r in ctx.Rooms
                        select r;


            DateTime test = DateTime.Now.AddMonths(1);


            int i = 0;
            int j = 0;
            foreach (var value in check)
            {

                DateTime []  days = new DateTime[31];
                j++;
                i = 0;
                for (DateTime y = DateTime.Now; y <= test; y = y.AddDays(1))
                {
                    foreach (var val in month)
                    {

                        if (val.Room.RoomId == j)
                        {
                            if (y.CompareTo(val.CheckInDate) > 0 && y.CompareTo(val.CheckOutDate) < 0)
                                y = y.AddDays(1);
                            else
                                break;
                        }
                        else
                            days[i] = y;
                    }
                    i++;
                }

                Console.WriteLine("The days of availability for Room {0} for a month is : ", j);
                for (int index = 0; index<31;index++)
                {

                    Console.Write(days[index]); 
                    
                }
                Console.WriteLine("");
                
            }

            ctx.SaveChanges();

            Console.ReadKey();
        }
    }
}