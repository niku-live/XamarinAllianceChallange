using XamarinAllianceApp.ViewModel;

namespace XamarinAllianceApp.Design
{
    public static class ViewModelLocator
    {
        static MainViewModel mainVM;
        public static MainViewModel MainModel => mainVM ?? (mainVM = new MainViewModel());
    }
}
