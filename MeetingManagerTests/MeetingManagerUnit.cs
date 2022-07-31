using MeetingManager.Helpers;
using MeetingManager.Models;

namespace MeetingManagerTests
{
    [TestClass]
    public class MeetingManagerUnit
    {
        internal List<Meeting> mockMeetings = JSONLoader.ParseJSON(@"mock_meetings.json");
        readonly List<Meeting> tempList = new();
        readonly Commands cmds = new();

        [TestMethod]
        public void TestCreateMeeting()
        {
            Meeting m = cmds.CreateMeeting(tempList,
                "Edgaro vienkartinis",
                new string[] { "Edgaras", "Sekas" },
                "Reikia apsitarti reikalus",
                Category.TeamBuilding,
                Type.Live,
                DateTime.Now,
                DateTime.Now.AddHours(4));

            Assert.IsNotNull(m);
        }

        [TestMethod]
        public void TestDeleteMeeting()
        {
            Meeting m = cmds.CreateMeeting(tempList,
                "Edgaro vienkartinis",
                new string[] { "Edgaras", "Sekas" },
                "Reikia apsitarti reikalus",
                Category.TeamBuilding,
                Type.Live,
                DateTime.Now,
                DateTime.Now.AddHours(4));

            ResponsiblePerson rPerson = new() { FirstName = "Edgaras", LastName = "Sekas" };
            tempList.Add(m);
            cmds.DeleteMeeting(tempList, 0, rPerson);
            Assert.AreEqual(0, tempList.Count);
        }

        [TestMethod]
        public void TestAddPersonToMeeting()
        {
            Meeting m = cmds.CreateMeeting(tempList,
                "Edgaro vienkartinis",
                new string[] { "Edgaras", "Sekas" },
                "Reikia apsitarti reikalus",
                Category.TeamBuilding,
                Type.Live,
                DateTime.UtcNow.AddHours(-1),
                DateTime.UtcNow.AddHours(4));

            tempList.Add(m);

            Attendee a = new () { FirstName = "Ugnius", LastName = "Pypkius" };
            cmds.AddToMeeting(tempList, m, a);
            Assert.IsTrue(tempList[0].Attendees.Contains(a));
        }

        [TestMethod]
        public void TestRemoveResponsiblePersonFromMeeting()
        {
            Meeting m = cmds.CreateMeeting(tempList,
                "Edgaro vienkartinis",
                new string[] { "Edgaras", "Sekas" },
                "Reikia apsitarti reikalus",
                Category.TeamBuilding,
                Type.Live,
                DateTime.Now,
                DateTime.Now.AddHours(4));

            tempList.Add(m);
            ResponsiblePerson rPerson = new() { FirstName = "Edgaras", LastName = "Sekas" };
            cmds.RemoveFromMeeting(tempList,m, rPerson);
            Assert.IsTrue(tempList[0].Attendees.Contains(rPerson));
        }

        [TestMethod]
        public void TestRemoveUsualAttendeeFromMeeting()
        {
            Meeting m = cmds.CreateMeeting(tempList,
                "Edgaro vienkartinis",
                new string[] { "Edgaras", "Sekas" },
                "Reikia apsitarti reikalus",
                Category.TeamBuilding,
                Type.Live,
                DateTime.Now,
                DateTime.Now.AddHours(4));

            Attendee a = new() { FirstName = "Ugnius", LastName = "Pypkius" };
            m.Attendees.Add(a);
            tempList.Add(m);
            cmds.RemoveFromMeeting(tempList, m, a);
            Assert.IsTrue(!tempList[0].Attendees.Contains(a));
        }

        //[TestMethod]
        public void TestFilterByResponsiblePerson()
        {
            List<Meeting> fMeetings = cmds.FilterByResponsiblePerson(mockMeetings, new string[] {"Vytautas", "Maris" });
            List<Meeting> answer = new() { mockMeetings[3], mockMeetings[4] };
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }


        [TestMethod]
        public void TestFilterByCategory()
        {
            Category c = Category.Hub;
            List<Meeting> fMeetings = cmds.FilterByCategory(mockMeetings, c);
            List<Meeting> answer = new() { mockMeetings[6] };
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }

        [TestMethod]
        public void TestFilterByType()
        {
            Type t = Type.InPerson;
            List<Meeting> fMeetings = cmds.FilterByType(mockMeetings, t);
            List<Meeting> answer = new() { mockMeetings[1], mockMeetings[2], mockMeetings[4]};
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }

        [TestMethod]
        public void TestFilterByDescription()
        {
            List<Meeting> fMeetings = cmds.FilterByDescription(mockMeetings, "savaitinis");
            List<Meeting> answer = new() { mockMeetings[0], mockMeetings[2], mockMeetings[3] };
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }

        [TestMethod]
        public void TestFilterByDates()
        {
            DateTime start = DateTime.Parse("2022-07-31 11:00");
            DateTime end = start.AddHours(10);
            List<Meeting> fMeetings = cmds.FilterByDates(mockMeetings, start, end);
            List<Meeting> answer = new() { mockMeetings[2], mockMeetings[3]};
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }

        [TestMethod]
        public void TestFilterByAttendees()
        {
            int attendeeCount = 3;
            List<Meeting> fMeetings = cmds.FilterByNumberOfAttendees(mockMeetings, attendeeCount);
            List<Meeting> answer = new() { mockMeetings[2], mockMeetings[3] };
            CollectionAssert.AreEquivalent(answer, fMeetings);
        }
    }
}