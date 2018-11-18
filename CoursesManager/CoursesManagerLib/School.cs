using System;
using System.Collections.Generic;

namespace CoursesManagerLib
{
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 
    // для скорости переделать листы на хэшсеты либо на сортетсеты

    // возможно стоит создать отдельный класс, в котором будет логика зачисления 
    public struct Claim
    {
        public Course course;
        public List<Client> clients;
        public Claim(Course course)
        {
            this.course = course;
            clients = new List<Client>();
        }
    }
    public class School
    {
        public readonly List<Group> Groups;
        public readonly HashSet<Client> Clients;
        public readonly List<Claim> Claims;

        public School()
        {
            Clients = new HashSet<Client>();
        }
        public void Admission() //зачисления клиентов
        {
            foreach (Client client in Clients)
            {
                if (client.GetCourseRequests() != null)
                {
                    foreach (Course course in client.GetCourseRequests())
                    {
                        Group group1 = null;
                        bool add = false;
                        foreach (Group group in Groups)
                        {
                            if (group.Course.Equals(course))
                            {
                                if (group.CanAddClient())
                                {
                                    group.AddClient(client);
                                    client.DeleteCourseRequest(course);
                                    client.JoinGroup(group);
                                    add = true;
                                    break;
                                }
                                else if (group1 != null) group1 = group;
                            }
                        }
                        if (!add && group1 != null) //если нет подходящих групп
                        {
                            NewClaim(course, client);
                        }
                        else if (!add)//нет незаполненных подходящих групп//разделение на 2 группы
                        {
                            ShareGroup(group1, client);
                        }
                    }
                }
            }
            ViewClaims();
        }
        public void NewClaim(Course course, Client client)
        {
            bool add = false;
            if (Claims.Count != 0)
                foreach (Claim claim in Claims)
                {
                    if (claim.course.Equals(course))
                    {
                        claim.clients.Add(client);
                        add = true;
                        break;
                    }
                }
            if (!add)
            {
                Claim claim = new Claim(course);
                claim.clients.Add(client);
                Claims.Add(claim);
            }
        }
        public void ShareGroup(Group group1, Client client)
        {
            Groups.Remove(group1);
            Group gr = group1;
            Group group2 = group1;
            for (int i = gr.GetCount() - 1; i >= group1.GetCount() / 2; i--)
            {
                group1.RemoveClient(gr.Clients[i]);
            }
            for (int i = 0; i < group1.GetCount() / 2; i++)
            {
                group2.RemoveClient(gr.Clients[i]);
            }
            foreach (Client cl in group1.Clients)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group1);
            }
            group2.AddClient(client);
            foreach (Client cl in group2.Clients)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group2);
            }
            Groups.Add(group1);
            Groups.Add(group2);
        }
        public void ViewClaims()
        {
            foreach (Claim claim in Claims)
            {
                int k = claim.clients.Count;
                int m = (k + k / 10 - 1) / (k / 10 + 1);
                if (k > 4)
                {
                    Group gr = new Group(claim.course);
                    for (int i = 0; i < k / 10; i++)
                    {
                        gr = new Group(claim.course);
                        for (int j = i * m; j < (i + 1) * m; j++)
                        {
                            gr.AddClient(claim.clients[j]);
                            claim.clients[j].JoinGroup(gr);
                        }
                        Groups.Add(gr);
                    }
                    gr = new Group(claim.course);
                    for (int l = k / 10 * m; l < k; l++)
                    {
                        gr.AddClient(claim.clients[l]);
                        claim.clients[l].JoinGroup(gr);
                    }
                    Groups.Add(gr);
                }
                Claims.Remove(claim);

            }
        }
    }
}
