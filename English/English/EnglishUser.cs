using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace English
{
  public class EnglishUser
    {
        public String Name { get; set; }
        public String Id { get; set; }
        public String CurrentLessonCode { get; set; }

        XDocument doc;
        XNamespace xn;


        public EnglishUser() { }
      
        
        public EnglishUser(XElement elem)
        {
            doc = XDocument.Load(@"xml\dataFiles\users.xml");
            xn = doc.Root.Name.Namespace;

            Name=elem.Element(xn.GetName("userName")).Value;
            Id=elem.Attribute("id").Value;
            CurrentLessonCode = elem.Element(xn.GetName("currentLessonCode")).Value;
        }
     
    }
}
