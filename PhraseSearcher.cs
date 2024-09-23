using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Bible_Word_Finder
{
    internal class PhraseSearcher
    {
        public string Search(string BiblePassage, SearchInOptions searchIn, BookOptions? book, int? chapter, string searchedPhrase, TestamentOptions? testament)
        {
            string BibleXml = "<bible translation=\"", bookXmlEnd = "</book>", chapterXmlEnd = "</chapter>", BibleXmlEnd = "</bible>", testamentXmlEnd = "</testament>", xml = "", xmlEnd = "";

            if (searchIn == SearchInOptions.Book || searchIn == SearchInOptions.Chapter)
            {
                if (book is not null)
                {
                    xml = $"<book number=\"{(int)book + 1}\">";
                }
                xmlEnd = bookXmlEnd;
            }

            if (searchIn == SearchInOptions.Chapter)
            {
                xml = $"<chapter number=\"{chapter}\">";
                xmlEnd = chapterXmlEnd;
            }

            if (searchIn == SearchInOptions.Testament)
            {
                xml = $"<testament name=\"{testament}\">";
                xmlEnd = testamentXmlEnd;
            }

            if (searchIn == SearchInOptions.Bible)
            {
                xml = BiblePassage.Substring(BiblePassage.IndexOf(BibleXml));
                xml = xml.Remove(xml.IndexOf('>') + 1);
                xmlEnd = BibleXmlEnd;
            }

            BiblePassage = shortenPassage(BiblePassage, xml, xmlEnd);

            return searchPhrase(BiblePassage, searchedPhrase, searchIn, book, chapter);
        }

        private string shortenPassage(string passage, string xml, string xmlEnd)
        {
            int index, indexEnd;
            
            passage = passage.Substring(passage.IndexOf(xml));
            index = passage.IndexOf(">") + 1;
            passage = passage.Substring(index);
            indexEnd = passage.IndexOf(xmlEnd);
            passage = passage.Remove(indexEnd);

            return passage;
        }

        private string searchPhrase(string passage, string searchedPhrase, SearchInOptions searchIn, BookOptions? bookInput, int? chapterInput)
        {
            int index, chapterLast = 0;
            BookOptions bookLast = BookOptions.Genesis;
            string output = "";
            
            do
            {
                int chapter = 0, verse, useless = 0, indexVerseEnd;
                BookOptions book = BookOptions.Genesis;
                string? verseTemp;
                string verseEndXml = "</verse>";

                index = passage.IndexOf(searchedPhrase);

                if (index == -1)
                {
                    break;
                }
                
                if (searchIn == SearchInOptions.Bible || searchIn == SearchInOptions.Testament)
                {
                    int bookLastInt = (int)bookLast + 1;

                    book = (BookOptions)(getXmlNumber("<book number=\"", passage, index, ref bookLastInt) - 1);
                    bookLast = (BookOptions)(bookLastInt - 1);
                }

                if (searchIn == SearchInOptions.Book)
                {
                    if (bookInput is not null)
                    {
                        book = (BookOptions)bookInput;
                    }
                }

                if (searchIn == SearchInOptions.Bible || searchIn == SearchInOptions.Testament || searchIn == SearchInOptions.Book)
                {
                    chapter = getXmlNumber("<chapter number=\"", passage, index, ref chapterLast);
                }
                else if (searchIn == SearchInOptions.Chapter)
                {
                    if (bookInput is not null)
                    {
                        book = (BookOptions)bookInput;
                    }
                    
                    if (chapterInput is not null)
                    {
                        chapter = (int)chapterInput;
                    }
                }

                verse = getXmlNumber("<verse number=\"", passage, index, ref useless);

                passage = passage.Substring(passage.Substring(0, index + 1).LastIndexOf('>') + 1);

                indexVerseEnd = passage.IndexOf(verseEndXml);

                if (passage.IndexOf(verseEndXml) > passage.IndexOf("<verse number=\""))
                {
                    continue;
                }

                verseTemp = passage.Substring(0, indexVerseEnd);

                output += $"{book} {chapter}:{verse}\n{verseTemp}\n\n";

                passage = passage.Substring(indexVerseEnd + verseEndXml.Length);
            }
            while (index != -1);

            return output;
        }

        private int getXmlNumber(string xml, string passage, int index, ref int lastNumber)
        {
            int number, lastIndexXml;

            lastIndexXml = passage.Substring(0, index + 1).LastIndexOf(xml);
                    
            if (lastIndexXml == -1)
            {
                number = lastNumber;
            }
            else
            {
                lastIndexXml += xml.Length;
                
                int.TryParse(passage.Substring(lastIndexXml, passage.Substring(lastIndexXml).IndexOf('\"')), out number);

                lastNumber = number;
            }

            return number;
        }
    }
}