using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmarks.Wpf.Utility
{
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
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
