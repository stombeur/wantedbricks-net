using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace WantedBricks.Core
{
    public class LddProject
    {
        private readonly Image _image;
        private readonly string _xml;


        public LddProject(Image image, string xml)
        {
            _image = image;
            _xml = xml;
        }

        public static LddProject Load(Stream stream)
        {
            ZipFile zf = null;
            zf = new ZipFile(stream);

            var thumbEntry = zf.GetEntry("IMAGE100.PNG");
            
            var image = Image.FromStream(zf.GetInputStream(thumbEntry));

            var xmlEntry = zf.GetEntry("IMAGE100.LXFML");
            string xmlContent = null;
            using (var sr = new StreamReader(zf.GetInputStream(xmlEntry)))
            {
                xmlContent = sr.ReadToEnd();
            }

            return new LddProject(image, xmlContent);
        }

        public static LddProject Load(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return Load(fs);
            }
        }

        public BrickCollection ToBricks()
        {
            //TODO

            return new BrickCollection();
        }
    }

    public class BrickLinkPart
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public List<int> ColorIds { get; set; }

        public BrickLinkPart()
        {
            ColorIds = new List<int>();
        }
    }

    public class BrickLinkCategory
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<BrickLinkPart> Parts { get; set; }

        public BrickLinkCategory()
        {
            Parts = new List<BrickLinkPart>();
        }

    }

    public class BrickMapping
    {
        public string LegoDesignId { get; set; }
        public bool UseItemNrs { get; set; }
        public string BrickLinkItemNr { get; set; }
        public bool Ignore { get; set; }

        public Dictionary<string, string> ItemNrList { get; set; }

        public BrickMapping()
        {
            ItemNrList = new Dictionary<string, string>();
        }

    }

    public class BrickLinkColor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color ColorRGB { get; set; }
    }

    public class LegoColor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BrickLinkColor BLColor { get; set; }

    }

    public class Brick
    {
        public string ItemNr { get; set; }
        public int ColorId { get; set; }
        public LegoColor Color { get; set; }
        public string DesignId { get; set; }
        public int Count { get; set; }
        public string BrickLinkItemId { get; set; }
        public string BrickLinkItemName { get; set; }
        public int BrickLinkCountDiff { get; set; }
        public BrickLinkColor BrickLinkColor { get; set; }
        public Image Image { get; set; }
        public bool FoundInStore { get; set; }
        public bool Exclude { get; set; }
        public string Note { get; set; }
    }

    public class BrickCollection
    {
        public Image ThumbnailImage { get; set; }
        public List<Brick> Bricks { get; set; }
        public Dictionary<string, Brick> BrickDict { get; set; }
        public string Name { get; set; }
        public List<LegoColor> LegoColors { get; set; }
        public List<BrickLinkColor> BrickLinkColors { get; set; }
        public Dictionary<string, BrickMapping> BrickMappings { get; set; }
        public List<BrickLinkPart> BrickLinkParts { get; set; }
    }
}
