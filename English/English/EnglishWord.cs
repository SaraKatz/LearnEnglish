using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Media3D;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

namespace English
{
    public class EnglishWord
    {
        XDocument doc;
        XNamespace xn;
        public string Word { get; set; }
        public ImageSource WordPictuer { get; set; }
        public Boolean WordPictuerUsed { get; set; }
        public string WordSound { get; set; }
        public string LettersBigShape { get; set; }
        public string LettersSmallShape { get; set; }

        public EnglishWord() { }

        public EnglishWord(XElement elem)
        {
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace;

            LettersBigShape = elem.Attribute("lettersBigShape").Value;
            LettersSmallShape = elem.Attribute("lettersSmallShape").Value;
            Word = elem.Attribute("word").Value;
            WordPictuer = new BitmapImage(new Uri(elem.Element(xn.GetName("wordPictuer")).Value));
            WordSound = elem.Element(xn.GetName("wordSound")).Value;
            WordPictuerUsed = false;

        }
    }
}
