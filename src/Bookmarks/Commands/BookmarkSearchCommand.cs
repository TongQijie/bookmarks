using Petecat.Console.Command;

namespace Bookmarks.Commands
{
    [TerminalCommand(new string[] { "search" })]
    public class BookmarkSearchCommand : TerminalCommandBase
    {
        public BookmarkSearchCommand(TerminalCommandLine terminalCommandLine)
            : base(terminalCommandLine)
        {
        }

        public override void Execute(ITerminalCommandChannel terminalCommandChannel, System.Action<string> prompt)
        {
            if (terminalCommandChannel is BookmarkTerminalCommandChannel)
            {
                Handled = true;

                var bookmarkTerminalCommandChannel = terminalCommandChannel as BookmarkTerminalCommandChannel;

                var bookmarkPage = bookmarkTerminalCommandChannel.BookmarkPage;

                if (TerminalCommandLine.ContainKeys("kw"))
                {
                    Result = bookmarkPage.GetItemsByKeyword(TerminalCommandLine["kw"]);
                }
                else
                {
                    prompt.Invoke("keyword is empty.");
                }
            }
        }
    }
}
