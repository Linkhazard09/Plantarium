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
        public string Username;
        public IList<Forums> GetForum { get; private set; }
        public Forum(string Username)
        {
            InitializeComponent();
            this.Username = Username;
            List<string> UN = new List<string>();
            List<string> HL = new List<string>();
            List<string> DE = new List<string>();
            List<string> TE = new List<string>();
            string[] AA; //Date
            string[] BB; //Headline
            string[] CC; //Time

            int x = 0;
            Service1Client srvc = new Service1Client();
            UN = srvc.GetForumsAll(out AA,out BB,out CC).ToList() ;
            HL = BB.ToList();
            DE = AA.ToList();
            TE = CC.ToList();
            x = UN.Count() - 1;

            GetForum = new List<Forums>();
            foreach(string s in UN)
            {
                string y = DE[x].Substring(0, 10);
                GetForum.Add(new Forums { Username = "u/" + UN[x], Headline = HL[x], Date = y, Time = TE[x] }   );
                x--;
            }

            BindingContext = this;

        }

        private void ForumListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {

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







    }
}