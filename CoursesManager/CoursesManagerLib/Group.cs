using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    // поля и свойства с большой буквы
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 

    public class Group
    {
        public readonly Course Course;
        public readonly List<Client> Clients;
        public readonly Attedance Attendance;

        public Group(Course course) //создание новой группы
        {
            Course = course;
            Clients = new List<Client>();
            Attendance = new Attedance();
        }

        public bool CanAddClient()
        {
            return (Clients.Count < 10);
        }
        public void AddClient(Client client) //добавление клиента в группу
        {
            if (Clients.Count > 9) throw new ArgumentException("This group is full.");
            else
            {
                Clients.Add(client);
                client.JoinGroup(this);
            }
        }

        public int GetCount()
        {
            return Clients.Count;
        }
        public void RemoveClient(Client client) //удаление клиента в группу
        {
            if (Clients.Count < 6) throw new ArgumentException("This group is too small.");
            else
            {
                Clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
                client.LeaveGroup(this);
            }
        }

        public bool FindClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}