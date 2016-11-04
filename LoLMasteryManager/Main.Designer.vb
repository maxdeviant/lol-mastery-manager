<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.cboChampion = New System.Windows.Forms.ComboBox()
        Me.lblChampion = New System.Windows.Forms.Label()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.cboStats = New System.Windows.Forms.ComboBox()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.btnAssignMasteries = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.LinkLabel()
        Me.lblClientVersion = New System.Windows.Forms.LinkLabel()
        Me.CbClienName = New System.Windows.Forms.ComboBox()
        Me.CbMenuOrSelect = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cboChampion
        '
        Me.cboChampion.FormattingEnabled = True
        Me.cboChampion.Location = New System.Drawing.Point(90, 12)
        Me.cboChampion.Name = "cboChampion"
        Me.cboChampion.Size = New System.Drawing.Size(261, 21)
        Me.cboChampion.TabIndex = 0
        '
        'lblChampion
        '
        Me.lblChampion.AutoSize = True
        Me.lblChampion.Location = New System.Drawing.Point(30, 15)
        Me.lblChampion.Name = "lblChampion"
        Me.lblChampion.Size = New System.Drawing.Size(54, 13)
        Me.lblChampion.TabIndex = 1
        Me.lblChampion.Text = "Champion"
        '
        'cboRole
        '
        Me.cboRole.FormattingEnabled = True
        Me.cboRole.Location = New System.Drawing.Point(90, 39)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(261, 21)
        Me.cboRole.TabIndex = 2
        '
        'cboStats
        '
        Me.cboStats.FormattingEnabled = True
        Me.cboStats.Location = New System.Drawing.Point(90, 66)
        Me.cboStats.Name = "cboStats"
        Me.cboStats.Size = New System.Drawing.Size(261, 21)
        Me.cboStats.TabIndex = 3
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Location = New System.Drawing.Point(55, 42)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(29, 13)
        Me.lblRole.TabIndex = 4
        Me.lblRole.Text = "Role"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Location = New System.Drawing.Point(53, 69)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(31, 13)
        Me.lblStats.TabIndex = 5
        Me.lblStats.Text = "Stats"
        '
        'btnAssignMasteries
        '
        Me.btnAssignMasteries.Location = New System.Drawing.Point(219, 150)
        Me.btnAssignMasteries.Name = "btnAssignMasteries"
        Me.btnAssignMasteries.Size = New System.Drawing.Size(128, 23)
        Me.btnAssignMasteries.TabIndex = 6
        Me.btnAssignMasteries.Text = "Assign Masteries"
        Me.btnAssignMasteries.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        Me.lblVersion.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVersion.AutoSize = True
        Me.lblVersion.LinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(26, 181)
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
        Me.lblClientVersion.Location = New System.Drawing.Point(288, 181)
        Me.lblClientVersion.Name = "lblClientVersion"
        Me.lblClientVersion.Size = New System.Drawing.Size(59, 13)
        Me.lblClientVersion.TabIndex = 10
        Me.lblClientVersion.TabStop = True
        Me.lblClientVersion.Text = "Patch 1.11"
        Me.lblClientVersion.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(192, Byte), Integer))
        '
        'CbClienName
        '
        Me.CbClienName.Enabled = False
        Me.CbClienName.FormattingEnabled = True
        Me.CbClienName.Items.AddRange(New Object() {"PVP.net Client", "Protect by Son Nguyen (www.sonnguyen.xyz)"})
        Me.CbClienName.Location = New System.Drawing.Point(90, 93)
        Me.CbClienName.Name = "CbClienName"
        Me.CbClienName.Size = New System.Drawing.Size(261, 21)
        Me.CbClienName.TabIndex = 3
        Me.CbClienName.Text = "PVP.net Client"
        '
        'CbMenuOrSelect
        '
        Me.CbMenuOrSelect.FormattingEnabled = True
        Me.CbMenuOrSelect.Items.AddRange(New Object() {"Mastery in Menu", "Mastery in Champion Select Old", "Mastery in Champion Select New (ver 6)"})
        Me.CbMenuOrSelect.Location = New System.Drawing.Point(90, 120)
        Me.CbMenuOrSelect.Name = "CbMenuOrSelect"
        Me.CbMenuOrSelect.Size = New System.Drawing.Size(261, 21)
        Me.CbMenuOrSelect.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Client Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Menu or Select"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Version"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(109, 150)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Change Size LOL"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 206)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblClientVersion)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.btnAssignMasteries)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblStats)
        Me.Controls.Add(Me.lblRole)
        Me.Controls.Add(Me.CbMenuOrSelect)
        Me.Controls.Add(Me.CbClienName)
        Me.Controls.Add(Me.cboStats)
        Me.Controls.Add(Me.cboRole)
        Me.Controls.Add(Me.lblChampion)
        Me.Controls.Add(Me.cboChampion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LoL Mastery Manager edit by Hoang0109"
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
    Friend WithEvents lblVersion As LinkLabel
    Friend WithEvents lblClientVersion As LinkLabel
    Friend WithEvents CbClienName As ComboBox
    Friend WithEvents CbMenuOrSelect As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
End Class
