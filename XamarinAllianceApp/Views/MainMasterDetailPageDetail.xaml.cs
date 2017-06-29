using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAllianceApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPageDetail : ContentPage
    {
        public MainMasterDetailPageDetail()
        {
            InitializeComponent();
        }

        private async Task Button_Clicked(object sender, EventArgs e)
        {
            var token = await Controllers.CharacterService.DefaultManager.GetToken();
            TokenLabel.Text = token;

            // Assign the Source property of your image            
            var stream = await Controllers.CharacterService.DefaultManager.DownloadPicture(token);
            ImagePlace.Source = ImageSource.FromStream(() => new MemoryStream(stream));
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var guid = await Controllers.CharacterService.DefaultManager.GetDiploma();
            DiplomaLabel.Text = guid;
        }
    }
}