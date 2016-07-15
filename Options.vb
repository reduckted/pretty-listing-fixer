Imports Microsoft.VisualStudio.Shell
Imports System.ComponentModel


Public Class Options
    Inherits DialogPage


    Public Sub New()
        Notify = True
        Frequency = 30
    End Sub


    <Category("Settings")>
    <DefaultValue(True)>
    <Description("Displays an alert at the bottom of an editor window when it's discovered that Pretty Listing has been turned off.")>
    <DisplayName("Notify when turned off")>
    Public Property Notify As Boolean


    <Category("Settings")>
    <DefaultValue(30)>
    <Description("How often (in seconds) to check if Pretty Listing has been turned off.")>
    <DisplayName("Polling frequency")>
    Public Property Frequency As Integer

End Class
