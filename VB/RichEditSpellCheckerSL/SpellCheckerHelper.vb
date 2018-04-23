Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraSpellChecker
Imports System.IO
Imports System.Globalization
Imports System.Reflection
Imports DevExpress.XtraSpellChecker.Native
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.SpellChecker

Namespace RichEditSpellCheckerSL.SpellChecker
	Public NotInheritable Class SpellCheckerHelper
		Private Shared spellChecker_Renamed As DevExpress.Xpf.SpellChecker.SpellChecker
		Private Sub New()
		End Sub
		Public Shared ReadOnly Property SpellChecker() As DevExpress.Xpf.SpellChecker.SpellChecker
			Get
				If spellChecker_Renamed Is Nothing Then
					spellChecker_Renamed = CreateSpellChecker()
				End If
				Return spellChecker_Renamed
			End Get
		End Property
		Private Shared Function CreateSpellChecker() As DevExpress.Xpf.SpellChecker.SpellChecker
			SpellCheckTextControllersManager.Default.RegisterClass(GetType(RichEditControl), GetType(RichEditSpellCheckController))
			Dim spellChecker As New DevExpress.Xpf.SpellChecker.SpellChecker()
			spellChecker.Culture = New CultureInfo("en-US")
			Try
				RegisterDictionary(spellChecker, GetDefaultDictionary())
				RegisterDictionary(spellChecker, GetCustomDictionary(spellChecker))
			Catch
			End Try
			Return spellChecker
		End Function
		Private Shared Function GetDefaultDictionary() As SpellCheckerDictionaryBase
			Dim dic As New SpellCheckerISpellDictionary()
			dic.LoadFromStream(GetDataStream("american.xlg"), GetDataStream("english.aff"), GetDataStream("EnglishAlphabet.txt"))
			Return dic
		End Function
		Private Shared Function GetDataStream(ByVal fileName As String) As Stream
			If String.IsNullOrEmpty(fileName) Then
				Return Nothing
			End If
			Dim path As String = "RichEditSpellCheckerSL.DefaultDictionary." & fileName
			Return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path)
		End Function
		Private Shared Sub RegisterDictionary(ByVal spellChecker As DevExpress.Xpf.SpellChecker.SpellChecker, ByVal dic As SpellCheckerDictionaryBase)
			dic.Culture = spellChecker.Culture
			spellChecker.Dictionaries.Add(dic)
		End Sub
		Private Shared Function GetCustomDictionary(ByVal spellChecker As DevExpress.Xpf.SpellChecker.SpellChecker) As SpellCheckerDictionaryBase
			Return New SpellCheckerCustomDictionary("CustomEnglish.dic", spellChecker.Culture)
		End Function
	End Class
End Namespace
