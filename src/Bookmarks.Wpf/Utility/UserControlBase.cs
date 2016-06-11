using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bookmarks.Wpf.Utility
{
    public abstract class UserControlBase : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertiesChanged(params string[] properties)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                foreach (string property in properties)
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(property));
                }
            }
        }
    }
}
