Module Module1

    Function Main(args() As String) As Integer
        If args.Length = 0 Then
            Console.WriteLine("->Error {0}", args.Length)
            Return -1
        ElseIf args.Length = 1 Then
            Console.WriteLine("->Call process {1}", args(0))
            Return 0
        Else
            Console.WriteLine("->Error {0}", args.Length)
            Return -1
        End If
    End Function

End Module
