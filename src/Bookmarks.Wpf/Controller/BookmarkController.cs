namespace Bookmarks.Wpf.Controller
{
    public class BookmarkController : Utility.NotifyPropertyChangedImpl
    {
        public BookmarkController(string fullname)
        {
            Fullname = fullname;
            Channel = BookmarkTerminalCommandChannelManager.Instance.Load(fullname);
            Reload();
        }

        #region 数据通道

        public string Fullname { get; set; }

        public BookmarkTerminalCommandChannel Channel { get; private set; }

        #endregion

        #region 视图模型数据

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

        public ViewModel.BookmarkItem SelectedBookmarkItem { get; set; }

        #endregion

        public void Reload()
        {
            Channel.BookmarkContainer.Get();
            BookmarkPage = new ViewModel.BookmarkPage(Channel.BookmarkContainer.CurrentPage);
        }

        public void Save()
        {
            Channel.BookmarkContainer.Set();
        }

        #region 数据命令

        public void CreateBookmark(Bookmarks.BookmarkItem addedBookmark)
        {
            if (addedBookmark.Id == 0)
            {
                var commandText = string.Format("create -cat \"{0}\" -idx {1} -desc \"{2}\" -path \"{3}\" -text \"{4}\" -line {5}",
                    ConvertString(addedBookmark.Catalog),
                    addedBookmark.Index,
                    ConvertString(addedBookmark.Description),
                    ConvertString(addedBookmark.GetLocation().FileLocation),
                    ConvertString(addedBookmark.GetLocation().LocateLineText),
                    addedBookmark.GetLocation().LocateLineNumber);
                Channel.Consume(commandText);
            }
            else
            {
                var commandText = string.Format("create -id {0} -cat \"{1}\" -idx {2} -desc \"{3}\" -path \"{4}\" -text \"{5}\" -line {6}",
                    addedBookmark.Id,
                    ConvertString(addedBookmark.Catalog),
                    addedBookmark.Index,
                    ConvertString(addedBookmark.Description),
                    ConvertString(addedBookmark.GetLocation().FileLocation),
                    ConvertString(addedBookmark.GetLocation().LocateLineText),
                    addedBookmark.GetLocation().LocateLineNumber);
                Channel.Consume(commandText);
            }

            Reload();
        }

        public void DeleteBookmark(Bookmarks.BookmarkItem removedBookmark)
        {
            var commandText = string.Format("delete -id {0}", removedBookmark.Id);
            Channel.Consume(commandText);

            Reload();
        }

        public void UpdateBookmark(Bookmarks.BookmarkItem updatedBookmark)
        {
            var commandText = string.Format("create -id {0} -cat \"{1}\" -idx {2} -desc \"{3}\" -path \"{4}\" -text \"{5}\" -line {6}",
                updatedBookmark.Id,
                ConvertString(updatedBookmark.Catalog),
                updatedBookmark.Index,
                ConvertString(updatedBookmark.Description),
                ConvertString(updatedBookmark.GetLocation().FileLocation),
                ConvertString(updatedBookmark.GetLocation().LocateLineText),
                updatedBookmark.GetLocation().LocateLineNumber);
            Channel.Consume(commandText);

            Reload();
        }

        private string ConvertString(string rawString)
        {
            return rawString.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        #endregion
    }
}