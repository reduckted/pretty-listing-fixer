Imports Microsoft.VisualStudio.Text.Editor
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media


Public NotInheritable Class EditorMargin
    Inherits Border
    Implements IWpfTextViewMargin
    Implements INotificationListener


    Public Const EditorMarginName As String = "FixerEditorMargin"


    Private ReadOnly cgContext As FixerContext


    Public Sub New(context As FixerContext)
        Dim grid As Grid
        Dim label As TextBlock
        Dim button As Button


        cgContext = context

        grid = New Grid
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = GridLength.Auto})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = GridLength.Auto})

        label = New TextBlock With {
            .Background = Brushes.Transparent,
            .Text = "Pretty Listing was turned off! It's back on now. :)",
            .Foreground = Brushes.White,
            .VerticalAlignment = VerticalAlignment.Center,
            .Margin = New Thickness(5, 0, 0, 0)
        }

        button = New Button With {
            .Content = New TextBlock With {.Text = "OK"},
            .VerticalAlignment = VerticalAlignment.Center,
            .Width = 75,
            .Margin = New Thickness(10, 0, 0, 0)
        }

        AddHandler button.Click, Sub() cgContext.Notify = False

        Grid.SetColumn(label, 0)
        Grid.SetColumn(button, 1)

        grid.Children.Add(label)
        grid.Children.Add(button)

        ClipToBounds = True
        Background = Brushes.OrangeRed
        Child = grid

        cgContext.AddNotificationListener(Me)

        SetHeight()
    End Sub


    Public ReadOnly Property VisualElement As FrameworkElement _
        Implements IWpfTextViewMargin.VisualElement

        Get
            Return Me
        End Get
    End Property


    Public ReadOnly Property MarginSize As Double _
        Implements IWpfTextViewMargin.MarginSize

        Get
            Return ActualHeight
        End Get
    End Property


    Public ReadOnly Property Enabled As Boolean _
        Implements IWpfTextViewMargin.Enabled

        Get
            Return True
        End Get
    End Property


    Public Function GetTextViewMargin(marginName As String) As ITextViewMargin _
        Implements IWpfTextViewMargin.GetTextViewMargin

        If String.Equals(marginName, EditorMarginName, StringComparison.OrdinalIgnoreCase) Then
            Return Me
        End If

        Return Nothing
    End Function


    Public Sub OnChanged() _
        Implements INotificationListener.OnChanged

        If Not Dispatcher.CheckAccess() Then
            Dispatcher.BeginInvoke(Sub() OnChanged())
            Return
        End If

        SetHeight()
    End Sub


    Private Sub SetHeight()
        If cgContext.Notify Then
            Height = 32
        Else
            Height = 0
        End If
    End Sub


    Public Sub Dispose() _
        Implements IDisposable.Dispose

        cgContext.RemoveNotificationListener(Me)
    End Sub

End Class
