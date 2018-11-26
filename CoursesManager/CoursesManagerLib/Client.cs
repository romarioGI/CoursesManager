using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    [Serializable]
    public class Client
    {
        public string Name;
        public string Surname;
        public readonly int Id;
        public static int LastId = 0;
        public readonly List<Group> Groups;
        public readonly List<Course> CourseRequests;
        public int Account { get; private set; }

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Id = LastId++;
            Groups = new List<Group>();
            CourseRequests = new List<Course>();
            Account = 0;
        }

        public List<Course> GetCourseRequests()
        {
            if (CourseRequests.Count != 0)
                return CourseRequests;
            else return null;
        }
        public void JoinGroup(Group group)
        {
            if (Groups.Contains(group) == false) Groups.Add(group);
            else throw new ArgumentException("This group is already in the list.");
        }

        public void LeaveGroup(Group group)
        {
            if(Groups.Contains(group)) Groups.Remove(group);
            else throw new ArgumentException("This group is not in the list.");
        }

        public void AddCourseRequest(Course course)
        {
            if (CourseRequests.Contains(course) == false) CourseRequests.Add(course);
            else throw new ArgumentException("This course is already in the list.");
        }

        public void DeleteCourseRequest(Course course)
        {
            if (CourseRequests.Contains(course)) CourseRequests.Remove(course);
            else throw new ArgumentException("This course is not in the list.");
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public override string ToString()
        {
            return string.Format("Name  {0}\nSurname   {1}\nID  {2}\r\n", Name, Surname, Id);
        }

        public void ChangeAccountSum(int money)
        {
            Account += money;
        }
    }
}
