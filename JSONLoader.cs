using Newtonsoft.Json;

namespace MeetingManager
{
    public class JSONLoader
    {
        // meeting data is store in a JSON

        public List<Meeting> LoadJson(String filename)
        {
            //List<Meeting> meetingDetails;
            List<Meeting> meetingDetails;
            using (StreamReader r = new StreamReader(filename))
            {
                string json = File.ReadAllText(filename);
                meetingDetails = JsonConvert.DeserializeObject<List<Meeting>>(json)!;
            }
            return meetingDetails;
        }
    }
}
