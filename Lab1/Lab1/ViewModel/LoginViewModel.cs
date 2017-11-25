using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Lab1.View;

namespace Lab1.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Instances
        public ICommand LoginCommand { get; set; }

        #endregion Instances

        public LoginViewModel() {
            InitClass();
            InitCommands();
        }

        private void InitClass() {
            Console.WriteLine("INIT CLASS LoginViewModel");
        }

        private void InitCommands() {
            LoginCommand = new Command(Login);
        }

        private async void Login() {
            //  Console.WriteLine("BOTON LOGIN! ");
            NavigationPage nav = new NavigationPage(new MainPage());
            App.Current.MainPage = new MasterDetailPage
            {
                Master = new HomeMenu(),
                Detail = nav
            };
        }

       

        #region Notified Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                // Indica que algo en View cambio 
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion


    }
}
