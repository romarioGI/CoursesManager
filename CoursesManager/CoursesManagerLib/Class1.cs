using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CoursesManagerLib
{
    public class Client
    {
        public string name;
        public string surname;
        public int ID;
        public List<Group> groups;
        public List<Course> courses;
    }
    public class Group
    {
        public Group (Course course) //создание новой группы
        {
            this.course = course;
            clients = new List<Client>();
            attendance = new Attedance();
            count = 0;
        }
        public void AddClient(Client client)//добавление клиента в группу
        {
            clients.Add(client);
            count++;
        }
        public void RemoveClient(Client client)//удаление клиента в группу
        {
            clients.Remove(client); //сработает ли? может надо дописать сравнение для данного типа?
            count--;
        }
        public Course course;
        public List<Client> clients;
        public Attedance attendance;
        public int count;

    }
    public class Attedance
    {
        public List<int> attendance; //тут непонятно какой тип у переменной, лист интов для начала
                                     //это типа класс посещаемости и метод получить посещаемость в нем
        public List<int> GetAttedance()
        {
            return new List<int>();
        }
    }
    public class Course
    {
        public string language;
        public int intensity;
        public string level;
        public string format;
        public int price;
    }
    public class School
    {
        public List<Group> groups;
        public List <Client> clients;
        public List<Client> claims;
        public void Admission(Client client)//зачисление в группу
        {
            
        }
        public void Exclusion()//удаление из группы
        {

        }
    }
}
