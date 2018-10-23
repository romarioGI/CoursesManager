using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    // поля и свойства с большой буквы
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 
    // для скорости переделать листы на хэшсеты либо на сортетсеты

    // возможно стоит создать отдельный класс, в котором будет логика зачисления 

    public class School
    {
        public List<Group> groups;
        public HashSet<Client> clients;
        public List<Client> claims;

        public School()
        {
            clients = new HashSet<Client>();
        }
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
