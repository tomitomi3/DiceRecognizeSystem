Imports OpenCvSharp
Imports System.Text
Imports System.IO
Imports OxyPlot

Public Class frmMainDice
    Declare Function AllocConsole Lib "kernel32" () As Int32
    Declare Function FreeConsole Lib "kernel32" () As Int32

    Private objlock = New Object()

    Dim camDeviceList As New List(Of CvCapture)
    Private m_capture As CvCapture = Nothing

    Private GC_CALL_SIZE As Integer = 1024 * 1024 * 100 'ガベージコレクタの呼び出しサイズ
    Private m_draw As Bitmap = Nothing

    Private clickedPointOnPBX As New Point(0, 0)

    Private countRecognize As Integer = 0
    Private sumDiceValue As Double = 0

    Private RECENT_COUNT_FILE As String = "RecentDiceCount.txt"
    Private RECENT_PARAMETER_FILE As String = "RecentParameter.txt"
    Private RECENT_DICE_FILE As String = "RecentDice.txt"

    'dice recognize flg
    Private isRunSequence As Boolean = False

    'recognize parameters
    Private DICE_AVERAGE As Integer = 15
    Private minDist As Double
    Private p1 As Double
    Private p2 As Double
    Private minRadius As Integer
    Private maxRadius As Integer

    'for optimize parameter
    Private copyIPL As IplImage = Nothing
    Private isOpt As Boolean = False

    'dice result
    Private recognizedDiceResult(5) As Integer
    Private recognizedDiceResultList As New List(Of Integer)

    ''' <summary>状態遷移：Sleepの待機時間[ms]</summary>
    Private elapsedSleepMs As Long = 0

    ''' <summary>状態遷移：Sleepの最大待機時間[ms]</summary>
    Private MAX_SLEEP_MS As Integer = 1000

    ''' <summary>USBカメラの入力と表示サイズの比率違い</summary>
    Private zoomRatio As Double = 1.0

    Private recognizeDice As Integer = 0
    Private stateRecognize As DiceRecognizeState = DiceRecognizeState.RECOGNIZE

    Public Enum DiceRecognizeState
        RECOGNIZE = 0
        ROLLDICE = 1
        SLEEP = 2
    End Enum

    '直近の認識画像
    Private recentBmp As Bitmap = Nothing

    'クリップサイズ
    Private CLIP_SIZE = 128

    '画像保存クラス
    Private saveImage As New clsSaveImage()

    ''' <summary>
    ''' Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Console
        AllocConsole()

        'USBカメラチェック
        'Dim result = MessageBox.Show("USBカメラをUSBポートに刺しましたか？", "", MessageBoxButtons.YesNo)
        'If result = Windows.Forms.DialogResult.No Then
        '    Me.Close()
        '    Return
        'End If

        'list of Camera id
        Dim camIds As New List(Of Integer)
        For i As Integer = 0 To 10 - 1
            Dim temp As CvCapture = Nothing
            Try
                temp = Cv.CreateCameraCapture(i)
                camIds.Add(i)
                Console.WriteLine("Detect Camera ID:{0}", i)
            Catch ex As Exception
                'Exit For
            Finally
                If temp IsNot Nothing Then
                    temp.Dispose()
                End If
            End Try
        Next

        tbxRecognizeParam.Text = Me.DICE_AVERAGE.ToString()

        'Combo box
        cbxCamID.DropDownStyle = ComboBoxStyle.DropDownList
        cbxCamID.Items.Clear()
        For Each camId In camIds
            cbxCamID.Items.Add(camId.ToString())
        Next
        If cbxCamID.Items.Count = 0 Then
            'do nothing
        ElseIf cbxCamID.Items.Count = 1 Then
            cbxCamID.SelectedIndex = 0
        ElseIf cbxCamID.Items.Count = 2 Then
            cbxCamID.SelectedIndex = 1
        End If

        'size mode
        Me.pbxCamRawImg.SizeMode = PictureBoxSizeMode.Normal

        'bgworker setting
        Me.bgWorker.WorkerSupportsCancellation = True

        'hough circle parameters
        Me.tbxMinDist.Text = "8.0"
        Me.tbxP1.Text = "70.0"
        Me.tbxP2.Text = "32.0"
        Me.tbxMinRadius.Text = "3"
        Me.tbxMaxRadius.Text = "35"
        Me.minDist = Double.Parse(Me.tbxMinDist.Text)
        Me.p1 = Double.Parse(Me.tbxP1.Text)
        Me.p2 = Double.Parse(Me.tbxP2.Text)
        Me.minRadius = Integer.Parse(Me.tbxMinRadius.Text)
        Me.maxRadius = Integer.Parse(Me.tbxMaxRadius.Text)
        LoadParameter()

        'recognize parameters
        Me.tbxSleepMs.Text = MAX_SLEEP_MS.ToString()

        'SerialPort
        Dim ports = System.IO.Ports.SerialPort.GetPortNames()
        If ports.Length = 0 Then
            'Return
        Else
            For i As Integer = 0 To ports.Length - 1
                cbxPort.Items.Add(ports(i))
            Next
            cbxPort.SelectedIndex = 0
        End If

        'カメラ
        btnCamOpen.PerformClick()

        '結果の読み込み
        LoadResult()

        'クリック位置
        Me.clickedPointOnPBX.X = 128
        Me.clickedPointOnPBX.Y = 94

        'plot
        InitPlot()
        UpdateFrequency()
    End Sub

    ''' <summary>
    ''' Formを閉じたときに呼び出されるイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMainDice_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        bgWorker.CancelAsync()
        System.Threading.Thread.Sleep(500)

        'データの保存
        SaveData()

        'Dice
        Using writer As New StreamWriter(RECENT_DICE_FILE, True, Encoding.GetEncoding("Shift_JIS"))
            For Each dice In recognizedDiceResultList
                writer.WriteLine(String.Format("{0}", dice))
            Next
        End Using

        'serial port
        If (oSerialPort.IsOpen) Then
            oSerialPort.Close()
        End If
    End Sub

    ''' <summary>
    ''' save data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveData()
        Using writer As New StreamWriter(RECENT_PARAMETER_FILE, False, Encoding.GetEncoding("Shift_JIS"))
            writer.WriteLine(String.Format("{0}", tbxMinDist.Text))
            writer.WriteLine(String.Format("{0}", tbxP1.Text))
            writer.WriteLine(String.Format("{0}", tbxP2.Text))
            writer.WriteLine(String.Format("{0}", tbxMinRadius.Text))
            writer.WriteLine(String.Format("{0}", tbxMaxRadius.Text))
            writer.WriteLine(String.Format("{0}", tbxRecognizeParam.Text))
        End Using

        Using writer As New StreamWriter(RECENT_COUNT_FILE, False, Encoding.GetEncoding("Shift_JIS"))
            For i As Integer = 0 To Me.recognizedDiceResult.Length - 1
                writer.WriteLine(String.Format("{0}", Me.recognizedDiceResult(i)))
            Next
        End Using
    End Sub

    ''' <summary>
    ''' パラメータの読み込み
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadParameter()
        If System.IO.File.Exists(RECENT_PARAMETER_FILE) = False Then
            Return
        End If

        Dim ar As New List(Of String)
        Using sr As New StreamReader(RECENT_PARAMETER_FILE, Encoding.GetEncoding("Shift_JIS"))
            Dim line = sr.ReadLine()
            Do Until line Is Nothing
                ar.Add(line)
                line = sr.ReadLine()
            Loop
        End Using
        tbxMinDist.Text = ar(0)
        tbxP1.Text = ar(1)
        tbxP2.Text = ar(2)
        tbxMinRadius.Text = ar(3)
        tbxMaxRadius.Text = ar(4)
        tbxRecognizeParam.Text = ar(5)

        Me.minDist = Double.Parse(Me.tbxMinDist.Text)
        Me.p1 = Double.Parse(Me.tbxP1.Text)
        Me.p2 = Double.Parse(Me.tbxP2.Text)
        Me.minRadius = Integer.Parse(Me.tbxMinRadius.Text)
        Me.maxRadius = Integer.Parse(Me.tbxMaxRadius.Text)
        Me.DICE_AVERAGE = Integer.Parse(tbxRecognizeParam.Text)
    End Sub

    ''' <summary>
    ''' 結果を読み込み
    ''' </summary>
    Private Sub LoadResult()
        If System.IO.File.Exists(RECENT_COUNT_FILE) = False Then
            Return
        End If

        Dim ar As New List(Of String)
        Using sr As New StreamReader(RECENT_COUNT_FILE, Encoding.GetEncoding("Shift_JIS"))
            Dim line = sr.ReadLine()
            Do Until line Is Nothing
                ar.Add(line)
                line = sr.ReadLine()
            Loop
        End Using
        If ar.Count = 6 Then
            For i As Integer = 0 To ar.Count - 1
                Me.recognizedDiceResult(i) = Integer.Parse(ar(i))
            Next
        End If
    End Sub

    ''' <summary>
    ''' カメラデバイスオープン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCamOpen_Click(sender As Object, e As EventArgs) Handles btnCamOpen.Click
        'cam id
        Dim camId As Integer = CInt(Me.cbxCamID.SelectedItem.ToString())

        'open
        If m_capture Is Nothing Then
            m_capture = Cv.CreateCameraCapture(camId)
            Cv.SetCaptureProperty(m_capture, CaptureProperty.FrameWidth, 640)
            Cv.SetCaptureProperty(m_capture, CaptureProperty.FrameHeight, 480)
            bgWorker.RunWorkerAsync()

            btnCamOpen.Text = "CamClose"
            btnCamOpen.BackColor = Color.Aqua
        Else
            bgWorker.CancelAsync()

            btnCamOpen.Text = "CamOpen"
            btnCamOpen.BackColor = Color.AliceBlue
        End If
    End Sub

    ''' <summary>
    ''' サイコロ＆認識スタート
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'save image instance
        saveImage.Init()

        If isRunSequence = False Then
            If oSerialPort.IsOpen() = True AndAlso m_capture IsNot Nothing Then
                SendShoot()
                System.Threading.Thread.Sleep(2000)
                isRunSequence = True
                Me.countRecognize = 0
                sumDiceValue = 0.0

                btnStart.Text = "Stop"
                btnStart.BackColor = Color.DarkRed
            End If

            'reset values
            stateRecognize = DiceRecognizeState.RECOGNIZE

            'sleep count
            Dim temp As Integer
            If Integer.TryParse(Me.tbxSleepMs.Text, temp) = True Then
                Me.MAX_SLEEP_MS = temp
            Else
                Me.MAX_SLEEP_MS = 1000 'default
            End If
        Else
            isRunSequence = False
            stateRecognize = DiceRecognizeState.RECOGNIZE

            btnStart.Text = "Start"
            btnStart.BackColor = Color.AliceBlue
        End If
    End Sub

    ''' <summary>
    ''' Arduino　サイコロ振り信号を送信 1,2,3・・・ "d" is default 13[ms]
    ''' </summary>
    ''' <param name="v"></param>
    Private Sub SendShoot(Optional ByVal v As String = "d")
        Try
            If oSerialPort.IsOpen = False Then
                Return
            End If
            oSerialPort.Write(v, 0, 1)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' パラメータ更新
    ''' </summary>
    Private Sub UpdateRecognizeParameter()
        Dim temp As Double = 0.0
        If String.IsNullOrEmpty(tbxMinDist.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxMinDist.Text, temp) Then
                Me.minDist = Double.Parse(temp)
            End If
        End If

        If String.IsNullOrEmpty(tbxP1.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxP1.Text, temp) Then
                Me.p1 = Double.Parse(tbxP1.Text)
            End If
        End If

        If String.IsNullOrEmpty(tbxP2.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxP2.Text, temp) Then
                Me.p2 = Double.Parse(tbxP2.Text)
            End If
        End If

        If String.IsNullOrEmpty(tbxMinRadius.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxMinRadius.Text, temp) Then
                Me.minRadius = Integer.Parse(tbxMinRadius.Text)
            End If
        End If

        If String.IsNullOrEmpty(tbxMaxRadius.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxMaxRadius.Text, temp) Then
                Me.maxRadius = Integer.Parse(tbxMaxRadius.Text)
            End If
        End If

        '認識　平均数
        If String.IsNullOrEmpty(tbxRecognizeParam.Text) = True Then
            Return
        Else
            If Double.TryParse(tbxRecognizeParam.Text, temp) Then
                Me.DICE_AVERAGE = Integer.Parse(tbxRecognizeParam.Text)
            End If
        End If
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        Console.WriteLine("Done")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' マウスクリック時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub pbxIplImg_MouseDown(sender As Object, e As MouseEventArgs) Handles pbxCamRawImg.MouseDown
        Me.clickedPointOnPBX.X = e.X
        Me.clickedPointOnPBX.Y = e.Y
        Console.WriteLine("X={0},Y={1}", e.X, e.Y)
    End Sub

    ''' <summary>
    ''' Form size change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim pbxWidth = Me.pbxCamRawImg.Size.Width
        Dim pbxHeight = Me.pbxCamRawImg.Size.Height
        Me.lblStatus.Text = String.Format("PBXSize:{0}, {1}", pbxWidth, pbxHeight)
    End Sub

    Private Sub pbxIplImg_MouseMove(sender As Object, e As MouseEventArgs) Handles pbxCamRawImg.MouseMove
        Dim pbxWidth = Me.pbxCamRawImg.Size.Width
        Dim pbxHeight = Me.pbxCamRawImg.Size.Height

        'to rectangle cordinate
        Dim rectCoordinateX = e.X
        Dim rectCoordinateY = pbxHeight - e.Y

        Me.lblStatus.Text = String.Format("PBXSize:{0}, {1} Pos:{2}, {3}", pbxWidth, pbxHeight, rectCoordinateX, rectCoordinateY)
    End Sub

    Private Sub tbxMinDist_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxMinDist.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Me.minDist = Double.Parse(tbxMinDist.Text)
        End If
    End Sub

    Private Sub tbxP2_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxP1.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Me.p1 = Double.Parse(tbxP1.Text)
        End If
    End Sub

    Private Sub tbxP3_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxP2.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Me.p2 = Double.Parse(tbxP2.Text)
        End If
    End Sub

    Private Sub tbxMinRadius_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxMinRadius.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Me.minRadius = Integer.Parse(tbxMinRadius.Text)
        End If
    End Sub

    Private Sub tbxMaxRadius_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxMaxRadius.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Me.maxRadius = Integer.Parse(tbxMaxRadius.Text)
        End If
    End Sub

    ''' <summary>
    ''' Arduino ダイス振る送信
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        SendShoot("d")
    End Sub

    Private Sub tbxMinDist_TextChanged(sender As Object, e As EventArgs) Handles tbxMinDist.TextChanged
        UpdateRecognizeParameter()
    End Sub

    Private Sub tbxP1_TextChanged(sender As Object, e As EventArgs) Handles tbxP1.TextChanged
        UpdateRecognizeParameter()
    End Sub

    Private Sub tbxP2_TextChanged(sender As Object, e As EventArgs) Handles tbxP2.TextChanged
        UpdateRecognizeParameter()
    End Sub

    Private Sub tbxMinRadius_TextChanged(sender As Object, e As EventArgs) Handles tbxMinRadius.TextChanged
        UpdateRecognizeParameter()
    End Sub

    Private Sub tbxMaxRadius_TextChanged(sender As Object, e As EventArgs) Handles tbxMaxRadius.TextChanged
        UpdateRecognizeParameter()
    End Sub

    Private Sub tbxRecognizeParam_TextChanged(sender As Object, e As EventArgs) Handles tbxRecognizeParam.TextChanged
        UpdateRecognizeParameter()
        Me.countRecognize = 0
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////////
    'シリアルポート
    '/////////////////////////////////////////////////////////////////////////////////////////
    Private Sub btnPortOpen_Click(sender As Object, e As EventArgs) Handles btnPortOpen.Click
        Try
            If (oSerialPort.IsOpen) Then
                oSerialPort.Close()

                btnPortOpen.Text = "PortOpen"
                btnPortOpen.BackColor = Color.AliceBlue
            Else
                'port name
                Dim portName = cbxPort.SelectedItem.ToString()
                If portName.Length = 0 Then
                    Return
                End If

                'port open
                Me.oSerialPort.PortName = portName
                Me.oSerialPort.Open()

                btnPortOpen.Text = "PortClose"
                btnPortOpen.BackColor = Color.Aqua
            End If
        Catch ex As Exception

        End Try
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////////
    'グラフ
    '/////////////////////////////////////////////////////////////////////////////////////////
    Private Sub btnDebug_Click(sender As Object, e As EventArgs) Handles btnDebug.Click
        If recognizeDice > 0 AndAlso recognizeDice < 7 Then
            Me.recognizedDiceResult(recognizeDice - 1) += 1
            UpdateFrequency()
        End If
    End Sub

    Private Sub InitPlot()
        Me.oPlot.Model = New OxyPlot.PlotModel("Dice Frequency")
        Me.oPlot.BackColor = Color.White
        UpdatePlotYAxis(Me.recognizedDiceResult.Max)
    End Sub

    Private Sub UpdateFrequency()
        If Me.oPlot.Model Is Nothing Then
            Return
        End If

        'update Y Axis
        UpdatePlotYAxis(Me.recognizedDiceResult.Max)

        'update bar
        Me.oPlot.Model.Series.Clear()
        Dim series = New OxyPlot.Series.ColumnSeries()
        For i As Integer = 0 To Me.recognizedDiceResult.Length - 1
            series.Items.Add(New OxyPlot.Series.ColumnItem(Me.recognizedDiceResult(i)))
        Next
        Me.oPlot.Model.Series.Add(series)
        Me.oPlot.InvalidatePlot(True)

        'label update
        Dim sum = Me.recognizedDiceResult.Sum()
        Dim strLbl As String = String.Empty
        strLbl += String.Format("Total Shake count:{0}", sum)
        strLbl += vbCrLf
        For i As Integer = 0 To Me.recognizedDiceResult.Length - 1
            strLbl += String.Format("Dice {0} : {1}", i + 1, recognizedDiceResult(i))
            strLbl += vbCrLf
        Next
        lblDetail.Text = strLbl
    End Sub

    Private Sub UpdatePlotYAxis(ByVal max As Double)
        Me.oPlot.Model.Axes.Clear()
        Dim digit As Double = 0
        If max <= 0 Then
            digit = 1
        Else
            digit = Math.Floor(Math.Log10(max) + 1)
        End If
        Dim y = New Axes.LinearAxis()
        y.Position = Axes.AxisPosition.Left
        y.Minimum = 0
        y.Maximum = 10 ^ digit
        Me.oPlot.Model.Axes.Add(y)
    End Sub

    ''' <summary>
    ''' カウントを初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        '初期化
        For i As Integer = 0 To Me.recognizedDiceResult.Length - 1
            Me.recognizedDiceResult(i) = 0
        Next

        '累積ファイルを削除
        Try
            File.Delete(RECENT_DICE_FILE)
        Catch ex As Exception

        End Try

        UpdateFrequency()
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////////
    '最適化　実験
    '/////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>
    ''' 最適化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOpt_Click(sender As Object, e As EventArgs) Handles btnOpt.Click
        Try
            isOpt = True
            'opt
            Dim parameter = New optHoughParameter()
            parameter.SetIpl(Me.copyIPL, Integer.Parse(tbxCorrect.Text))
            Dim opt As New LibOptimization.Optimization.clsOptDE(parameter)
            opt.DEStrategy = LibOptimization.Optimization.clsOptDE.EnumDEStrategyType.DE_best_1_bin
            opt.PopulationSize = 30
            opt.IsUseCriterion = False
            opt.Iteration = 100
            opt.InitialPosition = {5, 50, 20, 10, 20}
            opt.LowerBounds = {1, 1, 1, 1, 1}
            opt.UpperBounds = {100, 100, 100, 100, 100}
            opt.Init()
            LibOptimization.Util.clsUtil.DebugValue(opt)
            While (opt.DoIteration(2) = False)
                LibOptimization.Util.clsUtil.DebugValue(opt)
            End While
            LibOptimization.Util.clsUtil.DebugValue(opt)
            Console.WriteLine("")
        Catch ex As Exception
            'nop
        Finally
            isOpt = False
        End Try
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////////
    'サイコロ認識部
    '/////////////////////////////////////////////////////////////////////////////////////////
