Imports System.Timers

Public Class Main

#If DEBUG Then

    Private WithEvents _Timer As Timer

#End If

    Private _MasteryManager As New MasteryManager

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim oVersion As New Version

            Version.TryParse(ProductVersion, oVersion)

            lblVersion.Text = String.Format("{0}.{1}.{2}", oVersion.Major, oVersion.Minor, oVersion.Build)

            Dim oGitHubLink As New LinkLabel.Link()

            oGitHubLink.LinkData = "https://github.com/maxdeviant/lol-mastery-manager/releases/latest"

            lblVersion.Links.Add(oGitHubLink)

#If DEBUG Then

            _Timer = New Timer(1000)

            _Timer.Enabled = True

#End If

            With cboChampion
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With

            With cboRole
                .DropDownStyle = ComboBoxStyle.DropDownList
            End With

            With cboStats
                .DropDownStyle = ComboBoxStyle.DropDownList
            End With

            _MasteryManager.PopulateChampions(cboChampion)

            cboChampion.SelectedIndex = 0

            _MasteryManager.PopulateRoles(cboRole, CType(cboChampion.SelectedItem, Champion))

            cboRole.SelectedIndex = 0

            _MasteryManager.PopulateStats(cboStats)

            cboStats.SelectedIndex = 1

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub btnAssignMasteries_Click(sender As Object, e As EventArgs) Handles btnAssignMasteries.Click

        Try

            Dim sChampionKey As String = CType(cboChampion.SelectedItem, Champion).Key
            Dim sRole As String = cboRole.SelectedItem.ToString
            Dim sStat As String = cboStats.SelectedItem.ToString

            _MasteryManager.AssignMasteries(sChampionKey, sRole, sStat)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub cboChampion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboChampion.SelectedIndexChanged

        Try

            _MasteryManager.PopulateRoles(cboRole, CType(cboChampion.SelectedItem, Champion))

            cboRole.SelectedIndex = 0

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

    Private Sub lblVersion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblVersion.LinkClicked

        Try

            Process.Start(e.Link.LinkData.ToString)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

#If DEBUG Then

    Private Sub _Timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _Timer.Elapsed

        Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle("PVP.net Client")

        Dim oLeaguePosition = HwndInterface.GetHwndPos(oLeagueWindow)

        Debug.WriteLine(New Point(Cursor.Position.X - oLeaguePosition.X, Cursor.Position.Y - oLeaguePosition.Y))

    End Sub

#End If

End Class
