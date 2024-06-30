using System.Windows;

namespace Bible_Word_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum SearchInOptions
    {
        Bible,
        Book,
        Chapter
    }

    enum BookOptions
    {
        Genesis,
        Exodus,
        Leviticus,
        Numbers,
        Deuteronomy,
        Joshua,
        Judges,
        Ruth,
        FirstSamuel,
        SecondSamuel,
        FirstKings,
        SecondKings,
        FirstChronicles,
        SecondChronicles,
        Ezra,
        Nehemiah,
        Esther,
        Job,
        Psalms,
        Proverbs,
        Ecclesiastes,
        SongOfSolomon,
        Isaiah,
        Jeremiah,
        Lamentations,
        Ezekiel,
        Daniel,
        Hosea,
        Joel,
        Amos,
        Obadiah,
        Jonah,
        Micah,
        Nahum,
        Habakkuk,
        Zephaniah,
        Haggai,
        Zechariah,
        Malachi,
        Matthew,
        Mark,
        Luke,
        John,
        Acts,
        Romans,
        FirstCorinthians,
        SecondCorinthians,
        Galatians,
        Ephesians,
        Philippians,
        Colossians,
        FirstThessalonians,
        SecondThessalonians,
        FirstTimothy,
        SecondTimothy,
        Titus,
        Philemon,
        Hebrews,
        James,
        FirstPeter,
        SecondPeter,
        FirstJohn,
        SecondJohn,
        ThirdJohn,
        Jude,
        Revelation
    }

    public partial class MainWindow : Window
    {
        private SearchInOptions? searchInOption;
        private BookOptions? bookOption;
        private byte[] bookChaptersNumber = {50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22};
        
        public MainWindow()
        {
            InitializeComponent();

            groupBox_book.Visibility = Visibility.Collapsed;
            groupBox_chapter.Visibility = Visibility.Collapsed;

            comboBox_search_in.SelectedValuePath = "Key";
            comboBox_search_in.DisplayMemberPath = "Value";
            foreach (SearchInOptions searchIn in Enum.GetValues(typeof(SearchInOptions)))
            {
                int searchInValue = (int)searchIn;
                string searchInTitle = searchIn.ToString();

                comboBox_search_in.Items.Add(new KeyValuePair<int, string>(searchInValue, searchInTitle));
            }
            comboBox_book.SelectedValuePath = "Key";
            comboBox_book.DisplayMemberPath = "Value";
            foreach (BookOptions book in Enum.GetValues(typeof(BookOptions)))
            {
                int bookValue = (int)book;
                string bookTitle = book.ToString();

                comboBox_book.Items.Add(new KeyValuePair<int, string>(bookValue, bookTitle));
            }

            comboBox_search_in.SelectedIndex = 0;
        }

        private void menu_open_click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_exit_click(object sender, RoutedEventArgs e)
        {

        }

        private void menu_about_click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_search_in_dropDownClosed(object sender, EventArgs e)
        {
            searchInOption = (SearchInOptions)comboBox_search_in.SelectedValue;
            groupBox_chapter.Visibility = Visibility.Collapsed;

            if (searchInOption is not null)
            {
                if (searchInOption == SearchInOptions.Bible)
                {
                    groupBox_book.Visibility = Visibility.Collapsed;

                    return;
                }

                groupBox_book.Visibility = Visibility.Visible;

                if (searchInOption == SearchInOptions.Chapter && comboBox_book.SelectedValue is not null)
                {
                    groupBox_chapter.Visibility = Visibility.Visible;
                }
            }
        }

        private void comboBox_book_dropDownClosed(object sender, EventArgs e)
        {
            bookOption = (BookOptions)comboBox_book.SelectedValue;

            if (searchInOption == SearchInOptions.Chapter && bookOption is not null)
            {
                comboBox_chapter.Items.Clear();
                
                for (int i = 1; i <= bookChaptersNumber[(int)bookOption]; i++)
                {
                    comboBox_chapter.Items.Add(i);
                }

                groupBox_chapter.Visibility = Visibility.Visible;
            }
        }
    }
}