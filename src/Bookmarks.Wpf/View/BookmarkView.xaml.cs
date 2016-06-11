using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bookmarks.Wpf.View
{
    /// <summary>
    /// Interaction logic for BookmarkView.xaml
    /// </summary>
    public partial class BookmarkView : UserControl
    {
        public BookmarkView()
        {
            DataContext = this;
            Controller = new Controller.BookmarkController("bookmarks.dat").Load();

            InitializeComponent();
        }

        public Controller.BookmarkController Controller { get; set; }

        public void EnterWriterView()
        {
            BookmarkWriterView.ViewContainer = this;
            BookmarkWriterView.ParentBookmarkItem = Controller.SelectedBookmarkItem;
            if (BookmarkWriterView.ParentBookmarkItem != null)
            {
                BookmarkWriterView.Catalog = BookmarkWriterView.ParentBookmarkItem.Catalog;
                BookmarkWriterView.Index = BookmarkWriterView.ParentBookmarkItem.ChildItems.Count + 1;
                BookmarkWriterView.Description = "";
                if (BookmarkWriterView.ParentBookmarkItem.ChildItems.Count > 0)
                {
                    BookmarkWriterView.Path = BookmarkWriterView.ParentBookmarkItem.ChildItems.OrderByDescending(x => x.Index).FirstOrDefault().Fullname;
                }
                BookmarkWriterView.LineText = "";
            }

            var storyboard = FindResource("BookmarkWriterViewEnterStoryboard") as Storyboard;
            storyboard.Begin();
        }

        public void LeaveWriterView()
        {
            var storyboard = FindResource("BookmarkWriterViewLeaveStoryboard") as Storyboard;
            storyboard.Begin();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var bookmarkItem = (sender as FrameworkElement).Tag as ViewModel.BookmarkItem;
            bookmarkItem.IsSelected = true;
            Controller.SelectedBookmarkItem = bookmarkItem;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EnterWriterView();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LeaveWriterView();
        }
    }
}