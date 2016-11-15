using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class lessonSummaryPage : English.Common.LayoutAwarePage
    {
        Lesson l;
        private List<Grid> letterGrids;
        private List<TextBlock> tbList;
        private EnglishUser currentUser;
        int x;
        int i;

        public lessonSummaryPage()
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
            letterGrids = new List<Grid>();
            tbList=new List<TextBlock>();
            FontFamily fontFamily = new FontFamily("Copperplate Gothic Bold");
            
            
            foreach (var letter in l.lettersForLesson)
            {
                if (letter.LettersSmallShape.Equals("a"))
                {
                    fontFamily= new FontFamily("Comic Sans MS");
                }
                
                letterGrids.Add(new Grid() { Name = letter.LettersBigShape, Background = new SolidColorBrush(Colors.LightBlue), Height = 400, Width = 180 });
                letterGrids[x].Children.Add(new TextBlock()
                {
                    Margin=new Thickness(0,0,0,20),
                    FontFamily=fontFamily,
                    Name = letter.LettersBigShape,
                    Height = 180,
                    Width = 180,
                    FontSize = 110,
                    TextAlignment = TextAlignment.Center,
                    Text =  letter.LettersBigShape + "" + letter.LettersSmallShape,
                    Foreground = new SolidColorBrush(Colors.Black)
                });
                
                tbList.Add(new TextBlock() { FontSize = 15, TextAlignment = Windows.UI.Xaml.TextAlignment.Center, Foreground = new SolidColorBrush(Colors.Fuchsia), Text ="\r"+letter.LettersBigShape + " לאות " + "\rיש את הצליל של האות העברית \r" + letter.HebrewLetter + " " });
                letterGrids[x].Tapped += lessonSummaryPage_Tapped;
                Grid.SetRow(letterGrids[x], 0);
                Grid.SetColumn(letterGrids[x], i);
                buttonGrid.Children.Add(letterGrids[x]);

                Grid.SetRow(tbList[x], 1);
                Grid.SetColumn(tbList[x], i);
                buttonGrid.Children.Add(tbList[x]);
                i++;
                x++;

            }
        }

        void lessonSummaryPage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            l.selectedLetter = (sender as Grid).Name;
            this.Frame.Navigate(typeof(howToWright),l);
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
        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage),currentUser);
        }

    }
}
