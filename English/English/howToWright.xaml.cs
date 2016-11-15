using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class howToWright : English.Common.LayoutAwarePage
    {
        Lesson l;
        private EnglishUser currentUser;
        public howToWright()
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
        /// 
        
        protected   override void  LoadState(object  navigatingLesson, Dictionary<String, Object> pageState)
        {
            howToWrightSound.MediaEnded += howToWrightSound_MediaEnded;
            howToWrightSound.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            l = navigatingLesson as Lesson;
            pageTitle.Foreground = new SolidColorBrush(Colors.DarkBlue);
            pageTitle.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
            if (l.selectedLetter.Equals("a"))
            {
                pageTitle.FontFamily = new FontFamily("Comic Sans MS");
            }
            pageTitle.Text = l.selectedLetter + " -איך כותבים את האות";
            foreach (var item in l.lettersForLesson)
            {
                if (item.LettersBigShape.Equals(l.selectedLetter))
                {
                    howToWrightClip.Source = new Uri(item.ClipBig);
                    howToWrightSound.Source = new Uri(item.Sound_BigletterExplanation);
                }
                if (item.LettersSmallShape.Equals(l.selectedLetter))
                {
                    howToWrightClip.Source = new Uri(item.ClipSmall);
                    howToWrightSound.Source = new Uri(item.Sound_smallletterExplanation);
                }

            }


        }


        /// <summary>
        /// Preserves state asosociated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            
        }

        void howToWrightSound_MediaEnded(object sender, RoutedEventArgs e)
        {
            mini.Stop();
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{

        //    this.Frame.Navigate(typeof(Review), l);
        //}

        private void learnSmallLetter_Click(object sender, RoutedEventArgs e)
        {
            l.selectedLetter = l.selectedLetter.ToLower();
            this.Frame.Navigate(typeof(howToWright), l);
        }

        private void learnNextlLetter_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonSummaryPage), l._lessonCode);
        }

        private void volumeOff_Click(object sender, RoutedEventArgs e)
        {
            howToWrightSound.Stop();
        }

        private void ToWright_Click(object sender, RoutedEventArgs e)
        {
          
            this.Frame.Navigate(typeof(Review), l);

        }

        private void repeatClip_Click(object sender, RoutedEventArgs e)
        {
            howToWrightClip.Play();
            howToWrightSound.Play();
        }

        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage), l);
        }
    }
}
