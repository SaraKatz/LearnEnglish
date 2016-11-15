using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace English
{
    public class letter
    {
        XDocument doc;
        XNamespace xn;
        public string ClipBig { get; set; }
        public string ClipSmall { get; set; }
        public string Sound { get; set; }
        public string Sound_letterExplanation { get; set; }
        public string LettersBigShape { get; set; }
        public string LettersSmallShape { get; set; }
        public string HebrewLetter { get; set; }

        public List<EnglishWord> wordsForLetter { get; set; }

        public letter()
        {
            wordsForLetter = new List<EnglishWord>();
        }
      
        
        public letter(XElement elem): this()
        {
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace;

            ClipBig = elem.Element(xn.GetName("clipBig")).Value;
            ClipSmall = elem.Element(xn.GetName("clipSmall")).Value;
            Sound = elem.Element(xn.GetName("sound")).Value;
            Sound_letterExplanation = elem.Element(xn.GetName("sound_letterExplanation")).Value;
            LettersBigShape = elem.Attribute("lettersBigShape").Value;
            LettersSmallShape = elem.Attribute("lettersSmallShape").Value;
            HebrewLetter = elem.Attribute("hebrewLetter").Value;

            setWordsForLetter(LettersBigShape);
        }

        public void setWordsForLetter(string _letter)
        {
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace;

            foreach (var elem in doc.Descendants(xn.GetName("word")).Where(c => c.Attribute("lettersBigShape").Value.Equals(_letter)))

            {
                wordsForLetter.Add(new EnglishWord (elem));
            }
        }
    }
}
