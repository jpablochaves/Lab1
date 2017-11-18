﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lab1.ViewModel;

namespace Lab1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormPage : ContentPage
	{
		public FormPage ()
		{
			InitializeComponent ();
            BindingContext = PersonViewModel.GetInstance();
		}
	}
}
