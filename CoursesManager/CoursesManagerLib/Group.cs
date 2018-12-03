using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoursesManagerLib
{
    [Serializable]
    public class Group:IEnumerable<Client>
    {
        private static int _lastId = 0;
        private static ClientNameComparator _clientCmp = new ClientNameComparator();
        private readonly Attendance _attendance;

        public readonly int Id;
        public readonly Course Course;

        private readonly List<Client> _clients;

        public int CountClients
        {
            get { return _clients.Count; }
        }

        public Client this[int index]
        {
            get { return _clients[index]; }
        }

        public Group(Course course)
        {
            Id = _lastId++;
            Course = course;
            _clients = new List<Client>();
            _attendance = new Attendance();
        }

        public bool CanAddClient()
        {
            return (_clients.Count < 10);
        }

        public void AddClient(Client client) //добавление клиента в группу
        {
            if (_clients.Count > 9)
                throw new ArgumentException("This group is full.");

            _clients.Add(client);
            client.JoinGroup(this);
            _clients.Sort(_clientCmp);
        }

        public int GetCount()
        {
            return _clients.Count;
        }

        public void RemoveClient(Client client) //удаление клиента из группы
        {
            if (_clients.Count < 6)
                throw new ArgumentException("This group is too small.");

            if (_clients.Contains(client) == false)
                throw new ArgumentException("This client is not a member of this group.");

            _clients.Remove(client); 
            client.LeaveGroup(this);
        }

        public IEnumerator<Client> GetEnumerator()
        {
            foreach (var c in _clients)
                yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            var s = "Course\n";
            s += Course.ToString()+"\n";
            s += "Students    " + _clients.Count.ToString() + "\n";
            var i = 0;
            foreach (var cl in this)
            {
                i++;
                s +=i.ToString()+") "+ cl.ToString() + "\n";
            }
            return s;
        }

        public SortedList<Client, SortedList<DateTime, bool?>> GetAttendance()
        {
            var res = new SortedList<Client, SortedList<DateTime, bool?>>(_clientCmp);
            var att = _attendance.GetAttendance(_clients);

            var dates = new SortedSet<DateTime>();
            foreach(var dateList in att.Values)
                foreach(var d in dateList.Keys)
                    dates.Add(d);

            foreach (var client in att.Keys)
            {
                res.Add(client,new SortedList<DateTime, bool?>());
                foreach (var date in dates)
                {
                    if (att[client].ContainsKey(date))
                        res[client][date] = att[client][date];
                    else
                        res[client][date] = null;
                }
            }

            return res;
        }

        public void SetAttendance(List<Client> attendedClients, DateTime date)
        {
            _attendance.MarkAttendance(_clients, attendedClients, date);

            foreach (var c in attendedClients)
                c.ChangeAccountSum(-1 * Course.Price);
        }

        public bool FindClient(Client client)
        {
            return _clients.Contains(client);
        }
    }
}