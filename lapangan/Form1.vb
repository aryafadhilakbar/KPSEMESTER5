Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.PasswordChar = "*"
    End Sub
    Sub kosongkan()
        txtPassword.Text = ""
        txtUsername.Text = ""
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtUsername.Text = "1111" And txtPassword.Text = "1111" Then
            MsgBox("Selamat datang admin ^_^", vbInformation, "SUKSES")
            Form2.Show()
            Me.Hide()
        Else
            txtUsername.Text = ""
            txtPassword.Text = ""
            MsgBox("Uppss,password atau username anda salah:( ", vbCritical, "GAGAL")
        End If
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtPassword.PasswordChar = ControlChars.NullChar

        Else
            txtPassword.PasswordChar = "*"
        End If
    End Sub
End Class
