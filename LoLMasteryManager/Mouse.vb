Public Class Mouse
    Inherits Input


    Public Shared Sub LeftClick()

        Try

            _InputSimulator.Mouse.LeftButtonClick()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Shared Sub Move(ByVal point As Point)

        Try

            Cursor.Position = point

        Catch ex As Exception

            Throw

        End Try

    End Sub

End Class