using System;
using System.Linq;

using Petecat.Console.Command;
using Petecat.Data.Xml;
using System.Text;

namespace Bookmarks.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("bookmark dat: ");
            var bookmarkConfigFile = Console.ReadLine();

            var bookmarkTerminalCommandChannel = Bookmarks.BookmarkTerminalCommandChannelManager.Instance.Load(bookmarkConfigFile);
            if (bookmarkTerminalCommandChannel == null)
            {
                Console.WriteLine("failed to load channel.");
                return;
            }

            bookmarkTerminalCommandChannel.TerminalCommandExecuting += OnTerminalCommandExecuting;
            bookmarkTerminalCommandChannel.TerminalCommandDidExecuted += OnTerminalCommandDidExecuted;

            var command = "";
            while ((command = ReadLine().Trim()) != "quit")
            {
                bookmarkTerminalCommandChannel.Consume(command);
            }
        }

        const int ReadLineBufferSize = 1024;

        static string ReadLine()
        {
            var inputStream = Console.OpenStandardInput(ReadLineBufferSize);
            byte[] bytes = new byte[ReadLineBufferSize];
            int outputLength = inputStream.Read(bytes, 0, ReadLineBufferSize);
            char[] chars = Encoding.UTF8.GetChars(bytes, 0, outputLength);
            return new string(chars);
        }

        static void OnTerminalCommandExecuting(ITerminalCommandChannel terminalCommandChannel, ITerminalCommand terminalCommand, string prompt)
        {
            Console.WriteLine(prompt);
        }

        static void OnTerminalCommandDidExecuted(ITerminalCommandChannel terminalCommandChannel, ITerminalCommand terminalCommand)
        {
            if (terminalCommand.Result is BookmarkPage || terminalCommand.Result is BookmarkItem || terminalCommand.Result is BookmarkItem[])
            {
                if (terminalCommand.Result == null)
                {
                    return;
                }

                if (terminalCommand.Result is BookmarkPage)
                {
                    foreach (var bookmarkItem in (terminalCommand.Result as BookmarkPage).RootItems)
                    {
                        Console.WriteLine(DumpBookmarkItem(bookmarkItem, 0, false));
                    }
                }
                else if (terminalCommand.Result is BookmarkItem)
                {
                    Console.WriteLine(DumpBookmarkItem(terminalCommand.Result as BookmarkItem, 0, false));
                }
                else if (terminalCommand.Result is BookmarkItem[])
                {
                    foreach (var bookmarkItem in (terminalCommand.Result as BookmarkItem[]))
                    {
                        Console.WriteLine(DumpBookmarkItem(bookmarkItem, 0, false));
                    }
                }
            }
            else
            {
                Console.WriteLine(terminalCommand.Result);
            }
        }

        static string DumpBookmarkItem(BookmarkItem bookmarkItem, int level, bool detail = false)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(SpaceChars(level * 5));
            stringBuilder.AppendFormat("{0,-4}|{1}", bookmarkItem.Id, bookmarkItem.Description);

            if (detail)
            {
                var location = bookmarkItem.GetLocation();
                if (location != null)
                {
                    stringBuilder.AppendFormat("|{0}|{1}", location.FileLocation, location.LocateLineNumber);
                }
            }

            if (bookmarkItem.ChildItems != null && bookmarkItem.ChildItems.Count > 0)
            {
                foreach (var childItem in bookmarkItem.ChildItems)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append(DumpBookmarkItem(childItem, level + 1, detail));
                }
            }

            return stringBuilder.ToString();
        }

        static string SpaceChars(int number)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < number; i++)
            {
                stringBuilder.Append(' ');
            }
            return stringBuilder.ToString();
        }
    }
}