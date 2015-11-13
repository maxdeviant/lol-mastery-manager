Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports HtmlAgilityPack
Imports Newtonsoft.Json

Module ChampionGG

    Public Structure Roles

        Public Const Top As String = "Top"
        Public Const Jungle As String = "Jungle"
        Public Const Middle As String = "Middle"
        Public Const ADC As String = "ADC"
        Public Const Support As String = "Support"

    End Structure

    Public Structure MasteryTrees

        Public Const Ferocity As String = "Ferocity"
        Public Const Resolve As String = "Resolve"
        Public Const Cunning As String = "Cunning"

    End Structure

    Public Class Downloader

        Public ReadOnly BaseUrl As String = My.Resources.ChampionGGUrl

        ''' <summary>
        ''' Scrapes the list of champions from Champion.GG.
        ''' </summary>
        ''' <returns>A list of champions.</returns>
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
                                .Key = ExtractChampionKey(oChampionNode)
                                .Name = ExtractChampionName(oChampionNode)
                                .Roles = ExtractChampionRoles(oChampionNode)
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

        ''' <summary>
        ''' Extracts the champion key from the given node.
        ''' </summary>
        ''' <param name="championNode">The node containing the champion data.</param>
        ''' <returns>The key for the champion in the given node.</returns>
        Private Function ExtractChampionKey(ByVal championNode As HtmlNode) As String

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

        ''' <summary>
        ''' Extracts the champion name from the given node.
        ''' </summary>
        ''' <param name="championNode">The node containing the champion data.</param>
        ''' <returns>The name of the champion in the given node.</returns>
        Private Function ExtractChampionName(ByVal championNode As HtmlNode) As String

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

        ''' <summary>
        ''' Extracts the list of possible roles for the champion in the given node.
        ''' </summary>
        ''' <param name="championNode">The node containing the champion data.</param>
        ''' <returns>The list of possible roles for the champion in the given node.</returns>
        Private Function ExtractChampionRoles(ByVal championNode As HtmlNode) As List(Of Role)

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

        ''' <summary>
        ''' Scrapes the current patch number from Champion.GG.
        ''' </summary>
        ''' <returns>The current patch number that Champion.GG is serving data for.</returns>
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

        ''' <summary>
        ''' Scrapes the list of mastery pages for the champion with the specified key.
        ''' </summary>
        ''' <param name="championKey">The key of the champion to retreive the mastery pages for.</param>
        ''' <param name="role">The role to retrieve the mastery pages for.</param>
        ''' <returns>A list of mastery pages.</returns>
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

                                oMasteryPages.Add(BuildMasteryPage(championKey, role, eStat, ExtractMasteries(oMasteryContainer)))

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

        ''' <summary>
        ''' Extracts the list of mastery pages from the given node.
        ''' </summary>
        ''' <param name="container">The node containing the mastery data.</param>
        ''' <returns>The list of mastery pages extracted from the given node.</returns>
        Private Function ExtractMasteries(ByVal container As HtmlNode) As List(Of Mastery)

            Try

                Dim oMasteries As New List(Of Mastery)
                Dim oMastery As Mastery
                Dim oMasteryRows As HtmlNodeCollection = container.SelectNodes("descendant::div[contains(@class, 'mastery-row')]")

                For Each oMasteryRow As HtmlNode In oMasteryRows

                    For Each oMasteryNode As HtmlNode In oMasteryRow.SelectNodes("descendant::div[contains(@class, 'mastery-icon')]")

                        Dim iMasteryID As Integer = CInt(oMasteryNode.Attributes.Item("api-primary-id").Value)

                        oMastery = GetMastery(iMasteryID)

                        oMastery.Ranks = ExtractAssignedMasteryRanks(oMasteryNode)

                        oMasteries.Add(oMastery)

                    Next oMasteryNode

                Next oMasteryRow

                Return oMasteries

            Catch ex As Exception

                Throw

            End Try

        End Function

        ''' <summary>
        ''' Builds a mastery page from the given information
        ''' </summary>
        ''' <param name="championKey">The key of the champion this mastery page is intended for.</param>
        ''' <param name="role">The role this mastery page is intended for.</param>
        ''' <param name="stat">The stats this mastery page is based on.</param>
        ''' <param name="masteries">The masteries belonging to this mastery page.</param>
        ''' <returns>A mastery page.</returns>
        ''' <remarks>The mastery page will not have a champion name set. This must be done afterwards.</remarks>
        Private Function BuildMasteryPage(ByVal championKey As String, ByVal role As String, ByVal stat As Stats, ByVal masteries As List(Of Mastery)) As MasteryPage

            Try
                Dim oMasteryPage As New MasteryPage

                With oMasteryPage
                    .ChampionKey = championKey
                    .Role = role
                    .Stat = stat
                End With

                For Each oMastery As Mastery In masteries

                    Select Case oMastery.Tree

                        Case MasteryTrees.Ferocity

                            oMasteryPage.FerocityTree.Add(oMastery)

                        Case MasteryTrees.Resolve

                            oMasteryPage.ResolveTree.Add(oMastery)

                        Case MasteryTrees.Cunning

                            oMasteryPage.CunningTree.Add(oMastery)

                        Case Else
                            Throw New ArgumentException(String.Format("Invalid mastery tree: '{0}'.", oMastery.Tree))

                    End Select

                Next oMastery

                Return oMasteryPage

            Catch ex As Exception

                Throw

            End Try

        End Function

        ''' <summary>
        ''' Retrieves a mastery from the local data file by its ID.
        ''' </summary>
        ''' <param name="id">The ID of the mastery to retrieve.</param>
        ''' <returns>A mastery.</returns>
        Private Function GetMastery(ByVal id As Integer) As Mastery

            Try

                Dim sJson As String

                Dim sMasteriesPath As String = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location), "Masteries.json")

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

        ''' <summary>
        ''' Extracts the number of assigned mastery ranks from the given node.
        ''' </summary>
        ''' <param name="masteryNode">The node to extract the mastery ranks from.</param>
        ''' <returns>The number of assigned ranks for the mastery contained in the given node.</returns>
        Private Function ExtractAssignedMasteryRanks(ByVal masteryNode As HtmlNode) As Integer

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
