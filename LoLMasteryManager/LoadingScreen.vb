Public Class LoadingScreen

    Private Sub LoadingScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            With lblLoading
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleCenter
                .Dock = DockStyle.Fill
            End With

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Friend Sub SetCurrentChampion(ByVal championName As String, ByVal role As String)

        Try

            lblLoading.Text = String.Format("Downloading Champion.GG data. This may take a little while.{0}{1} {2}", vbNewLine & vbNewLine, championName, role)

        Catch ex As Exception

            Throw

        End Try

    End Sub

End Class
