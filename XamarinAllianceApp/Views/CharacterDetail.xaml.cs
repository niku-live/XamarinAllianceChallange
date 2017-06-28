using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterDetail : ContentPage
	{
        private Character item;

        public CharacterDetail() : this(null) { }

        public CharacterDetail(Character item)
        {
            this.item = item;
            InitializeComponent();
            BindingContext = new CharacterDetailViewModel(item);
        }

        class CharacterDetailViewModel : INotifyPropertyChanged
        {
            public Character Item { get; set; }

            public CharacterDetailViewModel(Character item)
            {
                Item = item;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

    }
}