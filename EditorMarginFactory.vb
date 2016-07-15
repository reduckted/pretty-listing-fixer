Imports Microsoft.VisualStudio.Text.Editor
Imports Microsoft.VisualStudio.Utilities
Imports System.ComponentModel.Composition


<ContentType("text")>
<Export(GetType(IWpfTextViewMarginProvider))>
<MarginContainer(PredefinedMarginNames.Bottom)>
<Name(EditorMargin.EditorMarginName)>
<Order(After:=PredefinedMarginNames.HorizontalScrollBar)>
<TextViewRole(PredefinedTextViewRoles.Interactive)>
Public NotInheritable Class EditorMarginFactory
    Implements IWpfTextViewMarginProvider


    Private ReadOnly cgContext As FixerContext


    <ImportingConstructor()>
    Public Sub New(context As FixerContext)
        cgContext = context
    End Sub


    Public Function CreateMargin(
            wpfTextViewHost As IWpfTextViewHost,
            marginContainer As IWpfTextViewMargin
        ) As IWpfTextViewMargin _
        Implements IWpfTextViewMarginProvider.CreateMargin

        Return New EditorMargin(cgContext)
    End Function

End Class
