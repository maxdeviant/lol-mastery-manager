Public Class Main

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim oChampionGG As New Downloader

        oChampionGG.DownloadMasteries("Elise", "Jungle", Stats.HighestWin)

    End Sub

End Class
