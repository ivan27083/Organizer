using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using System.IO;
using Xamarin_test.iOS;

[assembly: Dependency(typeof(IosDbPath))]
namespace Xamarin_test.iOS
{
    public class IosDbPath : Xamarin_test.IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}