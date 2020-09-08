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
    public partial class Forum : ContentPage
    {
        private string Username;
        public IList<Forums> GetForum { get; private set; }
        public Forum(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            Task taskA = Task.Run(() => PopulateListView());
            taskA.Wait();

            // PopulateListView();
            BindingContext = this;
           

        }

        private async void ForumListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as Forums;
            string Poster = Selected.Username;
            string Headline = Selected.Headline;

            await Navigation.PushAsync(new Forum_Content(Poster,Headline,Username));




        }

        public class Forums
        {
           
            public string Username { get; set; }
            public string Date { get; set; }

            public string Time { get; set; }

            public string Headline { get; set; }

            public override string ToString()
            {
                return Username;

            }
        }

        private async void AddForumButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddForum(Username));
        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(Username));
        }

        private void PopulateListView()
        {
           
            List<string> UN = new List<string>();
            List<string> HL = new List<string>();
            List<string> DE = new List<string>();
            List<string> TE = new List<string>();
            string[] AA; //Date
            string[] BB; //Headline
            string[] CC; //Time




            int x = 0;
            Service1Client srvc = new Service1Client();
            UN = srvc.GetForumsAll(out AA, out BB, out CC).ToList();
            HL = BB.ToList();
            DE = AA.ToList();
            TE = CC.ToList();
            x = 0;
            GetForum = new List<Forums>();
            foreach (string s in UN)
            {
                if (x == 10)
                    break;
                string y = DE[x].Substring(0, 10);
                GetForum.Add(new Forums { Username = UN[x], Headline = HL[x], Date = y, Time = TE[x] });
                x++;
            }

        }

    }
}