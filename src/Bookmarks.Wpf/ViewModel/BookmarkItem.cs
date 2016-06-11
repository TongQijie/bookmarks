using System.Collections.ObjectModel;
using System.Linq;

namespace Bookmarks.Wpf.ViewModel
{
    public class BookmarkItem : Utility.NotifyPropertyChangedImpl, ITreeViewNode
    {
        public BookmarkItem(Bookmarks.BookmarkItem bookmarkItem, int level)
        {
            Level = level;
            Core = bookmarkItem;

            if (bookmarkItem.ChildItems != null)
            {
                foreach (var childItem in bookmarkItem.ChildItems)
                {
                    ChildItems.Add(new BookmarkItem(childItem, Level + 1));
                }
            }
        }

        public int Level { get; private set; }

        private bool _IsSelected = false;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    FirePropertiesChanged("IsSelected");
                }
            }
        }

        public Bookmarks.BookmarkItem Core { get; private set; }

        public string Catalog { get { return Core.Catalog; } }

        public int Index { get { return Core.Index; } }

        public string Description { get { return Core.Description; } }

        public string Fullname
        {
            get
            {
                if (Core.Locations != null && Core.Locations.Count > 0)
                {
                    return Core.Locations.FirstOrDefault().FileLocation;
                }
                else
                {
                    return "";
                }
            }
        }

        public string LocateText
        {
            get
            {
                if (Core.Locations != null && Core.Locations.Count > 0)
                {
                    return Core.Locations.FirstOrDefault().LocateLineText;
                }
                else
                {
                    return "";
                }
            }
        }

        public int LocateLine
        {
            get
            {
                if (Core.Locations != null && Core.Locations.Count > 0)
                {
                    return Core.Locations.FirstOrDefault().LocateLineNumber;
                }
                else
                {
                    return 0;
                }
            }
        }

        private ObservableCollection<BookmarkItem> _ChildItems = null;

        public ObservableCollection<BookmarkItem> ChildItems { get { return _ChildItems ?? (_ChildItems = new ObservableCollection<BookmarkItem>()); } }
    }
}
