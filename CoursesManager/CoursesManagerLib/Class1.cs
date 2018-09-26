using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    public class Client
    {
        public string name;
        public string surname;
        public int ID;
        public List<Group> groups;
        public List<Course> courseRequests;

        public void JoinGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void LeaveGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void AddCourseRequest(Group group)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourseRequest(Group group)
        {
            throw new NotImplementedException();
        }
    }

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

    public class Attedance
    {
        public List<int> attendance; //тут непонятно какой тип у переменной, лист интов для начала

        //это типа класс посещаемости и метод получить посещаемость в нем
        public List<int> GetAttedance()
        {
            throw new NotImplementedException();
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
        public List<Client> clients;
        public List<Client> claims;

        public void Admission(Client client) //зачисление в группу
        {
            throw new NotImplementedException();
        }

        public void Exclusion() //удаление из группы
        {
            throw new NotImplementedException();
        }
    }
}
