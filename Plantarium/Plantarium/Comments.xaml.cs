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
    public partial class Comments : ContentPage
    {
        private string User;
        private string Headline;
        private string Poster;
        public IList<ForumComments> GetComments { get; private set; }
        public Comments(string User, string Headline, string Poster)
        {
            InitializeComponent();
            this.User = User;
            this.Headline = Headline;
            this.Poster = Poster;
            List<string> CP = new List<string>();
            List<string> CC = new List<string>();
            List<string> DE = new List<string>();
            List<string> TE = new List<string>();
            string[] AA; //Date
            string[] BB; //Comment_Content
            string[] ZZ; //Time
            int x = 0;
            GetComments = new List<ForumComments>();
            Service1Client srvc = new Service1Client();
            CP = srvc.GetComments(Headline, Poster, out AA, out ZZ, out BB).ToList();
            CC = BB.ToList();
            DE = AA.ToList();
            TE = ZZ.ToList();

           
            foreach(string s in CP)
            {
                string y = DE[x].Substring(0, 10);
                GetComments.Add(new ForumComments { CommentPoster = CP[x], Comment_Content = CC[x], Date = y, Time = TE[x] });
                x++;




            }


            BindingContext = this;





        }



        public class ForumComments
        {
            public string CommentPoster { get; set; }
            public string Date { get; set; }

            public string Time { get; set; }

            public string Comment_Content { get; set; }

            public override string ToString()
            {
                return CommentPoster;

            }
        }

        private async void Enter_Clicked(object sender, EventArgs e)
        {
            Service1Client srvc = new Service1Client();
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            string Time = DateTime.Now.ToString("hh:mm tt");

            if(AddCommentEntry.Text==null || AddCommentEntry.Text == "" || AddCommentEntry.Text == " ")
            {
                await DisplayAlert("Fields", "Please fill up all fields!", "OK");
                return;
            }



            srvc.InsertComment(Poster, User, Headline, AddCommentEntry.Text, Date, Time);
            await DisplayAlert("Success", "Comment Added!", "OK");

            List<string> CP = new List<string>();
            List<string> CC = new List<string>();
            List<string> DE = new List<string>();
            List<string> TE = new List<string>();
            string[] AA; //Date
            string[] BB; //Comment_Content
            string[] ZZ; //Time
            int x = 0;
            GetComments = new List<ForumComments>();

            CP = srvc.GetComments(Headline, Poster, out AA, out ZZ, out BB).ToList();
            CC = BB.ToList();
            DE = AA.ToList();
            TE = ZZ.ToList();


            foreach (string s in CP)
            {
                string y = DE[x].Substring(0, 10);
                GetComments.Add(new ForumComments { CommentPoster = CP[x], Comment_Content = CC[x], Date = y, Time = TE[x] });
                x++;




            }

            CommentListView.ItemsSource = GetComments;
            AddCommentEntry.Text = null;




        }
    }
}