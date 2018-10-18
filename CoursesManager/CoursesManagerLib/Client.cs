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
}
