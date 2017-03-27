using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Xml;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;

namespace English
{
    class Users
    {
        public int Counter { get; set; }
        public List<EnglishUser> UserList { get; set; }

        public XDocument doc;
        XNamespace xn;

        public Users()
        {
            UserList = new List<EnglishUser>();
            m1();

        }

        public async void read1()
        {
            await read();
        }

        public Users(bool read)
        {

        }

        public void m1()
        {
            var local = Windows.Storage.ApplicationData.Current.LocalFolder.Path + @"\XML\users.xml";

            try
            {
                doc = XDocument.Load(local);
            }
            catch (Exception)
            {

                doc = new XDocument();
                doc.Add(new XElement("AppUsers"));
            }

            foreach (var elem in doc.Descendants("user"))
            {
                Counter++;
                UserList.Add(new EnglishUser(elem));
            }
        }

        public async Task read()
        {
            //var local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //StorageFolder storageFolder = local;
            //StorageFile sampleFile =await  storageFolder.GetFileAsync(@"XML\dataFiles\users.xml");
            //var users = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            //StringReader sr = new StringReader(users);
            //doc = XDocument.Load(sr);
            //UserList = new List<EnglishUser>();

            //save();
        }

        public async Task createNewEnglishUser(String name)
        {
            XElement userElement = new XElement("user");
            userElement.Add(new XElement("userName", name));
            userElement.Add(new XElement("currentLessonCode", "1"));
            Counter++;
            userElement.Add(new XAttribute("id", Counter.ToString()));
            UserList.Add(new EnglishUser(userElement));
            doc.Root.Add(userElement);
            await save(doc);
        }

        //public async Task deleteEnglishUser(String name)
        //{
        //    EnglishUser eu;
        //    foreach (var elem in doc.Descendants("user"))
        //    {
        //        eu = new EnglishUser(elem);
        //        if (eu.Name.Equals(name))
        //        {
        //            doc.Root.Remove();
        //            await save(doc);
        //        }

        //    }



        //}

       


        public async Task save(XDocument doc)
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync("XML\\users.xml", CreationCollisionOption.ReplaceExisting);
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
                    {
                        using (DataWriter dataWriter = new DataWriter(outputStream))
                        {
                            //TODO: Replace "Bytes" with the type you want to write.
                            dataWriter.WriteBytes(UTF8Encoding.UTF8.GetBytes(doc.ToString()));
                            await dataWriter.StoreAsync();
                            dataWriter.DetachStream();
                        }

                        await outputStream.FlushAsync();
                    }
                }
            }
            catch
            {


            }



            //    var filePath = ApplicationData.Current.LocalFolder.Path + @"XML\dataFiles\users.xml";
            //    XDocument x;
            //    try
            //    {
            //        x = XDocument.Load(filePath);
            //    }
            //    catch
            //    {
            //        x = new XDocument();
            //        x.Add(new XElement("AppUsers"));
            //    }

            //    //הוספת הנתונים לקובץ הקיים

            //    //שמירה

            //    try
            //    {
            //        StorageFolder folder = ApplicationData.Current.LocalFolder;
            //        StorageFile file = await folder.CreateFileAsync(@"XML\dataFiles\users.xml", CreationCollisionOption.ReplaceExisting);

            //        using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            //        {
            //            using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
            //            {
            //                using (DataWriter dataWriter = new DataWriter(outputStream))
            //                {
            //                    //TODO: Replace "Bytes" with the type you want to write.
            //                    dataWriter.WriteBytes(UTF8Encoding.UTF8.GetBytes(x.ToString()));
            //                    await dataWriter.StoreAsync();
            //                    dataWriter.DetachStream();
            //                }

            //                await outputStream.FlushAsync();
            //            }
            //        }
            //    }
            //    catch
            //    {


            //    }


        }

    }
}
