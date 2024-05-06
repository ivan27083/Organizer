﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.ViewModels;

namespace Xamarin_test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewDailyPage : ContentPage
	{
		public NewDailyPage ()
		{
			InitializeComponent ();
            BindingContext = new NewDailyViewModel();
        }
	}
}