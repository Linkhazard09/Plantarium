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
    public partial class Guides : ContentPage
    {

        public string Username;
        public IList<PlantGuides> PlantGuide { get; private set; }
        public Guides(string Username)
        {
            this.Username = Username;
            InitializeComponent();
            Task taskA = Task.Run(() => PopulateListView());
            taskA.Wait();


            GuideListView.ItemsSource = PlantGuide;

             
            
           
            BindingContext = this;

        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            PlantGuides tappedItem = e.Item as PlantGuides;
            string x = tappedItem.Guide_Name;
            string y = tappedItem.Plant_Name;

            await Navigation.PushAsync(new Guides_Content(x,y, Username));

        }

        public class PlantGuides
        {
            public string Guide_Name { get; set; }
            public string Plant_Name { get; set; }
            

            public override string ToString()
            {
                return Guide_Name;
            }
        }

        private void FilterButton_Clicked(object sender, EventArgs e)
        {
            List<string> GN = new List<string>();
            List<string> PN = new List<string>();
            string[] essex; ;
                 int x = 0;
                Service1Client srvc = new Service1Client();
                GN = srvc.GuideGetByPlant(PlantNamePicker.SelectedItem.ToString(), out essex).ToList();
                PN = essex.ToList();
                PlantGuide = new List<PlantGuides>();
                foreach (string s in GN)
                {
                    PlantGuide.Add(new PlantGuides { Guide_Name = GN[x], Plant_Name = PN[x] }); ;
                    x++;
                }
                GuideListView.ItemsSource = PlantGuide;
       
          


        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {

            Task taskA = Task.Run(() => PopulateListView());
            taskA.Wait();
            GuideListView.ItemsSource = PlantGuide;
        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(Username));
        }


        public void PopulateListView()
        {
            List<string> GN = new List<string>();
            List<string> PN = new List<string>();
            string[] essex; ;
            int x = 0;
            Service1Client srvc = new Service1Client();
            GN = srvc.GuidesGet(out essex).ToList();
            PN = essex.ToList();
            PlantNamePicker.ItemsSource = PN;
            PlantGuide = new List<PlantGuides>();
            foreach (string s in GN)
            {
                PlantGuide.Add(new PlantGuides { Guide_Name = GN[x], Plant_Name = PN[x] }); ;
                x++;
            }





        }



    }
}