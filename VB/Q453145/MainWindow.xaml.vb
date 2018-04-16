Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Grid
Imports System.Diagnostics

Namespace Q453145
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			treeListControl1.ItemsSource = CreateData(10)
		End Sub

		Private Function CreateData(ByVal length As Integer) As List(Of DataItem)
			Dim list As New List(Of DataItem)()
			For i As Integer = 0 To length - 1
				list.Add(New DataItem() With {.ParentId = i Mod 2, .Name = "Item " & i})
			Next i
			Return list
		End Function

		Private Sub treeListControl1_PreviewKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
			If e.Key = Key.LeftCtrl AndAlso dropManager.DraggingRows IsNot Nothing AndAlso dropManager.DraggingRows.Count > 0 Then
				If e.IsUp Then
					dropManager.ViewInfo.SetValue(CopyRowsHelper.CopyRowsProperty, False)
				ElseIf (Not CBool(dropManager.ViewInfo.GetValue(CopyRowsHelper.CopyRowsProperty))) Then
					dropManager.ViewInfo.SetValue(CopyRowsHelper.CopyRowsProperty, True)
				End If
			End If
		End Sub

		Private Sub dropManager_Drop(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.DragDrop.TreeListDropEventArgs)
			If CBool(e.Manager.ViewInfo.GetValue(CopyRowsHelper.CopyRowsProperty)) Then
				For Each node As TreeListNode In e.DraggedRows
					Dim item As DataItem = TryCast(node.Content, DataItem)
					node.Content = New DataItem() With {.Name = item.Name}
				Next node
			End If
		End Sub
	End Class

	Public Class DataItem
		Private Shared IdSequence As Integer = 0
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateParentId As Integer
		Public Property ParentId() As Integer
			Get
				Return privateParentId
			End Get
			Set(ByVal value As Integer)
				privateParentId = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property

		Public Sub New()
			Id = IdSequence
			IdSequence += 1
		End Sub

		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class

	Public Class CopyRowsHelper
		Inherits DependencyObject
		Public Shared ReadOnly CopyRowsProperty As DependencyProperty = DependencyProperty.RegisterAttached("CopyRows", GetType(Boolean), GetType(CopyRowsHelper), New UIPropertyMetadata(False))

		Public Shared Function GetCopyRows(ByVal target As DependencyObject) As Boolean
			Return CBool(target.GetValue(CopyRowsProperty))
		End Function
		Public Shared Sub SetCopyRows(ByVal target As DependencyObject, ByVal value As Boolean)
			target.SetValue(CopyRowsProperty, value)
		End Sub
	End Class
End Namespace
