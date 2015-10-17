Partial Public Module ClientControls

    Public Class ClientMasteryPageNameInputField

        Public Const Width As Integer = 165
        Public Const Height As Integer = 20

        Public Structure ChampionSelect

            Public Structure Small

                Public Const X As Double = ClientSize.Small.Width / 112
                Public Const Y As Double = ClientSize.Small.Height / 165

            End Structure

            Public Structure Medium

                Public Const X As Double = ClientSize.Medium.Width / 132
                Public Const Y As Double = ClientSize.Medium.Height / 172

            End Structure

            Public Structure Large

                Public Const X As Double = ClientSize.Large.Width / 70
                Public Const Y As Double = ClientSize.Medium.Height / 150

            End Structure

        End Structure

        Public Structure Menu

            Public Const X As Double = ClientSize.Small.Width / 98
            Public Const Y As Double = ClientSize.Small.Height / 204

        End Structure

    End Class

End Module
