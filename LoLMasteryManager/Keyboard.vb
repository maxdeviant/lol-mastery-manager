Public Class Keyboard
    Inherits Input

    Public Shared Sub Type(ByVal inputString As String)

        Try

            Lock()

            _InputSimulator.Keyboard.TextEntry(inputString)

        Catch ex As Exception

            Throw

        Finally

            Unlock()

        End Try

    End Sub

End Class
