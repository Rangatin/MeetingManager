namespace MeetingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONLoader jSONLoader = new JSONLoader();
            List<Meeting> m = jSONLoader.LoadJson("meetings.json");

            Console.WriteLine(m[0].getName());
        }

        public Meeting createMeeting() {
             return null;
        }
    }
}
