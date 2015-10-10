Imports System.IO
Imports Newtonsoft.Json

Public Class MasteryManager

    Private _Downloader As New Downloader

    Public Function AssignMasteries(ByVal championKey As String, ByVal role As String, ByVal stat As String) As Boolean

        Try

            Dim bResult As Boolean

            If Not String.IsNullOrWhiteSpace(championKey) AndAlso Not String.IsNullOrWhiteSpace(role) Then

                Dim oMasteryPages As List(Of MasteryPage) = _Downloader.DownloadMasteries(championKey, role)

                bResult = True

            End If

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Function GetChampions() As List(Of RiotChampion)

        Try

            Dim sJson As String

            Dim sChampionsPath As String = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), "Data", "Champions.json")

            Using oStreamReader As New StreamReader(sChampionsPath)

                sJson = oStreamReader.ReadToEnd()

            End Using

            Dim oRiotChampions As Dictionary(Of String, RiotChampion) = JsonConvert.DeserializeObject(Of RiotChampionListFile)(sJson).Champions
            Dim oChampions As New List(Of RiotChampion)

            For Each sKey As String In oRiotChampions.Keys

                oChampions.Add(oRiotChampions(sKey))

            Next sKey

            Return oChampions

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Sub PopulateChampions(ByRef cboChampion As ComboBox)

        Try

            Dim oChampions As List(Of RiotChampion) = GetChampions()

            For Each oChampion As RiotChampion In oChampions

                cboChampion.Items.Add(oChampion)

            Next oChampion

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Sub PopulateRoles(ByRef cboRole As ComboBox)

        Try

            Dim oRoles As New List(Of String) From {Roles.Top, Roles.Jungle, Roles.Middle, Roles.ADC, Roles.Support}

            For Each sRole As String In oRoles

                cboRole.Items.Add(sRole)

            Next sRole

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Sub PopulateStats(ByRef cboStat As ComboBox)

        Try

            Dim oStats As New List(Of String) From {"Most Frequent", "Highest Win"}

            For Each sStat As String In oStats

                cboStat.Items.Add(sStat)

            Next sStat

        Catch ex As Exception

            Throw

        End Try

    End Sub

End Class
