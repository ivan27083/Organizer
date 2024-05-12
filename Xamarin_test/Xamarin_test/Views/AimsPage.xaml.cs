using SkiaSharp.Views.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_test.ViewModels;
using Xamarin_test.Classes;
using Xamarin_test.Models;
using Xamarin.Essentials;
using Xamarin_test.Services;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Xamarin_test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AimsPage : ContentPage
    {
        bool showFill = true;
        bool no_aims = false;
        AimsViewModel _viewModel;
        public List<Circle> circles = new List<Circle>();
        private IGlobalTouch service = DependencyService.Get<IGlobalTouch>();
        public Command RefreshCommand { get; private set; }
        public AimsPage()
        {
            InitializeComponent();
            BindingContext = new AimsViewModel();
            _viewModel = BindingContext as AimsViewModel;
            frame_w = xamarinWidth;
            frame_h = xamarinHeight / 2;
            RefreshCommand = new Command(ExecuteRefreshCommand);
        }

        static DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        static double xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        static double xamarinHeight = mainDisplayInfo.Height / mainDisplayInfo.Density;
        

        public double frame_w { get; set; } = xamarinWidth;
        public double frame_h { get; set; } = xamarinHeight / 2;

        public void ExecuteRefreshCommand()
        {
            canvasView.InvalidateSurface();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            (BindingContext as AimsViewModel).OnCanvasViewPaintSurface(sender, args);
        }
        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    // координаты можно считать из события
                    // это свойства (e.Location.X, e.Location.Y);
                    // ваш код
                    
                    break;
                case SKTouchAction.Moved:
                    break;
                case SKTouchAction.Released:
                    break;
                case SKTouchAction.Cancelled:
                    break;
            }

            // запрашиваем перерисовку
            if (e.InContact)
                ((SKCanvasView)sender).InvalidateSurface();

            // событие обработано
            e.Handled = true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}