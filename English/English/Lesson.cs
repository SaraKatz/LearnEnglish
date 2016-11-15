using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using English.Common;



namespace English
{
    public class Lesson
    {
        XDocument doc;
        XNamespace xn;
        public List<letter> lettersForLesson { get; set; }
        public string selectedLetter;
        public string _lessonCode;
        public EnglishUser currentUser;

        public Lesson(String lessonCode)
        {
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace;

            lettersForLesson = new List<letter>();
            foreach (var elem in doc.Descendants(xn.GetName("letter")).Where(c => c.Parent.Attribute("lessonCode").Value.Equals(lessonCode)))
            {
                lettersForLesson.Add(new letter(elem));
            }

            _lessonCode = lessonCode;
        }
    }



}
