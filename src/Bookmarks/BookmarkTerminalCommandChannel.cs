using Petecat.Console.Command;
using System;
using System.IO;
using System.Reflection;

namespace Bookmarks
{
    public class BookmarkTerminalCommandChannel : TerminalCommandChannelBase
    {
        public BookmarkTerminalCommandChannel(string bookmarkConfigFile)
        {
            BookmarkConfigFile = bookmarkConfigFile;

            try
            {
                if (File.Exists(bookmarkConfigFile))
                {
                    BookmarkPage = BookmarkUtility.GetBookmarkPage(bookmarkConfigFile);
                }
                else
                {
                    BookmarkPage = new BookmarkPage();
                }
            }
            catch (Exception)
            {
                throw new FormatException(bookmarkConfigFile);
            }
        }

        public BookmarkPage BookmarkPage { get; private set; }

        public string BookmarkConfigFile { get; private set; }

        public void Consume(string commandText)
        {
            var terminalCommand = TerminalCommandUtility.Create(Assembly.GetExecutingAssembly(), commandText);
            if (terminalCommand != null)
            {
                Consume(terminalCommand);
            }
        }
    }
}