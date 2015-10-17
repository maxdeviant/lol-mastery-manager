Partial Public Module ClientControls

    Public Class ClientReturnPointsButton

        Public Const Width As Integer = 175
        Public Const Height As Integer = 25

        Public Structure ChampionSelect

            Public Structure Small

                Public Const X As Double = ClientSize.Small.Width / 120
                Public Const Y As Double = ClientSize.Small.Height / 285

            End Structure

            Public Structure Medium

                Public Const X As Double = ClientSize.Medium.Width / 145
                Public Const Y As Double = ClientSize.Medium.Height / 325

            End Structure

            Public Structure Large

                Public Const X As Double = ClientSize.Large.Width / 85
                Public Const Y As Double = ClientSize.Large.Height / 320

            End Structure

        End Structure

        Public Structure Menu

            Public Const X As Double = ClientSize.Small.Width / 105
            Public Const Y As Double = ClientSize.Small.Height / 325

        End Structure

    End Class

End Module
