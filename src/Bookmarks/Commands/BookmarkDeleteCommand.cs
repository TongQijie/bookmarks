using Petecat.Console.Command;
using System;

namespace Bookmarks.Commands
{
    [TerminalCommand(new string[] { "delete" })]
    public class BookmarkDeleteCommand : TerminalCommandBase
    {
        public BookmarkDeleteCommand(TerminalCommandLine terminalCommandLine)
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

                var bookmarkPage = bookmarkTerminalCommandChannel.BookmarkContainer.CurrentPage;

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

                var parentItem = bookmarkPage.GetParentItemById((int)Convert.ChangeType(TerminalCommandLine["id"], typeof(int)));
                if (parentItem == null)
                {
                    bookmarkPage.RootItems.Remove(olderBookmarkItem);
                }
                else
                {
                    parentItem.ChildItems.Remove(olderBookmarkItem);
                }

                bookmarkTerminalCommandChannel.BookmarkContainer.Set();
            }
        }
    }
}
