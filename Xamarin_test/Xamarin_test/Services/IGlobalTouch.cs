using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin_test.Services
{
    public interface IGlobalTouch
    {
        void Subscribe(EventHandler handler);
        void Unsubscribe(EventHandler handler);
        Point GetPosition(VisualElement element);
        double GetSafeAreaTop();
        double GetSafeAreaBottom();
        int GetNavBarHeight();
    }
}
