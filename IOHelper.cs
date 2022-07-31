using MeetingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager
{
    public class IOHelper
    {
        public void CreateMeetingIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("name: ");
            string? name = Console.ReadLine();
            Console.Write("description: ");
            string? desc = Console.ReadLine();
            Console.Write("responsible person first and last name: ");
            string[] fullName = Console.ReadLine().Split(" ");

            ListCategories();
            Category c = TryParseCategory(Console.ReadLine());
            ListTypes();
            Type t = TryParseType(Console.ReadLine());


            Console.Write("start date and time: ");
            DateTime start = TryParseDate(Console.ReadLine());
            Console.Write("end date and time: ");
            DateTime end = TryParseDate(Console.ReadLine());

            Meeting? m = cmds.CreateMeeting(meetings, name, fullName, desc, c, t, start, end);
            cmds.AddMeetingToList(meetings, m);
        }

        public void AddToMeeting(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("meeting id: ");
            int id = TryParseId(Console.ReadLine());
            Attendee? p = (Attendee?)GetPerson(false);

            Meeting? m = meetings.FirstOrDefault(m => m.Id == id);
            if(p != null && m != null)
            {
                cmds.AddToMeeting(meetings, m, p);
            }
        }

        public static Person? GetPerson(bool responsible)
        {
            Console.Write("\nfirst and last name: ");
            string[] fullName = Console.ReadLine().Split(" ");
            if(fullName.Length < 2)
            {
                return null; 
            }
            else if(responsible)
            {
                return new ResponsiblePerson { FirstName = fullName[0], LastName = fullName[1] };
            }
            else
            {
                return new Attendee { FirstName = fullName[0], LastName = fullName[1] };
            }   
        }

        public void DeleteMeetingIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("meeting id: ");
            int id = TryParseId(Console.ReadLine());
            ResponsiblePerson? p = (ResponsiblePerson?)GetPerson(true);
            cmds.DeleteMeeting(meetings, id, p);   
        }

        public void RemoveFromMeetingIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("meeting id: ");
            int id = TryParseId(Console.ReadLine());
            Meeting? m = meetings.FirstOrDefault(m => m.Id == id);

            if (m != null)
            {
                ShowAttendees(m);
                Person? p = GetPerson(true);
                if (p != null)
                {
                    cmds.RemoveFromMeeting(meetings, m, p);
                }
            }
        }

        private int TryParseId(String s)
        {
            if (!Int32.TryParse(s, out int id))
            {
                Console.WriteLine("Incorrect meeting ID. Try again");
            }
            return id;
        }

        public void MeetingsByResponsiblePersonIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("meeting id: ");
            int id = TryParseId(Console.ReadLine());
            Console.Write("first and last name: ");
            string[] fullName = Console.ReadLine().Split(" ");
            cmds.FilterByResponsiblePerson(meetings, fullName);
        }

        public void MeetingsByCategoryIO(Commands cmds, List<Meeting> meetings)
        {
            ListCategories();
            Category c = TryParseCategory(Console.ReadLine());
            Commands.ListMeetings(cmds.FilterByCategory(meetings, c));
        }

        private Category TryParseCategory(String s)
        {
            if (!Enum.TryParse(s, out Category c))
            {
                Console.WriteLine("Incorrect category. Try again");
            }
            return c;
        }

        private void ListCategories()
        {
            Console.Write("Available categories: " +
                            "\nCodeMonkey" +
                            "\nHub" +
                            "\nShort" +
                            "\nTeamBuilding\n" +
                            "----------------------\n" +
                            "enter category: ");
        }

        public void MeetingsByTypeIO(Commands cmds, List<Meeting> meetings)
        {
            ListTypes();
            Type t = TryParseType(Console.ReadLine());
            Commands.ListMeetings(cmds.FilterByType(meetings, t));
        }

        private void ListTypes()
        {
            Console.Write("Types: " +
                           "\nLive" +
                           "\nInPerson\n" +
                           "----------------------\n" +
                           "enter type: ");
        }

        private Type TryParseType(String s)
        {
            if (!Enum.TryParse(s, out Type t))
            {
                Console.WriteLine("Incorrect type. Try again");
            }
            return t; 
        }


        public void MeetingsByDateIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("start date (and time): ");
            DateTime start = TryParseDate(Console.ReadLine());
            Console.Write("end date (and time): ");
            DateTime end = TryParseDate(Console.ReadLine());
            Commands.ListMeetings(cmds.FilterByDates(meetings, start, end));
        }

        private DateTime TryParseDate(String s)
        {
            if (!DateTime.TryParse(s, out DateTime d))
            {
                Console.WriteLine("Incorrect date format. Try again");
            }
            return d;
        }


        public void MeetingsByAttendeeCountIO(Commands cmds, List<Meeting> meetings)
        {
            Console.Write("count: ");
            if (Int32.TryParse(Console.ReadLine(), out int count))
            {
                Commands.ListMeetings(cmds.FilterByNumberOfAttendees(meetings, count));
            }
            else
            {
                Console.WriteLine("Incorrect count value. Try again");
            }
        }

        private void ShowAttendees(Meeting m)
        {
            Console.WriteLine("Currently in this meeting: ");
            foreach (Person p in m.Attendees)
            {
                Console.WriteLine($"{p.FirstName} {p.LastName}");
            }
        }
    }
}
