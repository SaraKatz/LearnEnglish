using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.StartScreen;
using System.Xml;
using System.Xml.Serialization;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace English
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //string path = @"E:\המסמכים שלי\תכנות\finalProject\English\English\XML\dataFiles\lessons.xml";
        XDocument doc;
        XNamespace xn;
        Users users = new Users();
     
        public MainPage()
        {
            InitializeComponent();
            doc = XDocument.Load(@"xml\dataFiles\users.xml");
            xn = doc.Root.Name.Namespace;

            foreach (EnglishUser item in users.UserList)
            {
                cmb_users.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            cmb_users.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            tb_newUser.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }


        private async void ok_Click(object sender, RoutedEventArgs e)
        {
            if (tb_newUser.Text.Equals(""))
            {
                //הודעת שגיאה משתמש ריק
                MessageDialog messageDialog = new MessageDialog("לא הוכנס שם משתמש");
                await messageDialog.ShowAsync();
       
            }
            //יצירת משתמש חדש
            if (!tb_newUser.Text.Equals(""))
            {
                users.createNewEnglishUser(tb_newUser.Text);
                //save();
            }

            var x = users.UserList[cmb_users.SelectedIndex].CurrentLessonCode;
            this.Frame.Navigate(typeof(lessonMapPage), x);
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tb_newUser.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            cmb_users.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }


        private  void cmb_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Frame.Navigate(typeof(abcSong),cmb_users.SelectedIndex);
        }
        
        private void _newUser_Click(object sender, RoutedEventArgs e)
        {
            cmb_users.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            tb_newUser.Visibility = Windows.UI.Xaml.Visibility.Visible;

            //XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            //xmlWriterSettings.Indent = true;
            //ProfileList = ReadProfileList();
            //using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            //{
            //    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("profile.xml", FileMode.Create))
            //    {
            //        XmlSerializer serializer = new XmlSerializer(typeof(List<Profiles>));
            //        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
            //        {
            //            serializer.Serialize(xmlWriter, GenerateProfileData(profileData));
            //        }
            //    }
            //}
        }

        private void FlipView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ok.Background = new SolidColorBrush(Colors.Red);
        }
    }
}