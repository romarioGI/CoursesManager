using System;
using System.Collections.Generic;
using System.Numerics;

namespace CoursesManagerLib
{
    [Serializable]
    public class Client
    {
        private string _name;
        private string _surname;
        private readonly List<Group> _groups;
        private readonly List<Course> _courseRequests;

        public readonly int Id;
        public static int LastId = 0;

        public int CountGroups
        {
            get { return _groups.Count; }
        }

        public int CountCourseRequests
        {
            get { return _courseRequests.Count; }
        }

        public BigInteger Account { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length == 0)
                    throw new ArgumentException();
                _name = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length == 0)
                    throw new ArgumentException();
                _surname = value;
            }
        }

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Id = LastId++;
            _groups = new List<Group>();
            _courseRequests = new List<Course>();
            Account = 0;
        }

        public void JoinGroup(Group group)
        {
            if (_groups.Contains(group) == false) _groups.Add(group);
            else throw new ArgumentException("This group is already in the list.");
        }

        public void LeaveGroup(Group group)
        {
            if(_groups.Contains(group)) _groups.Remove(group);
            else throw new ArgumentException("This group is not in the list.");
        }

        public Group GetGroup(int index)
        {
            if (index >= CountGroups)
                return null;
            return _groups[index];
        }

        public void AddCourseRequest(Course course)
        {
            if (_courseRequests.Contains(course) == false) _courseRequests.Add(course);
            else throw new ArgumentException("This course is already in the list.");
        }

        public void DeleteCourseRequest(Course course)
        {
            if (_courseRequests.Contains(course)) _courseRequests.Remove(course);
            else throw new ArgumentException("This course is not in the list.");
        }

        public Course GetCourseRequest(int index)
        {
            if (index >= CountCourseRequests)
                return null;
            return _courseRequests[index];
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

    [Serializable]
    internal class ClientNameComparator : IComparer<Client>
    {
        public int Compare(Client x, Client y)
        {
            var res = string.Compare(x.Surname, y.Surname);
            if (res != 0)
                return res;
            res = string.Compare(x.Name, y.Name);
            return res;
        }
    }
}
