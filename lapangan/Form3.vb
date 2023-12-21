Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form3
    Sub kondisiAwal()
        Using conn As New SqlConnection(stringConnection)
            conn.Open()
            Using sda As New SqlDataAdapter("select * from TBL_USER order by id desc ", conn)
                Using ds As New DataSet
                    ds.Clear()
                    sda.Fill(ds, "TBL_USER")
                    DataGridView1.DataSource = ds.Tables("TBL_USER")
                End Using
            End Using
            conn.Close()
        End Using
    End Sub
    Sub kosongkan()
        txtKode.Text = ""
        txtNama.Text = ""
        txtid.Text = ""
        txtjam.Text = ""
        txtwaktu.Text = ""
        cb1.Text = ""
    End Sub
    Sub txtenabled()
        txtKode.Enabled = True
        txtNama.Enabled = True
        txtid.Enabled = True
        txtjam.Enabled = True
        cb1.Enabled = True
        txtwaktu.Enabled = True
    End Sub
    Sub txtdisabled()
        txtKode.Enabled = False
        txtNama.Enabled = False
        txtid.Enabled = False
        txtjam.Enabled = False
        cb1.Enabled = False
        txtwaktu.Enabled = False

    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kondisiAwal()
        txtdisabled()

    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs)

        Me.Visible = False
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If btnTambah.Text = "Tambah" Then
            btnTambah.Text = "Simpan"
            txtenabled()
            kosongkan()
        Else
            If txtKode.Text = "" Or txtNama.Text = "" Or txtid.Text = "" Then
                MsgBox("Semua Field Harus Terisi")
            Else
                If MessageBox.Show("Yakin Tambah Data Menu?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Using conn As New SqlConnection(stringConnection)
                        conn.Open()
                        Using cmd As New SqlCommand("INSERT INTO TBL_USER (id, nama, no_telp, lapangan, tanggal, jam, waktu) VALUES (@id, @nama, @kode, @lapangan, @tanggal, @jam, @waktu); SELECT SCOPE_IDENTITY();", conn)
                            cmd.Parameters.AddWithValue("@id", txtid.Text)
                            cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                            cmd.Parameters.AddWithValue("@kode", txtKode.Text)
                            cmd.Parameters.AddWithValue("@lapangan", cb1.Text)
                            cmd.Parameters.AddWithValue("@tanggal", dtp.Value.ToString("yyyy-MM-dd")) ' Format the date correctly
                            cmd.Parameters.AddWithValue("@jam", txtjam.Text)
                            cmd.Parameters.AddWithValue("@waktu", txtwaktu.Text)
                            cmd.ExecuteNonQuery()
                            kondisiAwal()
                            kosongkan()
                            txtdisabled()
                            btnTambah.Text = "Tambah"
                        End Using

                        conn.Close()


                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If btnEdit.Text = "Edit" Then
            btnEdit.Text = "Simpan"
            txtenabled()
        Else
            If txtKode.Text = "" Or txtNama.Text = "" Or txtid.Text = "" Then
                MsgBox("Semua Field Harus Terisi")
            Else
                If MessageBox.Show("Yakin Update Data Menu?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Using conn As New SqlConnection(stringConnection)
                        conn.Open()
                        Using cmd As New SqlCommand("UPDATE TBL_USER SET id=@id, nama=@nama, no_telp=@kode, lapangan=@lapangan, tanggal=@tanggal, jam=@jam, waktu=@waktu WHERE id=@id", conn)
                            cmd.Parameters.AddWithValue("@id", txtid.Text)
                            cmd.Parameters.AddWithValue("@nama", txtNama.Text)
                            cmd.Parameters.AddWithValue("@kode", txtKode.Text)
                            cmd.Parameters.AddWithValue("@lapangan", cb1.Text)
                            cmd.Parameters.AddWithValue("@tanggal", dtp.Value.ToString("yyyy-MM-dd"))
                            cmd.Parameters.AddWithValue("@jam", txtjam.Text)
                            cmd.Parameters.AddWithValue("@waktu", txtwaktu.Text)
                            cmd.ExecuteNonQuery()
                            kondisiAwal()
                            kosongkan()
                            txtdisabled()
                            btnEdit.Text = "Edit"
                        End Using
                        conn.Close()

                    End Using
                End If
            End If
        End If
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtKode.Text = "" Or txtNama.Text = "" Or txtid.Text = "" Then
            MsgBox("Field Masih Kosong")
        Else
            If MessageBox.Show("Yakin Update Data Menu?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Using conn As New SqlConnection(stringConnection)
                    conn.Open()
                    Using cmd As New SqlCommand("delete from TBL_USER where id=@id", conn)
                        cmd.Parameters.AddWithValue("@id", txtid.Text) ' Adding the parameter for id
                        cmd.ExecuteNonQuery()
                        kondisiAwal()
                        kosongkan()
                    End Using
                    conn.Close()
                End Using
            End If
        End If
    End Sub
    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            txtid.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(0).Value), "", DataGridView1.CurrentRow.Cells(0).Value)
            txtNama.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(1).Value), "", DataGridView1.CurrentRow.Cells(1).Value)
            txtKode.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(2).Value), "", DataGridView1.CurrentRow.Cells(2).Value)
            cb1.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(3).Value), "", DataGridView1.CurrentRow.Cells(3).Value)
            txtjam.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(5).Value), "", DataGridView1.CurrentRow.Cells(5).Value)
            txtwaktu.Text = IIf(IsDBNull(DataGridView1.CurrentRow.Cells(6).Value), "", DataGridView1.CurrentRow.Cells(6).Value)


            Dim dateCellValue As Object = DataGridView1.CurrentRow.Cells(4).Value
            Dim dateString As String = If(dateCellValue Is DBNull.Value, String.Empty, dateCellValue.ToString())

            Dim parsedDate As DateTime
            If DateTime.TryParse(dateString, parsedDate) Then
                dtp.Value = parsedDate
            Else

            End If
        End If


    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Using conn As New SqlConnection(stringConnection)
            conn.Open()
            Using cmd As New SqlCommand("select * from TBL_USER where nama Like Concat('%',@cari,'%')", conn)
                cmd.Parameters.AddWithValue("@cari", txtCari.Text)
                Dim sda As New SqlDataAdapter(cmd)
                Using ds As New DataSet
                    sda.Fill(ds, "TBL_USER")
                    DataGridView1.DataSource = ds.Tables("TBL_USER")
                End Using
            End Using
            conn.Close()
        End Using
    End Sub

    Private Sub LaporanPenjualanToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Me.Visible = False
        Form4.Visible = True
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Visible = False
        Form1.Visible = True
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub txtid_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtid_TextChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtjam.TextChanged

    End Sub

    Private Sub DashboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DashboardToolStripMenuItem.Click
        Me.Visible = False
        Form2.Visible = True

    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class