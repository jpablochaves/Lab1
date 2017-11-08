using Lab1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Lab1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            // Bindear el ViewModel con la capa View (Unir el Modelo con el UI o View)
            BindingContext = new PersonViewModel();
		}
	}
}
