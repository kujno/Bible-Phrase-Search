using System.Windows;

namespace Bible_Word_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum SearchIn
    {
        Bible = 1,
        Book = 2,
        Chapter = 3
    }

    enum Book
    {
        Genesis = 1,
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
        public MainWindow()
        {
            InitializeComponent();
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
    }
}