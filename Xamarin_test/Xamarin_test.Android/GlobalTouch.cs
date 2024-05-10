using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin_test.Services;

namespace Xamarin_test.Droid
{
    public class GlobalTouch : IGlobalTouch
    {
        public void Subscribe(EventHandler handler) =>
            (Platform.CurrentActivity as MainActivity).globalTouchHandler += handler;

        public void Unsubscribe(EventHandler handler) =>
            (Platform.CurrentActivity as MainActivity).globalTouchHandler -= handler;

        public Point GetPosition(VisualElement element)
        {
            var d = DeviceDisplay.MainDisplayInfo.Density;
            var view = Xamarin.Forms.Platform.Android.Platform.GetRenderer(element).View;
            return new Point(view.GetX() / d, view.GetY() / d);
        }

        public double GetSafeAreaBottom() => 0;

        public double GetSafeAreaTop() => 0;

        public int GetNavBarHeight()
        {
            var d = DeviceDisplay.MainDisplayInfo.Density;
            int statusBarHeight = -1;
            int resourceId = Platform.CurrentActivity.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Platform.CurrentActivity.Resources.GetDimensionPixelSize(resourceId);
            }
            return (int)(statusBarHeight / d);
        }
    }
}