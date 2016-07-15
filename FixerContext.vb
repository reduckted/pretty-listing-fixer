Imports System.ComponentModel.Composition


<Export()>
Public Class FixerContext

    Private ReadOnly cgListeners As New List(Of INotificationListener)
    Private cgNotify As Boolean


    Public Sub AddNotificationListener(listener As INotificationListener)
        SyncLock cgListeners
            cgListeners.Add(listener)
        End SyncLock
    End Sub


    Public Sub RemoveNotificationListener(listener As INotificationListener)
        SyncLock cgListeners
            cgListeners.Remove(listener)
        End SyncLock
    End Sub


    Public Property Notify As Boolean
        Get
            Return cgNotify
        End Get

        Set(value As Boolean)
            If cgNotify <> value Then
                cgNotify = value

                SyncLock cgListeners
                    For Each item In cgListeners
                        item.OnChanged()
                    Next item
                End SyncLock
            End If
        End Set
    End Property

End Class
