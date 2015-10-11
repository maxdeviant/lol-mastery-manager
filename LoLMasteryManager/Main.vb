Imports System.Timers

Public Class Main

    Private _MasteryManager As New MasteryManager

    Private WithEvents _Timer As Timer

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            _Timer = New Timer(1000)

            _Timer.Enabled = False

            _MasteryManager.PopulateChampions(cboChampion)

            _MasteryManager.PopulateRoles(cboRole)

            _MasteryManager.PopulateStats(cboStats)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub btnAssignMasteries_Click(sender As Object, e As EventArgs) Handles btnAssignMasteries.Click

        Try

            Dim sChampionKey As String = CType(cboChampion.SelectedItem, RiotChampion).Key
            Dim sRole As String = cboRole.SelectedItem.ToString
            Dim sStat As String = cboStats.SelectedItem.ToString

            _MasteryManager.AssignMasteries(sChampionKey, sRole, sStat)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub _Timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _Timer.Elapsed

        Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle("PVP.net Client")

        HwndInterface.ActivateWindow(oLeagueWindow)

        Dim oLeaguePosition = HwndInterface.GetHwndPos(oLeagueWindow)

        Debug.WriteLine(New Point(oLeaguePosition.X - System.Windows.Forms.Cursor.Position.X, oLeaguePosition.Y - System.Windows.Forms.Cursor.Position.Y))

        'Debug.WriteLine(Cursor.Position)

    End Sub

End Class
