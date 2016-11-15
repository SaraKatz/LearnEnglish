using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Xml;
using System.IO;

namespace English
{
    class Users
    {
        public int Counter { get; set; }
        public List<EnglishUser> UserList { get; set; }

        XDocument doc;
        XNamespace xn;

        public Users()
        {
           doc = XDocument.Load(@"XML\dataFiles\users.xml");
           xn = doc.Root.Name.Namespace;
         
           UserList = new List<EnglishUser>();
           foreach (var elem in doc.Descendants(xn.GetName("user")))
           {

               Counter++;
               UserList.Add(new EnglishUser(elem));
           }
        }

        //private async void load()
        //{
        //    var local = Windows.Storage.ApplicationData.Current.LocalFolder;
        //    //var file = await local.GetFileAsync(@"C:\Users\One\Desktop\English\English\XML\dataFiles\users.xml");
        //    using (Stream file = await local.OpenStreamForWriteAsync(@"users.xml", Windows.Storage.CreationCollisionOption.OpenIfExists) as Stream)
        //    {
        //        //var writeStream = await file.OpenStreamForWriteAsync() as Stream;
        //        file.Position = 0;

        //        doc = XDocument.Load(file);
        //    }
        //    xn = doc.Root.Name.Namespace;
        //    UserList = new List<EnglishUser>();
        //    foreach (var elem in doc.Descendants(xn.GetName("user")))
        //    {

        //        Counter++;
        //        UserList.Add(new EnglishUser(elem));
        //    }

        //}
        public void createNewEnglishUser(String name)
        {
            XElement userElement = new XElement("user");
            userElement.Add(new XElement("userName", name));
            userElement.Add(new XElement("currentLessonCode", "1"));
            Counter++;
            userElement.Add(new XAttribute("id", Counter.ToString()));

            doc.Root.Add(userElement);
            save();
            //doc.Save(@"xml\dataFiles\users.xml");
        }
        private async void save()
        {

            var local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var file = await local.GetFileAsync(@"C:\Users\One\Desktop\English\English\XML\dataFiles\users.xml");
            using (Stream file = await local.OpenStreamForWriteAsync(@"users3.xml", Windows.Storage.CreationCollisionOption.OpenIfExists) as Stream)
            {
                //var writeStream = await file.OpenStreamForWriteAsync() as Stream;


                doc.Save(file);
            }
        }

    }
}
