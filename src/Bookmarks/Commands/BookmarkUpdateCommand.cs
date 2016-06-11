using Petecat.Console.Command;
using System;

namespace Bookmarks.Commands
{
    [TerminalCommand(new string[] { "update" })]
    public class BookmarkUpdateCommand : TerminalCommandBase
    {
        public BookmarkUpdateCommand(TerminalCommandLine terminalCommandLine)
            : base(terminalCommandLine)
        {
        }

        public override void Execute(ITerminalCommandChannel terminalCommandChannel, System.Action<string> prompt)
        {
            if (terminalCommandChannel is BookmarkTerminalCommandChannel)
            {
                Handled = true;

                Result = "done.";

                var bookmarkTerminalCommandChannel = terminalCommandChannel as BookmarkTerminalCommandChannel;

                var bookmarkPage = bookmarkTerminalCommandChannel.BookmarkPage;

                BookmarkItem olderBookmarkItem = null;
                if (TerminalCommandLine.ContainKeys("id"))
                {
                    olderBookmarkItem = bookmarkPage.GetItemById((int)Convert.ChangeType(TerminalCommandLine["id"], typeof(int)));
                }

                if (olderBookmarkItem == null)
                {
                    prompt.Invoke("bookmark not found.");
                    return;
                }

                if (TerminalCommandLine.ContainKeys("cat"))
                {
                    olderBookmarkItem.Catalog = TerminalCommandLine["cat"];
                }

                if (TerminalCommandLine.ContainKeys("idx"))
                {
                    olderBookmarkItem.Index = (int)Convert.ChangeType(TerminalCommandLine["idx"], typeof(int));
                }

                if (TerminalCommandLine.ContainKeys("desc"))
                {
                    olderBookmarkItem.Description = TerminalCommandLine["desc"];
                }

                BookmarkUtility.SetBookmarkPage(bookmarkPage, bookmarkTerminalCommandChannel.BookmarkConfigFile);
            }
        }
    }
}
