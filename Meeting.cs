using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MeetingManager
{
    public class Meeting {
        // meeting data is store in a JSON

        public String Name { get; set; }

        public String ResponsiblePerson { get; set; }

        public String Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type Type { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
     
    }   
}
