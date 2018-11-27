using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    [Serializable]
    public class Attendance
    {
        private SortedList<Client, SortedList<DateTime, bool>> _attendance;

        public Attendance()
        {
            _attendance= new SortedList<Client, SortedList<DateTime, bool>>(new ClientNameComparator());
        }

        public void MarkAttendance(List<Client> allClients, List<Client> attendantClients, DateTime date)
        {
            foreach (var c in allClients)
            {
                if (_attendance.ContainsKey(c) == false)
                    _attendance.Add(c, new SortedList<DateTime, bool>());
                if (attendantClients.Contains(c))
                    _attendance[c].Add(date, true);
                else
                    _attendance[c].Add(date, false);
            }
        }

        public SortedList<Client, SortedList<DateTime, bool>> GetAttendance(List<Client> clients)
        {
            var res = new SortedList<Client, SortedList<DateTime, bool>>();
            foreach (var c in _attendance)
                if (clients.Contains(c.Key))
                    res.Add(c.Key, c.Value);

            return res;
        }
    }
}
