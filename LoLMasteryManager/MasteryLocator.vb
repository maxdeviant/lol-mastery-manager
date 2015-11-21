Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

''' <summary>
''' A class for handling the location of masteries within an image.
''' </summary>
Public Class MasteryLocator

    ''' <summary>
    ''' The representation of an ARGB color.
    ''' </summary>
    Private Structure ArgbColor

        ''' <summary>
        ''' The value of the alpha channel.
        ''' </summary>
        Public A As Byte

        ''' <summary>
        ''' The value of the red channel.
        ''' </summary>
        Public R As Byte

        ''' <summary>
        ''' The value of the green channel.
        ''' </summary>
        Public G As Byte

        ''' <summary>
        ''' The value of the blue channel.
        ''' </summary>
        Public B As Byte

        ''' <summary>
        ''' Creates a new <see cref="ArgbColor"/> from the specified ARGB channels.
        ''' </summary>
        ''' <param name="a">The value of the alpha channel.</param>
        ''' <param name="r">The value of the red channel.</param>
        ''' <param name="g">The value of the green channel.</param>
        ''' <param name="b">The value of the blue channel.</param>
        ''' <returns>An ARGB color with the specified channels.</returns>
        Public Shared Function FromArgb(ByVal a As Byte, ByVal r As Byte, ByVal g As Byte, ByVal b As Byte) As ArgbColor

            ' Instantiate a new ARGB color
            Dim color As New ArgbColor()

            ' Set the color properties
            With color
                .A = a
                .R = r
                .G = g
                .B = b
            End With ' Set the color properties

            ' Return the color
            Return color

        End Function

        ''' <summary>
        ''' Determines whether this instance and a specified object, which must also be an <see cref="ArgbColor"/> object, have the same value.
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <returns></returns>
        Public Overrides Function Equals(obj As Object) As Boolean

            ' Check if the obj is not nothing and has the same type as the current object instance
            If obj Is Nothing OrElse Not obj.GetType Is [GetType]() Then

                ' Objects are not equal
                Return False

            End If ' Check if the obj is not nothing and has the same type as the current object instance

            ' Cast the object to an ARGB color
            Dim color As ArgbColor = CType(obj, ArgbColor)

            ' Return whether or not the values of the ARGB channels are all equal
            Return A = color.A AndAlso R = color.R AndAlso G = color.G AndAlso B = color.B

        End Function

    End Structure

    ''' <summary>
    ''' Gets the position of the specified mastery image within the client image.
    ''' </summary>
    ''' <param name="clientImagePath">The path to the image of the client to search for the mastery in.</param>
    ''' <param name="masteryImagePath">The path to the mastery image to search for in the client image.</param>
    ''' <returns>The position of the mastery image within the client image.</returns>
    Public Function GetMasteryPosition(ByVal clientImagePath As String, masteryImagePath As String) As Point

        Try

            ' Load the client image
            Dim oClientImage As Image = Image.FromFile(clientImagePath)

            ' Load the mastery image
            Dim oMasteryImage As Image = Image.FromFile(masteryImagePath)

            ' Get the position of the mastery
            Return GetMasteryPosition(oClientImage, oMasteryImage)

        Catch ex As Exception

            ' Throw the exception

        End Try

    End Function

    ''' <summary>
    ''' Gets the position of the specified mastery image within the client image.
    ''' </summary>
    ''' <param name="clientImage">The iamge of the client to search for the mastery in.</param>
    ''' <param name="masteryImage">The mastery iamge to search for in the client image.</param>
    ''' <returns>The position of the mastery image within the client image.</returns>
    Public Function GetMasteryPosition(ByVal clientImage As Image, ByVal masteryImage As Image) As Point

        Try

            ' Create a bitmap representation of the client image
            Dim oClientImage As New Bitmap(clientImage)

            ' Create a bitmap representation of the mastery image
            Dim oMasteryImage As New Bitmap(masteryImage)

            ' Get all of the positions that the mastery image appears at in the client image
            Dim oPositions As List(Of Point) = GetSubImagePositions(oClientImage, oMasteryImage)

            ' If there is at least one position
            If oPositions IsNot Nothing AndAlso oPositions.Count > 0 Then

                ' Return the first position
                Return oPositions.First

            End If ' If there is at least one position

            ' Return nothing
            Return Nothing

        Catch ex As Exception

            ' Throw the exception
            Throw

        End Try

    End Function

    ''' <summary>
    ''' Returns all positions of the specified subimage within the specified image.
    ''' </summary>
    ''' <param name="image">The bitmap to search for the subimage in.</param>
    ''' <param name="subImage">The bitmap to search for within the main image.</param>
    ''' <returns>A list of positions that the subimage was found in the main image.</returns>
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
