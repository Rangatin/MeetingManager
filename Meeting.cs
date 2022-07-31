using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeetingManager
{
    public class Meeting {
        // meeting data is store in a JSON
        public int Id { get; set; }

        public String? Name { get; set; }

        public ResponsiblePerson? ResponsiblePerson { get; set; }

        public String? Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public HashSet<Person> Attendees { get; set; } = new HashSet<Person>();

        public int ParticipantCount { get; set; } = 1;

        public override bool Equals(object? obj)
        {
            return obj is Meeting meeting &&
                   Id == meeting.Id &&
                   Name == meeting.Name &&
                   EqualityComparer<ResponsiblePerson>.Default.Equals(ResponsiblePerson, meeting.ResponsiblePerson) &&
                   Description == meeting.Description &&
                   Category == meeting.Category &&
                   Type == meeting.Type &&
                   StartDate == meeting.StartDate &&
                   EndDate == meeting.EndDate &&
                   EqualityComparer<HashSet<Person>>.Default.Equals(Attendees, meeting.Attendees) &&
                   ParticipantCount == meeting.ParticipantCount;
        }
    }   
}
