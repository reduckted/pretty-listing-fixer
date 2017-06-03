Imports EnvDTE
Imports Microsoft.VisualStudio.ComponentModelHost
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Shell.Interop
Imports System
Imports System.Runtime.InteropServices
Imports System.Threading


<Guid(FixerPackage.PackageGuidString)>
<InstalledProductRegistration("#110", "#112", "1.0", IconResourceID:=1400)>
<PackageRegistration(UseManagedResourcesOnly:=True)>
<ProvideAutoLoad(UIContextGuids80.SolutionExists)>
Public NotInheritable Class FixerPackage
    Inherits Package


    Public Const PackageGuidString As String = "ff021410-a239-46d0-a40b-fe4806b93ac8"


    Private Shared ReadOnly Frequency As TimeSpan = TimeSpan.FromSeconds(10)


    Protected Overrides Sub Initialize()
        Dim dte As DTE
        Dim provider As ServiceProvider
        Dim componentModel As IComponentModel


        dte = DirectCast(GetService(GetType(DTE)), DTE)

        provider = New ServiceProvider(DirectCast(dte, Microsoft.VisualStudio.OLE.Interop.IServiceProvider))
        componentModel = DirectCast(provider.GetService(GetType(SComponentModel)), IComponentModel)

        Tasks.Task.Delay(5000).ContinueWith(Sub() CheckPrettyListing())
    End Sub


    Private Sub CheckPrettyListing()
        Dim dte As DTE


        dte = DirectCast(GetService(GetType(DTE)), DTE)

        If dte IsNot Nothing Then
            Dim pretty As [Property]


            pretty = dte.Properties("TextEditor", "Basic-Specific")?.Item("PrettyListing")

            If (pretty IsNot Nothing) AndAlso (TypeOf pretty.Value Is Boolean) Then
                If Not CBool(pretty.Value) Then
                    pretty.Value = True
                End If
            End If
        End If

        Tasks.Task.Delay(Frequency).ContinueWith(Sub(x) CheckPrettyListing())
    End Sub

End Class
