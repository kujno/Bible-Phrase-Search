using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bible_Word_Finder
{
    internal class InputChecker
    {
        private bool inputNotNullCheck(string? BibleXml, SearchInOptions? searchIn, BookOptions? book, byte[] bookChaptersNumbers, int? chapter, string? searchedPhrase, TestamentOptions? testament)
        {
            if (String.IsNullOrEmpty(BibleXml) || String.IsNullOrEmpty(searchedPhrase) || searchIn is null || ((searchIn == SearchInOptions.Book || searchIn == SearchInOptions.Chapter) && book is null) || (searchIn == SearchInOptions.Chapter && chapter is null) || (searchIn == SearchInOptions.Testament && testament is null))
            {
                return false;
            }

            return true;
        }

        public void PreSearchCheck(string? BibleXml, SearchInOptions? searchIn, BookOptions? book, byte[] bookChaptersNumbers, int? chapter, string? searchedPhrase, TestamentOptions? testament, out bool check)
        {
            if (!inputNotNullCheck(BibleXml, searchIn, book, bookChaptersNumbers, chapter, searchedPhrase, testament))
            {
                MessageBox.Show("Cannot search\nPlease fill all fields, open a Beblia XML file and try again");

                check = false;

                return;
            }

            check = true;
        }
    }
}
