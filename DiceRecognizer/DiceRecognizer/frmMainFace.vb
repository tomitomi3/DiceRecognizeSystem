Imports OpenCvSharp

Public Class frmMainFace
    Private m_capture As CvCapture = Nothing
    Private GC_CALL_SIZE As Integer = 1024 * 1024 * 100
    Private m_draw As Bitmap = Nothing
    Private objlock = New Object()
    Private isOpen As Boolean = False

    Private clickedPoint As New Point(0, 0)

    Declare Function AllocConsole Lib "kernel32" () As Int32
    Declare Function FreeConsole Lib "kernel32" () As Int32

    Private camDeviceList As New List(Of CvCapture)

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Console
        AllocConsole()

        'list of Camera id
        Dim camIds As New List(Of Integer)
        Dim temp As CvCapture = Nothing
        For i As Integer = 0 To 10 - 1
            temp = Nothing
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

        'Combo box
        cbxCamID.DropDownStyle = ComboBoxStyle.DropDownList
        cbxCamID.Items.Clear()
        For Each camId In camIds
            cbxCamID.Items.Add(camId.ToString())
        Next
        If cbxCamID.Items.Count <> 0 Then
            cbxCamID.SelectedIndex = 0
        End If

        'size mode
        Me.pbxIplImg.SizeMode = PictureBoxSizeMode.Normal

        'bgworker setting
        Me.bgWorker.WorkerSupportsCancellation = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        'cam id
        Dim camId As Integer = CInt(Me.cbxCamID.SelectedItem.ToString())

        'open
        If m_capture Is Nothing Then
            m_capture = Cv.CreateCameraCapture(camId)
            Cv.SetCaptureProperty(m_capture, CaptureProperty.FrameWidth, 320)
            Cv.SetCaptureProperty(m_capture, CaptureProperty.FrameHeight, 240)
            bgWorker.RunWorkerAsync()
        Else
            bgWorker.CancelAsync()
        End If
    End Sub

    Private Sub bgWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        If m_capture Is Nothing Then
            Return
        End If

        'continuous capture
        Dim mainBmp As Bitmap = Nothing
        Dim sw = New System.Diagnostics.Stopwatch()

        While True
            sw.Restart()

            'check cancel
            If bgWorker.CancellationPending Then
                e.Cancel = True
                m_capture.Dispose()
                m_capture = Nothing
                Console.WriteLine("WorkerDone")
                Return
            End If

            'get image - full color
            Using frame = Cv.QueryFrame(m_capture)
                'rare case. e.g. reject usb cam etc.
                If frame Is Nothing Then
                    Continue While
                End If

                'one shot create bmp instance
                mainBmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame)

                'check rect
                If clickedPoint.X <> 0 AndAlso clickedPoint.Y <> 0 Then
                    'clip clone image
                    Using temp = clsUtil.ClipIplROI(frame, clickedPoint, 100, 100)
                        Me.pbxCliped.ImageIpl = temp
                    End Using
                    'draw rectangle
                    Using g = Graphics.FromImage(mainBmp)
                        Dim skyBluePen = New Pen(Brushes.DeepSkyBlue)
                        skyBluePen.Width = 2.0
                        g.DrawRectangle(skyBluePen, New Rectangle(Me.clickedPoint, New Size(100, 100)))
                    End Using
                End If

                'hoge
                clsUtil.DetectFace(frame)
                Me.pbxImageFeature.ImageIpl = frame

                'to PictureBox
                Me.pbxIplImg.Image = mainBmp
            End Using

            'wait
            Dim targetMs As Integer = 200
            Dim remain = targetMs - sw.ElapsedMilliseconds
            If remain > 0 Then
                Threading.Thread.Sleep(remain)
            End If

            'calc fps
            sw.Stop()
            Dim fps As Double = 1000.0 / sw.ElapsedMilliseconds
            Me.BeginInvoke( _
                Sub()
                    Me.lblFps.Text = String.Format("{0:f1}[FPS]", fps)
                End Sub
            )

            'gc
            If GC.GetTotalMemory(False) > 1024 * 1024 * 64 Then
                GC.Collect()
            End If
        End While
    End Sub

    Private Sub bgWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        Console.WriteLine("Done")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub pbxIplImg_MouseDown(sender As Object, e As MouseEventArgs) Handles pbxIplImg.MouseDown, pbxImageFeature.MouseDown
        Me.clickedPoint.X = e.X
        Me.clickedPoint.Y = e.Y
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim pbxWidth = Me.pbxIplImg.Size.Width
        Dim pbxHeight = Me.pbxIplImg.Size.Height
        Me.statusLabel.Text = String.Format("PBXSize:{0}, {1}", pbxWidth, pbxHeight)
    End Sub

    Private Sub pbxIplImg_MouseMove(sender As Object, e As MouseEventArgs) Handles pbxIplImg.MouseMove, pbxImageFeature.MouseMove
        Dim pbxWidth = Me.pbxIplImg.Size.Width
        Dim pbxHeight = Me.pbxIplImg.Size.Height

        'to rectangle cordinate
        Dim rectCoordinateX = e.X
        Dim rectCoordinateY = pbxHeight - e.Y

        Me.statusLabel.Text = String.Format("PBXSize:{0}, {1} Pos:{2}, {3}", pbxWidth, pbxHeight, rectCoordinateX, rectCoordinateY)
    End Sub
End Class
