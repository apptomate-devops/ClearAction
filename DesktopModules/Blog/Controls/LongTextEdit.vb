'
' DNN Connect - http://dnn-connect.org
' Copyright (c) 2015
' by DNN Connect
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'


Namespace Controls

 Public Class LongTextEdit
  Inherits LocalizedTextEdit

#Region " Private Members "
  Private _showRichTextBox As Boolean = False
  Private pnlMain As System.Web.UI.WebControls.Table
  Private pnlContent As System.Web.UI.WebControls.Table
#End Region

#Region " Protected Methods "
  Protected Overrides Sub CreateChildControls()

   Controls.Clear()
   pnlMain = New System.Web.UI.WebControls.Table
   pnlMain.ID = ID & "_mainTable"
   pnlMain.CssClass = CssPrefix & "default"
   Controls.Add(pnlMain)
   Dim tr As New TableRow
   pnlMain.Rows.Add(tr)
   Dim td As New TableCell
   td.CssClass = CssPrefix & "defaulttext"
   tr.Cells.Add(td)

   If ShowRichTextBox Then
    Dim t As DotNetNuke.UI.UserControls.TextEditor = CType(Page.LoadControl("~/controls/texteditor.ascx"), UI.UserControls.TextEditor)
    With t
     .ID = ID & "_txtMain"
     If TextBoxWidth <> Unit.Pixel(0) Then .Width = TextBoxWidth
     If TextBoxHeight <> Unit.Pixel(0) Then .Height = TextBoxHeight
     .EnableViewState = True
    End With
    td.Controls.Add(t)
   Else
    Dim txtMain As New TextBox
    With txtMain
     .ID = ID & "_txtMain"
     If TextBoxWidth <> Unit.Pixel(0) Then .Width = TextBoxWidth
     If TextBoxHeight <> Unit.Pixel(0) Then .Height = TextBoxHeight
     .TextMode = TextBoxMode.MultiLine
    End With
    td.Controls.Add(txtMain)
   End If

   If SupportedLocales.Count > 1 And ShowTranslations Then
    ' Create max/min cell
    td = New TableCell
    td.CssClass = CssPrefix & "defflag"
    tr.Cells.Add(td)
    Dim img As WebControls.Image
    If UseFlags Then
     img = New WebControls.Image
     With img
      .ImageUrl = DotNetNuke.Common.Globals.ResolveUrl("~/images/flags/" & DefaultLanguage & ".gif")
      .AlternateText = SupportedLocales(DefaultLanguage).Text
     End With
     td.Controls.Add(img)
    Else
     td.Controls.Add(New LiteralControl(SupportedLocales(DefaultLanguage).Text))
    End If
    Dim ib As New ImageButton
    ib.ID = ID & "_cmdMinmax"
    td.Controls.Add(New LiteralControl("&nbsp;"))
    td.Controls.Add(ib)

    ' Make the dropdown box
    pnlBox = New System.Web.UI.WebControls.Panel
    pnlBox.CssClass = CssPrefix & "minmaxbox"
    Controls.Add(pnlBox)
    pnlContent = New System.Web.UI.WebControls.Table
    pnlContent.ID = ID & "_contentTable"
    pnlContent.CssClass = CssPrefix & "localizations"
    pnlBox.Controls.Add(pnlContent)
    DotNetNuke.UI.Utilities.DNNClientAPI.EnableMinMax(ib, pnlContent, -1, Not StartMaximized, DotNetNuke.Common.ResolveUrl("~/images/min.gif"), DotNetNuke.Common.ResolveUrl("~/images/max.gif"), DotNetNuke.UI.Utilities.DNNClientAPI.MinMaxPersistanceType.None)

    For Each localeCode As String In SupportedLocales
     If Not localeCode = DefaultLanguage Then
      ' Add the row
      tr = New TableRow
      pnlContent.Rows.Add(tr)
      td = New TableCell
      td.CssClass = CssPrefix & "localization"
      tr.Cells.Add(td)
      If ShowRichTextBox Then
       Dim t As DotNetNuke.UI.UserControls.TextEditor = CType(Page.LoadControl("~/controls/texteditor.ascx"), UI.UserControls.TextEditor)
       With t
        .ID = ID & "_txt" & localeCode
        If TextBoxWidth <> Unit.Pixel(0) Then .Width = TextBoxWidth
        If TextBoxHeight <> Unit.Pixel(0) Then .Height = TextBoxHeight
        .EnableViewState = True
       End With
       td.Controls.Add(t)
      Else
       Dim tb As New TextBox
       With tb
        .ID = ID & "_txt" & localeCode
        If TextBoxWidth <> Unit.Pixel(0) Then .Width = TextBoxWidth
        If TextBoxHeight <> Unit.Pixel(0) Then .Height = TextBoxHeight
        .TextMode = TextBoxMode.MultiLine
       End With
       td.Controls.Add(tb)
      End If
      td = New TableCell
      td.CssClass = CssPrefix & "flag"
      tr.Cells.Add(td)
      If UseFlags Then
       Dim i As New WebControls.Image
       i.ImageUrl = DotNetNuke.Common.ResolveUrl("~/images/flags/" & localeCode & ".gif")
       i.AlternateText = SupportedLocales(localeCode).Text
       td.Controls.Add(i)
      Else
       td.Controls.Add(New LiteralControl(SupportedLocales(localeCode).Text))
      End If
     End If
    Next
   End If

  End Sub