#Region "Dice Recognize"
    ''' <summary>
    ''' USBカメラ画像更新
    ''' </summary>
    ''' <param name="frame"></param>
    Private Sub UpdateUsbCam(ByRef frame As IplImage)
        zoomRatio = frame.Width / pbxCamRawImg.Width '倍率
        Dim camWindowWidth = pbxCamRawImg.Width
        Dim camWindowHeight = pbxCamRawImg.Height
        Dim tempCamIPL = New IplImage(camWindowWidth, camWindowHeight, frame.Depth, frame.NChannels)
        Cv.Resize(frame, tempCamIPL, Interpolation.Linear)

        Dim mainBmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(tempCamIPL)
        If clickedPointOnPBX.X <> 0 AndAlso clickedPointOnPBX.Y <> 0 Then
            Using g = Graphics.FromImage(mainBmp)
                Dim skyBluePen = New Pen(Brushes.DeepSkyBlue)
                skyBluePen.Width = 2.0
                g.DrawRectangle(skyBluePen, New Rectangle(Me.clickedPointOnPBX, New Size(CLIP_SIZE / zoomRatio, CLIP_SIZE / zoomRatio)))
            End Using
        End If

        Me.BeginInvoke(
                    Sub()
                        Me.pbxCamRawImg.Image = mainBmp
                    End Sub
                    )
    End Sub

    ''' <summary>
    ''' サイコロ認識
    ''' </summary>
    ''' <param name="frame"></param>
    Private Sub RecognizeDiceFunc(ByRef frame As IplImage)
        Dim zoomIpl As IplImage = Nothing
        Dim clipedImage As IplImage = Nothing
        Using tempipl = clsUtil.ToGrayScale(frame)
            'クリック位置の座標取得（クリックされていない場合は0,0）
            If clickedPointOnPBX.X <> 0 AndAlso clickedPointOnPBX.Y <> 0 Then
                'nop
            Else
                clickedPointOnPBX.X = 0
                clickedPointOnPBX.Y = 0
            End If

            'ガウシアンフィルタ
            Cv.Smooth(tempipl, tempipl, SmoothType.Gaussian, 3, 3)

            'クリック位置→カメラ画像の位置に変換
            Dim camClickedPosition As New Point(clickedPointOnPBX.X * zoomRatio, clickedPointOnPBX.Y * zoomRatio)

            '拡大（ハフ変換を行う場合ある程度大きい方がよい）
            clipedImage = clsUtil.ClipIplROI(tempipl, camClickedPosition, CLIP_SIZE, CLIP_SIZE)
            Me.pbxCliped.ImageIpl = clipedImage
            Dim zoomSize = clipedImage.Size
            zoomSize.Height = 300   '300
            zoomSize.Width = 300    '300
            zoomIpl = New IplImage(zoomSize, BitDepth.U8, 1)
            Cv.Resize(clipedImage, zoomIpl, Interpolation.Cubic)
        End Using

        'Recent クリップ画像
        SyncLock objlock
            Me.recentBmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(clipedImage)
        End SyncLock

        'for opt
        Me.copyIPL = Cv.CloneImage(zoomIpl)

        'ハフ変換によるサイコロの目の読み取り認識
        Dim circleCount As Integer = 0
        Using storage = Cv.CreateMemStorage(0)
            'minDist – 検出される円の中心同士の最小距離．このパラメータが小さすぎると，正しい円の周辺に別の円が複数誤って検出されることになります．逆に大きすぎると，検出できない円がでてくる可能性があります．
            'param1 – 手法依存の 1 番目のパラメータ． CV_HOUGH_GRADIENT の場合は， Canny() エッジ検出器に渡される2つの閾値の内，大きい方の閾値を表します（小さい閾値は，この値の半分になります）．
            'param2 – 手法依存の 2 番目のパラメータ． CV_HOUGH_GRADIENT の場合は，円の中心を検出する際の投票数の閾値を表します．これが小さくなるほど，より多くの誤検出が起こる可能性があります．より多くの投票を獲得した円が，最初に出力されます．
            'minRadius – 円の半径の最小値．
            'maxRadius – 円の半径の最大値．
            Using circles = Cv.HoughCircles(zoomIpl, storage, HoughCirclesMethod.Gradient, 1, minDist, p1, p2, minRadius, maxRadius)
                For i = 0 To circles.Total - 1
                    Dim p = Cv.GetSeqElem(circles, i)
                    Cv.Circle(zoomIpl, Cv.Point(p.Value.Center.X, p.Value.Center.Y), p.Value.Radius, Cv.RGB(255, 0, 0))
                Next

                'UI側へ画像更新
                Me.BeginInvoke(
                    Sub()
                        Me.pbxImageFeature.ImageIpl = zoomIpl
                    End Sub)

                '円の数＝サイコロの数
                circleCount = circles.Total
            End Using
        End Using

        'status label
        Me.BeginInvoke(
                    Sub()
                        lblState.Text = "Recognize..."
                    End Sub)

        'debug
        Console.Write("{0} ", circleCount)

        'sum detect dice
        Me.sumDiceValue += CDbl(circleCount)

        'dice detect using average
        Me.countRecognize += 1
        If Me.countRecognize = DICE_AVERAGE Then
            'recognize Dice
            recognizeDice = CInt(Math.Round(sumDiceValue / CDbl(DICE_AVERAGE), MidpointRounding.AwayFromZero))

            'debug
            Console.WriteLine("")
            Console.WriteLine("DetectDice:{0}", recognizeDice)

            'output label(invokeしないと例外エラー）
            Dim isFailRecognize = (recognizeDice = 0) OrElse (recognizeDice > 6)
            Me.BeginInvoke(
                            Sub()
                                If isFailRecognize Then
                                    lblDice.Text = "unknown"
                                Else
                                    lblDice.Text = "Dice is " & recognizeDice.ToString()
                                End If
                            End Sub)

            'サイコロの目確認
            If isFailRecognize = False AndAlso isRunSequence = True Then
                '結果の保存
                Me.recognizedDiceResult(recognizeDice - 1) += 1 'update dice count
                Me.recognizedDiceResultList.Add(recognizeDice)

                'スレッドからUI部の変更
                Me.BeginInvoke(
                                Sub()
                                    UpdateFrequency()
                                    'Dim dices = New StringBuilder()
                                    'Dim diceRecogCount = Me.recognizedDice.Count
                                    'If diceRecogCount <> 200 Then
                                    '    For i As Integer = 0 To 200 - 1
                                    '        dices.Append(String.Format("{0} , ", recognizedDiceList(i)))
                                    '    Next
                                    'Else
                                    '    For i As Integer = diceRecogCount - 200 To 200 - 1
                                    '        dices.Append(String.Format("{0} , ", recognizedDiceList(i)))
                                    '    Next
                                    'End If
                                    'recentDice.Text = dices.ToString
                                End Sub)

                '定期保存
                If (Me.recognizedDiceResultList.Count Mod 50) = 0 Then
                    SaveData()
                End If

                '画像の保存
                Dim tempBmp As Bitmap = Nothing
                If Me.cbxCollectDataset.Checked = True Then
                    tempBmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(clipedImage)
                    saveImage.Save(recognizeDice, tempBmp)
                End If
            End If

            '初期化
            Me.countRecognize = 0
            Me.sumDiceValue = 0.0

            '次の状態へ
            If isRunSequence = True Then
                stateRecognize = DiceRecognizeState.ROLLDICE
            End If
        End If
    End Sub

    ''' <summary>
    ''' カメラ認識部分
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        If m_capture Is Nothing Then
            Return
        End If
        If isOpt = True Then
            Return
        End If

        'continuous capture
        Dim sw = New Stopwatch()
        While True
            'check cancel
            If bgWorker.CancellationPending Then
                e.Cancel = True
                m_capture.Dispose()
                m_capture = Nothing
                Console.WriteLine("WorkerDone")
                Return
            End If

            'sw start
            sw.Restart()

            '--------------------------------------------------------------------------
            'get image - full color
            '--------------------------------------------------------------------------
            Using frame = Cv.QueryFrame(m_capture)
                'rare case. e.g. reject usb cam etc.
                If frame Is Nothing Then
                    Continue While
                End If

                'USBカメラの画像更新
                UpdateUsbCam(frame)

                'state
                If stateRecognize = DiceRecognizeState.RECOGNIZE Then
                    '--------------------------------------------------------------------------
                    'Dice Recognize using hough
                    '--------------------------------------------------------------------------
                    RecognizeDiceFunc(frame)

                ElseIf stateRecognize = DiceRecognizeState.ROLLDICE Then
                    '--------------------------------------------------------------------------
                    'Rool a Dice
                    '--------------------------------------------------------------------------
                    'status label
                    Me.BeginInvoke(
                        Sub()
                            lblState.Text = "Shoot..."
                        End Sub)

                    SendShoot("g") 'a:10, d:13(default) 

                    'init
                    isRunSequence = False
                    Me.countRecognize = 0
                    sumDiceValue = 0.0

                    'move next state
                    stateRecognize = DiceRecognizeState.SLEEP
                ElseIf stateRecognize = DiceRecognizeState.SLEEP Then
                    '--------------------------------------------------------------------------
                    'Sleep
                    '--------------------------------------------------------------------------
                    'status label
                    Me.BeginInvoke(
                        Sub()
                            lblState.Text = "wait..."
                        End Sub)

                    elapsedSleepMs += sw.ElapsedMilliseconds
                    If elapsedSleepMs > MAX_SLEEP_MS Then
                        'サイコロが斜めになるのを抑止
                        SendShoot("1") 'a:10, d:13(default) 
                        Threading.Thread.Sleep(150)
                        Console.WriteLine("elapsedSleepMs:{0}", elapsedSleepMs)

                        '次の状態へ
                        elapsedSleepMs = 0
                        isRunSequence = True
                        Me.countRecognize = 0
                        sumDiceValue = 0.0

                        'move next state
                        stateRecognize = DiceRecognizeState.RECOGNIZE
                    End If
                End If
            End Using

            'calc fps
            If Me.countRecognize = 0 Then
                'Console.WriteLine("{0}[FPS]", 1000.0 / sw.ElapsedMilliseconds)
            End If

            'カメラ画像取得タイミング調整　コメントアウトすると最速
            Threading.Thread.Sleep(1)

            'ガーベージコレクト　これをしないとメモリがたまりつづける。
            If GC.GetTotalMemory(False) > 1024 * 1024 * 128 Then
                GC.Collect()
            End If
        End While
    End Sub

    ''' <summary>
    ''' サイコロ認識画像の保存(デバッグ用)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOneshotCollect_Click(sender As Object, e As EventArgs) Handles btnOneshotCollect.Click
        SyncLock objlock
            If recentBmp IsNot Nothing Then
                Me.recentBmp.Save("test.bmp")
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' プロセス起動
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Console.WriteLine("Check Start Process")

        'スレッドの共有資源をロックしてから実行
        SyncLock objlock
            If recentBmp IsNot Nothing Then
                '直近の画像を保存
                Me.recentBmp.Save("test.bmp")

                '同期で呼び出し

                'テスト用プログラム
                Dim programPath = "..\..\..\CheckExitStatus\bin\Debug\CheckExitStatus.exe"
                Dim imgPath = "test.bmp"

                Dim commandStr = String.Format("{0} {1}", programPath, imgPath)

                'プロセス起動
                Dim psi As New System.Diagnostics.ProcessStartInfo
                psi.FileName = programPath
                'psi.Arguments = "test"
                'psi.Arguments = imgPath
                Dim p = System.Diagnostics.Process.Start(psi)
                p.WaitForExit()

                '終了コード取得
                If p.ExitCode = -1 Then
                    Console.WriteLine(" Exit code : {0}", p.ExitCode)
                Else
                    Console.WriteLine(" Exit code : {0}", p.ExitCode)
                End If
            End If
        End SyncLock
    End Sub
#End Region

End Class
