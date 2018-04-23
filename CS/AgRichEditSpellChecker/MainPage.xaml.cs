using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSpellChecker;

namespace AgRichEditSpellChecker
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            richEdit1.ApplyTemplate();

            Stream dict = Assembly.GetExecutingAssembly().GetManifestResourceStream("AgRichEditSpellChecker.Dic.el_GR.dic");
            Stream grammar = Assembly.GetExecutingAssembly().GetManifestResourceStream("AgRichEditSpellChecker.Dic.el_GR.aff");
            SpellCheckerOpenOfficeDictionary dictionary = new SpellCheckerOpenOfficeDictionary();
            CultureInfo ellinika = new CultureInfo("el-GR");        
            dictionary.LoadFromStream(dict, grammar, null);
            dictionary.Culture = ellinika;

            SpellCheckerHelper.SpellChecker.Dictionaries.Add(dictionary);
            SpellCheckerHelper.SpellChecker.Culture = ellinika;
        }

        void Open_Click(object sender, RoutedEventArgs e)
        {
            System.Net.WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("../Texts/sample.rtf", UriKind.Relative));
            

        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            richEdit1.RichControl.RtfText = e.Result;
        }
    }
}