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
        List<Image> img;
        List<TextBlock> tb;
        List<MediaElement> mdel;

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

            tb = new List<TextBlock>();
            img = new List<Image>();
            mdel = new List<MediaElement>();

            tb_letter.Text = currentLesson.selectedLetter.ToUpper();

            //מציאת האות הנבחרת ללימוד והצגת תמונותיה
            foreach (var word in lessonHadLearnt.pictuers_HadLearnt)
            {
                if (word.LettersBigShape.Equals(currentLesson.selectedLetter) || word.LettersSmallShape.Equals(currentLesson.selectedLetter))
                {
                    //הוספת המילה
                    tb.Add(new TextBlock() { Text = word.Word, Foreground = new SolidColorBrush(Colors.Blue), FontSize = 25, TextAlignment = TextAlignment.Center });

                    //הוספת השמע
                    mdel.Add(new MediaElement() { Source = new Uri(word.WordSound), Visibility = Windows.UI.Xaml.Visibility.Collapsed });

                    //הוספת התמונה
                    img.Add(new Image() { Source = word.WordPictuer, Tag = 0.0, Height = 100, Width = 100 });

                }
            }
            //הפעלה
            var storyboard = new Storyboard();
            var Animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(new TimeSpan(0, 0, 0, 5, 0)),
            };
            storyboard.Children.Add(Animation);

            Storyboard.SetTargetProperty(Animation, "Opacity");

            //1
            tb_Word1.Text = tb[0].Text;
            mdel_Word1.Source = mdel[0].Source;
            mdel[0].Play();
            //await System.Threading.Tasks.Task.Delay(4000);
            //mdel[0].Stop();
            img_Word1.Source = img[0].Source;
            Storyboard.SetTarget(storyboard, img_Word1);

            storyboard.Begin();
            await System.Threading.Tasks.Task.Delay(4000);


            //2
            tb_Word2.Text = tb[1].Text;
            mdel_Word2.Source = mdel[1].Source;
            mdel[1].Play();

            img_Word2.Source = img[1].Source;
            storyboard.Stop();
            Storyboard.SetTarget(storyboard, img_Word2);

            storyboard.Begin();
            storyboard.Stop();
            await System.Threading.Tasks.Task.Delay(4000);

            //3
            tb_Word3.Text = tb[2].Text;
            mdel_Word3.Source = mdel[2].Source;
            mdel[2].Play();
            
            img_Word3.Source = img[2].Source;
            storyboard.Stop();
            Storyboard.SetTarget(storyboard, img_Word3);


            storyboard.Begin();
            await System.Threading.Tasks.Task.Delay(4000);

            //4
            tb_Word4.Text = tb[3].Text;
            mdel_Word4.Source = mdel[3].Source;
            mdel[3].Play();
           

            img_Word4.Source = img[3].Source;
            storyboard.Stop();
            Storyboard.SetTarget(storyboard, img_Word4);


            storyboard.Begin();



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
            this.Frame.Navigate(typeof(lessonMapPage), currentLesson);
        }
        private void learnNextlLetter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonSummaryPage), currentLesson);
        }

    }
}
