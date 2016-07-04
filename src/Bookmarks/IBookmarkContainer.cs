namespace Bookmarks
{
    public interface IBookmarkContainer
    {
        BookmarkPage CurrentPage { get; }

        void Get();

        void Set();
    }
}
