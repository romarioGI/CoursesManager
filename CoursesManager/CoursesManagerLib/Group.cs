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

        public void AddClient(Client client) //добавление клиента в группу
        {
            Clients.Add(client);
        }

        public void RemoveClient(Client client) //удаление клиента в группу
        {
            Clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
        }

        public bool FindClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}