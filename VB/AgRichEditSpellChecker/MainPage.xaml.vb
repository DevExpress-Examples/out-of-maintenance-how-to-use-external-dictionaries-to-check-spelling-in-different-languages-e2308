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

Namespace AgRichEditSpellChecker
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()

			richEdit1.ApplyTemplate()

			Dim dict As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("el_GR.dic")
			Dim grammar As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("el_GR.aff")
			Dim dictionary As New SpellCheckerOpenOfficeDictionary()
			Dim ellinika As New CultureInfo("el-GR")
			dictionary.LoadFromStream(dict, grammar, Nothing)
			dictionary.Culture = ellinika

			SpellCheckerHelper.SpellChecker.Dictionaries.Add(dictionary)
			SpellCheckerHelper.SpellChecker.Culture = ellinika
		End Sub

		Private Sub Open_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim webClient As System.Net.WebClient = New WebClient()
			AddHandler webClient.DownloadStringCompleted, AddressOf webClient_DownloadStringCompleted
			webClient.DownloadStringAsync(New Uri("../Texts/sample.rtf", UriKind.Relative))


		End Sub

		Private Sub webClient_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
			richEdit1.RichControl.RtfText = e.Result
		End Sub
	End Class
End Namespace