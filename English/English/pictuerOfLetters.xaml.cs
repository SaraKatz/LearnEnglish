using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media.Animation;
using Windows.Devices.Input;
using Windows.UI.Input;
using Windows.UI.Input.Inking;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class pictuerOfLetters : English.Common.LayoutAwarePage
    {
        Lesson currentLesson;
        HadLearnt lessonHadLearnt;
        Image img;
        TextBlock tb;
        MediaElement mdel;

        public pictuerOfLetters()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(object navigationParameter, Dictionary<String, Object> pageState)
        {            
            currentLesson = navigationParameter as Lesson;
            int i = 0;
            lessonHadLearnt = new HadLearnt(currentLesson);
            //מציאת האות הנבחרת ללימוד והצגת תמונותיה

            foreach (var word in lessonHadLearnt.pictuers_HadLearnt)
            {
                if (word.LettersBigShape.Equals(currentLesson.selectedLetter) || word.LettersSmallShape.Equals(currentLesson.selectedLetter))
                {
                    //הוספת המילה
                    tb = new TextBlock() { Text = "\r" + word.Word, Foreground = new SolidColorBrush(Colors.Blue), FontSize = 25, TextAlignment = TextAlignment.Center };
                    Grid.SetColumn(tb, 0);
                    Grid.SetRow(tb, i);
                    Picture_grid.Children.Add(tb);

                    var storyboard = new Storyboard();
                    //var Animation = new DoubleAnimation
                    //{
                    //    From = 0,
                    //    To = 200,
                    //    Duration = new Duration(new TimeSpan(0, 0, 0, 7, 0)),
                    //};
                    //storyboard.Children.Add(Animation);


                    //Storyboard.SetTargetProperty(Animation, "Tag");
                    var Animation = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(new TimeSpan(0, 0, 0, 5, 0)),
                    };
                    storyboard.Children.Add(Animation);

                    Storyboard.SetTargetProperty(Animation, "Opacity");

                    //הוספת התמונה
                    img = new Image() { Source = word.WordPictuer,Tag=0.0,Height=100,Width=100};
                    //img.Margin=new Thickness(Double.Parse(img.Tag.ToString()),0,0,0);
                    Grid.SetColumn(img, 2);
                    Grid.SetRow(img, i);

                    Storyboard.SetTarget(storyboard, img);
                    Picture_grid.Children.Add(img);
                    
                    storyboard.Begin();
                    
                    //הוספת השמע
                    mdel = new MediaElement() { Source = new Uri(word.WordSound),Visibility=Windows.UI.Xaml.Visibility.Collapsed };
                    mdel.Play();
                    Grid.SetColumn(mdel, 0);
                    Grid.SetRow(mdel, i++);
                    Picture_grid.Children.Add(mdel);
                    //הפעלת השמע ל-5 שניות
                    await System.Threading.Tasks.Task.Delay(4000);
                    mdel.Pause();
                   
                }
            }
        }
        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(workPage), currentLesson);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(game_discoverThePicture), currentLesson);
        }
  
        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage),currentLesson);
        }

    }
}
