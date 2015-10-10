Imports System.Net.Http
Imports HtmlAgilityPack

Module ChampionGG

    Public Enum Stats

        MostFrequent
        HighestWin

    End Enum

    Public Structure Roles

        Public Shared Top As String = "Top"
        Public Shared Jungle As String = "Jungle"
        Public Shared Middle As String = "Middle"
        Public Shared ADC As String = "ADC"
        Public Shared Support As String = "Support"

    End Structure

    Public Class Downloader

        Public Shared BaseUrl As String = "http://champion.gg/"

        Public Function DownloadMasteries(ByVal championName As String, ByVal role As String, ByVal stats As Stats) As List(Of MasteryPage)

            Try

                Dim oMasteryPages As New List(Of MasteryPage)

                Using oWeb As New HttpClient

                    oWeb.BaseAddress = New Uri(BaseUrl)

                    Dim oResponse As HttpResponseMessage
                    Dim sRequestUrl As String = String.Format("champion/{0}/{1}", championName, role)

                    oResponse = oWeb.GetAsync(sRequestUrl).Result

                    Dim sHTML As String = oResponse.Content.ReadAsStringAsync().Result
                    Dim oDocument As New HtmlDocument

                    oDocument.LoadHtml(sHTML)

                    For Each oMasteryContainer As HtmlNode In oDocument.DocumentNode.SelectNodes("//div[contains(@class, 'mastery-container')]")

                        oMasteryPages.Add(BuildMasteryPage(GenerateMasteryPageName(championName, role, stats), ParseMasteries(oMasteryContainer)))

                    Next oMasteryContainer

                End Using

                Return oMasteryPages

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function GenerateMasteryPageName(ByVal championName As String, ByVal role As String, ByVal stats As Stats) As String

            Try

                Dim sStats As String

                Select Case stats

                    Case Stats.MostFrequent
                        sStats = "MF"

                    Case Stats.HighestWin
                        sStats = "HW"

                    Case Else
                        Throw New ArgumentException(String.Format("Unrecognized stat type '{0}'.", stats))

                End Select

                Return String.Format("[{0}] {1} - {2}", sStats, championName, role)

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function ParseMasteries(ByVal container As HtmlNode) As List(Of Mastery)

            Try

                Dim oMasteries As New List(Of Mastery)
                Dim oMastery As Mastery
                Dim oMasteryRows As HtmlNodeCollection = container.SelectNodes("descendant::div[contains(@class, 'mastery-row')]")

                For Each oMasteryRow As HtmlNode In oMasteryRows

                    For Each oMasteryNode As HtmlNode In oMasteryRow.SelectNodes("descendant::div[contains(@class, 'mastery-icon')]")

                        Dim iMasteryID As Integer = CInt(oMasteryNode.Attributes.Item("api-primary-id").Value)

                        oMastery = GetMastery(iMasteryID)

                        oMastery.Ranks = CountMasteryRanks(oMasteryNode)

                        oMasteries.Add(oMastery)

                    Next oMasteryNode

                Next oMasteryRow

                Return oMasteries

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function BuildMasteryPage(ByVal name As String, ByVal masteries As List(Of Mastery)) As MasteryPage

            Try

                Dim oMasteryPage As New MasteryPage With {.Name = name}

                For Each oMastery As Mastery In masteries

                    Select Case oMastery.Tree

                        Case MasteryTree.Offense
                            oMasteryPage.OffenseTree.Add(oMastery)

                        Case MasteryTree.Defense
                            oMasteryPage.DefenseTree.Add(oMastery)

                        Case MasteryTree.Utility
                            oMasteryPage.UtilityTree.Add(oMastery)

                        Case Else
                            Throw New ArgumentException(String.Format("Invalid mastery tree: '{0}'.", oMastery.Tree))

                    End Select

                Next oMastery

                Return oMasteryPage

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function GetMastery(ByVal id As Integer) As Mastery

            Try

                Return New Mastery

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function CountMasteryRanks(ByVal masteryNode As HtmlNode) As Integer

            Try

                Dim iRank As Integer = 0

                Dim oPointNodes As HtmlNodeCollection = masteryNode.SelectNodes("descendant::div[@class='point']")

                If oPointNodes IsNot Nothing Then

                    iRank = oPointNodes.Count

                End If

                Return iRank

            Catch ex As Exception

                Throw

            End Try

        End Function

    End Class

End Module