#End Region

#Region " Public Methods "
  Public Overrides Sub Update()

   If JustUpdated Then Exit Sub
   Try
    If ShowRichTextBox Then
     Dim txtMain As DotNetNuke.UI.UserControls.TextEditor = CType(pnlMain.FindControlByID(ID & "_txtMain"), DotNetNuke.UI.UserControls.TextEditor)
     Dim returnText As String = CStr(txtMain.Text)
     If returnText <> "<p>&#160;</p>" Then
      DefaultText = returnText
     End If
    Else
     Dim txtMain As TextBox = CType(pnlMain.FindControlByID(ID & "_txtMain"), TextBox)
     DefaultText = txtMain.Text
    End If
   Catch ex As Exception
   End Try
   If ShowTranslations Then
    For Each localeCode As String In SupportedLocales
     If Not localeCode = DefaultLanguage Then
      Try
       If ShowRichTextBox Then
        Dim txtBox As DotNetNuke.UI.UserControls.TextEditor = CType(pnlContent.FindControlByID(ID & "_txt" & localeCode), DotNetNuke.UI.UserControls.TextEditor)
        If LocalizedTexts.ContainsKey(localeCode) Then
         LocalizedTexts(localeCode) = CStr(txtBox.Text)
        Else
         LocalizedTexts.Add(localeCode, CStr(txtBox.Text))
        End If
       Else
        Dim txtBox As TextBox = CType(pnlContent.FindControlByID(ID & "_txt" & localeCode), TextBox)
        If LocalizedTexts.ContainsKey(localeCode) Then
         LocalizedTexts(localeCode) = txtBox.Text.Trim
        Else
         LocalizedTexts.Add(localeCode, txtBox.Text.Trim)
        End If
       End If
      Catch ex As Exception
      End Try
     End If
    Next
   End If
   JustUpdated = True

  End Sub

  Public Overrides Sub Rebind()
   Try
    If ShowRichTextBox Then
     Dim txtMain As DotNetNuke.UI.UserControls.TextEditor = CType(pnlMain.FindControlByID(ID & "_txtMain"), DotNetNuke.UI.UserControls.TextEditor)
     txtMain.Text = DefaultText
    Else
     Dim txtMain As TextBox = CType(pnlMain.FindControlByID(ID & "_txtMain"), TextBox)
     txtMain.Text = DefaultText
    End If
   Catch ex As Exception
   End Try
   If ShowTranslations Then
    For Each localeCode As String In SupportedLocales
     If Not localeCode = DefaultLanguage Then
      If ShowRichTextBox Then
       CType(pnlContent.FindControlByID(ID & "_txt" & localeCode), DotNetNuke.UI.UserControls.TextEditor).Text = LocalizedTexts(localeCode)
      Else
       CType(pnlContent.FindControlByID(ID & "_txt" & localeCode), TextBox).Text = LocalizedTexts(localeCode)
      End If
     End If
    Next
   End If
  End Sub
#End Region

#Region " Properties "
  Public Property ShowRichTextBox() As Boolean
   Get
    Return _showRichTextBox
   End Get
   Set(ByVal value As Boolean)
    _showRichTextBox = value
   End Set
  End Property
#End Region

 End Class
End Namespace
