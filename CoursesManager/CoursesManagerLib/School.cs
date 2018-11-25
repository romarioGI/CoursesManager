using System.Collections.Generic;

namespace CoursesManagerLib
{
    // что можно переделать на свойства и где необходимо закрыть чтение/измение 
    // для скорости переделать листы на хэшсеты либо на сортетсеты

    // возможно стоит создать отдельный класс, в котором будет логика зачисления 
    public struct Claim
    {
        public Course Сourse;
        public List<Client> Сlients;
        public Claim(Course сourse)
        {
            Сourse = сourse;
            Сlients = new List<Client>();
        }
        public override string ToString()
        {
            string s = "";
            s += this.Сourse.ToString();
            foreach (Client cl in this.Сlients)
                s += cl.ToString();
            return s;
        }
    }
    public class School
    {
        public readonly List<Group> Groups;
        public readonly HashSet<Client> Clients;
        public readonly List<Claim> Claims;

        public School()
        {
            Groups = new List<Group>();
            Clients = new HashSet<Client>();
            Claims = new List<Claim>();
        }
        public void Admission() //зачисления клиентов
        {
            foreach (Client client in Clients)
            {
                if (client.GetCourseRequests() != null)
                {
                    for(int c=0;c<client.CourseRequests.Count;c++)
                    //foreach (Course course in client.GetCourseRequests())
                    {
                            Course course = client.CourseRequests[c];
                        if (course.Format == Format.Individual)
                        {
                            Group group = new Group(course);
                            group.AddClient(client);
                            client.DeleteCourseRequest(course);
                            Groups.Add(group);
                            c--;
                        }
                        else
                        {
                            Group group1 = null;
                            var add = false;
                            foreach (var group in Groups)
                            {
                                if (group.Course.Equals(course))
                                {
                                    if (group.CanAddClient())
                                    {
                                        group.AddClient(client);
                                        client.DeleteCourseRequest(course);
                                        c--;
                                        add = true;
                                        break;
                                    }
                                    else if (group1 == null) group1 = group;
                                }
                            }
                            if (!add && group1 == null) //если нет подходящих групп
                            {
                                NewClaim(course, client);
                                client.DeleteCourseRequest(course);
                                c--;
                            }
                            else if (!add)//нет незаполненных подходящих групп//разделение на 2 группы
                            {
                                ShareGroup(group1, client);
                                client.DeleteCourseRequest(course);
                                c--;
                            }
                        }
                    }
                }
            }
            ViewClaims();
        }
        public void NewClaim(Course course, Client client)
        {
            var add = false;
            if (Claims.Count != 0)
                foreach (var claim in Claims)
                {
                    if (claim.Сourse.Equals(course))
                    {
                        claim.Сlients.Add(client);
                        add = true;
                        break;
                    }
                }
            if (!add)
            {
                var claim = new Claim(course);
                claim.Сlients.Add(client);
                Claims.Add(claim);
            }
        }
        public void ShareGroup(Group group1, Client client)
        {
            Groups.Remove(group1);
            var gr = group1;
            var group2 = group1;
            for (var i = gr.GetCount() - 1; i >= group1.GetCount() / 2; i--)
            {
                group1.RemoveClient(gr.Clients[i]);
            }
            for (var i = 0; i < group1.GetCount() / 2; i++)
            {
                group2.RemoveClient(gr.Clients[i]);
            }
            foreach (var cl in group1.Clients)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group1);
            }
            group2.AddClient(client);
            foreach (var cl in group2.Clients)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group2);
            }
            Groups.Add(group1);
            Groups.Add(group2);
        }
        public void ViewClaims()
        {
            for (int c=0;c<Claims.Count;c++)
            //foreach (var claim in Claims)
            {
                Claim claim = Claims[c];
                var k = claim.Сlients.Count;
                var m = (k + k / 10 - 1) / (k / 10 + 1);
                if (k > 4)
                {
                    Group gr;
                    for (var i = 0; i < k / 10; i++)
                    {
                        gr = new Group(claim.Сourse);
                        for (var j = i * m; j < (i + 1) * m; j++)
                        {
                            gr.AddClient(claim.Сlients[j]);
                            //claim.Сlients[j].JoinGroup(gr);
                        }
                        Groups.Add(gr);
                    }
                    gr = new Group(claim.Сourse);
                    for (var l = k / 10 * m; l < k; l++)
                    {
                        gr.AddClient(claim.Сlients[l]);
                        //claim.Сlients[l].JoinGroup(gr);
                    }
                    Groups.Add(gr);
                    Claims.Remove(claim);
                    c--;
                }

            }
        }
    }
}