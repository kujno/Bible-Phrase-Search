using System.IO;
using System.Windows;

namespace Bible_Word_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum SearchInOptions
    {
        Bible,
        Testament,
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

    enum TestamentOptions
    {
        Old,
        New
    }

    public partial class MainWindow : Window
    {
        private SearchInOptions? searchInOption;
        private BookOptions? bookOption;
        private TestamentOptions? testamentOption;
        private byte[] bookChaptersNumber = {50, 40, 27, 36, 34, 24, 21, 4, 31, 24, 22, 25, 29, 36, 10, 13, 10, 42, 150, 31, 12, 8, 66, 52, 5, 48, 12, 14, 3, 9, 1, 4, 7, 3, 3, 3, 2, 14, 4, 28, 16, 24, 21, 28, 16, 16, 13, 6, 6, 4, 4, 5, 3, 6, 4, 3, 1, 13, 5, 5, 3, 5, 1, 1, 1, 22};
        private int? chapter;
        private string? BibleXml;
        InputChecker inputChecks = new InputChecker();
        PhraseSearcher phraseSearcher = new PhraseSearcher();

        public MainWindow()
        {
            InitializeComponent();

            groupBox_book.Visibility = Visibility.Collapsed;
            groupBox_chapter.Visibility = Visibility.Collapsed;
            groupBox_testament.Visibility = Visibility.Collapsed;

            // Adding items to comboboxes
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
            comboBox_testament.SelectedValuePath = "Key";
            comboBox_testament.DisplayMemberPath = "Value";
            foreach (TestamentOptions testament in Enum.GetValues(typeof(TestamentOptions)))
            {
                int testamentValue = (int)testament;
                string testamentTitle = testament.ToString();

                comboBox_testament.Items.Add(new KeyValuePair<int, string>(testamentValue, testamentTitle));
            }

            comboBox_search_in.SelectedIndex = 0;
            searchInOption = (SearchInOptions)comboBox_search_in.SelectedValue;
        }

        // opens open file dialog & saves the picked file path
        private void menu_open_click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Bible"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "Beblia XML documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string BibleFileName = dialog.FileName;

                BibleXml = File.ReadAllText(BibleFileName);
            }
        }

        private void menu_exit_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menu_about_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Purpose of this application is to help you study The Bible.\nThis application is free.");
        }

        private void comboBox_search_in_dropDownClosed(object sender, EventArgs e)
        {
            searchInOption = (SearchInOptions?)(int?)comboBox_search_in.SelectedValue;
            groupBox_chapter.Visibility = Visibility.Collapsed;
            groupBox_testament.Visibility = Visibility.Collapsed;

            if (searchInOption is null)
            {
                return;
            }
            if (searchInOption == SearchInOptions.Bible || searchInOption == SearchInOptions.Testament)
            {
                groupBox_book.Visibility = Visibility.Collapsed;

                if (searchInOption == SearchInOptions.Testament)
                {
                    groupBox_testament.Visibility = Visibility.Visible;
                }

                return;
            }

            groupBox_book.Visibility = Visibility.Visible;

            if (searchInOption == SearchInOptions.Chapter && comboBox_book.SelectedValue is not null)
            {
                groupBox_chapter.Visibility = Visibility.Visible;
            }
        }

        private void comboBox_book_dropDownClosed(object sender, EventArgs e)
        {
            bookOption = (BookOptions?)(int?)comboBox_book.SelectedValue;

            if (searchInOption == SearchInOptions.Chapter && bookOption is not null)
            {
                comboBox_chapter.Items.Clear();

                // Delay to prevent combobox from not updating
                System.Threading.Thread.Sleep(100);
                
                for (int i = 1; i <= bookChaptersNumber[(int)bookOption]; i++)
                {
                    comboBox_chapter.Items.Add(i);
                }

                groupBox_chapter.Visibility = Visibility.Visible;
            }
        }

        private void button_search_click(object sender, RoutedEventArgs e)
        {
            bool inputsCheck;
            string? searchedPhrase = textBox_searched_phrase.Text.Trim();

            inputChecks.PreSearchCheck(BibleXml, searchInOption, bookOption, bookChaptersNumber, chapter, searchedPhrase, testamentOption, out inputsCheck);
            if (!inputsCheck)
            {
                return;
            }

            if (String.IsNullOrEmpty(BibleXml) || searchInOption is null)
            {
                return;
            }
            textBlock_search_results.Text = phraseSearcher.Search(BibleXml, (SearchInOptions)searchInOption, bookOption, chapter, searchedPhrase, testamentOption);
        }

        private void comboBox_chapter_dropDownClosed(object sender, EventArgs e)
        {
            chapter = (int?)comboBox_chapter.SelectedValue;
        }

        private void comboBox_testament_dropDownClosed(object sender, EventArgs e)
        {
            testamentOption = (TestamentOptions?)(int?)comboBox_testament.SelectedValue;
        }
    }
}