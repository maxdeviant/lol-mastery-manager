Public Class Main

    Private _MasteryManager As New MasteryManager

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _MasteryManager.PopulateChampions(cboChampion)

        _MasteryManager.PopulateRoles(cboRole)

        _MasteryManager.PopulateStats(cboStats)

        Dim oChampionGG As New Downloader

        'oChampionGG.DownloadMasteries("Elise", "Jungle")

    End Sub

    Private Sub btnAssignMasteries_Click(sender As Object, e As EventArgs) Handles btnAssignMasteries.Click

        Dim sChampionKey As String = CType(cboChampion.SelectedItem, RiotChampion).Key
        Dim sRole As String = cboRole.SelectedItem.ToString
        Dim sStat As String = cboStats.SelectedItem.ToString

        _MasteryManager.AssignMasteries(sChampionKey, sRole, sStat)

    End Sub

End Class
