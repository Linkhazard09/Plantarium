
using Java.Util;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantarium
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();

         
           

        }



        public interface INotificationManager
        {
            event EventHandler NotificationReceived;

            void Initialize();

            int ScheduleNotification(string title, string message);

            void ReceiveNotification(string title, string message);
        }


        TimeSpan getDateDifference(DateTime date1, DateTime date2)
        {
            TimeSpan ts = date1 - date2;

            return ts;
        }
        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            TimeSpan difference = getDateDifference(NotificationDate.Date, DateTime.Now );
           
            string TNow = DateTime.Now.ToString("hh:mm:ss");
            TimeSpan ts = DateTime.ParseExact(TNow, "hh:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
            TimeSpan TDiff =ts - NotificationTime.Time;
            string saxon2 = NotificationTime.Time.ToString();
            int DayDifference = difference.Days;
            if (DayDifference < 0)
            {
                await DisplayAlert("Error", "Please Set A Correct Date for the Notification", "OK");
                return;
            }
            
            int HourDifference = TDiff.Hours;
            if (HourDifference < 0)
                HourDifference = -1 * HourDifference;

            int MinuteDifference = TDiff.Minutes;
            if (MinuteDifference < 0)
                MinuteDifference = -1 * MinuteDifference;
            int SecondDifference = TDiff.Seconds;
            if (SecondDifference < 0)
                SecondDifference = -1 * SecondDifference;
           
            CrossLocalNotifications.Current.Show("Your Notification!", NotificationTitleEntry.Text, 101, DateTime.Now.AddSeconds(SecondDifference).AddHours(HourDifference).AddMinutes(MinuteDifference).AddDays(DayDifference));
        }
    }
}