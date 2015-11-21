<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Admin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnGenerateCoordinates = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnGenerateCoordinates
        '
        Me.btnGenerateCoordinates.Location = New System.Drawing.Point(12, 12)
        Me.btnGenerateCoordinates.Name = "btnGenerateCoordinates"
        Me.btnGenerateCoordinates.Size = New System.Drawing.Size(126, 23)
        Me.btnGenerateCoordinates.TabIndex = 0
        Me.btnGenerateCoordinates.Text = "Generate Coordinates"
        Me.btnGenerateCoordinates.UseVisualStyleBackColor = True
        '
        'Admin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.btnGenerateCoordinates)
        Me.Name = "Admin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnGenerateCoordinates As Button
End Class
