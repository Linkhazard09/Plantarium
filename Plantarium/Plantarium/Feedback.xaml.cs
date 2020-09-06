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
    public partial class Feedback : ContentPage
    {
        public string Username;
        public string Guide_Name;
        public string Plant_Name;
        public IList<Feedbacks> AllFeedbacks { get; private set; }
        public Feedback(string Username,string Plant_Name, string Guide_Name)
        {
            this.Username = Username;
            this.Guide_Name = Guide_Name;
            this.Plant_Name = Plant_Name;
            InitializeComponent();
            List<string> UN = new List<string>();
            List<string> FC = new List<string>();
            List<string> DE = new List<string>();
            List<string> TE = new List<string>();
            List<string> RT = new List<string>();
            string[] AA;
            string[] BB;
            string[] CC;
            string[] DD;
            int x = 0;
            Service1Client srvc = new Service1Client();
            UN = srvc.GetFeedbacks(Guide_Name,Plant_Name,out AA,out BB, out CC,out DD).ToList();
            FC = CC.ToList();
            DE = AA.ToList();
            TE = BB.ToList();
            RT = DD.ToList();
            x = UN.Count()-1;
           
            AllFeedbacks = new List<Feedbacks>();
            foreach(string s in UN)
            {

                string y = DE[x].Substring(0, 10);
                AllFeedbacks.Add(new Feedbacks { Username = UN[x], Date = y, Time = TE[x], Rating = RT[x], Feedback_Content = FC[x] });
                    x--;
                     
            }

            BindingContext = this;

         


        }


        public class Feedbacks
        {
            public string Username { get; set; }
            public string Date { get; set; }
            public string Feedback_Content { get; set; }
            
            public string Time { get; set; }

            public string Rating { get; set; }



            public override string ToString()
            {
                return Username;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFeedbacks(Username,Guide_Name,Plant_Name));




        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(Username));
        }
    }
}