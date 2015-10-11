Imports System.Timers

Public Class Main

    Private _MasteryManager As New MasteryManager

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

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

    Private Sub chkInChampionSelect_CheckedChanged(sender As Object, e As EventArgs) Handles chkInChampionSelect.CheckedChanged

        Try

            If chkInChampionSelect.Checked Then

                _MasteryManager.SetMode(Modes.ChampionSelect)

            Else

                _MasteryManager.SetMode(Modes.Menu)

            End If


        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

End Class
