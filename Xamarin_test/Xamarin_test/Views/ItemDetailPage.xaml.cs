using System.ComponentModel;
using Xamarin.Forms;
using Xamarin_test.ViewModels;

namespace Xamarin_test.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}