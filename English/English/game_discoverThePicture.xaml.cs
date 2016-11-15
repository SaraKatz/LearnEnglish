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
using Windows.UI;
using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    /// 
    public sealed partial class game_discoverThePicture : English.Common.LayoutAwarePage
    {
        Random r = new Random();
        HadLearnt lessonHadLearnt;
        string lettersBigShape;
        string word;
        private Lesson currentLesson;
        List<TextBlock> tb = new List<TextBlock>();
        List<TextBlock> tbWord = new List<TextBlock>();
        List<ImageSource> images = new List<ImageSource>();
        int a;
        int count = 18;
        public game_discoverThePicture()
        {
            this.InitializeComponent();

        }
        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage), currentLesson);
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
        //protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        //{
        //    if (pageState != null && pageState.ContainsKey("game_pic_source"))
        //        game_pic.Source = pageState["game_pic_source"] as ImageSource;
        //    if (pageState != null && pageState.ContainsKey("mainGrid"))
        //        mainGrid = pageState["mainGrid"] as Grid;
        //    currentLesson = navigationParameter as Lesson;
        //    lessonHadLearnt = new HadLearnt(currentLesson);
        //    //int [,]matGrid=new int [3,4];

        //    int i = 0;
        //    foreach (var picture in lessonHadLearnt.pictuers_HadLearnt)
        //    {
        //        //r.Next(1, 4) r.Next(1, 3)

        //        //הוספת טב לליסט
        //        images.Add(picture.WordPictuer);
        //        tb.Add(new TextBlock() { Text = picture.LettersBigShape, FontSize = 50, Foreground = new SolidColorBrush(Colors.Maroon) });
        //        tb[i++].Tapped += game_discoverThePicture_Tapped;

        //        //הגרלה עד שמוגרל מקום פנוי


        //        //else
        //        //{


        //        //    //Grid.SetColumn(tb[i], k);
        //        //    //Grid.SetRow(tb[i], j);
        //        //    //matGrid[j, k] = 1;
        //        //    //game_grid.Children.Add(tb[i]);
        //        //    //i++;

        //        //}
        //        //}


        //        //Grid.SetColumn(tb[i],k );
        //        //Grid.SetRow(tb[i],j );


        //    }

        //    insertPictuer();
        //}
        //int y = 1;
        //int x = 1;
        //void game_discoverThePicture_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    TextBlock tb;
        //    //int [,]arrTb =new int[6,2];
        //    tb = sender as TextBlock;


        //    if (tb.Text.Equals(lettersBigShape))
        //    {
        //        tb.Margin = new Thickness(20, 0, 0, 0);
        //        tb.Foreground = new SolidColorBrush(Colors.Yellow);

        //        //מחיקה
        //        game_grid.Children.Remove(tb);
        //        //TextBlock tb1 = game_grid.Children[0] as TextBlock;
        //        //העברה
        //        int f = 0;

        //        if (y < 6 && f == 0)
        //        {
        //            Grid.SetColumn(tb, 0);
        //            Grid.SetRow(tb, y++);
        //        }
        //        else
        //        {
        //            f = 1;
        //            Grid.SetColumn(tb, 1);
        //            Grid.SetRow(tb, x++);
        //        }
        //        mainGrid.Children.Add(tb);
        //        insertPictuer();

        //    }
        //}
        //int c = 0;
        //private async void insertPictuer()
        //{
        //    int i = 0;
        //    int[,] matGrid = new int[6, 6];
        //    int j, k;
        //    //מגריל מקום
        //    j = r.Next(1, 6);
        //    k = r.Next(1, 6);
        //    int y;


        //    //אם נשארו תמונות שלא השתמשו
        //    if (c++ < x)
        //    {
        //        //מגריל תמונה כל עוד לא ציון שהיא כבר הגיעה
        //        do
        //        {
        //            y = r.Next(0,images.Count());
        //            game_pic.Source = images[y];
        //            lettersBigShape = tb[y].Text;
        //        }
        //        while (lessonHadLearnt.pictuers_HadLearnt[y].WordPictuerUsed == true);
        //        for (int l = 0; matGrid[j, k] == 1 && l < 36; l++)
        //            {
        //                k = r.Next(1, 6);
        //                j = r.Next(1, 6);
        //            }
        //            if (matGrid[j, k] == 0)
        //            {
        //                Grid.SetColumn(tb[i], k);
        //                Grid.SetRow(tb[i], j);
        //                matGrid[j, k] = 1;
        //                game_grid.Children.Add(tb[i]);
        //                i++;

        //            }
        //        lessonHadLearnt.pictuers_HadLearnt[y].WordPictuerUsed = true;
        //    }
        //    else
        //    {
        //        MessageDialog messageDialog = new MessageDialog("כל הכבוד !! הצלחת למצוא את כל האתיות והמשחק הסתיים ");
        //        messageDialog.Commands.Add(new UICommand("שחק שוב", new UICommandInvokedHandler(this.CommandInvokedHandler1)));
        //        messageDialog.Commands.Add(new UICommand("חזרה לשיעור", new UICommandInvokedHandler(this.CommandInvokedHandler2)));
        //        await messageDialog.ShowAsync();
        //    }

        //}
        ///// <summary>
        ///// Preserves state associated with this page in case the application is suspended or the
        ///// page is discarded from the navigation cache.  Values must conform to the serialization
        ///// requirements of <see cref="SuspensionManager.SessionState"/>.
        ///// </summary>
        ///// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        //protected override void SaveState(Dictionary<String, Object> pageState)
        //{
        //    pageState.Add("game_pic_source", game_pic.Source);
        //    pageState.Add("mainGrid", mainGrid);
        //}

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (pageState != null && pageState.ContainsKey("game_pic_source"))
                game_pic.Source = pageState["game_pic_source"] as ImageSource;
            if (pageState != null && pageState.ContainsKey("mainGrid"))
                mainGrid = pageState["mainGrid"] as Grid;
            currentLesson = navigationParameter as Lesson;
            lessonHadLearnt = new HadLearnt(currentLesson);
            int i = 0;
            int x;
            Random r = new Random();
            //שלא יגריל יותר תמונות ממה שלמדו בשיעור
            if (lessonHadLearnt.pictuers_HadLearnt.Count() < 20)
            {
                count = lessonHadLearnt.pictuers_HadLearnt.Count() - 1;
            }
            //מכניס לליסטים נתונים באופן אקראי מהשיעורים שנלמדו
            for (int j = 0; j < count; j++)
            {
                //מגריל תמונה כל עוד לא ציון שהיא כבר הגיעה
                do
                {
                    x = r.Next(1, lessonHadLearnt.pictuers_HadLearnt.Count());
                }
                while (lessonHadLearnt.pictuers_HadLearnt[x].WordPictuerUsed == true);
                lessonHadLearnt.pictuers_HadLearnt[x].WordPictuerUsed = true;
                var picture = lessonHadLearnt.pictuers_HadLearnt[x];
                //הוספת טב לליסט
                images.Add(picture.WordPictuer);
                //lessonHadLearnt.pictuers_HadLearnt[j].WordPictuerUsed = true;
                tb.Add(new TextBlock() { Text = picture.LettersBigShape, FontSize = 50, Foreground = new SolidColorBrush(Colors.Maroon) });
                tbWord.Add(new TextBlock() { Text = picture.Word });
                tb[i++].Tapped += game_discoverThePicture_Tapped;
            }
            insertPictuer();
            insertTextBloke();
        }

        int y = 2;
        int x = 2;
        int z = 2;
        //int t = 2;
        void game_discoverThePicture_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock tb;
            tb = sender as TextBlock;
            if (tb.Text.Equals(lettersBigShape))
            {
                tb_word.Text = word;
                tb.Margin = new Thickness(20, 0, 0, 0);
                tb.Foreground = new SolidColorBrush(Colors.Yellow);
                //מחיקה
                game_grid.Children.Remove(tb);
                //העברה
                if (y <= 8)
                {
                    Grid.SetColumn(tb, 0);
                    Grid.SetRow(tb, y++);
                }
                if (y > 8 && x <= 8)
                {
                    Grid.SetColumn(tb, 1);
                    Grid.SetRow(tb, x++);
                }
                if (y > 8 && x > 8)
                {
                    Grid.SetColumn(tb, 2);
                    Grid.SetRow(tb, z++);
                }
                //if (y > 8 && x > 8 && z>8)
                //{
                //    Grid.SetColumn(tb, 3);
                //    Grid.SetRow(tb, t++);
                //}
                mainGrid.Children.Add(tb);
                insertPictuer();
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
            pageState.Add("game_pic_source", game_pic.Source);
            pageState.Add("mainGrid", mainGrid);
        }

        // TextBloke מכניס לגריד   
        private void insertTextBloke()
        {
            int i = 0;
            int[,] matGrid = new int[6, 6];
            int j, k;
            //הגרלה עד שמוגרל מקום פנוי
            j = r.Next(1, 6);
            k = r.Next(1, 6);
            for (int m = 0; m < count; m++)
            {
                //for (int l = 0; matGrid[j, k] == 1 && l < count; l++)
                //{ //}
                //if (matGrid[j, k] == 0)
                //{//game_grid.Children.Add(tb[i]);
                do
                {
                    k = r.Next(1, 6);
                    j = r.Next(1, 6);
                } while (matGrid[j, k] == 1);
                game_grid.Children.Add(tb[i]);
               
                Grid.SetColumn(tb[i], k);
                Grid.SetRow(tb[i], j);
                matGrid[j, k] = 1;
                
                i++;
                //}
            }
        }
        int c = 0;
        private async void insertPictuer()
        {
            //אם נשארו תמונות שלא השתמשו
            if (c++ < count)
            {
                game_pic.Source = images[a];
                word = tbWord[a].Text; 
                lettersBigShape = tb[a++].Text;
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("כל הכבוד !! הצלחת למצוא את כל האתיות והמשחק הסתיים ");
                messageDialog.Commands.Add(new UICommand("שחק שוב", new UICommandInvokedHandler(this.CommandInvokedHandler1)));
                messageDialog.Commands.Add(new UICommand("חזרה לשיעור", new UICommandInvokedHandler(this.CommandInvokedHandler2)));
                await messageDialog.ShowAsync();
            }

        }
        private void CommandInvokedHandler1(IUICommand command)
        {
            this.Frame.Navigate(typeof(game_discoverThePicture), currentLesson);
        }
        private void CommandInvokedHandler2(IUICommand command)
        {
            this.Frame.Navigate(typeof(pictuerOfLetters), currentLesson);
        }
    }

}
