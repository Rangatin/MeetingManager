using Newtonsoft.Json;

namespace MeetingManager
{
    public class JSONLoader
    {
        // meeting data is store in a JSON

        public Meeting LoadJson(String filename)
        {
            //List<Meeting> meetingDetails;
            Meeting meetingDetails;
            using (StreamReader r = new StreamReader(filename))
            {
                string json = File.ReadAllText(filename);
                meetingDetails = JsonConvert.DeserializeObject<Meeting>(json)!;
            }
            return meetingDetails;
        }
    }
}
