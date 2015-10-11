Imports System.Runtime.InteropServices
Imports WindowsInput

Public Class Mouse

    Private Shared _InputSimulator As New InputSimulator

    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Public Shared Function BlockInput(<[In], MarshalAs(UnmanagedType.Bool)> ByVal fBlockIt As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

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

    Public Shared Sub Lock()

        Try

            BlockInput(True)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Shared Sub Unlock()

        Try

            BlockInput(False)

        Catch ex As Exception

            Throw

        End Try

    End Sub


End Class