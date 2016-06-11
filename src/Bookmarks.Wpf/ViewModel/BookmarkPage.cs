using System.Collections.ObjectModel;

namespace Bookmarks.Wpf.ViewModel
{
    public class BookmarkPage
    {
        public BookmarkPage(Bookmarks.BookmarkPage bookmarkPage)
        {
            Core = bookmarkPage;

            if (bookmarkPage.RootItems != null)
            {
                foreach (var rootItem in bookmarkPage.RootItems)
                {
                    RootItems.Add(new BookmarkItem(rootItem, 0));
                }
            }
        }

        public Bookmarks.BookmarkPage Core { get; private set; }

        private ObservableCollection<BookmarkItem> _RootItems = null;

        public ObservableCollection<BookmarkItem> RootItems { get { return _RootItems ?? (_RootItems = new ObservableCollection<BookmarkItem>()); } }
    }
}