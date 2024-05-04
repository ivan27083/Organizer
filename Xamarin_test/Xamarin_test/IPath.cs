using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
