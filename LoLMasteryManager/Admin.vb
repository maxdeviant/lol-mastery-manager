Imports System.IO
Imports Newtonsoft.Json

Public Class Admin

    Private Sub GenerateCoordinates()

        Try

            Dim oMasteryCoordinatesFile As New MasteryCoordinatesFile

            Dim sMenuScreenshotPath As String = "D:\LoL Mastery Manager\Client - Menu.png"
            Dim sChampionSelectScreenshotPath As String = "D:\LoL Mastery Manager\Client - Champion Select.png"
            Dim sMasteryIconDirectory As String = "D:\LoL Mastery Manager\Masteries"

            Dim oLocator As New MasteryLocator
            Dim oMasteryCoordinateFile As New MasteryCoordinatesFile
            Dim oPosition As Point

            oMasteryCoordinateFile.ReferenceClientSize = New Size(1280, 800)

            For Each sMasteryID As String In LoadMasteries().Keys

                oPosition = oLocator.GetMasteryPosition(sMenuScreenshotPath, Path.Combine(sMasteryIconDirectory, sMasteryID & ".png"))

                oMasteryCoordinateFile.MasteryCoordinatesMenu.Add(sMasteryID, oPosition)

                oPosition = oLocator.GetMasteryPosition(sChampionSelectScreenshotPath, Path.Combine(sMasteryIconDirectory, sMasteryID & ".png"))

                oMasteryCoordinateFile.MasteryCoordinatesChampionSelect.Add(sMasteryID, oPosition)

            Next sMasteryID

            SaveMasteryCoordinates(oMasteryCoordinateFile)

            MessageBox.Show("Saved mastery coordinates to file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function LoadMasteries() As Dictionary(Of String, RiotMastery)

        Try

            Dim sJson As String

            Dim sMasteriesPath As String = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), "Masteries.json")

            Using oStreamReader As New StreamReader(sMasteriesPath)

                sJson = oStreamReader.ReadToEnd()

            End Using

            Dim oRiotMasteries As Dictionary(Of String, RiotMastery) = JsonConvert.DeserializeObject(Of RiotMasteryListFile)(sJson).Masteries

            Return oRiotMasteries

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Sub SaveMasteryCoordinates(ByVal masteryCoordinateListFile As MasteryCoordinatesFile)

        Try

            Dim sMasteryCoordinatesPath As String = Path.Combine("D:\LoL Mastery Manager", "Coordinates.json")
            Dim sMasteryCoordinatesJson As String = JsonConvert.SerializeObject(masteryCoordinateListFile)

            Using oStreamWriter As New StreamWriter(sMasteryCoordinatesPath)

                oStreamWriter.Write(sMasteryCoordinatesJson)

            End Using

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub btnGenerateCoordinates_Click(sender As Object, e As EventArgs) Handles btnGenerateCoordinates.Click

        Try

            GenerateCoordinates()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

End Class
