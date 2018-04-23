Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraSpellChecker
Imports RichEditSpellCheckerSL.SpellChecker

Namespace RichEditSpellCheckerSL
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			AddHandler richEdit1.Loaded, AddressOf richEdit1_Loaded
		End Sub

		Private Sub richEdit1_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim spellChecker As DevExpress.Xpf.SpellChecker.SpellChecker = SpellCheckerHelper.SpellChecker
			spellChecker.SpellCheckMode = SpellCheckMode.AsYouType

			Dim dictionary As SpellCheckerOpenOfficeDictionary = LoadGreekDictionary()

			SpellCheckerHelper.SpellChecker.Dictionaries.Add(dictionary)
			SpellCheckerHelper.SpellChecker.Culture = dictionary.Culture

			Me.richEdit1.SpellChecker = spellChecker
		End Sub

		Private Shared Function LoadGreekDictionary() As SpellCheckerOpenOfficeDictionary
			Dim dict As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Dic.el_GR.dic")
			Dim grammar As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Dic.el_GR.aff")
			Dim dictionary As New SpellCheckerOpenOfficeDictionary()
			dictionary.LoadFromStream(dict, grammar, Nothing)
			dictionary.Culture = New CultureInfo("el-GR")
			Return dictionary
		End Function

		Private Sub Open_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim webClient As System.Net.WebClient = New WebClient()
			AddHandler webClient.DownloadStringCompleted, AddressOf webClient_DownloadStringCompleted
			webClient.DownloadStringAsync(New Uri("../Texts/sample.rtf", UriKind.RelativeOrAbsolute))


		End Sub

		Private Sub webClient_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
			richEdit1.RtfText = e.Result
		End Sub
	End Class
End Namespace