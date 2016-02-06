Imports System.Timers
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Class Main

#If DEBUG Then

    Private WithEvents _Timer As Timer

#End If

    Private _MasteryManager As New MasteryManager

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CbClienName.SelectedIndex = 0

        Try

            InitializeApplicationVersion()

            InitializeLoLClientVersion()

            InitializeChampions()

            InitializeRoles()

            InitializeStats()

#If DEBUG Then

            _Timer = New Timer(1000)

            _Timer.Enabled = False


#End If

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub InitializeChampions()

        With cboChampion
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.ListItems
        End With

        _MasteryManager.PopulateChampions(cboChampion)

        cboChampion.SelectedIndex = 0

    End Sub

    Private Sub InitializeRoles()

        With cboRole
            .DropDownStyle = ComboBoxStyle.DropDownList
        End With

        _MasteryManager.PopulateRoles(cboRole, CType(cboChampion.SelectedItem, Champion))

        cboRole.SelectedIndex = 0

    End Sub

    Private Sub InitializeStats()

        With cboStats
            .DropDownStyle = ComboBoxStyle.DropDownList
        End With

        _MasteryManager.PopulateStats(cboStats)

        ' Use the "Highest Win" stat by default
        cboStats.SelectedIndex = 1

    End Sub

    Private Sub InitializeApplicationVersion()

        Dim oVersion As New Version

        Version.TryParse(ProductVersion, oVersion)

        lblVersion.Text = String.Format("{0}.{1}.{2}", oVersion.Major, oVersion.Minor, oVersion.Build)

        Dim oGitHubLink As New LinkLabel.Link()

        oGitHubLink.LinkData = My.Resources.GitHubLatestReleaseUrl

        lblVersion.Links.Add(oGitHubLink)

    End Sub

    Private Sub InitializeLoLClientVersion()

        lblClientVersion.Text = String.Format("Patch {0}", _MasteryManager.PatchNumber)

        Dim oChampionGGLink As New LinkLabel.Link

        oChampionGGLink.LinkData = My.Resources.ChampionGGUrl

        lblClientVersion.Links.Add(oChampionGGLink)

    End Sub
    <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, ExactSpelling:=True, SetLastError:=True)>
    Friend Shared Sub MoveWindow(hwnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Boolean)
    End Sub
    Friend Shared Function GetWindowRect(hWnd As IntPtr, ByRef rect As RECT) As Boolean
    End Function
    Private Sub ChangeSizeWindow()
        Dim processes As Process() = Process.GetProcesses()
        If processes.Length > 0 Then
            For i As Integer = 0 To processes.Length - 1
                If processes(i).MainWindowTitle = CbClienName.Text Then

                    Console.Write("tim thay cua so")

                    MoveWindow(processes(i).MainWindowHandle, 0, 0, 1152, 720, False)

                End If
            Next
        End If

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



    Private Sub lblVersion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblVersion.LinkClicked

        Try

            Process.Start(e.Link.LinkData.ToString)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub lblClientVersion_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblClientVersion.LinkClicked

        Try

            Process.Start(e.Link.LinkData.ToString)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

#If DEBUG Then

    Private Sub _Timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _Timer.Elapsed

        Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle(My.Resources.LeagueClientWindowTitle)


        Dim oLeagueSize = HwndInterface.GetHwndSize(oLeagueWindow)
        Dim oLeaguePosition = HwndInterface.GetHwndPos(oLeagueWindow)

        Debug.WriteLine(My.Resources.LeagueClientWindowTitle)


    End Sub

    Private Sub cboStats_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStats.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbClienName.SelectedIndexChanged
        My.Resources.LeagueClientWindowTitle = CbClienName.Text

        Debug.WriteLine(My.Resources.LeagueClientWindowTitle)
    End Sub

    Private Sub CbMenuOrSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbMenuOrSelect.SelectedIndexChanged
        Try
            If CbMenuOrSelect.SelectedIndex = 0 Then
                _MasteryManager.SetMode(Modes.Menu)
            End If
            If CbMenuOrSelect.SelectedIndex = 1 Then
                _MasteryManager.SetMode(Modes.ChampionSelect_Old)
            End If
            If CbMenuOrSelect.SelectedIndex = 2 Then
                _MasteryManager.SetMode(Modes.ChampionSelect)
            End If
        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ChangeSizeWindow()
    End Sub

#End If

End Class
