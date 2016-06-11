namespace Bookmarks.Wpf.Controller
{
    public class BookmarkController : Utility.NotifyPropertyChangedImpl
    {
        public BookmarkController(string fullname)
        {
            Fullname = fullname;
        }

        public string Fullname { get; set; }

        private ViewModel.BookmarkPage _BookmarkPage = null;

        public ViewModel.BookmarkPage BookmarkPage
        {
            get { return _BookmarkPage; }
            set
            {
                if (_BookmarkPage != value)
                {
                    _BookmarkPage = value;
                    FirePropertiesChanged("BookmarkPage");
                }
            }
        }

        public BookmarkController Load()
        {
            var bookmarkPage = Bookmarks.BookmarkUtility.GetBookmarkPage(Fullname);
            if (bookmarkPage == null)
            {
                bookmarkPage = new BookmarkPage();
            }

            BookmarkPage = new ViewModel.BookmarkPage(bookmarkPage);

            return this;
        }

        public BookmarkController Save()
        {
            Bookmarks.BookmarkUtility.SetBookmarkPage(BookmarkPage.Core, Fullname);
            return Load();
        }

        public ViewModel.BookmarkItem SelectedBookmarkItem { get; set; }
    }
}
