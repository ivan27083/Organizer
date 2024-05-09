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

namespace Xamarin_test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AimsPage : ContentPage
    {
        bool showFill = true;
        AimsViewModel _viewModel;
        public AimsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AimsViewModel();
        }
        void OnCanvasViewTapped(object sender, EventArgs args)
        {
            showFill ^= true;
            (sender as SKCanvasView).InvalidateSurface();
        }

        /*public int Row { get; set; }
        public int ColumnSpan { get; set; }
        public int Column { get; set; }*/

        static DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        static double xamarinWidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
        static double xamarinHeight = mainDisplayInfo.Height / mainDisplayInfo.Density;

        public double frame_w { get; set; } = xamarinWidth;
        public double frame_h { get; set; } = 3 * xamarinHeight / 5;
        

        void children_values(int i, ref float x, ref float y, ref float radius1, ref float radius2, float centerX, float centerY, int kolvo)
        {
            //child
            radius1 = (float)(Math.Min(frame_w, frame_h) / (kolvo * 2));
            radius2 = radius1;
            double radius = Math.Min(frame_w, frame_h) / 4;
            double angle = i * Math.PI / 4;
            x = (float)(centerX + radius * Math.Cos(angle));
            y = (float)(centerY - radius * Math.Sin(angle));
        }

        void values(int var, float info_Width, float info_Height, ref float x, ref float y, ref float radius1, ref float radius2,int kolvo)
        {
            if (var == 1)//current
            {
                x = (float)(frame_w / 2 - info_Width / 2);
                y = (float)(frame_h / 2 - info_Height / 2);
                radius1 = 50; radius2 = 50;
            }

            if (var == 2)//parent
            {
                x = (float)(frame_w / 2 - info_Width / 2);
                y = (float)(frame_h / 2 - info_Height / 2 + frame_h / 5);
                radius1 = 30; radius2 = 30;
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            SKColor stroke = new SKColor(20, 74, 77);
            SKColor inside = new SKColor(52, 198, 205);
            int counter = 0;
            float line_x_start = 0, line_x_end = 0, line_y_start = 0, line_y_end = 0;
            canvas.Clear();
            while (true)
            {
                int var = -1;
                
                int kolvo = 0;
                /*float w = info.Width;
                float h = info.Height;*/

                float centerX = 0, centerY = 0;
                float x = 0, y = 0;
                float radius1 = 0, radius2 = 0;

                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = stroke,
                    StrokeWidth = 10
                };

                SKPaint paint_line = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = stroke,
                    StrokeWidth = 2
                };


                if (_viewModel.current != null)
                {
                    if (counter == 0) var = 1;
                    if (_viewModel.current.parent != null && counter == 1) var = 2;

                    foreach (var child in _viewModel.current.children)
                    {
                        if (child != null) var = 3;
                        kolvo += 1;
                    }
                }
                else break;

                if (var == 1 || var == 2)
                {
                    values(var, info.Width, info.Height, ref x, ref y, ref radius1, ref radius2, kolvo);

                    if (var == 1) { centerX =x ; centerY =y; line_x_start = x;line_y_start = y; }
                    if(var ==2) { line_x_end = x; line_y_end = y; }
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
                        children_values(k, ref x, ref y, ref radius1,ref radius2, centerX, centerY,kolvo);
                        line_x_end = x; line_y_end = y;
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
            }
        }
    }
}