using MeetingManager.Models;
using Newtonsoft.Json;

namespace MeetingManager.Helpers
{
    public class JSONLoader
    {
        public static List<Meeting> ParseJSON(string filename)
        {
            List<Meeting> meetingDetails;
            using (StreamReader sr = new(filename))
            {
                string json = sr.ReadToEnd();
                meetingDetails = JsonConvert.DeserializeObject<List<Meeting>>(json)!;
            }
            return meetingDetails;
        }

        public static void ToJSON(List<Meeting> meetings, string filename)
        {
            using (StreamWriter sw = new(filename))
            {
                string output = JsonConvert.SerializeObject(meetings);
                sw.Write(output);
            }
        }
    }
}
