<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainDice
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
        Me.components = New System.ComponentModel.Container()
        Me.pbxCamRawImg = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.btnCamOpen = New System.Windows.Forms.Button()
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        Me.cbxCamID = New System.Windows.Forms.ComboBox()
        Me.mStrip = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbxCliped = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.lblExplain = New System.Windows.Forms.Label()
        Me.lblExplainMain = New System.Windows.Forms.Label()
        Me.pbxImageFeature = New OpenCvSharp.UserInterface.PictureBoxIpl()
        Me.lblExplainFeatures = New System.Windows.Forms.Label()
        Me.lblDice = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblP1 = New System.Windows.Forms.Label()
        Me.lblP2 = New System.Windows.Forms.Label()
        Me.lblP3 = New System.Windows.Forms.Label()
        Me.lblP4 = New System.Windows.Forms.Label()
        Me.lblP5 = New System.Windows.Forms.Label()
        Me.tbxMinDist = New System.Windows.Forms.TextBox()
        Me.tbxP1 = New System.Windows.Forms.TextBox()
        Me.tbxP2 = New System.Windows.Forms.TextBox()
        Me.tbxMinRadius = New System.Windows.Forms.TextBox()
        Me.tbxMaxRadius = New System.Windows.Forms.TextBox()
        Me.oSerialPort = New System.IO.Ports.SerialPort(Me.components)
        Me.btnPortOpen = New System.Windows.Forms.Button()
        Me.cbxPort = New System.Windows.Forms.ComboBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnOpt = New System.Windows.Forms.Button()
        Me.tbxCorrect = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.oPlot = New OxyPlot.WindowsForms.Plot()
        Me.btnDebug = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbxRecognizeParam = New System.Windows.Forms.TextBox()
        Me.lblState = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.recentDice = New System.Windows.Forms.TextBox()
        Me.cbxCollectDataset = New System.Windows.Forms.CheckBox()
        Me.btnOneshotCollect = New System.Windows.Forms.Button()
        Me.tbxSleepMs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.pbxCamRawImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mStrip.SuspendLayout()
        CType(Me.pbxCliped, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxImageFeature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pbxCamRawImg
        '
        Me.pbxCamRawImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxCamRawImg.Location = New System.Drawing.Point(12, 73)
        Me.pbxCamRawImg.Name = "pbxCamRawImg"
        Me.pbxCamRawImg.Size = New System.Drawing.Size(320, 240)
        Me.pbxCamRawImg.TabIndex = 0
        Me.pbxCamRawImg.TabStop = False
        '
        'btnCamOpen
        '
        Me.btnCamOpen.Location = New System.Drawing.Point(13, 27)
        Me.btnCamOpen.Name = "btnCamOpen"
        Me.btnCamOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnCamOpen.TabIndex = 0
        Me.btnCamOpen.Text = "CamOpen"
        Me.btnCamOpen.UseVisualStyleBackColor = True
        '
        'bgWorker
        '
        '
        'cbxCamID
        '
        Me.cbxCamID.FormattingEnabled = True
        Me.cbxCamID.Location = New System.Drawing.Point(94, 28)
        Me.cbxCamID.Name = "cbxCamID"
        Me.cbxCamID.Size = New System.Drawing.Size(77, 20)
        Me.cbxCamID.TabIndex = 1
        '
        'mStrip
        '
        Me.mStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem})
        Me.mStrip.Location = New System.Drawing.Point(0, 0)
        Me.mStrip.Name = "mStrip"
        Me.mStrip.Size = New System.Drawing.Size(1398, 24)
        Me.mStrip.TabIndex = 0
        Me.mStrip.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(93, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(114, 58)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(43, 12)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "status..."
        '
        'pbxCliped
        '
        Me.pbxCliped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxCliped.Location = New System.Drawing.Point(338, 73)
        Me.pbxCliped.Name = "pbxCliped"
        Me.pbxCliped.Size = New System.Drawing.Size(128, 128)
        Me.pbxCliped.TabIndex = 6
        Me.pbxCliped.TabStop = False
        '
        'lblExplain
        '
        Me.lblExplain.AutoSize = True
        Me.lblExplain.Location = New System.Drawing.Point(336, 58)
        Me.lblExplain.Name = "lblExplain"
        Me.lblExplain.Size = New System.Drawing.Size(67, 12)
        Me.lblExplain.TabIndex = 8
        Me.lblExplain.Text = "ClipedImage"
        '
        'lblExplainMain
        '
        Me.lblExplainMain.AutoSize = True
        Me.lblExplainMain.Location = New System.Drawing.Point(10, 58)
        Me.lblExplainMain.Name = "lblExplainMain"
        Me.lblExplainMain.Size = New System.Drawing.Size(88, 12)
        Me.lblExplainMain.TabIndex = 6
        Me.lblExplainMain.Text = "Capture Camera"
        '
        'pbxImageFeature
        '
        Me.pbxImageFeature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbxImageFeature.Location = New System.Drawing.Point(472, 73)
        Me.pbxImageFeature.Name = "pbxImageFeature"
        Me.pbxImageFeature.Size = New System.Drawing.Size(300, 300)
        Me.pbxImageFeature.TabIndex = 0
        Me.pbxImageFeature.TabStop = False
        '
        'lblExplainFeatures
        '
        Me.lblExplainFeatures.AutoSize = True
        Me.lblExplainFeatures.Location = New System.Drawing.Point(469, 58)
        Me.lblExplainFeatures.Name = "lblExplainFeatures"
        Me.lblExplainFeatures.Size = New System.Drawing.Size(44, 12)
        Me.lblExplainFeatures.TabIndex = 9
        Me.lblExplainFeatures.Text = "Feature"
        '
        'lblDice
        '
        Me.lblDice.AutoSize = True
        Me.lblDice.Font = New System.Drawing.Font("MS UI Gothic", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDice.Location = New System.Drawing.Point(784, 322)
        Me.lblDice.Name = "lblDice"
        Me.lblDice.Size = New System.Drawing.Size(158, 35)
        Me.lblDice.TabIndex = 28
        Me.lblDice.Text = "Dice is ..."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(788, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 12)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Recognize paramters"
        '
        'lblP1
        '
        Me.lblP1.AutoSize = True
        Me.lblP1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblP1.Location = New System.Drawing.Point(798, 80)
        Me.lblP1.Name = "lblP1"
        Me.lblP1.Size = New System.Drawing.Size(48, 12)
        Me.lblP1.TabIndex = 11
        Me.lblP1.Text = "min dist:"
        '
        'lblP2
        '
        Me.lblP2.AutoSize = True
        Me.lblP2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblP2.Location = New System.Drawing.Point(798, 105)
        Me.lblP2.Name = "lblP2"
        Me.lblP2.Size = New System.Drawing.Size(19, 12)
        Me.lblP2.TabIndex = 13
        Me.lblP2.Text = "p1:"
        '
        'lblP3
        '
        Me.lblP3.AutoSize = True
        Me.lblP3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblP3.Location = New System.Drawing.Point(798, 130)
        Me.lblP3.Name = "lblP3"
        Me.lblP3.Size = New System.Drawing.Size(19, 12)
        Me.lblP3.TabIndex = 15
        Me.lblP3.Text = "p2:"
        '
        'lblP4
        '
        Me.lblP4.AutoSize = True
        Me.lblP4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblP4.Location = New System.Drawing.Point(798, 155)
        Me.lblP4.Name = "lblP4"
        Me.lblP4.Size = New System.Drawing.Size(60, 12)
        Me.lblP4.TabIndex = 17
        Me.lblP4.Text = "minRadius:"
        '
        'lblP5
        '
        Me.lblP5.AutoSize = True
        Me.lblP5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblP5.Location = New System.Drawing.Point(798, 180)
        Me.lblP5.Name = "lblP5"
        Me.lblP5.Size = New System.Drawing.Size(63, 12)
        Me.lblP5.TabIndex = 19
        Me.lblP5.Text = "maxRadius:"
        '
        'tbxMinDist
        '
        Me.tbxMinDist.Location = New System.Drawing.Point(877, 77)
        Me.tbxMinDist.Name = "tbxMinDist"
        Me.tbxMinDist.Size = New System.Drawing.Size(70, 19)
        Me.tbxMinDist.TabIndex = 12
        '
        'tbxP1
        '
        Me.tbxP1.Location = New System.Drawing.Point(877, 102)
        Me.tbxP1.Name = "tbxP1"
        Me.tbxP1.Size = New System.Drawing.Size(70, 19)
        Me.tbxP1.TabIndex = 14
        '
        'tbxP2
        '
        Me.tbxP2.Location = New System.Drawing.Point(877, 127)
        Me.tbxP2.Name = "tbxP2"
        Me.tbxP2.Size = New System.Drawing.Size(70, 19)
        Me.tbxP2.TabIndex = 16
        '
        'tbxMinRadius
        '
        Me.tbxMinRadius.Location = New System.Drawing.Point(877, 152)
        Me.tbxMinRadius.Name = "tbxMinRadius"
        Me.tbxMinRadius.Size = New System.Drawing.Size(70, 19)
        Me.tbxMinRadius.TabIndex = 18
        '
        'tbxMaxRadius
        '
        Me.tbxMaxRadius.Location = New System.Drawing.Point(877, 177)
        Me.tbxMaxRadius.Name = "tbxMaxRadius"
        Me.tbxMaxRadius.Size = New System.Drawing.Size(70, 19)
        Me.tbxMaxRadius.TabIndex = 20
        '
        'btnPortOpen
        '
        Me.btnPortOpen.Location = New System.Drawing.Point(221, 27)
        Me.btnPortOpen.Name = "btnPortOpen"
        Me.btnPortOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnPortOpen.TabIndex = 2
        Me.btnPortOpen.Text = "PortOpen"
        Me.btnPortOpen.UseVisualStyleBackColor = True
        '
        'cbxPort
        '
        Me.cbxPort.FormattingEnabled = True
        Me.cbxPort.Location = New System.Drawing.Point(302, 28)
        Me.cbxPort.Name = "cbxPort"
        Me.cbxPort.Size = New System.Drawing.Size(77, 20)
        Me.cbxPort.TabIndex = 3
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(385, 26)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(75, 23)
        Me.btnSend.TabIndex = 4
        Me.btnSend.Text = "Shake"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnOpt
        '
        Me.btnOpt.Location = New System.Drawing.Point(953, 280)
        Me.btnOpt.Name = "btnOpt"
        Me.btnOpt.Size = New System.Drawing.Size(75, 23)
        Me.btnOpt.TabIndex = 27
        Me.btnOpt.Text = "DoOpt"
        Me.btnOpt.UseVisualStyleBackColor = True
        '
        'tbxCorrect
        '
        Me.tbxCorrect.Location = New System.Drawing.Point(877, 282)
        Me.tbxCorrect.Name = "tbxCorrect"
        Me.tbxCorrect.Size = New System.Drawing.Size(70, 19)
        Me.tbxCorrect.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(788, 268)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 12)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "OptParameter debug"
        '
        'oPlot
        '
        Me.oPlot.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oPlot.BackColor = System.Drawing.Color.White
        Me.oPlot.Location = New System.Drawing.Point(3, 15)
        Me.oPlot.Name = "oPlot"
        Me.oPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.oPlot.Size = New System.Drawing.Size(816, 315)
        Me.oPlot.TabIndex = 0
        Me.oPlot.Text = "oPlot"
        Me.oPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.oPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.oPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'btnDebug
        '
        Me.btnDebug.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDebug.Location = New System.Drawing.Point(1279, 87)
        Me.btnDebug.Name = "btnDebug"
        Me.btnDebug.Size = New System.Drawing.Size(75, 23)
        Me.btnDebug.TabIndex = 3
        Me.btnDebug.Text = "debug"
        Me.btnDebug.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(503, 26)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 5
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lblDetail
        '
        Me.lblDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetail.AutoSize = True
        Me.lblDetail.Font = New System.Drawing.Font("MS UI Gothic", 32.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDetail.Location = New System.Drawing.Point(828, 15)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.Size = New System.Drawing.Size(332, 43)
        Me.lblDetail.TabIndex = 1
        Me.lblDetail.Text = "Total Roll count:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnClear)
        Me.GroupBox1.Controls.Add(Me.btnDebug)
        Me.GroupBox1.Controls.Add(Me.lblDetail)
        Me.GroupBox1.Controls.Add(Me.oPlot)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 382)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1371, 336)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(1279, 58)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(1009, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 12)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Recognize ave count"
        '
        'tbxRecognizeParam
        '
        Me.tbxRecognizeParam.Location = New System.Drawing.Point(1126, 77)
        Me.tbxRecognizeParam.Name = "tbxRecognizeParam"
        Me.tbxRecognizeParam.Size = New System.Drawing.Size(70, 19)
        Me.tbxRecognizeParam.TabIndex = 23
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(558, 58)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(37, 12)
        Me.lblState.TabIndex = 30
        Me.lblState.Text = "state..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 322)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(243, 12)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "↑画面をクリックして青枠でサイコロを含むようにする"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.recentDice)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 724)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1371, 68)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        '
        'recentDice
        '
        Me.recentDice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.recentDice.Location = New System.Drawing.Point(3, 15)
        Me.recentDice.Multiline = True
        Me.recentDice.Name = "recentDice"
        Me.recentDice.Size = New System.Drawing.Size(1365, 50)
        Me.recentDice.TabIndex = 0
        '
        'cbxCollectDataset
        '
        Me.cbxCollectDataset.AutoSize = True
        Me.cbxCollectDataset.Location = New System.Drawing.Point(614, 30)
        Me.cbxCollectDataset.Name = "cbxCollectDataset"
        Me.cbxCollectDataset.Size = New System.Drawing.Size(88, 16)
        Me.cbxCollectDataset.TabIndex = 33
        Me.cbxCollectDataset.Text = "データ収集用"
        Me.cbxCollectDataset.UseVisualStyleBackColor = True
        '
        'btnOneshotCollect
        '
        Me.btnOneshotCollect.Location = New System.Drawing.Point(708, 26)
        Me.btnOneshotCollect.Name = "btnOneshotCollect"
        Me.btnOneshotCollect.Size = New System.Drawing.Size(103, 23)
        Me.btnOneshotCollect.TabIndex = 34
        Me.btnOneshotCollect.Text = "OneshotCollect"
        Me.btnOneshotCollect.UseVisualStyleBackColor = True
        '
        'tbxSleepMs
        '
        Me.tbxSleepMs.Location = New System.Drawing.Point(1126, 102)
        Me.tbxSleepMs.Name = "tbxSleepMs"
        Me.tbxSleepMs.Size = New System.Drawing.Size(70, 19)
        Me.tbxSleepMs.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(1009, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 12)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Sleep[ms]:"
        '
        'frmMainDice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1398, 804)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbxSleepMs)
        Me.Controls.Add(Me.btnOneshotCollect)
        Me.Controls.Add(Me.cbxCollectDataset)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.tbxRecognizeParam)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.tbxCorrect)
        Me.Controls.Add(Me.btnOpt)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.btnPortOpen)
        Me.Controls.Add(Me.lblP3)
        Me.Controls.Add(Me.lblP5)
        Me.Controls.Add(Me.tbxMaxRadius)
        Me.Controls.Add(Me.lblP4)
        Me.Controls.Add(Me.tbxMinRadius)
        Me.Controls.Add(Me.tbxP2)
        Me.Controls.Add(Me.lblP1)
        Me.Controls.Add(Me.tbxP1)
        Me.Controls.Add(Me.lblP2)
        Me.Controls.Add(Me.tbxMinDist)
        Me.Controls.Add(Me.lblDice)
        Me.Controls.Add(Me.pbxCliped)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblExplainFeatures)
        Me.Controls.Add(Me.lblExplainMain)
        Me.Controls.Add(Me.lblExplain)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cbxPort)
        Me.Controls.Add(Me.cbxCamID)
        Me.Controls.Add(Me.btnCamOpen)
        Me.Controls.Add(Me.pbxImageFeature)
        Me.Controls.Add(Me.pbxCamRawImg)
        Me.Controls.Add(Me.mStrip)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.MainMenuStrip = Me.mStrip
        Me.Name = "frmMainDice"
        Me.Text = "DiceRecognizer"
        CType(Me.pbxCamRawImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mStrip.ResumeLayout(False)
        Me.mStrip.PerformLayout()
        CType(Me.pbxCliped, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxImageFeature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbxCamRawImg As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents btnCamOpen As System.Windows.Forms.Button
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents cbxCamID As System.Windows.Forms.ComboBox
    Friend WithEvents mStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbxCliped As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents lblExplain As System.Windows.Forms.Label
    Friend WithEvents lblExplainMain As System.Windows.Forms.Label
    Friend WithEvents pbxImageFeature As OpenCvSharp.UserInterface.PictureBoxIpl
    Friend WithEvents lblExplainFeatures As System.Windows.Forms.Label
    Friend WithEvents lblDice As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblP1 As System.Windows.Forms.Label
    Friend WithEvents lblP2 As System.Windows.Forms.Label
    Friend WithEvents lblP3 As System.Windows.Forms.Label
    Friend WithEvents lblP4 As System.Windows.Forms.Label
    Friend WithEvents lblP5 As System.Windows.Forms.Label
    Friend WithEvents tbxMinDist As System.Windows.Forms.TextBox
    Friend WithEvents tbxP1 As System.Windows.Forms.TextBox
    Friend WithEvents tbxP2 As System.Windows.Forms.TextBox
    Friend WithEvents tbxMinRadius As System.Windows.Forms.TextBox
    Friend WithEvents tbxMaxRadius As System.Windows.Forms.TextBox
    Friend WithEvents oSerialPort As System.IO.Ports.SerialPort
    Friend WithEvents btnPortOpen As System.Windows.Forms.Button
    Friend WithEvents cbxPort As System.Windows.Forms.ComboBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnOpt As System.Windows.Forms.Button
    Friend WithEvents tbxCorrect As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents oPlot As OxyPlot.WindowsForms.Plot
    Friend WithEvents btnDebug As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents lblDetail As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents tbxRecognizeParam As TextBox
    Friend WithEvents lblState As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents recentDice As TextBox
    Friend WithEvents cbxCollectDataset As CheckBox
    Friend WithEvents btnOneshotCollect As Button
    Friend WithEvents tbxSleepMs As TextBox
    Friend WithEvents Label5 As Label
End Class
