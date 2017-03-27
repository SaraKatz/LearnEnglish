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
using System.Xml.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using English.Common;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace English
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class workPage : English.Common.LayoutAwarePage
    {

        List<letter> lettersHadLearned = new List<letter>();
        Lesson currentLesson;
        public int i { get; set; }
        HadLearnt lessonHadLearnt;
        private int j;
        string big_letter_pressed = null;
        string small_letter_pressed = null;
        string l;
        int flag;
        SolidColorBrush[] arrayColors = new SolidColorBrush[5];
        int c1 = 0;
        //int j = 0;

        List<TextBlock> tbBig = new List<TextBlock>();
        List<TextBlock> tbsmall = new List<TextBlock>();
        Random r = new Random();
        string LettersBigShape;
        string LettersSmallShape;
        List<ComboBox> cboxSmall;
        List<Image> img_fidback;

        int[] arrayBig = new int[6];
        int[] arraySmall = new int[6];
        int m;
        int n;
        public workPage()
        {
            this.InitializeComponent();

        }


        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            currentLesson = navigationParameter as Lesson;
            lessonHadLearnt = new HadLearnt(currentLesson);
            arrayColors[0] = new SolidColorBrush(Colors.Blue); 
            arrayColors[1] = new SolidColorBrush(Colors.Red);
            arrayColors[2] = new SolidColorBrush(Colors.Green);
            arrayColors[3] = new SolidColorBrush(Colors.Turquoise);
            arrayColors[4] = new SolidColorBrush(Colors.Yellow);
            Random r = new Random();

            for (int k = 0; k < lessonHadLearnt.lesson_learnt.Count; k++)
            {

                foreach (var item in lessonHadLearnt.lesson_learnt[k].lettersForLesson)
                {

                    tbBig.Add(new TextBlock() { Text = item.LettersBigShape,Foreground=new SolidColorBrush(Colors.Black), Height = 100, Width = 100, FontSize = 60, Tag = false });
                    tbBig[i].Tapped += ex1_Tapped;

                    tbsmall.Add(new TextBlock() { Text = item.LettersSmallShape, Foreground = new SolidColorBrush(Colors.Black), Height = 100, Width = 100, FontSize = 60, });
                    tbsmall[i].Tapped += ex1_Tapped;
                    i++;
                }
            }



        }
        void ex1_start(object sender, RoutedEventArgs e)
        {
            ex1Start.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ex1Start.IsEnabled = false;
            Random rnm = new Random();
            for (int p = 0; p < 5 && p < tbBig.Count; p++)
            {
                int t = new int();

                //מגריל טקסט בלוק כל עוד לא ציון שהוא כבר הגיע
                do
                {
                    t = rnm.Next(0, tbBig.Count());
                }
                while (Boolean.Parse(tbBig[t].Tag.ToString()) == true);
                tbBig[t].Tag = true;

                //מגריל מקום
                m = r.Next(1, 6);
                for (int l = 0; arrayBig[m] == 1 && l < 36; l++)
                {
                    m = r.Next(1, 6);
                }
                arrayBig[m] = 1;
                Grid.SetColumn(tbBig[t], 1);
                Grid.SetRow(tbBig[t], m);

                Ex1.Children.Add(tbBig[t]);


                n = r.Next(1, 6);
                for (int l = 0; arraySmall[n] == 1 && l < 36; l++)
                {
                    n = r.Next(1, 6);
                }
                arraySmall[n] = 1;
                Grid.SetColumn(tbsmall[t], 3);
                Grid.SetRow(tbsmall[t], n);
                Ex1.Children.Add(tbsmall[t]);
            }
        }
        
        void ex1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                TextBlock tb = (sender as TextBlock);

                Ex1_fidback.Text = "";
                for (int k = 0; k < lessonHadLearnt.lesson_learnt.Count; k++)
                {
                    if (big_letter_pressed == null && small_letter_pressed == null)//אם זה פעם ראשונה 
                    {
                        foreach (var letter in lessonHadLearnt.lesson_learnt[k].lettersForLesson)
                        {

                            if (tb.Text.Equals(letter.LettersBigShape))
                            {
                                if (c1 > 4)
                                {
                                    c1 = 0;
                                }
                                big_letter_pressed = tb.Text;
                                tb.Foreground = arrayColors[c1];
                                //tb.Foreground = new SolidColorBrush(Colors.Yellow);
                                break;
                            }
                            if (tb.Text.Equals(letter.LettersSmallShape))
                            {
                                small_letter_pressed = tb.Text;
                                if (c1 > 4)
                                {
                                    c1 = 0;
                                }
                                tb.Foreground = arrayColors[c1];
                                //tb.Foreground = new SolidColorBrush(Colors.Yellow);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (big_letter_pressed == null && small_letter_pressed != null)//אם בפעם הראשונה לחץ על אות קטנה 
                        {
                            foreach (var letter in lessonHadLearnt.lesson_learnt[k].lettersForLesson)
                            {

                                if (tb.Text.Equals(letter.LettersBigShape) && letter.LettersSmallShape.Equals(small_letter_pressed))
                                {
                                    if (c1 > 4)
                                    {
                                        c1 = 0;
                                    }
                                    tb.Foreground = arrayColors[c1++];

                                    //tb.Foreground = new SolidColorBrush(Colors.Yellow);
                                    fidbakMedia3.Source = new Uri(@"ms-appx:/Assets/backgrounds/Yeah.WAV");
                                    fidbakMedia3.Play();
                                    Ex1_fidback.Text = "!יפה";
                                    big_letter_pressed = small_letter_pressed = null;
                                }

                            }
                        }
                        else
                        {
                            if (big_letter_pressed != null && small_letter_pressed == null)//אם בפעם הראשונה לחץ על אות גדולה  
                            {
                                foreach (var letter in lessonHadLearnt.lesson_learnt[k].lettersForLesson)
                                {
                                    if (tb.Text.Equals(letter.LettersSmallShape) && letter.LettersBigShape.Equals(big_letter_pressed))
                                    {
                                        if (c1 > 4)
                                        {
                                            c1 = 0;
                                        }
                                        tb.Foreground = arrayColors[c1++];
                                        //tb.Foreground = new SolidColorBrush(Colors.Yellow);
                                        fidbakMedia3.Source = new Uri(@"ms-appx:/Assets/backgrounds/Yeah.WAV");
                                        fidbakMedia3.Play();
                                        Ex1_fidback.Text = "!יפה";
                                        big_letter_pressed = small_letter_pressed = null;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
           
        }

        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            mediaFid.Stop();
            this.Frame.Navigate(typeof(lessonMapPage), currentLesson);
        }

        private void ex2_selctedChange(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.SelectedValue.Equals(cmb.Name))
            {
                Ex2_fidback.Text = "";
                fidbakMedia2.Source = new Uri(@"ms-appx:/Assets/backgrounds/Yeah.WAV");
                Ex2_fidback.Text = "\r!מצוין";
                foreach (Image item in img_fidback)
                {
                    if (item.Name.EndsWith(cmb.Name))
                    {
                        item.Source = new BitmapImage(new Uri(@"ms-appx:/Assets/v.png"));
                    }
                }
            }

            else
            {
                Ex2_fidback.Text = "";
                fidbakMedia2.Source = new Uri(@"ms-appx:/Assets/backgrounds/chrcbell.WAV");
                fidbakMedia2.Play();
                Ex2_fidback.Text = "זו לא האות המתאימה " + "\r" + "נסה שוב";
                foreach (Image item in img_fidback)
                {
                    if (item.Name.EndsWith(cmb.Name))
                    {
                        item.Source = new BitmapImage(new Uri(@"ms-appx:/Assets/x.png"));
                    }
                }
            }
        }
        void start_ex3(object sender, RoutedEventArgs e)
        {
            ex3Start.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ex3Start.IsEnabled = false;
            if (flag == 0)
            {
                flag = 1;
                insertPictuer();
            }

            Ex3_ans.Visibility = Windows.UI.Xaml.Visibility.Visible;
            brd_pic.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Ex3_pic.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        void start_ex2(object sender, RoutedEventArgs e)
        {
            ex2Start.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ex2Start.IsEnabled = false;
            int i = 0;
            int j;
            List<TextBlock> tbBig = new List<TextBlock>();
            cboxSmall = new List<ComboBox>();
            img_fidback = new List<Image>();
            foreach (var lesson in lessonHadLearnt.lesson_learnt)
            {


                foreach (var letter in lesson.lettersForLesson)
                {
                    //להכניס  אותיות או כמה שנלמדו עד כה
                    tbBig.Add(new TextBlock() { Text = letter.LettersBigShape,Foreground=new SolidColorBrush(Colors.Black), FontSize = 60, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom, Tag = false });


                    //שיכיל רשימה של כל האותיות האפשריותComboBox
                    cboxSmall.Add(new ComboBox() { Foreground = new SolidColorBrush(Colors.Black), Name = letter.LettersSmallShape, FontFamily = new FontFamily("Gill Sans Ultra Bold Condensed") });
                    cboxSmall[i].FontSize = 40;
                    cboxSmall[i].Tapped += workPage_Tapped;
                    cboxSmall[i].SelectionChanged += ex2_selctedChange;

                    img_fidback.Add(new Image() { Name = "a" + tbBig[i].Text.ToLower(), Height = 20, Width = 20, HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left });



                    i++;
                }

            }
            //הוספת אותיות ל cmb
            //j = i - 1;
            //do
            //{
            //    foreach (var lesson in lessonHadLearnt.lesson_learnt)
            //    {

            //        foreach (var letter in lesson.lettersForLesson)
            //        {
            //            cboxSmall[j].Items.Add(letter.LettersSmallShape);
            //        }

            //    }
            //    j--;
            //} while (j >= 0);
            j = i - 1;
            do
            {
                foreach (var lesson in lessonHadLearnt.lesson_learnt)
                {

                    foreach (var letter in lesson.lettersForLesson)
                    {
                        cboxSmall[j].Items.Add(letter.LettersSmallShape);
                    }

                }
                j--;
            } while (j >= 0);



            int t;
            Random r = new Random();
            for (int x = 0; x < 5 && x < tbBig.Count; x++)
            {

                //מגריל טקסט בלוק כל עוד לא ציון שהוא כבר הגיע
                do
                {
                    t = r.Next(0, tbBig.Count());
                }
                while (Boolean.Parse(tbBig[t].Tag.ToString()) == true);

                tbBig[t].Tag = true;
                Grid.SetColumn(tbBig[t], x);
                Grid.SetRow(tbBig[t], 1);
                Ex2.Children.Add(tbBig[t]);

                Grid.SetColumn(cboxSmall[t], x);
                Grid.SetRow(cboxSmall[t], 3);
                Ex2.Children.Add(cboxSmall[t]);



                Grid.SetColumn(img_fidback[t], x);
                Grid.SetRow(img_fidback[t], 2);
                Ex2.Children.Add(img_fidback[t]);
            }

        }

        private void workPage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ex2_fidback.Text = "";
        }

        private void insertPictuer()
        {
            
            int x = lessonHadLearnt.pictuers_HadLearnt.Count();
            int y = r.Next(0, x);
            Ex3_pic.Source = lessonHadLearnt.pictuers_HadLearnt[y].WordPictuer;
            LettersBigShape = lessonHadLearnt.pictuers_HadLearnt[y].LettersBigShape;
            LettersSmallShape = lessonHadLearnt.pictuers_HadLearnt[y].LettersSmallShape;
           
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


        private void Ex3_ok_Click(object sender, RoutedEventArgs e)
        {

            //Ex3_ans.Text.Substring(1);


        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //private void Ex3_ans_KeyDown(object sender, KeyRoutedEventArgs e)
        //{

        //}
 string str = "";
 string s;
        private async void Ex3_ans_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            Ex3_fidback.Text = "";
            if (e.Key.ToString().Equals("Enter"))
            {

                if (str.Equals(LettersBigShape) || str.Equals(LettersSmallShape))
                {
                    Ex3_fidback.Text = "!יפה מאד ";
                    fidbakMedia3.Source=new Uri(@"ms-appx:/Assets/backgrounds/Yeah.WAV");
                    fidbakMedia3.Play();
                    await System.Threading.Tasks.Task.Delay(2000);
                    str = "";
                    Ex3_ans.Text = "";
                    Ex3_fidback.Text = "";
                    insertPictuer();
                }
                else
                {
                    fidbakMedia3.Source = new Uri(@"ms-appx:/Assets/backgrounds/Checkpoint.WAV");
                    fidbakMedia3.Play();
                    Ex3_fidback.Text = "נסה שנית";
                }
            }
            else
            {
                str = Ex3_ans.Text;
            }

        }

        private void togame_Click(object sender, RoutedEventArgs e)
        {
            mediaFid.Stop();
            this.Frame.Navigate(typeof(game_discoverThePicture), currentLesson);
        }


    }
}
