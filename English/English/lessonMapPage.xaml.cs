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
        Users users = new Users();
        private List<Grid> lessonGrids;
        EnglishUser currentUser;
        Lesson l;
        private List<string> letterGrids = new List<string>();
        private List<Lesson> lesseons = new List<Lesson>();
        ToolTip t;
        int i = 0;
        int j;
        string tb = "";
        string userCurrentLesonCode;
        public lessonMapPage()
        {
            ImageBrush imgB = new ImageBrush();
            imgB.ImageSource = new BitmapImage(new Uri(@"ms-appx:/Assets/lessonPic.jpg"));
            //this.BottomAppBar = a.BottomAppBar;imagesCAQG90OK.jpg
            //t.Content = letterGrids[0]; 
            this.InitializeComponent();

            for (int i = 1; i < 22; i++)
            {
                lesseons.Add(new Lesson(i.ToString()));
            }
            foreach (var lesson in lesseons)
            {
                j++;
                for (int i = 0; i < lesson.lettersForLesson.Count(); i++)
                {
                    letterGrids.Add(lesson.lettersForLesson[i].LettersBigShape);

                    if (lesson.lettersForLesson.Count() > 1)
                    {
                        letterGrids.Remove(lesson.lettersForLesson[i].LettersBigShape);
                        //letterGrids.Add(lesson.lettersForLesson[i].LettersBigShape); 
                        do
                        {
                            tb = tb + lesson.lettersForLesson[i].LettersBigShape + " ,";
                            //i++;
                        } while (i++ < (lesson.lettersForLesson.Count() - 1));
                        tb=tb.Substring(0,tb.Length-1);
                        letterGrids.Add(tb);
                        tb = "";
                    }
                }
            }
            lessonGrids = new List<Grid>();
            for (int i = 0, x = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++, x++)
                {
                    lessonGrids.Add(new Grid() { Name = (x + 1).ToString() });
                    if (i == 0)
                    {
                        lessonGrids[x].Children.Add(new Image() { Height = 100, Width = 110, Source = new BitmapImage(new Uri(@"ms-appx:/Assets/backgrounds/לחצן שיעור ורוד.png")) });
                    }
                    if (i == 1)
                    {
                        lessonGrids[x].Children.Add(new Image() { Height = 100, Width = 110, Source = new BitmapImage(new Uri(@"ms-appx:/Assets/backgrounds/לחצן שיעור כתום.png")) });
                    } if (i == 2)
                    {
                        lessonGrids[x].Children.Add(new Image() { Height = 100, Width = 110, Source = new BitmapImage(new Uri(@"ms-appx:/Assets/backgrounds/לחצן שיעור ירוק.png")) });
                    }
                    lessonGrids[x].Children.Add(new TextBlock() { FontFamily = new FontFamily("Comic Sans MS"), FontSize = 30, Text = (x + 1).ToString(), Foreground = new SolidColorBrush(Colors.White), VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Center ,Margin= new Thickness (0,10,0,0)} );
                    t = new ToolTip();
                    t.Content = letterGrids[x];
                    ToolTipService.SetToolTip(lessonGrids[x], t);
                    lessonGrids[x].Height = 100;
                    lessonGrids[x].Width = 130;
                    lessonGrids[x].Tapped += Button_Click;

                    Grid.SetRow(lessonGrids[x], i);
                    Grid.SetColumn(lessonGrids[x], j);
                    buttonsGrid.Children.Add(lessonGrids[x]);
                }
            }
        }

        private object Margin(int p1, int p2, int p3, int p4)
        {
            throw new NotImplementedException();
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
                    i.Source = new BitmapImage(new Uri(@"ms-appx:/Assets/backgrounds/לחצן שיעור לבן.png"));
                }
            }

        }


        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            //pageState.Add("userCurrentLesonCode", currentUser.CurrentLessonCode);  
            pageState.Add("userCurrentLesonCode", userCurrentLesonCode);

        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            //currentUser.CurrentLessonCode = (sender as Button).Name;
            Users users = new Users();
            //Users users = new Users(false);
            //await users.read();
            //users.read();


            // שינוי השיעור הנוכחי
            currentUser.CurrentLessonCode = (sender as Grid).Name;
            //שמירה של השיעור הנוכחי לכל משתמש
            foreach (var elem in users.doc.Descendants("user").Where(c => c.Element("userName").Value.Equals(currentUser.Name)))
            {
                elem.Element("currentLessonCode").Value = currentUser.CurrentLessonCode;//שינוי השיעור הנוכחי
            }
            users.save(users.doc);
            this.Frame.Navigate(typeof(lessonSummaryPage), currentUser);
        }


    }
}
