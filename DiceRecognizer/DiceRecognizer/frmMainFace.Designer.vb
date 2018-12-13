<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainFace
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbxIplImg = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        Me.cbxCamID = New System.Windows.Forms.ComboBox()
        Me.statusLabel = New System.Windows.Forms.Label()
        Me.pbxCliped = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.lblExplain = New System.Windows.Forms.Label()
        Me.lblExplainMain = New System.Windows.Forms.Label()
        Me.pbxImageFeature = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.lblExplainFeatures = New System.Windows.Forms.Label()
        Me.lblCameraID = New System.Windows.Forms.Label()
        Me.lblFps = New System.Windows.Forms.Label()
        CType(Me.pbxIplImg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxCliped, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxImageFeature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxIplImg
        '
        Me.pbxIplImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxIplImg.Location = New System.Drawing.Point(14, 44)
        Me.pbxIplImg.Name = "pbxIplImg"
        Me.pbxIplImg.Size = New System.Drawing.Size(320, 240)
        Me.pbxIplImg.TabIndex = 0
        Me.pbxIplImg.TabStop = False
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(149, 2)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 2
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'bgWorker
        '
        '
        'cbxCamID
        '
        Me.cbxCamID.FormattingEnabled = True
        Me.cbxCamID.Location = New System.Drawing.Point(66, 4)
        Me.cbxCamID.Name = "cbxCamID"
        Me.cbxCamID.Size = New System.Drawing.Size(77, 20)
        Me.cbxCamID.TabIndex = 3
        '
        'statusLabel
        '
        Me.statusLabel.AutoSize = True
        Me.statusLabel.Location = New System.Drawing.Point(106, 29)
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(43, 12)
        Me.statusLabel.TabIndex = 5
        Me.statusLabel.Text = "status..."
        '
        'pbxCliped
        '
        Me.pbxCliped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxCliped.Location = New System.Drawing.Point(349, 44)
        Me.pbxCliped.Name = "pbxCliped"
        Me.pbxCliped.Size = New System.Drawing.Size(100, 100)
        Me.pbxCliped.TabIndex = 6
        Me.pbxCliped.TabStop = False
        '
        'lblExplain
        '
        Me.lblExplain.AutoSize = True
        Me.lblExplain.Location = New System.Drawing.Point(347, 29)
        Me.lblExplain.Name = "lblExplain"
        Me.lblExplain.Size = New System.Drawing.Size(67, 12)
        Me.lblExplain.TabIndex = 5
        Me.lblExplain.Text = "ClipedImage"
        '
        'lblExplainMain
        '
        Me.lblExplainMain.AutoSize = True
        Me.lblExplainMain.Location = New System.Drawing.Point(12, 29)
        Me.lblExplainMain.Name = "lblExplainMain"
        Me.lblExplainMain.Size = New System.Drawing.Size(44, 12)
        Me.lblExplainMain.TabIndex = 5
        Me.lblExplainMain.Text = "Camera"
        '
        'pbxImageFeature
        '
        Me.pbxImageFeature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxImageFeature.Location = New System.Drawing.Point(466, 44)
        Me.pbxImageFeature.Name = "pbxImageFeature"
        Me.pbxImageFeature.Size = New System.Drawing.Size(320, 240)
        Me.pbxImageFeature.TabIndex = 0
        Me.pbxImageFeature.TabStop = False
        '
        'lblExplainFeatures
        '
        Me.lblExplainFeatures.AutoSize = True
        Me.lblExplainFeatures.Location = New System.Drawing.Point(464, 29)
        Me.lblExplainFeatures.Name = "lblExplainFeatures"
        Me.lblExplainFeatures.Size = New System.Drawing.Size(64, 12)
        Me.lblExplainFeatures.TabIndex = 5
        Me.lblExplainFeatures.Text = "Processed..."
        '
        'lblCameraID
        '
        Me.lblCameraID.AutoSize = True
        Me.lblCameraID.Location = New System.Drawing.Point(5, 7)
        Me.lblCameraID.Name = "lblCameraID"
        Me.lblCameraID.Size = New System.Drawing.Size(55, 12)
        Me.lblCameraID.TabIndex = 5
        Me.lblCameraID.Text = "CameraID"
        '
        'lblFps
        '
        Me.lblFps.AutoSize = True
        Me.lblFps.Location = New System.Drawing.Point(577, 29)
        Me.lblFps.Name = "lblFps"
        Me.lblFps.Size = New System.Drawing.Size(34, 12)
        Me.lblFps.TabIndex = 7
        Me.lblFps.Text = "[FPS]"
        '
        'frmMainFace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(814, 306)
        Me.Controls.Add(Me.lblFps)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.cbxCamID)
        Me.Controls.Add(Me.pbxCliped)
        Me.Controls.Add(Me.lblExplainFeatures)
        Me.Controls.Add(Me.lblExplainMain)
        Me.Controls.Add(Me.lblCameraID)
        Me.Controls.Add(Me.lblExplain)
        Me.Controls.Add(Me.statusLabel)
        Me.Controls.Add(Me.pbxImageFeature)
        Me.Controls.Add(Me.pbxIplImg)
        Me.Name = "frmMainFace"
        Me.Text = "CameraTest"
        CType(Me.pbxIplImg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxCliped, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxImageFeature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbxIplImg As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents cbxCamID As System.Windows.Forms.ComboBox
    Friend WithEvents statusLabel As System.Windows.Forms.Label
    Friend WithEvents pbxCliped As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents lblExplain As System.Windows.Forms.Label
    Friend WithEvents lblExplainMain As System.Windows.Forms.Label
    Friend WithEvents pbxImageFeature As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents lblExplainFeatures As System.Windows.Forms.Label
    Friend WithEvents lblCameraID As System.Windows.Forms.Label
    Friend WithEvents lblFps As System.Windows.Forms.Label

End Class
