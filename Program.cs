namespace MeetingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONLoader jSONLoader = new();
            List<Meeting> meetings = jSONLoader.LoadJson("meetings.json");
            //Meeting m = jSONLoader.LoadJson("meetings.json");

            //Meeting x = m[0];

            Commands cmds = new();
            //Commands.ListMeetings(meetings);

            //filter by category
            Category c = Category.CodeMonkey;
            Console.WriteLine($"Categroy: {c}");
            Commands.ListMeetings(cmds.FilterByCategory(meetings, c));


            //filter by type

            Category c = Category.CodeMonkey;
            Console.WriteLine($"Categroy: {c}");
            Commands.ListMeetings(cmds.FilterByCategory(meetings, c));

            //filter by startend date
        }
    }
}
