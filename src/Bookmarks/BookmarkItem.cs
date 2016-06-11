using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System;

namespace Bookmarks
{
    public class BookmarkItem
    {
        public BookmarkItem()
        {
            Locations = new List<BookmarkLocation>();
            ChildItems = new List<BookmarkItem>();
        }

        public BookmarkItem(int id, string catalog, int index, string description)
            : this()
        {
            Id = id;
            Catalog = catalog;
            Index = index;
            Description = description;
        }

        public BookmarkItem(int id, string catalog, int index, string description, BookmarkLocation location)
            : this(id, catalog, index, description)
        {
            Locations.Add(location);
        }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("catalog")]
        public string Catalog { get; set; }

        [XmlAttribute("index")]
        public int Index { get; set; }
        
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlArray("locations")]
        [XmlArrayItem("location")]
        public List<BookmarkLocation> Locations { get; set; }

        [XmlArray("children")]
        [XmlArrayItem("bookmark")]
        public List<BookmarkItem> ChildItems { get; set; }

        [XmlIgnore]
        public BookmarkItem ParentItem { get; set; }

        public BookmarkLocation GetLocation()
        {
            if (Locations != null && Locations.Count > 0)
            {
                return Locations.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public int GetMaxItemId()
        {
            if (ChildItems.Count == 0)
            {
                return Id;
            }
            else
            {
                return Math.Max(ChildItems.Select(x => x.GetMaxItemId()).ToList().Max(), Id);
            }
        }

        public BookmarkItem GetItemById(int id)
        {
            if (Id == id)
            {
                return this;
            }
            else
            {
                return ChildItems.FirstOrDefault(x => x.Id == id);
            }
        }

        public void GetItemsByKeyword(string keyword, ref BookmarkItem[] bookmarkItems)
        {
            if (Description.ToLower().Contains(keyword.ToLower()) || Catalog.ToLower().Contains(keyword.ToLower()))
            {
                bookmarkItems = bookmarkItems.Concat(new BookmarkItem[] { this }).ToArray();
            }

            foreach (var childItem in ChildItems)
            {
                childItem.GetItemsByKeyword(keyword, ref bookmarkItems);
            }
        }

        public BookmarkItem GetParentItem(int id)
        {
            if (ChildItems.Exists(x => x.Id == id))
            {
                return this;
            }
            else
            {
                foreach (var childItem in ChildItems)
                {
                    var bookmarkItem = childItem.GetParentItem(id);
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