using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 
    // для скорости переделать листы на хэшсеты либо на сортетсеты

    // возможно стоит создать отдельный класс, в котором будет логика зачисления 

    public class School
    {
        public readonly List<Group> Groups;
        public readonly HashSet<Client> Clients;
        public readonly List<Client> Claims;

        public School()
        {
            Clients = new HashSet<Client>();
        }
        public void Admission() //зачисления клиентов
        {
            throw new NotImplementedException();
        }
    }
}
