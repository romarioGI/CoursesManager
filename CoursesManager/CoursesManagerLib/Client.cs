using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagerLib
{
    public class Client
    {
        public string name;
        public string surname;
        public int ID;
        public static int last_ID = 0;
        public List<Group> groups;
        public List<Course> courseRequests;

        public Client(string name, string surname, List<Course> courses)
        {
            this.name = name;
            this.surname = surname;
            courseRequests = courses;
            this.ID = last_ID++;
            groups = new List<Group>();
        }

        public void JoinGroup(Group group)
        {
            if (groups.IndexOf(group) != -1) groups.Add(group);
            else throw new ArgumentException("This group is already in the list.");
        }

        public void LeaveGroup(Group group)
        {
            if(groups.IndexOf(group) != -1) groups.Remove(group);
            else throw new ArgumentException("This group is not in the list.");
        }

        public void AddCourseRequest(Course course)
        {
            if (courseRequests.IndexOf(course) != -1) courseRequests.Add(course);
            else throw new ArgumentException("This course is already in the list.");
        }

        public void DeleteCourseRequest(Course course)
        {
            if (courseRequests.IndexOf(course) != -1) courseRequests.Remove(course);
            else throw new ArgumentException("This course is not in the list.");
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
