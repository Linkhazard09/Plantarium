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
    public partial class Guides_Content : ContentPage
    {
        public Guides_Content(string Guide_Name, string Plant_Name)
        {
            IService1 Ws = new DotNetService.Service1Client();
            InitializeComponent();
            string Guide_Content, Video_URL;
            Service1Client srvc = new Service1Client();
            Guide_Content=srvc.GuidesGetContent(Guide_Name, Plant_Name, out Video_URL);

            Guide_NameLabel.Text = Guide_Name;
            Plant_NameLabel.Text = Plant_Name;
            Guide_ContentLabel.Text = Guide_Content;
           

            Guide_Video.Source = Video_URL;
           






        }

        private async void Feedback_Button_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Feedback());




        }
    }
}