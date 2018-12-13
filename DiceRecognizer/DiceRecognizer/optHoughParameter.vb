Imports OpenCvSharp

Public Class optHoughParameter
    Inherits LibOptimization.Optimization.absObjectiveFunction
    Private _ipl As IplImage = Nothing
    Private _correct As Integer = 0

    ''' <summary>
    ''' パラメータを求めるための評価関数
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public Overrides Function F(x As List(Of Double)) As Double
        For Each temp In x
            If temp <= 0 Then
                Return 100.0
            End If
        Next

        Dim val = 100.0
        Using storage = Cv.CreateMemStorage(0)
            Using circles = Cv.HoughCircles(_ipl, storage, HoughCirclesMethod.Gradient, 1, x(0), x(1), x(2), CInt(x(3)), CInt(x(4)))
                If circles.Total Then
                    Return val
                Else
                    val = (_correct - circles.Total) ^ 2
                    Return val
                End If
            End Using
        End Using
        Return 100
    End Function

    Public Overrides Function Gradient(x As List(Of Double)) As List(Of Double)
        Return Nothing
    End Function

    Public Overrides Function Hessian(x As List(Of Double)) As List(Of List(Of Double))
        Return Nothing
    End Function

    Public Overrides Function NumberOfVariable() As Integer
        Return 5
    End Function

    Public Sub SetIpl(ByRef copyIPL As IplImage, ByVal correct As Integer)
        _ipl = copyIPL
        _correct = correct
    End Sub
End Class
