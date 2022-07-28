using System.Text.Json;
//using Newtonsoft.Json.Serialization.JsonConvert;

namespace MeetingManager
{
    public class JSONLoader
    {
        // meeting data is store in a JSON

        public List<Meeting> LoadJson(String filename)
        {
            List<Meeting> meetingDetails;
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                meetingDetails = JsonSerializer.Deserialize<List<Meeting>>(json);
            }
            return meetingDetails;
        }
    }
}
