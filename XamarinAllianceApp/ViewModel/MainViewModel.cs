using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAllianceApp.Models;

namespace XamarinAllianceApp.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Character> Characters
        {
            get
            {
                return new ObservableCollection<Character>()
                {
                    new Character() { Name =  "Test", Id = 0, Gender = "Male", Biography = "Long long ago", Height = 166, Version = "1" },
                    new Character() { Name =  "Test2", Id = 2, Gender = "Female", Biography = "Long long ago 2", Height = 166, Version = "1"}
                };
            }
        }
    }
}
