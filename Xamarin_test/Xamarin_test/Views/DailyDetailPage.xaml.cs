using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin_test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DailyDetailPage : ContentPage
	{
		public DailyDetailPage ()
		{
			InitializeComponent ();
			BindingContext = new DailyDetailViewModel();
		}
	}
}