using System;

using Petecat.Console.Command;

namespace Bookmarks.Commands
{
    [TerminalCommand(new string[] { "query" })]
    public class BookmarkQueryCommand : TerminalCommandBase
    {
        public BookmarkQueryCommand(TerminalCommandLine terminalCommandLine)
            : base(terminalCommandLine)
        {
        }

        public override void Execute(ITerminalCommandChannel terminalCommandChannel, System.Action<string> prompt)
        {
            if (terminalCommandChannel is BookmarkTerminalCommandChannel)
            {
                Handled = true;

                var bookmarkTerminalCommandChannel = terminalCommandChannel as BookmarkTerminalCommandChannel;

                var bookmarkPage = bookmarkTerminalCommandChannel.BookmarkContainer.CurrentPage;

                if (TerminalCommandLine.ContainKeys("id"))
                {
                    var bookmark = bookmarkPage.GetItemById((int)Convert.ChangeType(TerminalCommandLine["id"], typeof(int)));
                    if (bookmark == null)
                    {
                        prompt.Invoke("bookmark not found.");
                    }
                    else
                    {
                        Result = bookmark;
                    }
                }
                else
                {
                    Result = bookmarkPage;
                }
            }
        }
    }
}
