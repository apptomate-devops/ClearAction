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

Imports DotNetNuke.Modules.Blog.Security.Security

Namespace Security.Permissions

 <Serializable()>
 Public Class PermissionCollection
  Implements IDictionary(Of String, PermissionInfo)

  Private _permissions As New Dictionary(Of String, PermissionInfo)
  Private _keys As New SortedDictionary(Of Integer, String)

  Public Sub New()
   Add("ADD", New PermissionInfo With {.PermissionId = BlogPermissionTypes.ADD, .PermissionKey = "ADD"})
   Add("EDIT", New PermissionInfo With {.PermissionId = BlogPermissionTypes.EDIT, .PermissionKey = "EDIT"})
   Add("APPROVE", New PermissionInfo With {.PermissionId = BlogPermissionTypes.APPROVE, .PermissionKey = "APPROVE"})
   Add("VIEWCOMMENT", New PermissionInfo With {.PermissionId = BlogPermissionTypes.VIEWCOMMENT, .PermissionKey = "VIEWCOMMENT"})
   Add("ADDCOMMENT", New PermissionInfo With {.PermissionId = BlogPermissionTypes.ADDCOMMENT, .PermissionKey = "ADDCOMMENT"})
   Add("APPROVECOMMENT", New PermissionInfo With {.PermissionId = BlogPermissionTypes.APPROVECOMMENT, .PermissionKey = "APPROVECOMMENT"})
   Add("AUTOAPPROVECOMMENT", New PermissionInfo With {.PermissionId = BlogPermissionTypes.AUTOAPPROVECOMMENT, .PermissionKey = "AUTOAPPROVECOMMENT"})
  End Sub

  Public Function GetById(id As Integer) As PermissionInfo
   Select Case id
    Case BlogPermissionTypes.ADD
     Return Me("ADD")
    Case BlogPermissionTypes.EDIT
     Return Me("EDIT")
    Case BlogPermissionTypes.APPROVE
     Return Me("APPROVE")
    Case BlogPermissionTypes.ADDCOMMENT
     Return Me("ADDCOMMENT")
    Case BlogPermissionTypes.APPROVECOMMENT
     Return Me("APPROVECOMMENT")
    Case BlogPermissionTypes.AUTOAPPROVECOMMENT
     Return Me("AUTOAPPROVECOMMENT")
    Case BlogPermissionTypes.VIEWCOMMENT
     Return Me("VIEWCOMMENT")
   End Select
   Return Nothing
  End Function

  Public Sub Add(item As System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).Add
   If Not ContainsKey(item.Key) Then
    _permissions.Add(item.Key, item.Value)
    _keys.Add(_permissions.Count - 1, item.Key)
   End If
  End Sub

  Public Sub Clear() Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).Clear
   _permissions.Clear()
   _keys.Clear()
  End Sub

  Public Function Contains(item As System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).Contains
   Return _permissions.ContainsValue(item.Value)
  End Function

  Public Sub CopyTo(array() As System.Collections.Generic.KeyValuePair(Of String, PermissionInfo), arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).CopyTo
   'todo
  End Sub

  Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).Count
   Get
    Return _permissions.Count
   End Get
  End Property

  Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).IsReadOnly
   Get
    Return False
   End Get
  End Property

  Public Function Remove(item As System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).Remove
   If _permissions.ContainsKey(item.Key) Then
    _permissions.Remove(item.Key)
    Return True
   End If
   Return False
  End Function

  Public Sub Add(key As String, value As PermissionInfo) Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).Add
   If Not _permissions.ContainsKey(key) Then
    _permissions.Add(key, value)
    _keys.Add(_permissions.Count - 1, key)
   End If
  End Sub

  Public Function ContainsKey(key As String) As Boolean Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).ContainsKey
   Return _permissions.ContainsKey(key)
  End Function

  Default Public Property Item(key As String) As PermissionInfo Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).Item
   Get
    Return _permissions(key)
   End Get
   Set(value As PermissionInfo)
    _permissions(key) = value
   End Set
  End Property

  Default Public Property Item(index As Integer) As PermissionInfo
   Get
    Return Item(_keys(index))
   End Get
   Set(value As PermissionInfo)
    Item(_keys(index)) = value
   End Set
  End Property

  Public ReadOnly Property Keys() As System.Collections.Generic.ICollection(Of String) Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).Keys
   Get
    Return _keys.Values
   End Get
  End Property

  Public Function Remove(key As String) As Boolean Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).Remove
   If _permissions.ContainsKey(key) Then
    _permissions.Remove(key)
    Return True
   End If
   Return False
  End Function

  Public Function TryGetValue(key As String, ByRef value As PermissionInfo) As Boolean Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).TryGetValue
   Return _permissions.TryGetValue(key, value)
  End Function

  Public ReadOnly Property Values() As System.Collections.Generic.ICollection(Of PermissionInfo) Implements System.Collections.Generic.IDictionary(Of String, PermissionInfo).Values
   Get
    Return _permissions.Values
   End Get
  End Property

  Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of String, PermissionInfo)).GetEnumerator
   Return Nothing
  End Function

  Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
   Return Nothing
  End Function

 End Class
End Namespace
