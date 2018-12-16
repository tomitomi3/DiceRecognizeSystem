Imports System.IO
''' <summary>
''' CNNを使って認識
''' </summary>
Public Class clsRecognizeUsingCNN
    Inherits abstractDiceRecognize

    ''' <summary>
    ''' Recognize func status
    ''' </summary>
    Public Enum ReturnCode
        RecognizeError = -1
    End Enum

    Public Sub New(ByRef syncobj As Object)
        MyBase.New(syncobj)
    End Sub

    ''' <summary>
    ''' サイコロ認識(ret 0->1, 1->2 -1->error)
    ''' </summary>
    ''' <param name="recentBmp"></param>
    ''' <returns></returns>
    Public Overrides Function DiceRecognize(ByRef recentBmp As Bitmap) As Integer
        'スレッドの共有資源をロックしてから実行
        SyncLock _objlock
            If recentBmp IsNot Nothing Then
                'フォルダ確認
                If Directory.Exists("C:\_recentDice") = False Then
                    System.IO.Directory.CreateDirectory("C:\_recentDice")
                End If

                '直近の画像を保存
                Dim dicePath = "C:\_recentDice\recentDice.bmp"
                recentBmp.Save(dicePath)

                'Pythonを呼び出し
                Using p = New Process
                    'コマンドプロンプト設計
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.RedirectStandardInput = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

                    '起動
                    p.Start()

                    '実行
                    Using consoleWrite = p.StandardInput
                        '推定
                        '↓環境に応じpythonコードを変更
                        Dim programPath = "C:\_recentDice\exit_return_id.py"
                        If File.Exists(programPath) = False Then
                            Console.WriteLine("Not exist program")
                            Return -1
                        End If

                        '実行
                        If consoleWrite.BaseStream.CanWrite = True Then
                            '↓環境に応じ変更 anacondaをインストールした環境
                            consoleWrite.WriteLine("C:\Users\tomitomi\Anaconda3\Scripts\activate.bat C:\Users\tomitomi\Anaconda3")
                            consoleWrite.WriteLine("python " & programPath)
                            consoleWrite.WriteLine("exit()")
                        End If
                    End Using

                    '結果呼び出し
                    Dim results As New List(Of String)
                    While Not p.StandardOutput.EndOfStream
                        results.Add(p.StandardOutput.ReadLine())
                    End While

                    '終了まで待機
                    p.WaitForExit()

                    '結果を返却
                    Try
                        Dim resultDice As Integer = -1
                        If Integer.TryParse(results(8), resultDice) = True Then
                            Return resultDice
                        Else
                            Return -1
                        End If
                    Catch ex As Exception
                        Return -1
                    End Try
                End Using
            End If
        End SyncLock

        Return -1
    End Function
End Class
