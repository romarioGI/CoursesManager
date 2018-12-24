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

        public void Admission() //зачисления клиентов
        {
            foreach (var client in Clients)
            {
                if (client.CountCourseRequests != 0)
                {
                    for (var c = 0; c < client.CountCourseRequests; c++)
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

                            if (!add && group1 == null)
                            {
                                NewClaim(course, client);
                                client.DeleteCourseRequest(course);
                                c--;
                            }
                            else if (!add) 
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

        public void ShareGroup(Group group, Client client)
        {
            Groups.Remove(group);
            var gr1 = new Group(group.Course);
            var gr2 = new Group(group.Course);

            for (int i = 0; i < group.CountClients / 2; i++)
            {
                gr1.AddClient(group[i]);
                group[i].LeaveGroup(group);
            }

            for (int i = group.CountClients / 2; i < group.CountClients; i++)
            {
                gr2.AddClient(group[i]);
                group[i].LeaveGroup(group);
            }

            gr2.AddClient(client);

            Groups.Add(gr1);
            Groups.Add(gr2);
        }

        public void ViewClaims()
        {
            for (var c = 0; c < Claims.Count; c++)
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
                        }

                        Groups.Add(gr);
                    }

                    gr = new Group(claim.Сourse);
                    for (var l = k / 10 * m; l < k; l++)
                    {
                        gr.AddClient(claim.Сlients[l]);
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
                res = (School) f.Deserialize(stream);
            }

            Client.LastId = -1;
            Group.LastId = -1;

            foreach (var c in res.Clients)
                Client.LastId = Math.Max(Client.LastId, c.Id);

            foreach (var g in res.Groups)
                Group.LastId = Math.Max(Group.LastId, g.Id);

            return res;
        }
    }
}