Imports System.IO
Imports Newtonsoft.Json

Public Enum Modes

    ChampionSelect
    Menu

End Enum

Public Class MasteryManager

    Private _DataDirectory As String
    Private _MasteriesDirectory As String

    Private _Downloader As New Downloader
    Private _Assigner As New MasteryAssigner

    Private _Champions As New List(Of Champion)
    Private _MasteryPages As New List(Of MasteryPage)

    Public Sub New()

        _DataDirectory = Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, Reflection.Assembly.GetCallingAssembly().GetName().Name)

        If Not Directory.Exists(_DataDirectory) Then

            Directory.CreateDirectory(_DataDirectory)

        End If

        _MasteriesDirectory = Path.Combine(_DataDirectory, "Masteries")

        If Not Directory.Exists(_MasteriesDirectory) Then

            Directory.CreateDirectory(_MasteriesDirectory)

        End If

        _Champions = _Downloader.ScrapeChampions()

        SaveChampions()

        Dim oMasteryPages As List(Of MasteryPage)

        For Each oChampion As Champion In _Champions

            For Each oRole As Role In oChampion.Roles

                oMasteryPages = _Downloader.ScrapeChampionMasteries(oChampion.Key, oRole.Name)

                SaveMasteryPages(oMasteryPages)

            Next oRole

        Next oChampion

    End Sub

    Private Sub SaveChampions()

        Try

            SaveChampions(_Champions)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveChampions(ByVal champions As List(Of Champion))

        Try

            Dim sChampionsPath As String = Path.Combine(_DataDirectory, "Champions.json")
            Dim sChampionsJson As String = JsonConvert.SerializeObject(_Champions)

            Using oStreamWriter As New StreamWriter(sChampionsPath)

                oStreamWriter.Write(sChampionsJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteryPages()

        Try

            SaveMasteryPages(_MasteryPages)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteryPages(ByVal masteryPages As List(Of MasteryPage))

        Try

            For Each oMasteryPage As MasteryPage In masteryPages

                SaveMasteryPage(oMasteryPage)

            Next oMasteryPage

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteryPage(ByVal masteryPage As MasteryPage)

        Try

            Dim sMasteryPagePath As String = Path.Combine(_MasteriesDirectory, String.Format("{0}.json", masteryPage.Name))
            Dim sMasteryPageJson As String = JsonConvert.SerializeObject(masteryPage)

            Using oStreamWriter As New StreamWriter(sMasteryPagePath)

                oStreamWriter.Write(sMasteryPageJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Function AssignMasteries(ByVal championKey As String, ByVal role As String, ByVal stat As String) As Boolean

        Try

            Dim bResult As Boolean

            If Not String.IsNullOrWhiteSpace(championKey) AndAlso Not String.IsNullOrWhiteSpace(role) Then

                Dim oMasteryPages As List(Of MasteryPage) = _Downloader.ScrapeChampionMasteries(championKey, role)
                Dim oMasteryPage As MasteryPage

                If Not String.IsNullOrWhiteSpace(stat) Then

                    Select Case stat

                        Case "Most Frequent"
                            oMasteryPage = oMasteryPages.Find(Function(p)
                                                                  Return p.Name.Contains("[MF]")
                                                              End Function)

                        Case "Highest Win"
                            oMasteryPage = oMasteryPages.Find(Function(p)
                                                                  Return p.Name.Contains("[HW]")
                                                              End Function)

                        Case Else
                            oMasteryPage = oMasteryPages.Find(Function(p)
                                                                  Return p.Name.Contains("[HW]")
                                                              End Function)

                    End Select

                    _Assigner.Assign(oMasteryPage)

                    bResult = True

                End If

            End If

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Sub SetMode(ByVal mode As Modes)

        Try

            _Assigner.SetMode(mode)

        Catch ex As Exception

            Throw

        End Try

    End Sub

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

            oChampions.Sort(Function(ByVal championA As RiotChampion, ByVal championB As RiotChampion)
                                Return championA.Name.CompareTo(championB.Name)
                            End Function)

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
