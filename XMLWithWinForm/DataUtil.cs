using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace XMLWithWinForm
{
    class DataUtil
    {
        XmlDocument doc;
        XmlElement root;
        string fileName;

        public DataUtil()
        {
            fileName = "Course.xml";
            doc = new XmlDocument();
            if (!File.Exists(fileName))
            {
                XmlElement course = doc.CreateElement("course");
                doc.AppendChild(course);
                doc.Save(fileName);
            }
            doc.Load(fileName); // nap file xml vao bien doc
            root = doc.DocumentElement; //lay ra phan tu goc
        }
        
        // phuong thuc them Student vao xml 
        public void AddStudent(Student s)
        {
            XmlElement st = doc.CreateElement("student");
            st.SetAttribute("id", s.id);
            XmlElement name = doc.CreateElement("name");
            name.InnerText = s.name;
            XmlElement age = doc.CreateElement("age");
            age.InnerText = s.age;
            XmlElement city = doc.CreateElement("city");
            city.InnerText = s.city;

            st.AppendChild(name);
            st.AppendChild(age);
            st.AppendChild(city);
            root.AppendChild(st);
            doc.Save(fileName);// cuoi cung la ghi vao file

        }
        public List<Student> GetAllStudents()
        {
            XmlNodeList nodes = root.SelectNodes("student"); // lay ra danh sach cac nut

            List<Student> list = new List<Student>();

            // dung vong lap de chuyen danh sach node thanh danh sach cac doi tuong
            foreach( XmlNode item in nodes)
            {
                Student s = new Student();
                s.id = item.Attributes[0].InnerText;
                s.name = item.SelectSingleNode("name").InnerText;
                s.age = item.SelectSingleNode("age").InnerText;
                s.city = item.SelectSingleNode("city").InnerText;
                list.Add(s);
            }
            return list;
        }
        public bool UpdateStudent(Student s)
        {
            // tim student can sua theo thuoc tinh id
            XmlNode stFind = root.SelectSingleNode("student[@id='" + s.id + "']");
            if(stFind != null)
            {
                XmlElement st = doc.CreateElement("student");
                st.SetAttribute("id", s.id);
                XmlElement name = doc.CreateElement("name");
                name.InnerText = s.name;
                XmlElement age = doc.CreateElement("age");
                age.InnerText = s.age;
                XmlElement city = doc.CreateElement("city");
                city.InnerText = s.city;

                st.AppendChild(name);
                st.AppendChild(age);
                st.AppendChild(city);
                root.ReplaceChild(st, stFind);
                doc.Save(fileName);
                return true;
            }
            return false;
        }
        public bool DeleteStudent(string id)
        {
            XmlNode stFind = root.SelectSingleNode("student[@id='" + id + "']");
            if(stFind != null)
            {
                root.RemoveChild(stFind);
                doc.Save(fileName);
                return true;
            }
            return false;
        }
        public Student FindByID(string id)
        {
            XmlNode stFind = root.SelectSingleNode("student[@id='" + id + "']");
            Student s = null;
            if(stFind != null)
            {
                 s = new Student();
                s.id = stFind.Attributes[0].InnerText;
                s.name = stFind.SelectSingleNode("name").InnerText;
                s.age = stFind.SelectSingleNode("age").InnerText;
                s.city = stFind.SelectSingleNode("city").InnerText;
            }
            return s;
        }
    }
    
}
