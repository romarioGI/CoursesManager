using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagerLib
{
    // поля и свойства с большой буквы
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 

    public class Group
    {
        public Course course;
        public List<Client> clients;
        public Attedance attendance;

        public Group(Course course) //создание новой группы
        {
            this.course = course;
            clients = new List<Client>();
            attendance = new Attedance();
        }

        public void AddClient(Client client) //добавление клиента в группу
        {
            clients.Add(client);
        }

        public void RemoveClient(Client client) //удаление клиента в группу
        {
            clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
        }

        public bool FindClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}