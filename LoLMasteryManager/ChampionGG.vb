Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports HtmlAgilityPack
Imports Newtonsoft.Json

Module ChampionGG

    Public Enum Stats

        MostFrequent
        HighestWin

    End Enum

    Public Structure Roles

        Public Const Top As String = "Top"
        Public Const Jungle As String = "Jungle"
        Public Const Middle As String = "Middle"
        Public Const ADC As String = "ADC"
        Public Const Support As String = "Support"

    End Structure

    Public Structure MasteryTrees

        Public Const Offense As String = "Offense"
        Public Const Defense As String = "Defense"
        Public Const Utility As String = "Utility"

    End Structure

    Public Class Downloader

        Public Const BaseUrl As String = "http://champion.gg/"

        Public Function ScrapeChampions() As List(Of Champion)

            Try

                Dim oChampions As New List(Of Champion)

                Using oWeb As New HttpClient

                    oWeb.BaseAddress = New Uri(BaseUrl)

                    Dim oResponse As HttpResponseMessage
                    Dim sRequestUrl As String = String.Empty

                    oResponse = oWeb.GetAsync(sRequestUrl).Result

                    If oResponse.IsSuccessStatusCode Then

                        Dim sHTML As String = oResponse.Content.ReadAsStringAsync().Result
                        Dim oDocument As New HtmlDocument

                        oDocument.LoadHtml(sHTML)

                        Dim oChampionNodes As HtmlNodeCollection = oDocument.DocumentNode.SelectNodes("//div[contains(@class, 'champ-index-img')]")
                        Dim oChampion As Champion

                        For Each oChampionNode As HtmlNode In oChampionNodes

                            oChampion = New Champion

                            With oChampion
                                .Key = ParseChampionKey(oChampionNode)
                                .Name = ParseChampionName(oChampionNode)
                                .Roles = ParseChampionRoles(oChampionNode)
                            End With

                            oChampions.Add(oChampion)

                        Next oChampionNode

                    End If

                End Using

                Return oChampions

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function ParseChampionKey(ByVal championNode As HtmlNode) As String

            Try

                Dim sChampionKey As String = String.Empty
                Dim oChampionLink As HtmlNode = championNode.SelectSingleNode("a")

                If oChampionLink IsNot Nothing Then

                    Dim sChampionUrl As String = oChampionLink.Attributes("href").Value

                    sChampionKey = sChampionUrl.Split("/"c).Last

                End If

                Return sChampionKey

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function ParseChampionName(ByVal championNode As HtmlNode) As String

            Try

                Dim sChampionName As String = String.Empty
                Dim oChampionNameNode As HtmlNode = championNode.SelectSingleNode("descendant::span[@class='champion-name']")

                If oChampionNameNode IsNot Nothing Then

                    sChampionName = WebUtility.HtmlDecode(oChampionNameNode.InnerText)

                End If

                Return sChampionName

            Catch ex As Exception

                Throw

            End Try

        End Function

        Private Function ParseChampionRoles(ByVal championNode As HtmlNode) As List(Of Role)

            Try

                Dim oRoles As New List(Of Role)
                Dim oChampionRoleNodes As HtmlNodeCollection = championNode.SelectNodes("descendant::a")

                oChampionRoleNodes.Remove(0)

                Dim oRole As Role

                For Each oChampionRoleNode As HtmlNode In oChampionRoleNodes

                    oRole = New Role

                    With oRole
                        .Name = oChampionRoleNode.InnerText.Trim
                        .Rate = 0
                    End With

                    oRoles.Add(oRole)

                Next oChampionRoleNode

                Return oRoles

            Catch ex As Exception

                Throw

            End Try

        End Function

        Public Function ScrapePatchNumber() As String

            Try

                Dim sPatchNumber As String = String.Empty

                Using oWeb As New HttpClient

                    oWeb.BaseAddress = New Uri(BaseUrl)

                    Dim oResponse As HttpResponseMessage
                    Dim sRequestUrl As String = String.Empty

                    oResponse = oWeb.GetAsync(sRequestUrl).Result

                    If oResponse.IsSuccessStatusCode Then

                        Dim sHTML As String = oResponse.Content.ReadAsStringAsync().Result
                        Dim oDocument As New HtmlDocument

                        oDocument.LoadHtml(sHTML)

                        Dim oPatchNumberNode As HtmlNode = oDocument.DocumentNode.SelectSingleNode("//div[@class='analysis-holder']/small/strong")

                        If oPatchNumberNode IsNot Nothing Then

                            sPatchNumber = oPatchNumberNode.InnerText.Trim

                        End If

                    End If

                End Using

                Return sPatchNumber

            Catch ex As Exception

                Throw

            End Try

        End Function

        Public Function ScrapeChampionMasteries(ByVal championKey As String, ByVal role As String) As List(Of MasteryPage)

            Try

                Dim oMasteryPages As New List(Of MasteryPage)

                Using oWeb As New HttpClient

                    oWeb.BaseAddress = New Uri(BaseUrl)

                    Dim oResponse As HttpResponseMessage
                    Dim sRequestUrl As String = String.Format("champion/{0}/{1}", championKey, role)

                    oResponse = oWeb.GetAsync(sRequestUrl).Result

                    If oResponse.IsSuccessStatusCode Then

                        Dim sHTML As String = oResponse.Content.ReadAsStringAsync().Result
                        Dim oDocument As New HtmlDocument

                        oDocument.LoadHtml(sHTML)

                        Dim oMasteryContainers As HtmlNodeCollection = oDocument.DocumentNode.SelectNodes("//div[contains(@class, 'mastery-container')]")
                        Dim eStat As Stats
                        Dim iContainer As Integer = 0

                        If oMasteryContainers IsNot Nothing Then

                            For Each oMasteryContainer As HtmlNode In oMasteryContainers

                                Select Case iContainer

                                    Case Stats.MostFrequent
                                        eStat = Stats.MostFrequent

                                    Case Stats.HighestWin
                                        eStat = Stats.HighestWin

                                End Select

                                oMasteryPages.Add(BuildMasteryPage(GenerateMasteryPageName(championKey, role, eStat), ParseMasteries(oMasteryContainer)))

                                iContainer += 1

                            Next oMasteryContainer

                        End If

                    End If

                End Using

                Return oMasteryPages

            Catch ex As Exception

                Throw

            End Try

        End Function

        Friend Function GenerateMasteryPageName(ByVal championKey As String, ByVal role As String, ByVal stats As Stats) As String

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

                Return String.Format("[{0}] {1} - {2}", sStats, championKey, role)

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

                        Case MasteryTrees.Offense
                            oMasteryPage.OffenseTree.Add(oMastery)

                        Case MasteryTrees.Defense
                            oMasteryPage.DefenseTree.Add(oMastery)

                        Case MasteryTrees.Utility
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

                Dim sJson As String

                Dim sMasteriesPath As String = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), "Data", "Masteries.json")

                Using oStreamReader As New StreamReader(sMasteriesPath)

                    sJson = oStreamReader.ReadToEnd()

                End Using

                Dim oRiotMasteries As Dictionary(Of String, RiotMastery) = JsonConvert.DeserializeObject(Of RiotMasteryListFile)(sJson).Masteries
                Dim oRiotMastery As RiotMastery = oRiotMasteries(id.ToString)

                Dim oMastery As New Mastery

                With oMastery
                    .ID = oRiotMastery.ID
                    .Name = oRiotMastery.Name
                    .Tree = oRiotMastery.Tree
                End With

                Return oMastery

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
