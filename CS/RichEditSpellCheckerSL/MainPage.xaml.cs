using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSpellChecker;
using RichEditSpellCheckerSL.SpellChecker;

namespace RichEditSpellCheckerSL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            richEdit1.Loaded += new RoutedEventHandler(richEdit1_Loaded);
        }
        #region spellcheckerinit
        void richEdit1_Loaded(object sender, RoutedEventArgs e)
        {
            DevExpress.Xpf.SpellChecker.SpellChecker spellChecker = SpellCheckerHelper.SpellChecker;
            spellChecker.SpellCheckMode = SpellCheckMode.AsYouType;

            SpellCheckerOpenOfficeDictionary dictionary = LoadGreekDictionary();

            SpellCheckerHelper.SpellChecker.Dictionaries.Add(dictionary);
            SpellCheckerHelper.SpellChecker.Culture = dictionary.Culture;
            
            this.richEdit1.SpellChecker = spellChecker;
        }

        static SpellCheckerOpenOfficeDictionary LoadGreekDictionary()
        {
            Stream dict = Assembly.GetExecutingAssembly().GetManifestResourceStream("RichEditSpellCheckerSL.Dic.el_GR.dic");
            Stream grammar = Assembly.GetExecutingAssembly().GetManifestResourceStream("RichEditSpellCheckerSL.Dic.el_GR.aff");
            SpellCheckerOpenOfficeDictionary dictionary = new SpellCheckerOpenOfficeDictionary();
            dictionary.LoadFromStream(dict, grammar, null);
            dictionary.Culture = new CultureInfo("el-GR");
            return dictionary;
        }
        #endregion #spellcheckerinit
        void Open_Click(object sender, RoutedEventArgs e)
        {
            System.Net.WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("../Texts/sample.rtf", UriKind.RelativeOrAbsolute));
        }
        
        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            richEdit1.RtfText = e.Result;
        }
    }
}