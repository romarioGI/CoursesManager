using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
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
