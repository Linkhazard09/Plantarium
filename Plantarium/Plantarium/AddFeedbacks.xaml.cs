using DotNetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantarium
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFeedbacks : ContentPage
    {
        private string Username;
        private string Guide_Name;
        private string Plant_Name;
        public AddFeedbacks(string Username,string Guide_Name,string Plant_Name)
        {
            InitializeComponent();
            this.Username = Username;
            this.Plant_Name = Plant_Name;
            this.Guide_Name = Guide_Name;
            var RatingList = new List<int>();
            RatingList.Add(1);
            RatingList.Add(2);
            RatingList.Add(3);
            RatingList.Add(4);
            RatingList.Add(5);
            Picker_Feedback.ItemsSource = RatingList;



        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            Service1Client srvc = new Service1Client();
            try
            {
                srvc.FeedbackInsert(Username, Guide_Name, Plant_Name, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("hh:mm tt"), Editor_FeedbackContent.Text, int.Parse(Picker_Feedback.SelectedItem.ToString()));
            }
            catch
            {
                await DisplayAlert("Error", "Error in adding your feedback please check that all fields have been properly filled or that you have not already submitted your feedback on this guide!", "OK");
                return;
            }

            await DisplayAlert("Confirmation", "Your feedback has been submitted!", "OK");
            await Navigation.PushAsync(new Feedback(Username,Plant_Name,Guide_Name));

        }
    }
}