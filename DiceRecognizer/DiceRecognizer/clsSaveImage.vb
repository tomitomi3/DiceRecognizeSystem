Imports System.IO

Public Class clsSaveImage
    Private _nowExePath As String = String.Empty
    Private _dicePath As New List(Of String)
    Private _diceRecent As New List(Of Integer)

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        _nowExePath = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
    End Sub

    ''' <summary>
    ''' 初期化
    ''' </summary>
    Public Sub Init()
        _dicePath.Clear()
        _diceRecent.Clear()

        For i As Integer = 0 To 5
            Dim tempPath = String.Format("{0}\{1}", _nowExePath, i + 1)
            If System.IO.Directory.Exists(tempPath) = False Then
                'ディレクトリ作成
                System.IO.Directory.CreateDirectory(tempPath)
            End If

            'ダイスの目とパスの対応
            _dicePath.Add(tempPath)

            'ファイル情報を取得
            Dim allFile = Directory.GetFiles(tempPath, "*", SearchOption.AllDirectories)
            If allFile.Count = 0 Then
                _diceRecent.Add(0)
            Else
                Array.Sort(allFile)
                Dim recentFileName = IO.Path.GetFileNameWithoutExtension(allFile(allFile.Count - 1))
                Dim recentNo = Integer.Parse(recentFileName)
                _diceRecent.Add(recentNo)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <param name="recognizeDice"></param>
    ''' <param name="tempBmp"></param>
    Public Sub Save(ByVal recognizeDice As Integer, tempBmp As Bitmap)
        'これから書き込むファイルのNo
        _diceRecent(recognizeDice - 1) += 1

        'パスを取得
        Dim path As String = _dicePath(recognizeDice - 1)
        Dim savePath As String = String.Format("{0}\{1:D5}.bmp", path, _diceRecent(recognizeDice - 1))
        tempBmp.Save(savePath)
    End Sub
End Class
