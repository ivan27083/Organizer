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
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace Xamarin_test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AimsPage : ContentPage
    {
        public IDataStore<Purpose> DataStoreAims => DependencyService.Get<IDataStore<Purpose>>();
        public IDataStore<Mission> DataStoreMissions => DependencyService.Get<IDataStore<Mission>>();
        bool showFill = true;
        bool no_aims = false;
        AimsViewModel _viewModel;
        public List<Circle> circles = new List<Circle>();
        private IGlobalTouch service = DependencyService.Get<IGlobalTouch>();
        private string text;
        private string description;
        public Node<abstract_Item> root;
        public static Node<abstract_Item> current;
        public Command RefreshCommand { get; private set; }
        public AimsPage()
        {
            InitializeComponent();
            BindingContext = new AimsViewModel();
            _viewModel = BindingContext as AimsViewModel;
            frame_w = xamarinWidth;
            frame_h = xamarinHeight / 2;
            fillTree();
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
        public string Text
        {
            get => text;
            set => text = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
        public void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (current != null)
            {
                Text = current.data.Text;
                Description = current.data.Description;
            }
            SKColor color = new SKColor(255,255,255,0);
            args.Surface.Canvas.Clear(color);

            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var Width = info.Width;
            var Height = info.Height;
            CreateCircles(Width, Height);

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
            
            if (circles.Count > 0)
            {
                Circle main = circles.Find(c => c.Type == 0);
                for (int i = 0; i < circles.Count(); i++)
                {
                    canvas.DrawCircle(circles[i].x, circles[i].y, circles[i].Radius, paint);
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = inside;
                    canvas.DrawCircle(circles[i].x , circles[i].y, circles[i].Radius, paint);

                    if (circles[i].Type != 0)
                        canvas.DrawLine(main.x , main.y, circles[i].x, circles[i].y, paint_line);
                }
            }
            else
            {
                float x = AimsViewModel.plus.x,
                    y = AimsViewModel.plus.y,
                    r = AimsViewModel.plus.Radius;
                canvas.DrawCircle(x, y, r, paint);
                canvas.DrawLine(x - r, y, x + r, y, paint_line);
                canvas.DrawLine(x, y - r, x, y + r, paint_line);
            }
        }
        public void CreateCircles(float xamarinWidth, float xamarinHeight)
        {
            circles.Clear();
            //var d = (float)DeviceDisplay.MainDisplayInfo.Density;
            if (current != null)
            {
                //AimsViewModel.Text = current.data.Text;
                //AimsViewModel.Description = current.data.Description;
                current.circle = new Circle
                {
                    x = (float)xamarinWidth / 2,
                    y = (float)xamarinHeight / 2,
                    Radius = 80,
                    Type = 0
                };
                circles.Add(current.circle);
                if (current.parent != null)
                {
                    current.parent.circle = new Circle
                    {
                        x = (float)xamarinWidth / 2,
                        y = (float)(xamarinHeight / 2 + xamarinHeight / 5),
                        Radius = 50,
                        Type = 1
                    };
                    circles.Add(current.parent.circle);
                }

                float circleRadius = (float)(50 - (current.children.Count * 2));
                float totalWidth = circleRadius * 2 * current.children.Count + (current.children.Count - 1) * circleRadius;
                float centerX = xamarinWidth / 2f;
                float startY = (float)(current.circle.y/2.5);
                for (int i = 0; i < current.children.Count; i++)
                {
                    Node<abstract_Item> child = current.children[i];

                    float circleX = centerX - totalWidth / 2f + circleRadius + i * circleRadius * 3;
                    child.circle = new Circle
                    {
                        Radius = circleRadius,
                        x = circleX,
                        y = startY,
                        Type = 2
                    };

                    circles.Add(child.circle);
                }
                /*const float circleSpacing = 2f; // adjust this value to change the spacing between circles
                const float circleRadiusMultiplier = 3f; // adjust this value to change the circle radius

                float circleRadius = (float)(50 - (current.children.Count * circleSpacing));
                float totalWidth = circleRadius * 2 * current.children.Count + (current.children.Count - 1) * circleRadius;
                float centerX = xamarinWidth / 2f;
                float startY = (float)(current.circle.y - xamarinWidth / circleRadiusMultiplier);

                for (int i = 0; i < current.children.Count; i++)
                {
                    Node<abstract_Item> child = current.children[i];

                    float circleX = centerX - totalWidth / 2f + circleRadius + i * circleRadius * 2;
                    child.circle = new Circle
                    {
                        Radius = circleRadius,
                        x = circleX,
                        y = startY,
                        Type = 2
                    };

                    circles.Add(child.circle);
                }
                double radius = Math.Min(xamarinWidth, xamarinHeight) / 4;
              double angleIncrement = Math.PI / (current.children.Count);

              // Определяем центральный угол для начала дуги
              double startAngle = Math.PI / 2;

              for (int i = 0; i < current.children.Count; i++)
              {
                  double angle = startAngle + i * angleIncrement;
                  Node<abstract_Item> child = current.children[i];

                  child.circle = new Circle
                  {
                      x = (float)(current.circle.x + radius * Math.Cos(angle)),
                      y = (float)(current.circle.y - radius * Math.Sin(angle)),
                      Radius = (float)(50 - (current.children.Count * 2)),
                      Type = 2
                  };

                  circles.Add(child.circle);
              }

              double radius = Math.Min(xamarinWidth, xamarinHeight) / 4;
              double angleIncrement = Math.PI / (current.children.Count);

              for (int i = 0; i < current.children.Count; i++)
              {
                  double angle = i * angleIncrement;
                  Node<abstract_Item> child = current.children[i];

                  child.circle = new Circle
                  {
                      x = (float)(current.circle.x + radius * Math.Cos(angle)),
                      y = (float)(current.circle.y - radius * Math.Sin(angle)),
                      Radius = (float)(50 - (current.children.Count * 2)),
                      Type = 2
                  };

                  circles.Add(child.circle);
              }
              foreach (Node<abstract_Item> child in current.children)
              {
                  double radius = Math.Min(xamarinWidth, xamarinHeight) / 4;
                  double angle = current.children.IndexOf(child) * Math.PI / 4;
                  child.circle = new Circle
                  {
                      x = (float)(current.circle.x + radius * Math.Cos(angle)),
                      y = (float)(current.circle.y + radius * Math.Sin(angle)),
                      Radius = (float)(50 - (current.children.Count * 2)),
                      Type = 2
                  };
                  circles.Add(child.circle);
              }*/
            }
            else
            {
                AimsViewModel.plus = new Circle
                {
                    x = (float)(xamarinWidth / 2),
                    y = (float)(xamarinHeight / 2),
                    Radius = 80
                };
            }
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    // координаты можно считать из события
                    // это свойства (e.Location.X, e.Location.Y);
                    // ваш код
                    if (current == null)
                    {
                        var plus = AimsViewModel.plus;
                        var viewX = plus.x;
                        var viewY = plus.y;
                        if (Math.Abs(e.Location.X - viewX) <= plus.Radius && Math.Abs(e.Location.Y - viewY) <= plus.Radius)
                        {
                            //locker = true;
                            Shell.Current.GoToAsync(nameof(NewAimPage));
                        }
                    }
                    else
                    {
                        var radius = current.circle.Radius;
                        var viewX = current.circle.x;
                        var viewY = current.circle.y;
                        if (Math.Abs(e.Location.X - viewX) <= radius && Math.Abs(e.Location.Y - viewY) <= radius)
                        {
                            //locker = true;
                            Shell.Current.GoToAsync($"{nameof(AimEditPage)}?{nameof(AimEditViewModel.ItemId)}={current.data.Id}");

                        }
                        if (current.parent != null)
                        {
                            radius = current.parent.circle.Radius;
                            viewX = current.parent.circle.x;
                            viewY = current.parent.circle.y;

                            if (Math.Abs(e.Location.X - viewX) <= radius && Math.Abs(e.Location.Y - viewY) <= radius)
                            {
                                current = current.parent;
                                if (current.data is Purpose purp)
                                {
                                    canvasView.InvalidateSurface();
                                }
                            }
                        }
                        foreach (var child in current.children.ToList())
                        {
                            radius = child.circle.Radius;
                            viewX = child.circle.x;
                            viewY = child.circle.y;
                            if (Math.Abs(e.Location.X - viewX) <= radius && Math.Abs(e.Location.Y - viewY) <= radius)
                            {
                                if (child.data is Purpose purp)
                                {
                                    current = child;
                                    canvasView.InvalidateSurface();
                                }
                                else if (child.data is Mission miss)
                                {
                                    Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={child.data.Id}");
                                }
                            }
                        }
                    }
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
        public async void fillTree()
        {
            List<Purpose> purposes = new List<Purpose>();
            var aims = await DataStoreAims.GetItemsAsync(true);
            foreach (var aim in aims)
            {
                purposes.Add(aim);
            }
            List<Mission> missions = new List<Mission>();
            var items = await DataStoreMissions.GetItemsAsync(true);
            foreach (var item in items)
            {
                if (item.Group != null) missions.Add(item);
            }
            if (purposes.Count != 0)
            {
                root = new Node<abstract_Item>(purposes.Find(p => p.Group == 0));
                current = root;
            }
            void fillPurposes(List<Purpose> purposes)
            {
                if (purposes.Count == 0) return;
                if (current != null)
                {
                    var el = purposes.Where(p => p.Group == current.data.Id).ToList();
                    if (el.Any())
                    {
                        foreach (var item in el)
                        {
                            current.AddChild(new Node<abstract_Item>(item));
                            purposes.Remove(item);
                            current = current.children.Last();
                            fillPurposes(purposes);
                        }
                        if (current.parent != null)
                            current = current.parent;
                    }
                    else
                    {
                        if (current.parent != null)
                            current = current.parent;
                    }
                }

            }
            fillPurposes(purposes);
            current = root;
            void fillMissions(List<Mission> missions)
            {
                if (current != null)
                {
                    var el = missions.Where(m => m.Group == current.data.Id).ToList();
                    if (el.Any())
                    {
                        foreach (var item in el.ToList())
                        {
                            current.AddChild(new Node<abstract_Item>(item));
                            missions.Remove(item);
                        }
                        if (current.parent != null)
                            current = current.parent;

                    }
                    else
                    {
                        if (current.children.Count != 0)
                            foreach (Node<abstract_Item> item in current.children.ToList())
                            {
                                Node<abstract_Item> temp = item;
                                current = temp;
                                fillMissions(missions);
                            }
                    }
                }
            }
            fillMissions(missions);
            current = root;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}