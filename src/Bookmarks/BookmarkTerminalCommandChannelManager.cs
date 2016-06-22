using System;
using System.Collections.Generic;

using Petecat.Logging;

namespace Bookmarks
{
    public class BookmarkTerminalCommandChannelManager
    {
        private static BookmarkTerminalCommandChannelManager _Instance = null;

        public static BookmarkTerminalCommandChannelManager Instance { get { return _Instance ?? (_Instance = new BookmarkTerminalCommandChannelManager()); } }

        private BookmarkTerminalCommandChannelManager()
        {
            Loggers = new List<string>();
        }

        private string _LogCategory = "Bookmarks";

        public string LogCategory { get { return _LogCategory; } set { _LogCategory = value; } }

        public List<string> Loggers { get; private set; }

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
            catch (Exception e)
            {
                LogEvent(LoggerLevel.Fatal, e);
            }

            return null;
        }

        public void LogEvent(LoggerLevel loggerLevel, params object[] parameters)
        {
            LoggerManager.GetLoggers(Loggers.ToArray()).LogEvent(Instance.LogCategory, loggerLevel, parameters);
        }
    }
}
