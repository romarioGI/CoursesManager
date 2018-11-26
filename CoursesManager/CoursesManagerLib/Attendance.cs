using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    [Serializable]
    public class Attendance
    {
        [Serializable]
        private struct Date
        {
            public DateTime Day;
            public bool IsAttended;

            public Date(DateTime day, bool isAttended)
            {
                Day = day;
                IsAttended = isAttended;
            }
        }

        private Dictionary<Client, List<Date>> _attendance;

        public Attendance()
        {
            _attendance= new Dictionary<Client, List<Date>>();
        }

        public void MarkAttendance(List<Client> allClients, List<Client> attendantClients, DateTime date)
        {
            foreach (var c in allClients)
            {
                if (_attendance.ContainsKey(c) == false)
                    _attendance.Add(c, new List<Date>());
                if (attendantClients.Contains(c))
                    _attendance[c].Add(new Date(date, true));
                else
                    _attendance[c].Add(new Date(date, false));
            }
        }

        public void RemoveClient(Client client)
        {
            _attendance.Remove(client);
        }

        public void RemoveClients(List<Client> clients)
        {
            foreach (var c in clients)
                RemoveClient(c);
        }
    }
}
