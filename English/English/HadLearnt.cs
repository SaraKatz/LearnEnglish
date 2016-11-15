using English.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Media;

namespace English
{
    public class HadLearnt
    {   
        //WordPictuer = new BitmapImage(new Uri(elem.Element(xn.GetName("wordPictuer")).Value));
        XDocument doc;
        XNamespace xn;

      
        public List<Lesson> lesson_learnt= new List<Lesson>();
        public List<EnglishWord> pictuers_HadLearnt = new List<EnglishWord>();
        public List<string> letterBigShape_HadLearnt = new List<string>();
        public List<string> letterSmallShape_HadLearnt = new List<string>();
        public List<letter> letters_HadLearnt = new List<letter>();

        public HadLearnt(Lesson currentLesson)
        {
            doc = XDocument.Load(@"xml\dataFiles\lessons.xml");
            xn = doc.Root.Name.Namespace; 
            
            
            foreach (var lesson in  doc.Descendants(xn.GetName("lesson")))
            {
                if (int.Parse(lesson.Attribute("lessonCode").Value)<=int.Parse(currentLesson._lessonCode))
                {
                    lesson_learnt.Add(new Lesson(lesson.Attribute("lessonCode").Value));
                }
            }
            foreach (var lesson in lesson_learnt)
            {
                foreach (var letter in lesson.lettersForLesson)
                {
                    letterBigShape_HadLearnt.Add(letter.LettersBigShape);
                    letterSmallShape_HadLearnt.Add(letter.LettersSmallShape);
                    letters_HadLearnt.Add(letter);

                    foreach (var word in letter.wordsForLetter)
                    {
                        pictuers_HadLearnt.Add(word);
                    }
                }
                
            }
        }
        
           
         
    }
}
