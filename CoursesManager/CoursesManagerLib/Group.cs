using System;
using System.Collections;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    [Serializable]
    public class Group:IEnumerable<Client>
    {
        private static int _lastId = 0;

        public readonly int Id;
        public readonly Course Course;
        public readonly Attendance Attendance;

        private readonly List<Client> _сlients;

        public int CountClients
        {
            get { return _сlients.Count; }
        }

        public Client this[int index]
        {
            get { return _сlients[index]; }
        }

        public Group(Course course)
        {
            Id = _lastId++;
            Course = course;
            _сlients = new List<Client>();
            Attendance = new Attendance();
        }

        public bool CanAddClient()
        {
            return (_сlients.Count < 10);
        }

        public void AddClient(Client client) //добавление клиента в группу
        {
            if (_сlients.Count > 9)
                throw new ArgumentException("This group is full.");

            _сlients.Add(client);
            client.JoinGroup(this);

        }

        public int GetCount()
        {
            return _сlients.Count;
        }

        public void RemoveClient(Client client) //удаление клиента в группу
        {
            if (_сlients.Count < 6)
                throw new ArgumentException("This group is too small.");

            if (_сlients.Contains(client) == false)
                throw new ArgumentException("This client is not a member of this group.");

            _сlients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
            client.LeaveGroup(this);
        }

        public IEnumerator<Client> GetEnumerator()
        {
            foreach (var c in _сlients)
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
            s += "Students    " + _сlients.Count.ToString() + "\n";
            var i = 0;
            foreach (var cl in this)
            {
                i++;
                s +=i.ToString()+") "+ cl.ToString() + "\n";
            }
            return s;
        }

        public bool FindClient(Client client)
        {
            return _сlients.Contains(client);
        }
    }
}