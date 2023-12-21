Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Drawing.Printing

Public Class Form4
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Sub kondisiAwal()
        Using Conn As New SqlConnection(stringConnection)
            Conn.Open()
            Using sda As New SqlDataAdapter("select id, nama, no_telp, lapangan, tanggal, jam from TBL_USER", Conn)
                Using ds As New DataSet
                    sda.Fill(ds, "TBL_USER")
                    DataGridView1.DataSource = (ds.Tables("TBL_USER"))
                End Using
            End Using
            Conn.Close()
        End Using
    End Sub
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kondisiAwal()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Visible = False
        Form2.Visible = True
    End Sub

    Private Sub KelolaMenuToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Visible = False
        Form3.Visible = True
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Visible = False
        Form1.Show()

    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        Using conn As New SqlConnection(stringConnection)
            conn.Open()
            Using cmd As New SqlCommand("select id,nama,no_telp,lapangan,tanggal, jam from TBL_USER where CONVERT(date,tanggal) between @from and @end order by tanggal desc", conn)
                cmd.Parameters.AddWithValue("@from", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@end", Format(DateTimePicker2.Value, "yyyy-MM-dd"))
                Using sda As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet
                    sda.Fill(ds, "TBL_USER")
                    DataGridView1.DataSource = ds.Tables("TBL_USER")
                End Using
            End Using
            conn.Close()
        End Using
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Using conn As New SqlConnection(stringConnection)
            conn.Open()
            Using cmd As New SqlCommand("SELECT tanggal FROM TBL_USER WHERE CONVERT(date, tanggal) BETWEEN @from AND @end", conn)
                cmd.Parameters.AddWithValue("@from", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@end", Format(DateTimePicker2.Value, "yyyy-MM-dd"))

                Dim sdr As SqlDataReader = cmd.ExecuteReader

                If sdr.HasRows Then
                    While sdr.Read()
                        ' Access data using sdr
                        Dim tanggal As DateTime = sdr.GetDateTime(0)
                        ' Do something with tanggal
                    End While
                End If

                sdr.Close()
            End Using

            conn.Close()

        End Using
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim printDialog As New PrintDialog()
        If printDialog.ShowDialog() = DialogResult.OK Then
            PD.Print()
        End If
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim FONTJUDUL As New Font("Times New Roman", 18, FontStyle.Bold)
        Dim FONTDATA As New Font("Times New Roman", 11, FontStyle.Regular)
        Dim FONTHEADER As New Font("Times New Roman", 11, FontStyle.Bold)

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        Dim kiri As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center
        kiri.Alignment = StringAlignment.Near
        e.Graphics.DrawString("GOR MEKARSARI BADMINTON CLUB", FONTJUDUL, Brushes.Blue, 450, 25, tengah)
        e.Graphics.DrawString("kel.Mekarsari .........", FONTJUDUL, Brushes.Blue, 450, 55, tengah)


        e.Graphics.DrawLine(Pens.Black, 30, 90, 805, 90)

        e.Graphics.DrawString("LAPORAN REKAPITULASI DATA PENYEWAAN LAPANGAN", FONTJUDUL, Brushes.Blue, 450, 95, tengah)

        e.Graphics.DrawLine(Pens.Black, 30, 130, 805, 130)
        e.Graphics.DrawLine(Pens.Black, 30, 133, 30, 259)

        e.Graphics.DrawString("NOMOR", FONTHEADER, Brushes.Black, 100, 135, kanan)
        e.Graphics.DrawLine(Pens.Black, 120, 133, 120, 259)

        e.Graphics.DrawString("TANGGAL", FONTHEADER, Brushes.Black, 150, 135, kiri)
        e.Graphics.DrawLine(Pens.Black, 250, 133, 250, 259)

        e.Graphics.DrawString("NAMA", FONTHEADER, Brushes.Black, 318, 135, kanan)
        e.Graphics.DrawLine(Pens.Black, 340, 133, 340, 259)

        e.Graphics.DrawString("JAM", FONTHEADER, Brushes.Black, 420, 135, kanan)
        e.Graphics.DrawLine(Pens.Black, 460, 133, 460, 259)

        e.Graphics.DrawString("WAKTU", FONTHEADER, Brushes.Black, 556, 135, kanan)
        e.Graphics.DrawLine(Pens.Black, 600, 133, 600, 259)

        e.Graphics.DrawString("LAPANGAN", FONTHEADER, Brushes.Black, 755, 135, kanan)
        e.Graphics.DrawLine(Pens.Black, 805, 133, 805, 259)

        e.Graphics.DrawLine(Pens.Black, 30, 155, 805, 155)
        Dim baris As Integer = 185
        Dim NOMOR As Integer = 1
        Dim jumlahBeli As Integer
        Using conn As New SqlConnection(stringConnection)
            conn.Open()
            Using cmd As New SqlCommand("select * from TBL_USER where CONVERT(date,tanggal) between @from and @end", conn)
                cmd.Parameters.AddWithValue("@from", Format(DateTimePicker1.Value, "yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@end", Format(DateTimePicker2.Value, "yyyy-MM-dd"))
                Dim dr As SqlDataReader = cmd.ExecuteReader
                dr.Read()
                While dr.Read
                    e.Graphics.DrawString(NOMOR, FONTDATA, Brushes.Black, 90, baris, kanan)

                    e.Graphics.DrawString(dr("tanggal"), FONTDATA, Brushes.Black, 215, baris, kanan)
                    e.Graphics.DrawString(dr("nama"), FONTDATA, Brushes.Black, 312, baris, kanan)


                    e.Graphics.DrawString(dr("jam"), FONTDATA, Brushes.Black, 420, baris, kanan)


                    e.Graphics.DrawString(dr("waktu"), FONTDATA, Brushes.Black, 550, baris, kanan)

                    e.Graphics.DrawString(dr("lapangan"), FONTDATA, Brushes.Black, 755, baris, kanan)


                    NOMOR = NOMOR + 1

                    baris = baris + 20


                    'jumlahBeli += dr("total_bayar")

                End While

                e.Graphics.DrawLine(Pens.Black, 30, baris + 35, 805, baris + 35)
                e.Graphics.DrawString("Mengetahui", FONTDATA, Brushes.Black, 700, 500, kanan)
                e.Graphics.DrawString("Admin", FONTDATA, Brushes.Black, 693, 516, kanan)
                e.Graphics.DrawString("(Arya Fadhil)", FONTDATA, Brushes.Black, 722, 595, kanan)
                e.Graphics.DrawLine(Pens.Black, 30, 985, 805, 985)

                e.Graphics.DrawString("PAGE 1 of 1", FONTDATA, Brushes.Black, 760, 995, kanan)

                dr.Close()
            End Using
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DashboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DashboardToolStripMenuItem.Click
        Me.Close()
        Form2.Show()

    End Sub
End Class