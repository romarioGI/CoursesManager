using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagerLib
{
    public class Group
    {
        public Course course;
        public List<Client> clients;
        public Attedance attendance;
        public int count;

        public Group(Course course) //создание новой группы
        {
            this.course = course;
            clients = new List<Client>();
            attendance = new Attedance();
            count = 0;
        }

        public void AddClient(Client client) //добавление клиента в группу
        {
            clients.Add(client);
            count++;
        }

        public void RemoveClient(Client client) //удаление клиента в группу
        {
            clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
            count--;
        }

        public bool FindClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}