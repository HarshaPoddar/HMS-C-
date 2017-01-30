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
            var guest = new Guest() { Name = name };
            ctx.Guests.Add(guest);

            //REMOVE GUESTS
            Console.WriteLine("Enter the GuesID you want to delete? : ");
            int id = Convert.ToInt16(Console.ReadLine());
            var removeGuest = from names in ctx.Guests
                         where names.GuestId == id
                         select names;
            foreach (var Name in removeGuest)
                ctx.Guests.Remove(Name);
                


            //INSERT BOOKING
            Console.WriteLine("Enter the GuestID:");
            int guestid = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter the Check In Date:");
            DateTime checkinDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter the Check Out Date:");
            DateTime checkoutDate = Convert.ToDateTime(Console.ReadLine());

            var insert = new Booking() { GuestID = guestid, CheckInDate = checkinDate, CheckOutDate = checkoutDate };
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
                for (DateTime DateCheck = DateTime.Now; DateCheck <= test; DateCheck = DateCheck.AddDays(1))
                {
                    foreach (var day in month)
                    {

                        if (day.Room.RoomId == j)
                        {
                            if (DateCheck.CompareTo(day.CheckInDate) > 0 && DateCheck.CompareTo(day.CheckOutDate) < 0)
                                DateCheck = DateCheck.AddDays(1);
                            else
                                break;
                        }
                        else
                            days[i] = DateCheck;
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


            Console.ReadKey();

            ctx.SaveChanges();
        }

    }
}