using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CoursesManagerLib
{
    [Serializable]
    public struct Claim
    {
        public readonly Course Сourse;
        public readonly List<Client> Сlients;

        public Claim(Course сourse)
        {
            Сourse = сourse;
            Сlients = new List<Client>();
        }

        public override string ToString()
        {
            var s = "";
            s += this.Сourse.ToString();
            foreach (var cl in this.Сlients)
                s += cl.ToString();
            return s;
        }
    }

    [Serializable]
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

        //можно немного упростить код
        public void Admission() //зачисления клиентов
        {
            foreach (var client in Clients)
            {
                if (client.CountCourseRequests != 0)
                {
                    for (var c = 0; c < client.CountCourseRequests; c++)
                    //foreach (Course course in client.GetCourseRequests())
                    {
                        var course = client.GetCourseRequest(c);
                        if (course.Format == Format.Individual)
                        {
                            var group = new Group(course);
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
                            else if (!add) //нет незаполненных подходящих групп//разделение на 2 группы
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

        //пробегание форычем можно заменить на .Contains, правда нужно проверить как .Contains  сравнивает объекты
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

        //так как группа это класс, то переменные gr и groop1 это один и тот же объект
        //нужен метод копирования?
        public void ShareGroup(Group group1, Client client)
        {
            Groups.Remove(group1);
            var gr = group1;
            var group2 = group1;
            for (var i = gr.GetCount() - 1; i >= group1.GetCount() / 2; i--)
            {
                group1.RemoveClient(gr[i]);
            }
            for (var i = 0; i < group1.GetCount() / 2; i++)
            {
                group2.RemoveClient(gr[i]);
            }
            foreach (var cl in group1)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group1);
            }
            group2.AddClient(client);
            foreach (var cl in group2)
            {
                cl.LeaveGroup(gr);
                cl.JoinGroup(group2);
            }
            Groups.Add(group1);
            Groups.Add(group2);
        }

        public void ViewClaims()
        {
            for (var c = 0; c < Claims.Count; c++)
                //foreach (var claim in Claims)
            {
                var claim = Claims[c];
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

        public static void Serialize(string filePath, School school)
        {
            var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            using (stream)
            {
                var f = new BinaryFormatter();
                f.Serialize(stream, school);
            }
        }

        public static School Deserialize(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Open);
            School res = null;
            using (stream)
            {
                var f = new BinaryFormatter();
                res = (School)f.Deserialize(stream);
            }

            return res;
        }
    }
}