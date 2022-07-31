using MeetingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager
{
    public class Commands
    {
        public Meeting CreateMeeting(
            List<Meeting> meetings, string name, string[] rFullName, string desc, Category c, Type t, DateTime start, DateTime end)
        {
            var r = new ResponsiblePerson
            {
                FirstName = rFullName[0],
                LastName = rFullName[1],
            };

            var m = new Meeting
            {
                Id = meetings.Count,
                Name = name,
                ResponsiblePerson = r,
                Description = desc,
                Category = c,
                Type = t,
                StartDate = start,
                EndDate = end,
                Attendees = { r }
            };
            
            return m;
        }

        public void AddMeetingToList(List<Meeting> meetings, Meeting m)
        {
            if (m != null)
            {
                meetings.Add(m);
                JSONLoader.ToJSON(meetings, "meetings.json");
                Console.WriteLine("Meeting created successfully");
            }
        }

        public void DeleteMeeting(List<Meeting> meetings, int id, Person person)
        {
            Meeting? m = meetings.FirstOrDefault(m => m.Id == id);
            if (m == null)
            {
                Console.WriteLine("No meeting with this ID. Try again");
                return;
            }
            else
            {
                Person? p = m.Attendees.FirstOrDefault(x => x.Equals(person) && x is ResponsiblePerson);
                if (p == null)
                {
                    Console.WriteLine("You are not the responsible person of this meeting");
                    return;
                }
                else
                {
                    meetings.Remove(m);
                    Console.WriteLine("Meeting deleted succesfully");
                }
            }
        }

        public static void ListMeetings(List<Meeting> meetings)
        {
            if(meetings == null)
            {
                Console.WriteLine("No results...");
            }

            else
            {
                meetings.ForEach(m => Console.WriteLine($"Id: {m?.Id}"
                    + $"\nName: {m?.Name}"
                    + $"\nResponsible: {m?.ResponsiblePerson.FirstName} {m?.ResponsiblePerson.LastName}"
                    + $"\nDesc: {m?.Description}"
                    + $"\nCategroy: {m?.Category}"
                    + $"\nType: {m?.Type}"
                    + $"\nStart date: {m?.StartDate}"
                    + $"\nEnd date: {m?.EndDate}\n"));
            }
        }

        public static void Help()
        {
            Console.WriteLine("Available commands:" +
                "\nnew meeting - creates a new meeting" +
                "\ndelete meeting - deletes a selected meeting" +
                "\nadd person to meeting - adds an attendee to a selected meeting" +
                "\nremove person from meeting - removes an attendee from a selected meeting" +
                "\nlist meetings - shows all meetings" +
                "\nmeetings by description - filters meetings by description" +
                "\nmeetings by responsible person - filters meetings by responsible person" +
                "\nmeetings by category - filters meetings by category" +
                "\nmeetings by type - filters meetings by type" +
                "\nmeetings by dates - filters meetings by dates" +
                "\nmeetings by attendee count - filters meetings by attendee count" +
                "\nexit - exit the program");
        }

        public void AddToMeeting(List<Meeting> meetings, Meeting m, Person p)
        {
            if (p == null)
            {
                return;
            }
            else if (m == null)
            {
               Console.WriteLine("No meeting with this ID. Try again");
               return;
            }
            else if (m.StartDate > DateTime.UtcNow)
            {
                Console.WriteLine("Meeting you are trying to add has not started");
                return;
            }
            else if (m.EndDate < DateTime.UtcNow)
            {
                Console.WriteLine("Meeting you are trying to add to is over");
                return;
            }

            Meeting? m2 = meetings.FirstOrDefault(
                x => x.Attendees.Contains(p) && x.Id != m.Id && x.StartDate < DateTime.UtcNow && x.EndDate > DateTime.UtcNow);

            if (m2 != null)
            {
                Console.WriteLine("Person you are trying to add is in another meeting now");
            }
            else
            {
                if (!m.Attendees.Add(p))
                {
                    Console.WriteLine("Person already in this meeting");
                }
                else
                {
                    m.ParticipantCount++;
                    JSONLoader.ToJSON(meetings, "meetings.json");
                    Console.WriteLine($"Person {p.FirstName} {p.LastName} added to {m.Name} on {DateTime.Now}");
                }
            }  
        }

        public void RemoveFromMeeting(List<Meeting> meetings, Meeting m, Person person)
        {
            if (m == null)
            {
                Console.WriteLine("No meeting with this ID. Try again");
                return;
            }

            Person? p = m.Attendees.FirstOrDefault(x => x.Equals(person));
            if(p == null)
            {
                Console.WriteLine("No such person in this meeting. Try again");
                return;
            }
            else if(p is ResponsiblePerson)
            {
                Console.WriteLine("Cannot remove the responsible person");
                return;
            }
            else
            {
                m.Attendees.Remove(p);
                m.ParticipantCount--;
                JSONLoader.ToJSON(meetings, "meetings.json");
                Console.WriteLine("Attendee removed succesfully");
            }
        }

        public List<Meeting> FilterByDescription(List<Meeting> meetings, string searchPhrase) {
            string[] keywords = searchPhrase.ToLower().Split(" ");
            return meetings.Where(m => ContainsAny(m?.Description, keywords)).ToList();
        }
        public List<Meeting> FilterByResponsiblePerson(List<Meeting> meetings, string[] fullName) { 
            return meetings.Where(
                m => m.ResponsiblePerson.FirstName.Equals(fullName[0]) && m.ResponsiblePerson.LastName.Equals(fullName[1])).ToList();
        }

        public List<Meeting> FilterByCategory(List<Meeting> meetings, Category category) {
            return meetings.Where(m => m.Category == category).ToList();
        }

        public List<Meeting> FilterByType(List<Meeting> meetings, Type type) { 
            return meetings.Where(m => m.Type == type).ToList();
        }

        public List<Meeting> FilterByDates(List<Meeting> meetings, DateTime startDate, DateTime endDate) {
            return meetings.Where(m => m.StartDate >= startDate && m.EndDate <= endDate).ToList();
        }

        public List<Meeting> FilterByNumberOfAttendees(List<Meeting> meetings, int count) {
            return meetings.Where(m => m.ParticipantCount >= count).ToList(); 
        }

        private bool ContainsAny(string haystack, params string[] needles)
        {
            return needles.Any(haystack.ToLower().Contains);
        }
    }
}
