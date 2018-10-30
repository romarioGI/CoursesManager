using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagerLib
{
    // IComparable по имени, фамилии, ид
    // indexof на contains поменять можно
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 
    // для скорости переделать листы на хэшсеты

    public class Client
    {
        public string Name;
        public string Surname;
        public readonly int Id;
        public static int LastId = 0;
        public readonly List<Group> Groups;
        public readonly List<Course> CourseRequests;

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Id = LastId++;
            Groups = new List<Group>();
            CourseRequests = new List<Course>();
        }

        public void JoinGroup(Group group)
        {
            if (Groups.IndexOf(group) != -1) Groups.Add(group);
            else throw new ArgumentException("This group is already in the list.");
        }

        public void LeaveGroup(Group group)
        {
            if(Groups.IndexOf(group) != -1) Groups.Remove(group);
            else throw new ArgumentException("This group is not in the list.");
        }

        public void AddCourseRequest(Course course)
        {
            if (CourseRequests.IndexOf(course) != -1) CourseRequests.Add(course);
            else throw new ArgumentException("This course is already in the list.");
        }

        public void DeleteCourseRequest(Course course)
        {
            if (CourseRequests.IndexOf(course) != -1) CourseRequests.Remove(course);
            else throw new ArgumentException("This course is not in the list.");
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
