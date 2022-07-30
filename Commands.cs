using MeetingManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager
{
    public class Commands
    {
        public static void ListMeetings(List<Meeting> meetings)
        {
            foreach (Meeting m in meetings)
            {
                Console.WriteLine($"Id: {m?.Id}"
                    + $"\nName: {m?.Name}"
                    + $"\nResponsible: {m?.ResponsiblePerson}"
                    + $"\nDesc: {m?.Description}"
                    + $"\nCategroy: {m?.Category}"
                    + $"\nType: {m?.Type}"
                    + $"\nStart date: {m?.StartDate}"
                    + $"\nEnd date: {m?.EndDate}\n");
            }
        }


        public void AddToMeeting(Meeting m, Person p)
        {
            if (p == null) throw new ArgumentNullException();

            Console.WriteLine($"Person: {p.Name} added on {DateTime.UtcNow}");

            if (!m.Participants.Add(p)) throw new InvalidOperationException();
        }

        public void RemoveFromMeeting(Meeting m, Person p)
        {
            /*
            if (p == null || responsiblePerson)
            {
                throw new InvalidOperationException();
            }
            else
            {
                m.Participants.Remove(p);
            }*/
        }

        public void DeleteMeeting(List<Meeting> meetings, Meeting m)
        {
            /*
            if (m == null || !responsiblePerson)
            {
                throw new InvalidOperationException();
            }
            else
            {
                meetings.Remove(m);
            }*/

        }

        public List<Meeting> FilterByDescription(List<Meeting> meetings, String searchPhrase) {
            string[] keywords = searchPhrase.Split(" ");

            //meetings.Where(m => m.Description.Contains<String>(keywords)).ToList();

            //meetings.AsQueryable().SkipWhile(m => keywords.Contains(m.Name)).Take(keywords.Length);

            return null;
        }
        public List<Meeting> FilterByResponsiblePerson(List<Meeting> meetings, String respPerson) { 
            return meetings.Where(m => m.ResponsiblePerson.Equals(respPerson)).ToList();
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
            //return meetings.Where(m => m.ParticipantCount > count).ToList(); 
            return null;
        }
    }
}
