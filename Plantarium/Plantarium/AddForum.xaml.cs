using Plugin.Media;
using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DotNetService;
using Plugin.Media.Abstractions;

namespace Plantarium
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddForum : ContentPage
    {
         private byte[] Image;
        private readonly string User;

        public AddForum(string Username)
        {
            InitializeComponent();
            User = Username;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
              await  DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            Service1Client srvc = new Service1Client();
            


            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name =  "test" + HeadlineEntry.Text.Replace(" ","_") + ".png",
                 SaveToAlbum = true,
                CompressionQuality = 50
            });

            if (file == null)
                return;


            string saxon = file.Path;
            
            MemoryStream ms = new MemoryStream();
            CopyStream(file.GetStream(), ms);

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                Image = memoryStream.ToArray();
            }


          







        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        private async void UploadButton_Clicked(object sender, EventArgs e)
        {

            Service1Client srvc = new Service1Client();
            //Code Disabled for testing purposes!
            // int spc = srvc.GetSpamCounter(User);
            //if (spc == 2)
            //{
            //    await DisplayAlert("Forum", "You have exceeded your daily amount of posts!", "OK");
            //    return;
            //}
            //else 
            //{
            //    srvc.UpdateSpamCounter(User, spc + 1);
            
            
            //}


            if(Image == null)
            {
                srvc.InsertForum(User, HeadlineEntry.Text, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("hh:mm tt"), ForumContentEditor.Text);
                
            }
            else
            {
                srvc.InsertForum(User, HeadlineEntry.Text, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("hh:mm tt"), ForumContentEditor.Text);
                srvc.InsertPhoto(Image, User, HeadlineEntry.Text);

            }
            await DisplayAlert("Forum", "Forum Created!", "OK");
            await Navigation.PushAsync(new Forum(User));

        }

        public byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }




    }
}