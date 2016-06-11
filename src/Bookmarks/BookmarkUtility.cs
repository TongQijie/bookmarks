using System.Text;

using Petecat.Data.Xml;
using Petecat.Console.Command;
using System;

namespace Bookmarks
{
    public static class BookmarkUtility
    {
        public static BookmarkPage GetBookmarkPage(string bookmarkConfigFile)
        {
            return Serializer.ReadObject<BookmarkPage>(bookmarkConfigFile, Encoding.UTF8);
        }

        public static void SetBookmarkPage(BookmarkPage bookmarkPage, string bookmarkConfigFile)
        {
            Serializer.WriteObject(bookmarkPage, bookmarkConfigFile, Encoding.UTF8);
        }

        public static void SetBookmark(BookmarkPage bookmarkPage, BookmarkItem addedBookmarkItem)
        {
            bookmarkPage.RootItems.Add(addedBookmarkItem);
        }

        public static void RemoveBookmark(BookmarkPage bookmarkPage, BookmarkItem removedBookmarkItem)
        {
            bookmarkPage.RootItems.Remove(removedBookmarkItem);
        }

        public static void SetBookmark(BookmarkItem bookmarkItem, BookmarkItem addedBookmarkItem)
        {
            bookmarkItem.ChildItems.Add(addedBookmarkItem);
        }

        public static void RemoveBookmark(BookmarkItem bookmarkItem, BookmarkItem removedBookmarkItem)
        {
            bookmarkItem.ChildItems.Remove(removedBookmarkItem);
        }
    }
}