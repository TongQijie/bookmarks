using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace Bookmarks
{
    [XmlRoot("bookmarks")]
    public class BookmarkPage
    {
        public BookmarkPage()
        {
            RootItems = new List<BookmarkItem>();
        }

        [XmlElement("bookmark")]
        public List<BookmarkItem> RootItems { get; set; }

        public int GenerateItemId()
        {
            if (RootItems.Count == 0)
            {
                return 1;
            }
            else
            {
                return RootItems.Select(x => x.GetMaxItemId()).ToList().Max() + 1;
            }
        }

        public BookmarkItem GetItemById(int id)
        {
            foreach (var item in RootItems)
            {
                var bookmarkItem = item.GetItemById(id);
                if (bookmarkItem != null)
                {
                    return bookmarkItem;
                }
            }

            return null;
        }

        public BookmarkItem[] GetItemsByKeyword(string keyword)
        {
            var bookmarkItems = new BookmarkItem[0];
            foreach (var rootItem in RootItems)
            {
                rootItem.GetItemsByKeyword(keyword, ref bookmarkItems);
            }
            return bookmarkItems;
        }

        public BookmarkItem GetParentItemById(int id)
        {
            if (RootItems.Exists(x => x.Id == id))
            {
                return null;
            }
            else
            {
                foreach (var rootItem in RootItems)
                {
                    var bookmarkItem = rootItem.GetParentItem(id);
                    if (bookmarkItem != null)
                    {
                        return bookmarkItem;
                    }
                }

                return null;
            }
        }
    }
}
