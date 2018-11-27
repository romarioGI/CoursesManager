using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    [Serializable]
    public class Group
    {
        public readonly Course Course;
        public readonly List<Client> Clients;
        public readonly Attendance Attendance;

        public Group(Course course)
        {
            Course = course;
            Clients = new List<Client>();
            Attendance = new Attendance();
        }

        public bool CanAddClient()
        {
            return (Clients.Count < 10);
        }

        public void AddClient(Client client) //добавление клиента в группу
        {
            if (Clients.Count > 9)
                throw new ArgumentException("This group is full.");

            Clients.Add(client);
            client.JoinGroup(this);

        }

        public int GetCount()
        {
            return Clients.Count;
        }

        public void RemoveClient(Client client) //удаление клиента в группу
        {
            if (Clients.Count < 6)
                throw new ArgumentException("This group is too small.");

            if (Clients.Contains(client) == false)
                throw new ArgumentException("This client is not a member of this group.");

            Clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
            client.LeaveGroup(this);
        }

        public override string ToString()
        {
            var s = "Course\n";
            s += Course.ToString()+"\n";
            s += "Students    " + Clients.Count.ToString() + "\n";
            var i = 0;
            foreach (var cl in Clients)
            {
                i++;
                s +=i.ToString()+") "+ cl.ToString() + "\n";
            }
            return s;
        }

        public bool FindClient(Client client)
        {
            return Clients.Contains(client);
        }
    }
}