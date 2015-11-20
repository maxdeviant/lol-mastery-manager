<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.cboChampion = New System.Windows.Forms.ComboBox()
        Me.lblChampion = New System.Windows.Forms.Label()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.cboStats = New System.Windows.Forms.ComboBox()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.btnAssignMasteries = New System.Windows.Forms.Button()
        Me.chkInChampionSelect = New System.Windows.Forms.CheckBox()
        Me.lblVersion = New System.Windows.Forms.LinkLabel()
        Me.lblClientVersion = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'cboChampion
        '
        Me.cboChampion.FormattingEnabled = True
        Me.cboChampion.Location = New System.Drawing.Point(69, 12)
        Me.cboChampion.Name = "cboChampion"
        Me.cboChampion.Size = New System.Drawing.Size(261, 21)
        Me.cboChampion.TabIndex = 0
        '
        'lblChampion
        '
        Me.lblChampion.AutoSize = True
        Me.lblChampion.Location = New System.Drawing.Point(9, 15)
        Me.lblChampion.Name = "lblChampion"
        Me.lblChampion.Size = New System.Drawing.Size(54, 13)
        Me.lblChampion.TabIndex = 1
        Me.lblChampion.Text = "Champion"
        '
        'cboRole
        '
        Me.cboRole.FormattingEnabled = True
        Me.cboRole.Location = New System.Drawing.Point(69, 39)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(261, 21)
        Me.cboRole.TabIndex = 2
        '
        'cboStats
        '
        Me.cboStats.FormattingEnabled = True
        Me.cboStats.Location = New System.Drawing.Point(69, 66)
        Me.cboStats.Name = "cboStats"
        Me.cboStats.Size = New System.Drawing.Size(261, 21)
        Me.cboStats.TabIndex = 3
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Location = New System.Drawing.Point(34, 42)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(29, 13)
        Me.lblRole.TabIndex = 4
        Me.lblRole.Text = "Role"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Location = New System.Drawing.Point(32, 69)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(31, 13)
        Me.lblStats.TabIndex = 5
        Me.lblStats.Text = "Stats"
        '
        'btnAssignMasteries
        '
        Me.btnAssignMasteries.Location = New System.Drawing.Point(202, 105)
        Me.btnAssignMasteries.Name = "btnAssignMasteries"
        Me.btnAssignMasteries.Size = New System.Drawing.Size(128, 23)
        Me.btnAssignMasteries.TabIndex = 6
        Me.btnAssignMasteries.Text = "Assign Masteries"
        Me.btnAssignMasteries.UseVisualStyleBackColor = True
        '
        'chkInChampionSelect
        '
        Me.chkInChampionSelect.AutoSize = True
        Me.chkInChampionSelect.Checked = True
        Me.chkInChampionSelect.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInChampionSelect.Location = New System.Drawing.Point(12, 111)
        Me.chkInChampionSelect.Name = "chkInChampionSelect"
        Me.chkInChampionSelect.Size = New System.Drawing.Size(118, 17)
        Me.chkInChampionSelect.TabIndex = 8
        Me.chkInChampionSelect.Text = "In Champion Select"
        Me.chkInChampionSelect.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        Me.lblVersion.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVersion.AutoSize = True
        Me.lblVersion.LinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(9, 136)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(31, 13)
        Me.lblVersion.TabIndex = 9
        Me.lblVersion.TabStop = True
        Me.lblVersion.Text = "1.0.0"
        Me.lblVersion.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'lblClientVersion
        '
        Me.lblClientVersion.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblClientVersion.AutoSize = True
        Me.lblClientVersion.LinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblClientVersion.Location = New System.Drawing.Point(271, 136)
        Me.lblClientVersion.Name = "lblClientVersion"
        Me.lblClientVersion.Size = New System.Drawing.Size(59, 13)
        Me.lblClientVersion.TabIndex = 10
        Me.lblClientVersion.TabStop = True
        Me.lblClientVersion.Text = "Patch 1.11"
        Me.lblClientVersion.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 158)
        Me.Controls.Add(Me.lblClientVersion)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.chkInChampionSelect)
        Me.Controls.Add(Me.btnAssignMasteries)
        Me.Controls.Add(Me.lblStats)
        Me.Controls.Add(Me.lblRole)
        Me.Controls.Add(Me.cboStats)
        Me.Controls.Add(Me.cboRole)
        Me.Controls.Add(Me.lblChampion)
        Me.Controls.Add(Me.cboChampion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LoL Mastery Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboChampion As ComboBox
    Friend WithEvents lblChampion As Label
    Friend WithEvents cboRole As ComboBox
    Friend WithEvents cboStats As ComboBox
    Friend WithEvents lblRole As Label
    Friend WithEvents lblStats As Label
    Friend WithEvents btnAssignMasteries As Button
    Friend WithEvents chkInChampionSelect As CheckBox
    Friend WithEvents lblVersion As LinkLabel
    Friend WithEvents lblClientVersion As LinkLabel
End Class
