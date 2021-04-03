Imports DotNetNuke.Modules.Blog.Data
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DotNetNuke.Modules.Blog.Security


Namespace DAL2.BlogContent

    Public Class Util

        Public Shared PostKey As String = "ContentItemId"
        Public Shared Function GetBlogParam(ByVal iContentItemID As Integer, UpdateStatus As Boolean, ByVal Title As String) As List(Of String)
            Dim additionalParameters As New List(Of String)()
            additionalParameters.Add(DAL2.BlogContent.Util.PostKey & "=" & iContentItemID.ToString())

            If Not String.IsNullOrEmpty(Title) Then
                Dim replaced As String = System.Text.RegularExpressions.Regex.Replace(Title, "[^a-zA-Z0-9]", " ")

                Title = replaced.Replace(" ", "-")

                additionalParameters.Add(Title)
            End If
            If UpdateStatus Then
                additionalParameters.Add("UpdateStatus=True")
            End If
            Return additionalParameters
        End Function

        Public Shared Function GetFormattedUrl(ByVal iTabId As Integer, ByVal additionalParameters As List(Of String)) As String

            Dim parameters As String() = New String(additionalParameters.Count - 1) {}
            For i As Integer = 0 To additionalParameters.Count - 1
                parameters(i) = additionalParameters(i)
            Next

            Return DotNetNuke.Common.Globals.NavigateURL(iTabId, "", parameters)
        End Function

        Public Shared Function GetFormattedUrl(ByVal additionalParameters As List(Of String)) As String

            Dim parameters As String() = New String(additionalParameters.Count - 1) {}
            For i As Integer = 0 To additionalParameters.Count - 1
                parameters(i) = additionalParameters(i)
            Next
            Dim iTabID As Integer = DotNetNuke.Entities.Tabs.TabController.Instance.GetTabByName("Insights", 0).TabID
            Return DotNetNuke.Common.Globals.NavigateURL(iTabId, "", parameters)
        End Function

        Public Shared ComponentID As Integer = 2


        Public Shared Function HumanFriendlyDate(newDate As DateTime, ByVal IsFormatonly As Boolean, ShowTime As Boolean) As String
            If IsFormatonly = False Then
                Return HumanFriendlyDate(newDate)
            End If

            Dim format As String
            If ShowTime Then
                format = "MMM dd, yyyy  hh:mm tt"
            Else
                format = "MMMM dd, yyyy"

            End If
            Return String.Format(newDate.ToString(format))


        End Function
        Public Shared Function HumanFriendlyDate(newDate As DateTime) As String
            '  Dim newDate = DateTime.Parse(GetDate(displayDate, instanceId, timeZoneOffset))
            Dim ts As TimeSpan = New TimeSpan(DateTime.Now.Ticks - newDate.Ticks)
            Dim delta As Double = ts.TotalSeconds
            If delta <= 1 Then
                Return "second ago"
            End If

            If delta < 60 Then
                Return String.Format("{0} seconds", ts.Seconds)
            End If

            If delta < 120 Then
                '   Return GetSharedResource("[RESX:TimeSpan:MinuteAgo]")
                Return String.Format("{0} mins", ts.Minutes)
            End If

            If delta < (45 * 60) Then
                Return String.Format("{0} mins", ts.Minutes)

            End If

            If delta < (90 * 60) Then
                '    Return GetSharedResource("[RESX:TimeSpan:HourAgo]")
                Return String.Format("{0} Hour Ago", ts.Hours)
            End If

            If delta < (24 * 60 * 60) Then
                '   Return String.Format(GetSharedResource("[RESX:TimeSpan:HoursAgo]"), ts.Hours)
                Return String.Format("{0} Hours Ago", ts.Hours)
            End If

            If delta < (48 * 60 * 60) Then
                'Return GetSharedResource("[RESX:TimeSpan:DayAgo]")
                Return String.Format("{0} day Ago", ts.Days)
            End If

            If delta < (72 * 60 * 60) Then
                ' Return String.Format(GetSharedResource("[RESX:TimeSpan:DaysAgo]"), ts.Days)
                Return String.Format("{0} days Ago", ts.Days)
            End If

            If delta < Convert.ToDouble(New TimeSpan(24 * 32, 0, 0).TotalSeconds) Then
                'Return GetSharedResource("[RESX:TimeSpan:MonthAgo]")
                Return String.Format("{0} Month Ago", Math.Ceiling(ts.Days / 30.0))


            End If

            If delta < Convert.ToDouble(New TimeSpan(((24 * 30) * 11), 0, 0).TotalSeconds) Then
                Return String.Format("{0} Months Ago", Math.Ceiling(ts.Days / 30.0))
            End If

            If delta < Convert.ToDouble(New TimeSpan(((24 * 30) * 18), 0, 0).TotalSeconds) Then
                ' Return GetSharedResource("[RESX:TimeSpan:YearAgo]")
                Return String.Format("1 Year Ago")
            End If

            Return String.Format("{0} Years", Math.Ceiling(ts.Days / 365.0))

        End Function

    End Class

End Namespace
