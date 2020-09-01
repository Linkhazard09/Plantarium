using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Plantarium
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        IService1 Ws = new DotNetService.Service1Client();
        public SignUp()
        {
            InitializeComponent();
            this.Title = "New User Form";
           
        }

        private async void Submit_Button_Clicked(object sender, EventArgs e)
        {
            string sfx = Suffx_Entry.Text;
            if (sfx == null)
                sfx = " ";
          Ws.InsertUser(LastName_Entry.Text,GivenName_Entry.Text,MiddleName_Entry.Text,sfx,EmailAddress_Entry.Text,Username_Entry.Text, Password_Entry.Text);

            await DisplayAlert("User Creation", "Account Created!", "OK");
            await Navigation.PushAsync(new Login());


        }
    }
}