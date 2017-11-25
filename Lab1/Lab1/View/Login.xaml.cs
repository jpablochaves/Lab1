using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Lab1.ViewModel;

namespace Lab1.View
{
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
            // Bindear el ViewModel con la capa View (Unir el Modelo con el UI o View)
            BindingContext = new LoginViewModel();
        }
	}
}
