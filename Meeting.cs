using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeetingManager
{
    public class Meeting {
        // meeting data is store in a JSON

        public int Id { get; set; }

        public String Name { get; set; }

        public String ResponsiblePerson { get; set; }

        public String Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public HashSet<Person> Participants { get; set; }

        public int ParticipantCount { get; set; }

    }   
}
