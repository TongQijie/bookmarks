using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarks.Wpf.ViewModel
{
    public interface ITreeViewNode
    {
        bool IsSelected { get; set; }

        int Level { get; }
    }
}
