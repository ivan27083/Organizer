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
       // SKCanvas Canvas;
        public AimsPage()
        {
            InitializeComponent();
            BindingContext = new AimsViewModel();
            _viewModel = BindingContext as AimsViewModel;
            frame_w = xamarinWidth;
            frame_h = xamarinHeight / 2;
        }

        /*public int Row { get; set; }
        public int ColumnSpan { get; set; }
        public int Column { get; set; }*/

        static DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        static double xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        static double xamarinHeight = mainDisplayInfo.Height / mainDisplayInfo.Density;
        

        public double frame_w { get; set; } = xamarinWidth;
        public double frame_h { get; set; } = xamarinHeight / 2;
        
        void refresh()
        {
            //CanvasView.InvalidateSurface();
        }

        /*void children_values(int i, float info_Width, float info_Height, ref float x, ref float y, ref float radius1, ref float radius2, float centerX, float centerY, int kolvo)
        {
            //child
            radius1 = (float)(Math.Min(info_Width, info_Height) / (kolvo * 2));
            radius2 = radius1;
            double radius = Math.Min(info_Width, info_Height) / 4;
            double angle = i * Math.PI / 4;
            x = (float)(centerX + radius * Math.Cos(angle));
            y = (float)(centerY - radius * Math.Sin(angle));
        }

        void values(int var, float info_Width, float info_Height, ref float x, ref float y, ref float radius1, ref float radius2, int kolvo)
        {
            if (var == 0)//current
            {
                x = (float)(info_Width / 2);
                y = (float)(info_Height / 2);
                radius1 = 80; radius2 = 80;
            }

            if (var == 1)//current
            {
                x = (float)(info_Width / 2);
                y = (float)(info_Height / 2);
                radius1 = 50; radius2 = 50;
            }

            if (var == 2)//parent
            {
                x = (float)(info_Width / 2);
                y = (float)(info_Height / 2 + info_Height / 5);
                radius1 = 30; radius2 = 30;
            }
        }*/

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            float d = (float)mainDisplayInfo.Density;
            float x0 = 8,
                  y0 = 160 + service.GetNavBarHeight()*d + 18 * d;

            SKColor stroke = new SKColor(20, 74, 77);
            SKColor inside = new SKColor(52, 198, 205);
            
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = stroke,
                StrokeWidth = 10
            };

            SKPaint paint_line = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = inside,
                StrokeWidth = 4
            };
            //float x_centre=0, y_centre=0;

            if (_viewModel.circles.Count > 0)
            {
                Circle main = _viewModel.circles.Find(c => c.Type == 0);
                for (int i = 0; i < _viewModel.circles.Count(); i++)
                {
                    canvas.DrawCircle(_viewModel.circles[i].x * d - x0, _viewModel.circles[i].y * d - y0, _viewModel.circles[i].Radius * d, paint);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = inside;
                    canvas.DrawCircle(_viewModel.circles[i].x * d - x0, _viewModel.circles[i].y * d - y0, _viewModel.circles[i].Radius * d, paint);

                    if (_viewModel.circles[i].Type != 0)
                        canvas.DrawLine(main.x * d- x0, main.y * d - y0, _viewModel.circles[i].x * d - x0, _viewModel.circles[i].y * d - y0, paint_line);
                }
            }
            else
            {
                float x = _viewModel.plus.x * d - x0,
                    y = _viewModel.plus.y * d - y0,
                    r = _viewModel.plus.Radius * d;
                canvas.DrawCircle(x, y, r, paint);
                canvas.DrawLine(x - r, y, x + r, y, paint_line);
                canvas.DrawLine(x, y - r, x, y + r, paint_line);
            }


            /*int counter = 0;
            float line_x_start = 0, line_x_end = 0, line_y_start = 0, line_y_end = 0;
            canvas.Clear();
            while (true)
            {
                int var = -1;
                
                int kolvo = 0;
                /*float w = info.Width;
                float h = info.Height;

                float centerX = 0, centerY = 0;
                float x = 0, y = 0;
                float radius1 = 0, radius2 = 0;

                


                if (_viewModel.current != null)
                {
                    no_aims = false;
                    if (counter == 0) var = 1;
                    if (_viewModel.current.parent != null && counter == 1) var = 2;

                    foreach (var child in _viewModel.current.children)
                    {
                        if (child != null) var = 3;
                        kolvo += 1;
                    }
                }
                else //плюсик
                {
                    Circle c = new Circle();
                    no_aims = true;
                    var = 0;
                    paint.Color = inside;
                    values(var, info.Width, info.Height, ref x, ref y, ref radius1, ref radius2, kolvo);
                    c.Radius = radius1;
                    c.x = x; c.y = y;
                    circles.Add(c);
                    canvas.DrawCircle(x, y, radius1, paint);
                    line_x_start = x - radius1; line_y_start = y; line_x_end = x + radius1; line_y_end = y;
                    canvas.DrawLine(line_x_start, line_y_start, line_x_end, line_y_end, paint_line);
                    line_x_start = x; line_y_start = y-radius1; line_x_end = x; line_y_end = y+radius1;
                    canvas.DrawLine(line_x_start, line_y_start, line_x_end, line_y_end, paint_line);
                    break;
                }
                    

                if (var == 1 || var == 2)
                {
                    values(var, info.Width, info.Height, ref x, ref y, ref radius1, ref radius2, kolvo);
                    Circle c = new Circle();
                    if (var == 1) { centerX =x ; centerY =y; line_x_start = x;line_y_start = y; c.Id = _viewModel.current.data.Id; }
                    if (var ==2) { line_x_end = x; line_y_end = y; c.Id = _viewModel.current.parent.data.Id; }
                    
                    c.Radius = radius1;
                    c.x = x;
                    c.y = y;
                    
                    circles.Add(c);
                    canvas.DrawCircle(x, y, radius1, paint);
                    if (showFill)
                    {
                        paint.Style = SKPaintStyle.Fill;
                        paint.Color = inside;
                        canvas.DrawCircle(x, y, radius2, paint);
                        //canvas.DrawCircle(info.Width / 2, info.Height / 2, 50, paint);
                    }
                    canvas.DrawLine(line_x_start, line_y_start, line_x_end, line_y_end, paint_line);
                }
                counter++;

                if (var == 3)
                {

                    int k = 0;
                    while (k!=kolvo)
                    {
                        Circle c = new Circle();
                        c.Id = _viewModel.current.children[k].data.Id;
                        children_values(k, info.Width, info.Height, ref x, ref y, ref radius1,ref radius2, centerX, centerY,kolvo);
                        line_x_end = x; line_y_end = y;
                        c.Radius = radius1;
                        c.x = x;
                        c.y = y;
                        circles.Add(c);
                        canvas.DrawCircle(x, y, radius1, paint);
                        if (showFill)
                        {
                            paint.Style = SKPaintStyle.Fill;
                            paint.Color = inside;
                            canvas.DrawCircle(x, y, radius2, paint);
                        }
                        canvas.DrawLine(line_x_start, line_y_start, line_x_end, line_y_end, paint_line);
                        k++;
                    }
                    break;
                }
                if (var == -1) break;
            }*/

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}