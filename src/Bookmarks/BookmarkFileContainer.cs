using System.IO;
using System.Text;

using Petecat.Data.Formatters;

namespace Bookmarks
{
    public class BookmarkFileContainer : IBookmarkContainer
    {
        public BookmarkFileContainer(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }

        private BookmarkPage _CurrentPage = null;

        public BookmarkPage CurrentPage
        {
            get
            {
                if (_CurrentPage == null)
                {
                    Get();
                }
                return _CurrentPage;
            }
            set { _CurrentPage = value; }
        }

        private object _Locker = new object();

        public void Get()
        {
            lock (_Locker)
            {
                if (File.Exists(Path))
                {
                    CurrentPage = new XmlFormatter().ReadObject<BookmarkPage>(Path, Encoding.UTF8);
                }
                else
                {
                    CurrentPage = new BookmarkPage();
                }
            }
        }

        public void Set()
        {
            lock (_Locker)
            {
                new XmlFormatter().WriteObject(CurrentPage, Path, Encoding.UTF8);
            }
        }
    }
}
