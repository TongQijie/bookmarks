using Bookmarks.Wpf.Utility;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bookmarks.Wpf.View
{
    /// <summary>
    /// Interaction logic for BookmarkWriterView.xaml
    /// </summary>
    public partial class BookmarkWriterView : UserControlBase
    {
        public BookmarkWriterView()
        {
            DataContext = this;

            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Catalog) || string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(Path) || string.IsNullOrWhiteSpace(LineText))
            {
                MessageBox.Show("data cannot be empty.");
                return;
            }

            var bookmarkItem = new Bookmarks.BookmarkItem();
            bookmarkItem.Id = ParentBookmarkItem == null ? 0 : ParentBookmarkItem.Core.Id;
            bookmarkItem.Catalog = Catalog;
            bookmarkItem.Index = Index;
            bookmarkItem.Description = Description;
            bookmarkItem.Locations = new List<BookmarkLocation>();
            bookmarkItem.Locations.Add(new BookmarkLocation()
            {
                FileLocation = Path,
                LocateLineNumber = LineNumber,
                LocateLineText = LineText,
            });

            ViewContainer.Controller.CreateBookmark(bookmarkItem);

            ViewContainer.LeaveWriterView();
        }

        public BookmarkView ViewContainer { get; set; }

        public ViewModel.BookmarkItem ParentBookmarkItem { get; set; }

        private string _Catalog = null;

        public string Catalog
        {
            get { return _Catalog; }
            set
            {
                if (_Catalog != value)
                {
                    _Catalog = value;
                    FirePropertiesChanged("Catalog");
                }
            }
        }

        private int _Index = 1;

        public int Index
        {
            get { return _Index; }
            set
            {
                if (_Index != value)
                {
                    _Index = value;
                    FirePropertiesChanged("Index");
                }
            }
        }

        private string _Description = null;

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    FirePropertiesChanged("Description");
                }
            }
        }

        private string _Path = null;

        public string Path
        {
            get { return _Path; }
            set
            {
                if (_Path != value)
                {
                    _Path = value;
                    FirePropertiesChanged("Path");
                }
            }
        }

        public int LineNumber { get; set; }

        private string _LineText = null;

        public string LineText
        {
            get { return _LineText; }
            set
            {
                if (_LineText != value)
                {
                    _LineText = value;
                    FirePropertiesChanged("LineText");
                }
            }
        }
    }
}
