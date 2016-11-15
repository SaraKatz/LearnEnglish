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
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            ABC_song.MediaEnded += ABC_song_MediaEnded;
            index = int.Parse(e.Parameter.ToString()) ;
            MessageDialog messageDialog = new MessageDialog(  "שלום ל" +users.UserList[index].Name + "");
            messageDialog.Commands.Add(new UICommand("לשיר", new UICommandInvokedHandler(this.CommandInvokedHandler1)));
            messageDialog.Commands.Add(new UICommand("לשיעור הקודם", new UICommandInvokedHandler(this.CommandInvokedHandler2)));
            messageDialog.Commands.Add(new UICommand("למפת השיעורים", new UICommandInvokedHandler(this.CommandInvokedHandler3)));
            await messageDialog.ShowAsync();
        }

        async void  ABC_song_MediaEnded(object sender, RoutedEventArgs e)
        {
            ABC_song.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MessageDialog messageDialog = new MessageDialog("שלום ל" + users.UserList[index].Name + "");
            messageDialog.Commands.Add(new UICommand("לשיעור הקודם", new UICommandInvokedHandler(this.CommandInvokedHandler2)));
            messageDialog.Commands.Add(new UICommand("למפת השיעורים", new UICommandInvokedHandler(this.CommandInvokedHandler3)));
            await messageDialog.ShowAsync();
        }

        private  void CommandInvokedHandler1(IUICommand command)
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
            this.Frame.Navigate(typeof(lessonMapPage),users.UserList[index]);
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            ABC_song.Pause();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            ABC_song.Play();
        }
    }
}
