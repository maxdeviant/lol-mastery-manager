Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class MasteryLocator

    Private Structure ArgbColor

        Public A As Byte
        Public R As Byte
        Public G As Byte
        Public B As Byte

        Public Shared Function FromArgb(ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte) As ArgbColor

            Dim color As New ArgbColor()

            color.A = a
            color.R = r
            color.G = g
            color.B = b

            Return color

        End Function

        Public Overrides Function Equals(obj As Object) As Boolean

            If obj Is Nothing OrElse Not obj.GetType Is [GetType]() Then

                Return False

            End If

            Dim color As ArgbColor = CType(obj, ArgbColor)

            Return A = color.A AndAlso R = color.R AndAlso G = color.G AndAlso B = color.B

        End Function

    End Structure

    Public Function GetMasteryPosition(ByVal clientImagePath As String, masteryImagePath As String) As Point

        Dim oClientImage As Image = Image.FromFile(clientImagePath)
        Dim oMasteryImage As Image = Image.FromFile(masteryImagePath)

        Return GetMasteryPosition(oClientImage, oMasteryImage)

    End Function

    Public Function GetMasteryPosition(ByVal clientImage As Image, ByVal masteryImage As Image) As Point

        Try

            Dim oClientImage As New Bitmap(clientImage)
            Dim oMasteryImage As New Bitmap(masteryImage)

            Dim oPositions As List(Of Point) = GetSubImagePositions(oClientImage, oMasteryImage)

            If oPositions IsNot Nothing AndAlso oPositions.Count > 0 Then

                Return oPositions.First

            End If

            Return Nothing

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetSubImagePositions(ByVal image As Bitmap, ByVal subImage As Bitmap) As List(Of Point)

        Try

            Dim oPossiblePositions As New List(Of Point)

            Dim iImageWidth As Integer = image.Width
            Dim iImageHeight As Integer = image.Height

            Dim iSubImageWidth As Integer = subImage.Width
            Dim iSubImageHeight As Integer = subImage.Height

            Dim iMoveWidth As Integer = iImageWidth - iSubImageWidth
            Dim iMoveHeight As Integer = iImageHeight - iSubImageHeight

            Dim oImageData As BitmapData = image.LockBits(New Rectangle(0, 0, iImageWidth, iImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb)
            Dim oSubImageData As BitmapData = subImage.LockBits(New Rectangle(0, 0, iSubImageWidth, iSubImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb)

            Dim iImageByteLength As Integer = Math.Abs(oImageData.Stride) * iImageHeight
            Dim iImageStride As Integer = oImageData.Stride
            Dim iImageScan0 As IntPtr = oImageData.Scan0
            Dim bImageData(iImageByteLength) As Byte

            Marshal.Copy(iImageScan0, bImageData, 0, iImageByteLength)

            Dim iSubImageByteLength As Integer = Math.Abs(oSubImageData.Stride) * iSubImageHeight
            Dim iSubImageStride As Integer = oSubImageData.Stride
            Dim iSubImageScan0 As IntPtr = oSubImageData.Scan0
            Dim bSubImageData(iSubImageByteLength) As Byte

            Marshal.Copy(iSubImageScan0, bSubImageData, 0, iSubImageByteLength)

            For y As Integer = 0 To iMoveHeight - 1

                For x As Integer = 0 To iMoveWidth - 1

                    Dim oCurrentColor As ArgbColor = GetColor(x, y, iImageStride, bImageData)

                    For Each oPosition As Point In oPossiblePositions.ToArray

                        Dim iSubImageX As Integer = x - oPosition.X
                        Dim iSubImageY As Integer = y - oPosition.Y

                        If iSubImageX >= iSubImageWidth OrElse iSubImageY >= iSubImageHeight OrElse iSubImageX < 0 Then

                            Continue For

                        End If

                        Dim oSubImageColor As ArgbColor = GetColor(iSubImageX, iSubImageY, iSubImageStride, bSubImageData)

                        If Not oCurrentColor.Equals(oSubImageColor) Then

                            oPossiblePositions.Remove(oPosition)

                        End If

                    Next oPosition

                    If oCurrentColor.Equals(GetColor(0, 0, iSubImageStride, bSubImageData)) Then

                        oPossiblePositions.Add(New Point(x, y))

                    End If

                Next x

            Next y

            Marshal.Copy(bSubImageData, 0, iSubImageScan0, iSubImageByteLength)
            subImage.UnlockBits(oSubImageData)

            Marshal.Copy(bImageData, 0, iImageScan0, iImageByteLength)
            image.UnlockBits(oImageData)

            Return oPossiblePositions

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetColor(ByVal point As Point, ByVal stride As Integer, ByVal data As Byte()) As ArgbColor

        Return GetColor(point.X, point.Y, stride, data)

    End Function

    Private Function GetColor(ByVal x As Integer, ByVal y As Integer, ByVal stride As Integer, ByVal data As Byte()) As ArgbColor

        Try

            Dim iPosition As Integer = y * stride + x * 4

            Dim bA As Byte = data(iPosition + 3)
            Dim bR As Byte = data(iPosition + 2)
            Dim bG As Byte = data(iPosition + 1)
            Dim bB As Byte = data(iPosition + 0)

            Return ArgbColor.FromArgb(bA, bR, bG, bB)

        Catch ex As Exception

            Throw

        End Try

    End Function

End Class
