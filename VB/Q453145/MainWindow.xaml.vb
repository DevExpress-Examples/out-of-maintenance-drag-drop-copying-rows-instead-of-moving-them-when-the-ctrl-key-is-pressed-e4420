Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Grid
Imports System.Diagnostics

Namespace Q453145

    Public Partial Class MainWindow
        Inherits System.Windows.Window

        Public Sub New()
            Me.InitializeComponent()
            Me.treeListControl1.ItemsSource = Me.CreateData(10)
        End Sub

        Private Function CreateData(ByVal length As Integer) As List(Of Q453145.DataItem)
            Dim list As System.Collections.Generic.List(Of Q453145.DataItem) = New System.Collections.Generic.List(Of Q453145.DataItem)()
            For i As Integer = 0 To length - 1
                list.Add(New Q453145.DataItem() With {.ParentId = i Mod 2, .Name = "Item " & i})
            Next

            Return list
        End Function

        Private Sub treeListControl1_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs)
            If e.Key = System.Windows.Input.Key.LeftCtrl AndAlso Me.dropManager.DraggingRows IsNot Nothing AndAlso Me.dropManager.DraggingRows.Count > 0 Then
                If e.IsUp Then
                    Me.dropManager.ViewInfo.SetValue(Q453145.CopyRowsHelper.CopyRowsProperty, False)
                ElseIf Not CBool(Me.dropManager.ViewInfo.GetValue(Q453145.CopyRowsHelper.CopyRowsProperty)) Then
                    Me.dropManager.ViewInfo.SetValue(Q453145.CopyRowsHelper.CopyRowsProperty, True)
                End If
            End If
        End Sub

        Private Sub dropManager_Drop(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.DragDrop.TreeListDropEventArgs)
            If CBool(e.Manager.ViewInfo.GetValue(Q453145.CopyRowsHelper.CopyRowsProperty)) Then
                For Each node As DevExpress.Xpf.Grid.TreeListNode In e.DraggedRows
                    Dim item As Q453145.DataItem = TryCast(node.Content, Q453145.DataItem)
                    node.Content = New Q453145.DataItem() With {.Name = item.Name}
                Next
            End If
        End Sub
    End Class

    Public Class DataItem

        Private Shared IdSequence As Integer = 0

        Public Property Id As Integer

        Public Property ParentId As Integer

        Public Property Name As String

        Public Sub New()
            Me.Id = System.Math.Min(System.Threading.Interlocked.Increment(Q453145.DataItem.IdSequence), Q453145.DataItem.IdSequence - 1)
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Class CopyRowsHelper
        Inherits System.Windows.DependencyObject

        Public Shared ReadOnly CopyRowsProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.RegisterAttached("CopyRows", GetType(Boolean), GetType(Q453145.CopyRowsHelper), New System.Windows.UIPropertyMetadata(False))

        Public Shared Function GetCopyRows(ByVal target As System.Windows.DependencyObject) As Boolean
            Return CBool(target.GetValue(Q453145.CopyRowsHelper.CopyRowsProperty))
        End Function

        Public Shared Sub SetCopyRows(ByVal target As System.Windows.DependencyObject, ByVal value As Boolean)
            target.SetValue(Q453145.CopyRowsHelper.CopyRowsProperty, value)
        End Sub
    End Class
End Namespace
