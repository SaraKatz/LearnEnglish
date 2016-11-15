using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    public sealed partial class lessonMapPage : English.Common.LayoutAwarePage
    {
        //private List<Button> lessonButtns;
        private List<Grid> lessonGrids;
        MainPage a = new MainPage();
        EnglishUser currentUser;
        Lesson l;
        string userCurrentLesonCode;
        public lessonMapPage()
        {
            this.InitializeComponent();

            ImageBrush imgB = new ImageBrush();
            imgB.ImageSource = new BitmapImage(new Uri(@"ms-appx:/Assets/lessonPic.jpg"));

            //this.BottomAppBar = a.BottomAppBar;
            
            lessonGrids = new List<Grid>();
            for (int i = 0,x=0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++, x++)
                {
                        lessonGrids.Add(new Grid() { Name = (x + 1).ToString() });
                        lessonGrids[x].Children.Add(new Image() { Height=110,Width=110, Source = new BitmapImage(new Uri(@"ms-appx:/Assets/imagesCAQG90OK.jpg")) });
                        lessonGrids[x].Children.Add(new TextBlock() { FontSize = 15, Text = (x + 1).ToString(), Foreground = new SolidColorBrush(Colors.Red), VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center, HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center });
                       
                        lessonGrids[x].Height = 130;
                        lessonGrids[x].Width = 130;
                        lessonGrids[x].Tapped += Button_Click;
                 
                    Grid.SetRow(lessonGrids[x], i);
                    Grid.SetColumn(lessonGrids[x], j);
                    buttonsGrid.Children.Add(lessonGrids[x]);
                }
            }
        }

        protected override void LoadState(Object parameter, Dictionary<String, Object> pageState)
        {
            if (parameter is EnglishUser)
            {
                currentUser = parameter as EnglishUser;
                l = new Lesson(currentUser.CurrentLessonCode.ToString());
                l.currentUser = currentUser;
            }
            if (parameter is Lesson)
            {
                l = parameter as Lesson;
                currentUser = l.currentUser;
            }

            if (pageState != null && pageState.ContainsKey("userCurrentLesonCode"))
            {
                currentUser.CurrentLessonCode = pageState["userCurrentLesonCode"] as string;
            }
            
            foreach (Grid grid in lessonGrids)
            {
                if (grid.Name.Equals(currentUser.CurrentLessonCode))
                {
                    Image i = grid.Children.First() as Image;
                    i.Source = new BitmapImage(new Uri(@"ms-appx:/Assets/lessonPic.jpg"));
                }
            }
        }


        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //pageState.Add("userCurrentLesonCode", currentUser.CurrentLessonCode);  
            pageState.Add("userCurrentLesonCode", userCurrentLesonCode);  

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //currentUser.CurrentLessonCode = (sender as Button).Name;
            XDocument doc;
            XNamespace xn;
           
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace;
            // שינוי השיעור הנוכחי
            currentUser.CurrentLessonCode = (sender as Grid).Name;
            //שמירה של השיעור הנוכחי לכל משתמש
            foreach (var elem in doc.Descendants(xn.GetName("user")).Where(c => c.Element(xn.GetName("userName")).Value.Equals(currentUser.Name)))
            {
                //elem.Element(xn.GetName("currentLessonCode")) = currentUser.CurrentLessonCode;שינוי השיעור הנוכחי
            }
         
            this.Frame.Navigate(typeof(lessonSummaryPage),currentUser);
        }
    }
}
