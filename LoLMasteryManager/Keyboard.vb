Public Class Keyboard
    Inherits Input

    Public Shared Sub Type(ByVal inputString As String)

        Try

            _InputSimulator.Keyboard.TextEntry(inputString)

        Catch ex As Exception

            Throw

        End Try

    End Sub

End Class
