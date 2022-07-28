namespace MeetingManager
{
    public class JSONLoader
    {
        // meeting data is store in a JSON

        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("meetInfo.json"))
            {
                string json = r.ReadToEnd();
                List<Meeting> meetingDetails = JsonConvert.DeserializeObject<List<Meeting>>(json);
            }
            
        }
    }
}
