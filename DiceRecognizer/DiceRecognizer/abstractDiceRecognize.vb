Public MustInherit Class abstractDiceRecognize
    Protected _objlock As Object = Nothing

    ''' <summary>
    ''' デフォルトコンストラクタ
    ''' </summary>
    Private Sub New()
        'nop
    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="syncobj"></param>
    Public Sub New(ByRef syncobj As Object)
        Me._objlock = syncobj
    End Sub

    ''' <summary>
    ''' サイコロ認識 仮想関数
    ''' </summary>
    ''' <param name="recentBmp"></param>
    ''' <returns></returns>
    Public MustOverride Function DiceRecognize(ByRef recentBmp As Bitmap) As Integer

End Class
