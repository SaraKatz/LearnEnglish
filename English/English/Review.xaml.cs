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
    public sealed partial class Review : English.Common.LayoutAwarePage
    {
        InkManager _inkKhaled;
        private uint _penID;
        private uint _touchID;
        private Point _previousContactPt;
        private Point currentContactPt;
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        private double thick;
        SolidColorBrush sbrush;
        PenTipShape ptsh;
        Lesson l;
        private EnglishUser currentUser;

        public int Flag { get; set; }


        public Review()
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {


            letterToWright.Text = "";
            l = navigationParameter as Lesson;
            letterToWright.Text = l.selectedLetter;

            if (l.selectedLetter.Equals("t"))
            {
                letterToWright.FontFamily = new FontFamily("Ariel black");
            }
            else
            {
letterToWright.FontFamily = new FontFamily("Comic Sans MS");
            }
            
            _inkKhaled = new InkManager();
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Black);
            ptsh = new PenTipShape(){};
            myCanvas.PointerPressed += new PointerEventHandler(MyCanvas_PointerPressed);
            myCanvas.PointerMoved += new PointerEventHandler(MyCanvas_PointerMoved);
            myCanvas.PointerReleased += new PointerEventHandler(MyCanvas_PointerReleased);
            myCanvas.PointerExited += new PointerEventHandler(MyCanvas_PointerReleased);
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
        #region PointerEvents
        private void MyCanvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            
            if (e.Pointer.PointerId == _penID)
            {
                Windows.UI.Input.PointerPoint pt = e.GetCurrentPoint(myCanvas);

                // Pass the pointer information to the InkManager. 
                _inkKhaled.ProcessPointerUp(pt);
            }

            else if (e.Pointer.PointerId == _touchID)
            {
                // Process touch input
            }

            _touchID = 0;
            _penID = 0;

            // Call an application-defined function to render the ink strokes.


            e.Handled = true;
        }

        private void MyCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
           
            if (e.Pointer.PointerId == _penID)
            {
                PointerPoint pt = e.GetCurrentPoint(myCanvas);

                // Render a red line on the canvas as the pointer moves. 
                // Distance() is an application-defined function that tests
                // whether the pointer has moved far enough to justify 
                // drawing a new line.
                currentContactPt = pt.Position;
                x1 = _previousContactPt.X;
                y1 = _previousContactPt.Y;
                x2 = currentContactPt.X;
                y2 = currentContactPt.Y;


                if (Distance(x1, y1, x2, y2) > 2.0) // We need to developp this method now 
                {
                    Line line = new Line()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        StrokeThickness = thick,
                        Stroke =sbrush,
                        StrokeStartLineCap=PenLineCap.Round,
                        StrokeLineJoin = PenLineJoin.Round,
                        
                       
                        Fill=new SolidColorBrush(Colors.BlueViolet)
                    };

                    _previousContactPt = currentContactPt;

                    // Draw the line on the canvas by adding the Line object as
                    // a child of the Canvas object.
                    myCanvas.Children.Add(line);

                    // Pass the pointer information to the InkManager.
                    _inkKhaled.ProcessPointerUpdate(pt);
                }
            }

            else if (e.Pointer.PointerId == _touchID)
            {
                // Process touch input
            }

        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            double d = 0;
            d = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return d;
        }

        private void MyCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Get information about the pointer location.
            PointerPoint pt = e.GetCurrentPoint(myCanvas);
            _previousContactPt = pt.Position;

            // Accept input only from a pen or mouse with the left button pressed. 
            PointerDeviceType pointerDevType = e.Pointer.PointerDeviceType;
            if (pointerDevType == PointerDeviceType.Pen ||
                    pointerDevType == PointerDeviceType.Mouse &&
                    pt.Properties.IsLeftButtonPressed||pointerDevType == PointerDeviceType.Touch)
            {
                // Pass the pointer information to the InkManager.
                _inkKhaled.ProcessPointerDown(pt);
                _penID = pt.PointerId;

                e.Handled = true;
            }

            //else if (pointerDevType == PointerDeviceType.Touch)
            //{
            //    // Process touch input
            //}
        }

        #endregion

        private void learnSmallLetter_Click(object sender, RoutedEventArgs e)
        {
            l.selectedLetter = l.selectedLetter.ToLower();
            this.Frame.Navigate(typeof(howToWright), l);
        }

        //private void learnNextlLetter_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Frame.Navigate(typeof(lessonSummaryPage), l);
        //}

     
        
        private void earase_Click(object sender, RoutedEventArgs e)
        {
            thick = 27.0;
            sbrush = new SolidColorBrush(Colors.White);
            //sbrush = new SolidColorBrush(Colors.OliveDrab);
        }

        private void wright_Click(object sender, RoutedEventArgs e)
        {
            Flag = 1;
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Black);
        }

        private void pictuers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(pictuerOfLetters), l);
        }

        private void color_Click(object sender, RoutedEventArgs e)
        {
            colorGrid.Visibility = Visibility.Visible;
        }

        private void toLesssonMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage), l);

        }

        private void RectangleColor1_Tapped(object sender, TappedRoutedEventArgs e)
        {
         //
            thick = 10.0;
            Rectangle r = sender as Rectangle;
            //string s = ;
           
            sbrush =new SolidColorBrush(Colors.BlueViolet) ;
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Red);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Yellow);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.DeepPink);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.GreenYellow);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Brush b;
            //b = sender as Brush;
            thick = 10.0;
            Rectangle r = sender as Rectangle;
            sbrush = new SolidColorBrush(Colors.Purple);
            //sbrush.Color=(r.Fill) as Brush;
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor7_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Orange);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Turquoise);
            colorGrid.Visibility = Visibility.Collapsed;
        }
        private void RectangleColor9_Tapped(object sender, TappedRoutedEventArgs e)
        {
            thick = 10.0;
            sbrush = new SolidColorBrush(Colors.Gray);
            colorGrid.Visibility = Visibility.Collapsed;
        }

 //       private void RectangleColor1_Click(object sender, RoutedEventArgs e)
 //       {
 //           Button b = sender as Button;
 //           thick = 10.0;
 //           sbrush.br
 //=(b.Background) as color;
 //           colorGrid.Visibility = Visibility.Collapsed;
 //       }

    }
}
