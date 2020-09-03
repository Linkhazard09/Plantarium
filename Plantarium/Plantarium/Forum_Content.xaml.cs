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
    public partial class Forum_Content : ContentPage
    {
        private string Username;
        private string Headline;


        public Forum_Content(string Username, string Headline)
        {
            InitializeComponent();
            this.Username = Username;
            this.Headline = Headline;
            string Date, Time, Forum_Content;

            Service1Client srvc = new Service1Client();
            Forum_Content = srvc.GetForumContent(Username, Headline, out Date, out Time);

            ForumContent_Label.Text = Forum_Content;
            DateLabel.Text = Date.Substring(0,10);
            TimeLabel.Text = Time;
            UsernameLabel.Text = Username;
            Forum_HeadlineLabel.Text = Headline;








        }

        private void ViewCommentsButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}