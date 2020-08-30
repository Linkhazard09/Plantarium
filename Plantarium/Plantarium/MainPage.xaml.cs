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
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
           
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMasterMenuItem;
            if (item == null)
                return;

          switch(item.Title)
            {
                case "Logout":
                    await Navigation.PushAsync(new Login());
                    break;
                case "Forums":
                    await Navigation.PushAsync(new Forum());
                    break;
                case "Guides":
                    await Navigation.PushAsync(new Guides());
                    break;



            }

            MasterPage.ListView.SelectedItem = null;
        }
    }
}