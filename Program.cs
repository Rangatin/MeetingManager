namespace MeetingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONLoader jSONLoader = new JSONLoader();
            //List<Meeting> m = jSONLoader.LoadJson("meetings.json");
            Meeting m = jSONLoader.LoadJson("meetings.json");

            //Meeting x = m[0];
            Console.WriteLine($"Name: {m?.Name}");
            Console.WriteLine($"Type: {m?.Type}");
        }

        public Meeting createMeeting() {
             return null;
        }
    }
}
