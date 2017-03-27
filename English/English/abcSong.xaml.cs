using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace English
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abcSong : Page
    {
        Users users = new Users();
        int index;
        public abcSong()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        ///// property is typically used to configure the page.</param>
        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            ABC_song.MediaEnded += ABC_song_MediaEnded;
            index = int.Parse(e.Parameter.ToString());
        }

        void ABC_song_MediaEnded(object sender, RoutedEventArgs e)
        {
            to_lastLesson.Visibility = Windows.UI.Xaml.Visibility.Visible;
            to_map.Visibility = Windows.UI.Xaml.Visibility.Visible;
            to_song.Visibility = Windows.UI.Xaml.Visibility.Visible;
            StopTimer();
            timelineSlider.Value = 0.0;
            timelineSlider.Visibility = Visibility.Collapsed;
            ABC_song.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //MessageDialog messageDialog = new MessageDialog("שלום ל" + users.UserList[index].Name + "");
            //messageDialog.Commands.Add(new UICommand("לשיעור הקודם", new UICommandInvokedHandler(this.CommandInvokedHandler2)));
            //messageDialog.Commands.Add(new UICommand("למפת השיעורים", new UICommandInvokedHandler(this.CommandInvokedHandler3)));
            //await messageDialog.ShowAsync();
        }
        private void to_song_Click(object sender, RoutedEventArgs e)
        {
            to_lastLesson.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            to_map.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            to_song.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            ABC_song.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ABC_song.Play();
        }

        private void to_lastLesson_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonSummaryPage), users.UserList[index]);
        }


        private void to_map_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(lessonMapPage), users.UserList[index]);
        }
        private void CommandInvokedHandler1(IUICommand command)
        {
            ABC_song.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ABC_song.Play();
        }

        private void CommandInvokedHandler2(IUICommand command)
        {
            this.Frame.Navigate(typeof(lessonSummaryPage), users.UserList[index]);

        }
        private void CommandInvokedHandler3(IUICommand command)
        {
            this.Frame.Navigate(typeof(lessonMapPage), users.UserList[index]);

        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            ABC_song.Pause();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            ABC_song.Play();
        }

        private DispatcherTimer _timer;

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(timelineSlider.StepFrequency);
            StartTimer();
        }

        private void _timer_Tick(object sender, object e)
        {
            if (!_sliderpressed)
            {
                timelineSlider.Value = ABC_song.Position.TotalSeconds;
            }
        }

        private void StartTimer()
        {
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void StopTimer()
        {
            _timer.Stop();
            _timer.Tick -= _timer_Tick;
        }


        public bool _sliderpressed { get; set; }

        private void ABC_song_MediaOpened(object sender, RoutedEventArgs e)
        {
            ////SetupTimer();
            double absvalue = (int)Math.Round(
       ABC_song.NaturalDuration.TimeSpan.TotalSeconds,
       MidpointRounding.AwayFromZero);

            timelineSlider.Maximum = absvalue;
            //timelineSlider.StepFrequency =
            //timelineSlider.StepFrequency =
            //SliderFrequency(ABC_song.NaturalDuration.TimeSpan);

            SetupTimer();

            // Helper method to populate the combobox with audio tracks.
            //PopulateAudioTracks(ABC_song, cbAudioTracks);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            timelineSlider.ValueChanged += timelineSlider_ValueChanged;

            PointerEventHandler pointerpressedhandler = new PointerEventHandler(timelineSlider_PointerEntered);
            timelineSlider.AddHandler(Control.PointerPressedEvent, pointerpressedhandler, true);

            PointerEventHandler pointerreleasedhandler = new PointerEventHandler(timelineSlider_PointerCaptureLost);
            timelineSlider.AddHandler(Control.PointerCaptureLostEvent, pointerreleasedhandler, true);
        }

        //void videoElement_MediaOpened(object sender, RoutedEventArgs e)
        //{
        //    double absvalue = (int)Math.Round(
        //        ABC_song.NaturalDuration.TimeSpan.TotalSeconds,
        //        MidpointRounding.AwayFromZero);

        //    timelineSlider.Maximum = absvalue;

        //    timelineSlider.StepFrequency =
        //        SliderFrequency(ABC_song.NaturalDuration.TimeSpan);

        //    SetupTimer();

        //    // Helper method to populate the combobox with audio tracks.
        //    PopulateAudioTracks(videoMediaElement, cbAudioTracks);
        //}


        //Finally, this code shows the event handlers for pointer position changes and for the ValueChanged, CurrentStateChanged, and MediaEnded events.
        //C# 
        //Copy 

        //private bool _sliderpressed = false;

        //void slider_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    _sliderpressed = true;
        //}

        //void slider_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        //{
        //    ABC_song.Position = TimeSpan.FromSeconds(timelineSlider.Value);
        //    _sliderpressed = false;
        //}

        void timelineSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (!_sliderpressed)
            {
                ABC_song.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        //void videoMediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        //{
        //    if (ABC_song.CurrentState == MediaElementState.Playing)
        //    {
        //        if (_sliderpressed)
        //        {
        //            _timer.Stop();
        //        }
        //        else
        //        {
        //            _timer.Start();
        //        }
        //    }

        //    if (ABC_song.CurrentState == MediaElementState.Paused)
        //    {
        //        _timer.Stop();
        //    }

        //    if (ABC_song.CurrentState == MediaElementState.Stopped)
        //    {
        //        _timer.Stop();
        //        timelineSlider.Value = 0;
        //    }
        //}

        //void videoMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    StopTimer();
        //    timelineSlider.Value = 0.0;
        //}

        //private void videoMediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        //{
        //    // get HRESULT from event args 
        //    string hr = GetHresultFromErrorMessage(e);

        //    // Handle media failed event appropriately 
        //}

        private string GetHresultFromErrorMessage(ExceptionRoutedEventArgs e)
        {
            String hr = String.Empty;
            String token = "HRESULT - ";
            const int hrLength = 10;     // eg "0xFFFFFFFF"

            int tokenPos = e.ErrorMessage.IndexOf(token, StringComparison.Ordinal);
            if (tokenPos != -1)
            {
                hr = e.ErrorMessage.Substring(tokenPos + token.Length, hrLength);
            }

            return hr;
        }



        private void timelineSlider_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _sliderpressed = false;
        }

        private void timelineSlider_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            ABC_song.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            _sliderpressed = false;
        }

        private void ABC_song_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (ABC_song.CurrentState == MediaElementState.Playing)
            {
                if (_sliderpressed)
                {
                    _timer.Stop();
                }
                else
                {
                    _timer.Start();
                }
            }

            if (ABC_song.CurrentState == MediaElementState.Paused)
            {
                _timer.Stop();
            }

            if (ABC_song.CurrentState == MediaElementState.Stopped)
            {
                _timer.Stop();
                timelineSlider.Value = 0;
            }
        }

        private void ABC_song_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // get HRESULT from event args 
            string hr = GetHresultFromErrorMessage(e);

            // Handle media failed event appropriately 

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        

        

       



    }
}
