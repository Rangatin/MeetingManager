namespace MeetingManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Meeting> meetings = JSONLoader.ParseJSON("meetings.json");
            Commands cmds = new();
            IOHelper ioHelper = new();
            string? input;

            Console.WriteLine("To start press ENTER or type help");
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "new meeting":
                        ioHelper.CreateMeetingIO(cmds, meetings);
                        break;

                    case "delete meeting":
                        Commands.ListMeetings(meetings);
                        ioHelper.DeleteMeetingIO(cmds, meetings);
                        break;

                    case "add person to meeting":
                        Commands.ListMeetings(meetings);
                        ioHelper.AddToMeeting(cmds, meetings);
                        break;

                    case "remove person from meeting":
                        Commands.ListMeetings(meetings);
                        ioHelper.RemoveFromMeetingIO(cmds, meetings);
                        break;

                    case "list meetings":
                        Commands.ListMeetings(meetings);
                        break;

                    case "meetings by description":
                        Commands.ListMeetings(meetings);
                        Console.Write("enter phrase: ");
                        string? searchPhrase = Console.ReadLine();
                        Commands.ListMeetings(cmds.FilterByDescription(meetings, searchPhrase));
                        break;

                    case "meetings by responsible person":
                        Commands.ListMeetings(meetings);
                        ioHelper.MeetingsByResponsiblePersonIO(cmds, meetings);
                        break;

                    case "meetings by category":
                        ioHelper.MeetingsByCategoryIO(cmds, meetings);
                        break;

                    case "meetings by type":
                        ioHelper.MeetingsByTypeIO(cmds, meetings);
                        break;

                    case "meetings by dates":
                        ioHelper.MeetingsByDateIO(cmds, meetings);
                        break;

                    case "meetings by attendee count":
                        ioHelper.MeetingsByAttendeeCountIO(cmds, meetings);
                        break;

                    default:
                        Commands.Help();
                        break;
                }
                Console.Write("\ncmd: ");
            } while (!input.Equals("exit"));   
        }
    }
}
