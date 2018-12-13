Imports OpenCvSharp

Public Class clsUtil
    'ref
    'http://schima.hatenablog.com/entry/20090621/1245595172

    ''' <summary>
    ''' Resize
    ''' </summary>
    ''' <param name="ai_iplImg"></param>
    ''' <param name="ai_ratio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ResizeIpl(ByVal ai_iplImg As IplImage, ByVal ai_ratio As Double, ByVal ai_intepolation As Interpolation) As IplImage
        Dim srcW = ai_iplImg.Width
        Dim srcH = ai_iplImg.Height

        Dim retImg = Cv.CreateImageHeader(New CvSize(CInt(srcW * ai_ratio), CInt(srcH * ai_ratio)), ai_iplImg.Depth, ai_iplImg.NChannels)
        Cv.Resize(ai_iplImg, retImg, ai_intepolation)

        Return retImg
    End Function

    ''' <summary>
    ''' convert grayscale
    ''' </summary>
    ''' <param name="iplimg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ToGrayScale(ByVal iplimg As IplImage) As IplImage
        Dim dst = New IplImage(iplimg.Size, BitDepth.U8, 1)
        Cv.CvtColor(iplimg, dst, ColorConversion.BgrToGray)
        Return dst
    End Function

    ''' <summary>
    ''' Clip IplImage
    ''' </summary>
    ''' <param name="iplimg"></param>
    ''' <param name="clickedPoint"></param>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ClipIplROI(ByVal iplimg As IplImage, ByVal clickedPoint As Point, ByVal width As Integer, ByVal height As Integer) As IplImage
        Dim retIpl As IplImage = Nothing
        Try
            Cv.SetImageROI(iplimg, New CvRect(clickedPoint.X, clickedPoint.Y, width, height))
            retIpl = Cv.CloneImage(iplimg)
            Return retIpl
        Finally
            Cv.ResetImageROI(iplimg)
        End Try
    End Function

    ''' <summary>
    ''' Hough
    ''' </summary>
    ''' <param name="frame"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DoHough(ByVal frame As IplImage) As CvSeq(Of CvCircleSegment)
        Dim ret As CvSeq(Of CvCircleSegment) = Nothing

        Using storage = Cv.CreateMemStorage(0)
            Using tempipl = clsUtil.ToGrayScale(frame)
                Cv.Smooth(tempipl, tempipl, SmoothType.Gaussian, 5, 5)
                Dim circles = Cv.HoughCircles(tempipl, storage, HoughCirclesMethod.Gradient, 1, 100, 20, 50, 10, 200)
                Return circles
            End Using
        End Using

        '                          for (i = 0; i < circles->total; i++)
        '{
        '  p = (float *) cvGetSeqElem (circles, i);
        '		  cvCircle (src_img, cvPoint (cvRound (p[0]), cvRound (p[1])), 3, CV_RGB (0, 255, 0), -1, 8, 0);//中心点
        '		  cvCircle (src_img, cvPoint (cvRound (p[0]), cvRound (p[1])), cvRound (p[2]), CV_RGB (255, 0, 0), 3, 8, 0);//円をトレース
        '     			printf ("円らしきもの%dつ発見 中心(%d,%d) 半径%d\r", circles->total,cvRound(p[0]),cvRound(p[1]),cvRound(p[2]));
        '  }

        Return ret
    End Function

    ''' <summary>
    ''' Detect face using CvHaar
    ''' </summary>
    ''' <param name="ai_inputImg"></param>
    ''' <remarks></remarks>
    Public Shared Sub DetectFace(ByRef ai_inputImg As IplImage)
        'Dim path = "..\..\..\..\_haarcascade\haarcascade_frontalface_default.xml"
        Dim path = "C:\_bitbucket_formft2015\DiceRecognizer\_haarcascade\haarcascade_frontalface_default.xml"
        Using faceDetector As OpenCvSharp.CvHaarClassifierCascade = OpenCvSharp.Cv.Load(Of OpenCvSharp.CvHaarClassifierCascade)(path)
            Using cvMem As New OpenCvSharp.CvMemStorage
                Using imgGray As New OpenCvSharp.IplImage(ai_inputImg.Size, OpenCvSharp.BitDepth.U8, 1)
                    OpenCvSharp.Cv.CvtColor(ai_inputImg, imgGray, OpenCvSharp.ColorConversion.BgrToGray)
                    OpenCvSharp.Cv.EqualizeHist(imgGray, imgGray)
                    cvMem.Clear()
                    For Each face In OpenCvSharp.Cv.HaarDetectObjects( _
                                            ai_inputImg,
                                            faceDetector,
                                            cvMem,
                                            1.1,
                                            10,
                                            0,
                                            New OpenCvSharp.CvSize(30, 30))
                        ai_inputImg.DrawRect(face, New OpenCvSharp.CvColor(255, 0, 0), 2)
                    Next
                End Using
            End Using
        End Using

        'Using cvImg = New OpenCvSharp.IplImage(filename, OpenCvSharp.LoadMode.Color)
        '    Me.Emgu_PictureBox.Image = cvImg.ToBitmap
        'End Using
    End Sub
End Class
