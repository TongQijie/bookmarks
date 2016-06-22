using System;
using System.IO;
using System.Reflection;
using System.Text;

using Petecat.Console.Command;
using Petecat.Logging;
using Petecat.Data.Formatters;

namespace Bookmarks
{
    public class BookmarkTerminalCommandChannel : TerminalCommandChannelBase
    {
        public BookmarkTerminalCommandChannel(string bookmarkConfigFile)
        {
            BookmarkConfigFile = bookmarkConfigFile;

            if (File.Exists(bookmarkConfigFile))
            {
                BookmarkPage = new XmlFormatter().ReadObject<BookmarkPage>(bookmarkConfigFile, Encoding.UTF8);
            }
            else
            {
                BookmarkPage = new BookmarkPage();
            }
        }

        public BookmarkPage BookmarkPage { get; private set; }

        public string BookmarkConfigFile { get; private set; }

        public void Consume(string commandText)
        {
            try
            {
                var terminalCommand = TerminalCommandUtility.Create(Assembly.GetExecutingAssembly(), commandText);
                if (terminalCommand != null)
                {
                    Consume(terminalCommand);
                }
            }
            catch (Exception e)
            {
                BookmarkTerminalCommandChannelManager.Instance.LogEvent(LoggerLevel.Error, e);
            }
        }
    }
}