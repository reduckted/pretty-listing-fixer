Imports EnvDTE
Imports EnvDTE80
Imports Microsoft.VisualStudio
Imports Microsoft.VisualStudio.Shell
Imports System
Imports System.Runtime.InteropServices
Imports System.Threading


<Guid(FixerPackage.PackageGuidString)>
<InstalledProductRegistration("#110", "#112", "1.0", IconResourceID:=1400)>
<PackageRegistration(UseManagedResourcesOnly:=True, AllowsBackgroundLoading:=True)>
<ProvideAutoLoad(VSConstants.UICONTEXT.SolutionOpening_string, PackageAutoLoadFlags.BackgroundLoad)>
Public NotInheritable Class FixerPackage
    Inherits AsyncPackage


    Public Const PackageGuidString As String = "ff021410-a239-46d0-a40b-fe4806b93ac8"


    Private Shared ReadOnly Frequency As TimeSpan = TimeSpan.FromSeconds(10)


    Protected Overrides Async Function InitializeAsync(cancellationToken As CancellationToken, progress As IProgress(Of ServiceProgressData)) As Tasks.Task
        Dim dte As DTE2


        Await JoinableTaskFactory.SwitchToMainThreadAsync()

        dte = DirectCast(GetService(GetType(DTE)), DTE2)

        CheckPrettyListing(dte)
    End Function


    Private Sub CheckPrettyListing(dte As DTE2)
        Dim pretty As [Property]


        pretty = dte.Properties("TextEditor", "Basic-Specific")?.Item("PrettyListing")

        If (pretty IsNot Nothing) AndAlso (TypeOf pretty.Value Is Boolean) Then
            If Not CBool(pretty.Value) Then
                pretty.Value = True
            End If
        End If

        Tasks.Task.Delay(Frequency).ContinueWith(Sub(x) CheckPrettyListing(dte))
    End Sub

End Class
