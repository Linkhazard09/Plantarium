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
    public partial class Login : ContentPage
    {
        IService1 Ws = new DotNetService.Service1Client();
        public Login()
        {
            InitializeComponent();
            this.Title = "Plantarium";
          


        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string Username = EntryUsername.Text;
            string Password = EntryPassword.Text;
            
            if (Ws.AccountAuthenticate(Username, Password) == true)
            {
                await Navigation.PushAsync(new MainPage());
            }
           else
              await DisplayAlert("Incorrect", "Account does not exist", "OK");


        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
           
        }
    }
}