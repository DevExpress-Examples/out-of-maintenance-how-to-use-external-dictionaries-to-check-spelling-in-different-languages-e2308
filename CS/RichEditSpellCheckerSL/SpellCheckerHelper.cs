using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraSpellChecker;
using System.IO;
using System.Globalization;
using System.Reflection;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.SpellChecker;

namespace RichEditSpellCheckerSL.SpellChecker
{
    #region #spellcheckerhelper
    public static class SpellCheckerHelper {
        static DevExpress.Xpf.SpellChecker.SpellChecker spellChecker;
        public static DevExpress.Xpf.SpellChecker.SpellChecker SpellChecker {
            get {
                if (spellChecker == null)
                    spellChecker = CreateSpellChecker();
                return spellChecker;
            }
        }
        static DevExpress.Xpf.SpellChecker.SpellChecker CreateSpellChecker() {
            SpellCheckTextControllersManager.Default.RegisterClass(typeof(RichEditControl), typeof(RichEditSpellCheckController));
            DevExpress.Xpf.SpellChecker.SpellChecker spellChecker = new DevExpress.Xpf.SpellChecker.SpellChecker();
            spellChecker.Culture = new CultureInfo("en-US");
            try {
                RegisterDictionary(spellChecker, GetDefaultDictionary());
                RegisterDictionary(spellChecker, GetCustomDictionary(spellChecker));
            }
            catch {
            }
            return spellChecker;
        }
        static SpellCheckerDictionaryBase GetDefaultDictionary() {
            SpellCheckerISpellDictionary dic = new SpellCheckerISpellDictionary();
            dic.LoadFromStream(GetDataStream("american.xlg"),
                               GetDataStream("english.aff"),
                               GetDataStream("EnglishAlphabet.txt"));
            return dic;
        }
        static Stream GetDataStream(string fileName) {
            if (String.IsNullOrEmpty(fileName))
                return null;
            string path = "RichEditSpellCheckerSL.DefaultDictionary." + fileName;
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        }
        static void RegisterDictionary(DevExpress.Xpf.SpellChecker.SpellChecker spellChecker, SpellCheckerDictionaryBase dic) {
            dic.Culture = spellChecker.Culture;
            spellChecker.Dictionaries.Add(dic);
        }
        static SpellCheckerDictionaryBase GetCustomDictionary(DevExpress.Xpf.SpellChecker.SpellChecker spellChecker) {
            return new SpellCheckerCustomDictionary("CustomEnglish.dic", spellChecker.Culture);
        }
    }
    #endregion #spellcheckerhelper
}
