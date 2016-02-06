Imports System.IO
Imports System.Threading
Imports Newtonsoft.Json

Public Enum Modes

    ChampionSelect_Old
    ChampionSelect
    Menu

End Enum

Public Enum Stats

    MostFrequent
    HighestWin

End Enum

Public Class MasteryManager

    Private Structure Directories

        Public Shared Data As String
        Public Shared Masteries As String

    End Structure

    Private Structure Paths

        Public Shared Metadata As String
        Public Shared Champions As String

    End Structure

    Private _ChampionGGDownloader As New ChampionGG.Downloader
    Private _Assigner As MasteryAssigner
    Private Shared _LoadingWindow As LoadingScreen = Nothing

    Private _Champions As New List(Of Champion)
    Private _MasteryPages As New List(Of MasteryPage)

    Public Property PatchNumber As String

    Public Sub New()

        Directories.Data = Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, Reflection.Assembly.GetCallingAssembly().GetName().Name)

        If Not Directory.Exists(Directories.Data) Then

            Directory.CreateDirectory(Directories.Data)

        End If

        Directories.Masteries = Path.Combine(Directories.Data, "Masteries")

        If Not Directory.Exists(Directories.Masteries) Then

            Directory.CreateDirectory(Directories.Masteries)

        End If

        Paths.Metadata = Path.Combine(Directories.Data, "Metadata.json")
        Paths.Champions = Path.Combine(Directories.Data, "Champions.json")

        _PatchNumber = _ChampionGGDownloader.ScrapePatchNumber()

        Dim oMetadata As Metadata = LoadMetadata()

        If oMetadata IsNot Nothing Then

            If Not String.Equals(_PatchNumber, oMetadata.PatchNumber) Then

                Directory.Delete(Directories.Data, True)

                Directory.CreateDirectory(Directories.Data)
                Directory.CreateDirectory(Directories.Masteries)

            End If

        End If

        If Not File.Exists(Paths.Champions) Then

            _Champions = _ChampionGGDownloader.ScrapeChampions()

            SaveChampions()

        Else

            _Champions = LoadChampions()

        End If

        If Not Directory.Exists(Directories.Masteries) OrElse Directory.GetFiles(Directories.Masteries).Count = 0 Then

            Dim oThread As New Thread(AddressOf ShowLoadingScreen)

            oThread.SetApartmentState(ApartmentState.STA)

            oThread.Start()

            While _LoadingWindow Is Nothing OrElse Not _LoadingWindow.IsHandleCreated

                Thread.Sleep(300)

            End While

            Dim oMasteryPages As List(Of MasteryPage)

            For Each oChampion As Champion In _Champions

                For Each oRole As Role In oChampion.Roles

                    _LoadingWindow.BeginInvoke(New Action(Sub()
                                                              _LoadingWindow.SetCurrentChampion(oChampion.Name, oRole.Name)
                                                          End Sub))

                    oMasteryPages = _ChampionGGDownloader.ScrapeChampionMasteries(oChampion.Key, oRole.Name)

                    SaveMasteryPages(oMasteryPages)

                Next oRole

            Next oChampion

            _LoadingWindow.BeginInvoke(New Action(Sub()
                                                      _LoadingWindow.Hide()
                                                  End Sub))

        End If

        SaveMetadata()

        _Assigner = New MasteryAssigner(Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), "Coordinates.json"))

    End Sub

    Private Sub ShowLoadingScreen()

        Try

            _LoadingWindow = New LoadingScreen

            _LoadingWindow.Show()

            Application.Run(_LoadingWindow)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMetadata()

        Try

            Dim oMetadata As New Metadata

            With oMetadata
                .PatchNumber = _PatchNumber
                .LastUpdated = Date.UtcNow
            End With

            Dim sMetadataJson As String = JsonConvert.SerializeObject(oMetadata)

            Using oStreamWriter As New StreamWriter(Paths.Metadata)

                oStreamWriter.Write(sMetadataJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function LoadMetadata() As Metadata

        Try

            Dim oMetadata As Metadata = Nothing
            Dim sMetadataJson As String

            If File.Exists(Paths.Metadata) Then

                Using oStreamReader As New StreamReader(Paths.Metadata)

                    sMetadataJson = oStreamReader.ReadToEnd()

                End Using

                oMetadata = JsonConvert.DeserializeObject(Of Metadata)(sMetadataJson)

            End If

            Return oMetadata

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Sub SaveChampions()

        Try

            SaveChampions(_Champions)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveChampions(ByVal champions As List(Of Champion))

        Try

            Dim sChampionsJson As String = JsonConvert.SerializeObject(_Champions)

            Using oStreamWriter As New StreamWriter(Paths.Champions)

                oStreamWriter.Write(sChampionsJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function LoadChampions() As List(Of Champion)

        Try

            Dim oChampions As List(Of Champion) = Nothing
            Dim sChampionsJson As String

            If File.Exists(Paths.Champions) Then

                Using oStreamReader As New StreamReader(Paths.Champions)

                    sChampionsJson = oStreamReader.ReadToEnd()

                End Using

                oChampions = JsonConvert.DeserializeObject(Of List(Of Champion))(sChampionsJson)

            End If

            Return oChampions

        Catch ex As Exception

            Throw

        End Try

    End Function


    Private Sub SaveMasteryPages()

        Try

            SaveMasteryPages(_MasteryPages)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteryPages(ByVal masteryPages As List(Of MasteryPage))

        Try

            Dim oChampion As Champion

            For Each oMasteryPage As MasteryPage In masteryPages

                oChampion = GetChampion(oMasteryPage.ChampionKey)

                oMasteryPage.ChampionName = oChampion.Name

                SaveMasteryPage(oMasteryPage)

            Next oMasteryPage

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteryPage(ByVal masteryPage As MasteryPage)

        Try

            Dim sMasteryPagePath As String = Path.Combine(Directories.Masteries, String.Format("{0}.json", masteryPage.FileName))
            Dim sMasteryPageJson As String = JsonConvert.SerializeObject(masteryPage)

            Using oStreamWriter As New StreamWriter(sMasteryPagePath)

                oStreamWriter.Write(sMasteryPageJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function LoadMasteryPage(ByVal championKey As String, ByVal role As String, ByVal stat As Stats) As MasteryPage

        Try

            Dim sMasteryPageFileName As String = MasteryPage.GenerateFileName(championKey, role, stat)

            Dim oMasteryPage As MasteryPage = LoadMasteryPage(sMasteryPageFileName)

            If oMasteryPage Is Nothing Then

                Dim oChampion As Champion = GetChampion(championKey)

                Throw New Exception(String.Format("Could not find a mastery page for {0} {1}.", oChampion.Name, role))

            End If

            Return LoadMasteryPage(sMasteryPageFileName)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function LoadMasteryPage(ByVal pageFileName As String) As MasteryPage

        Try

            Dim oMasteryPage As MasteryPage = Nothing
            Dim sMasteryPagePath As String = Path.Combine(Directories.Masteries, String.Format("{0}.json", pageFileName))
            Dim sMasteryPageJson As String

            If File.Exists(sMasteryPagePath) Then

                Using oStreamReader As New StreamReader(sMasteryPagePath)

                    sMasteryPageJson = oStreamReader.ReadToEnd()

                End Using

                oMasteryPage = JsonConvert.DeserializeObject(Of MasteryPage)(sMasteryPageJson)

            End If

            Return oMasteryPage

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Function AssignMasteries(ByVal championKey As String, ByVal role As String, ByVal stat As String) As Boolean

        Try

            Dim bResult As Boolean

            If Not String.IsNullOrWhiteSpace(championKey) AndAlso Not String.IsNullOrWhiteSpace(role) Then

                Dim oMasteryPage As MasteryPage
                Dim eStat As Stats

                If Not String.IsNullOrWhiteSpace(stat) Then

                    Select Case stat

                        Case Stats.MostFrequent.GetName()

                            eStat = Stats.MostFrequent

                        Case Stats.HighestWin.GetName()

                            eStat = Stats.HighestWin

                        Case Else

                            eStat = Stats.HighestWin

                    End Select

                    oMasteryPage = LoadMasteryPage(championKey, role, eStat)

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

    Public Function GetChampion(ByVal championKey As String) As Champion

        Try

            Return _Champions.Find(Function(champion) champion.Key.Equals(championKey))

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Function GetChampions() As List(Of Champion)

        Try

            Return _Champions

        Catch ex As Exception

            Throw

        End Try

    End Function

    Public Sub PopulateChampions(ByRef cboChampion As ComboBox)

        Try

            cboChampion.Items.Clear()

            Dim oChampions As List(Of Champion) = GetChampions()

            For Each oChampion As Champion In oChampions

                cboChampion.Items.Add(oChampion)

            Next oChampion

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Sub PopulateRoles(ByRef cboRole As ComboBox, ByVal champion As Champion)

        Try

            cboRole.Items.Clear()

            For Each oRole As Role In champion.Roles

                cboRole.Items.Add(oRole)

            Next oRole

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Public Sub PopulateStats(ByRef cboStats As ComboBox)

        Try

            cboStats.Items.Clear()

            Dim oStats As New List(Of String) From {Stats.MostFrequent.GetName, Stats.HighestWin.GetName}

            For Each sStat As String In oStats

                cboStats.Items.Add(sStat)

            Next sStat

        Catch ex As Exception

            Throw

        End Try

    End Sub

End Class
