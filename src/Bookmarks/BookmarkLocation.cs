using System.IO;
using System.Xml.Serialization;

namespace Bookmarks
{
    public class BookmarkLocation
    {
        public BookmarkLocation()
        {
        }

        public BookmarkLocation(string fileLocation, string locateLineText, int locateLineNumber)
        {
            FileLocation = fileLocation;
            LocateLineText = locateLineText;
            LocateLineNumber = locateLineNumber;
        }

        [XmlElement("file")]
        public string FileLocation { get; set; }

        [XmlElement("text")]
        public string LocateLineText { get; set; }

        [XmlElement("line")]
        public int LocateLineNumber { get; set; }

        public int Locate()
        {
            var lineNo = 0;

            if (!File.Exists(FileLocation))
            {
                return lineNo;
            }

            using (var streamReader = new StreamReader(FileLocation))
            {
                string line = null;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lineNo++;
                    if (line.Contains(LocateLineText))
                    {
                        return lineNo;
                    }
                }
            }

            return LocateLineNumber;
        }
    }
}