Imports System.IO
Imports System.Net.Http

Module DataDragon

    Public Class Downloader

        Public ReadOnly BaseUrl As String = My.Resources.DataDragonUrl
        Public ReadOnly BaseDirectory As String

        Public Sub New(ByVal baseDirectory As String)

            Me.BaseDirectory = baseDirectory

        End Sub

        Public Function DownloadMasteryImage(ByVal masteryID As String) As Boolean

            Try

                Dim bResult As Boolean = False

                Using oWeb As New HttpClient

                    oWeb.BaseAddress = New Uri(BaseUrl)

                    Dim oResponse As HttpResponseMessage
                    Dim sRequestUrl As String = String.Format("{0}/img/mastery/{1}.png", "5.22.3", masteryID)

                    oResponse = oWeb.GetAsync(sRequestUrl).Result

                    If oResponse.IsSuccessStatusCode Then

                        Dim oBytes As Byte() = oResponse.Content.ReadAsByteArrayAsync().Result

                        File.WriteAllBytes(Path.Combine(BaseDirectory, String.Format("{0}.png", masteryID)), oBytes)

                        bResult = True

                    End If

                End Using

                Return bResult

            Catch ex As Exception

                ' Throw the exception
                Throw

            End Try

        End Function

    End Class


End Module
