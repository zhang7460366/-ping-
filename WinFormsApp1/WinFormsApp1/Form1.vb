Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Threading

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i As Integer = 0 To 255
            Dim ip As String = TextBox1.Text & i.ToString()
            Dim pingSender As New Ping()
            AddHandler pingSender.PingCompleted, AddressOf PingCompleted
            pingSender.SendAsync(ip, 100, i)

        Next
    End Sub

    Private Sub PingCompleted(sender As Object, e As PingCompletedEventArgs)
        If e.Reply.Status = IPStatus.Success Then
            If e.Reply.RoundtripTime < 200 Then
                Me.Invoke(Sub() UpdateLabel(e.UserState.ToString(), Color.Green))
            Else
                Me.Invoke(Sub() UpdateLabel(e.UserState.ToString(), Color.Red))
            End If
        End If
    End Sub

    Private Sub UpdateLabel(labelIndex As String, color As Color)
        Dim label As Label = Me.Controls("Label" & labelIndex)
        label.BackColor = color
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As Integer = 20
        Dim y As Integer = 20
        Dim labelIndex As Integer = 0
        For i As Integer = 0 To 255
            Dim label As New Label()
            label.Text = i.ToString()
            label.AutoSize = True
            label.Location = New Point(x, y)
            label.Name = "Label" & i.ToString()
            label.BackColor = Color.Red
            label.ForeColor = Color.Black
            Me.Controls.Add(label)
            x += 40
            labelIndex += 1
            If labelIndex Mod 16 = 0 Then
                x = 20
                y += 20
            End If
        Next
    End Sub
End Class
