using System;
using System.Collections.Generic;

namespace Bookmarks
{
    public class BookmarkTerminalCommandChannelManager
    {
        private static BookmarkTerminalCommandChannelManager _Instance = null;

        public static BookmarkTerminalCommandChannelManager Instance { get { return _Instance ?? (_Instance = new BookmarkTerminalCommandChannelManager()); } }

        private List<BookmarkTerminalCommandChannel> _Channels = null;

        public IList<BookmarkTerminalCommandChannel> Channels { get { return _Channels ?? (_Channels = new List<BookmarkTerminalCommandChannel>()); } }

        public BookmarkTerminalCommandChannel Load(string bookmarkConfigFile)
        {
            try
            {
                var bookmarkTerminalCommandChannel = new BookmarkTerminalCommandChannel(bookmarkConfigFile);
                Channels.Add(bookmarkTerminalCommandChannel);
                return bookmarkTerminalCommandChannel;
            }
            catch (Exception) { }

            return null;
        }
    }
}
