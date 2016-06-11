using System;

using Petecat.Console.Command;

namespace Bookmarks.Commands
{
    [TerminalCommand(new string[] { "create" })]
    public class BookmarkCreateCommand : TerminalCommandBase
    {
        public BookmarkCreateCommand(TerminalCommandLine terminalCommandLine)
            : base(terminalCommandLine)
        {
        }

        public override void Execute(ITerminalCommandChannel terminalCommandChannel, Action<string> prompt)
        {
            if (terminalCommandChannel is BookmarkTerminalCommandChannel)
            {
                Handled = true;
                Result = "done.";

                var bookmarkTerminalCommandChannel = terminalCommandChannel as BookmarkTerminalCommandChannel;

                var bookmarkPage = bookmarkTerminalCommandChannel.BookmarkPage;

                BookmarkItem newerBookmarkItem = null;
                if (TerminalCommandLine.ContainKeys("cat", "idx", "desc", "path", "text", "line"))
                {
                    newerBookmarkItem = new BookmarkItem()
                    {
                        Id = bookmarkPage.GenerateItemId(),
                        Catalog = TerminalCommandLine["cat"],
                        Index = (int)Convert.ChangeType(TerminalCommandLine["idx"], typeof(int)),
                        Description = TerminalCommandLine["desc"],
                    };
                    newerBookmarkItem.Locations.Add(new BookmarkLocation()
                    {
                        FileLocation = TerminalCommandLine["path"],
                        LocateLineText = TerminalCommandLine["text"],
                        LocateLineNumber = (int)Convert.ChangeType(TerminalCommandLine["line"], typeof(int)),
                    });
                }

                if (newerBookmarkItem == null)
                {
                    prompt.Invoke("data is incomplete.");
                }

                if (TerminalCommandLine.ContainKeys("id"))
                {
                    var olderBookmarkItem = bookmarkPage.GetItemById((int)Convert.ChangeType(TerminalCommandLine["id"], typeof(int)));
                    if (olderBookmarkItem == null)
                    {
                        prompt.Invoke("id is invalid.");
                    }

                    olderBookmarkItem.ChildItems.Add(newerBookmarkItem);
                }
                else
                {
                    bookmarkPage.RootItems.Add(newerBookmarkItem);
                }

                BookmarkUtility.SetBookmarkPage(bookmarkPage, bookmarkTerminalCommandChannel.BookmarkConfigFile);
            }
        }
    }
}
