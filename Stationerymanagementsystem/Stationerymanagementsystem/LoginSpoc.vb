Imports System.Data.SqlClient

Public Class LoginSPOC

    Dim con As New SqlClient.SqlConnection
    Dim da As New SqlClient.SqlDataAdapter
    Dim ds As New DataSet
    Dim sqlquery As String

    Private Sub btn_Login1_Click(sender As Object, e As EventArgs) Handles btn_Login1.Click
        Try
            'checking if the username and password field is null

            If Len(Trim(txtUsername1.Text)) = 0 Then
                MessageBox.Show("Enter the user name", "Input Error !", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
                txtUsername1.Focus()
            End If
            If Len(Trim(txtPassword1.Text)) = 0 Then
                MessageBox.Show("Enter the password", "Input Error !", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
                txtPassword1.Focus()
            End If

            'executing SQL Query for retrieving the username and password from the database table
            con.ConnectionString = "Data Source=lmbpcfbd0301;Initial Catalog=stationerymanagementsystem;Integrated Security=True"
            con.Open()
            sqlquery = "SELECT * FROM LoginSPOC WHERE Username='" & txtUsername1.Text & "' and Password='" & txtPassword1.Text & "' "
            da = New SqlClient.SqlDataAdapter(sqlquery, con)
            da.Fill(ds, "LoginSPOC")
            If ds.Tables("LoginSPOC").Rows.Count <> 0 Then
                MessageBox.Show("Login succeed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                admin.Show()

            Else
                MessageBox.Show("Invalid user name and password", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            con.Close()

            clear()    'calling clear method here

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception generated", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' declaring a method for clearing the controls
    Public Sub clear()
        txtPassword1.Clear()
        txtUsername1.Clear()
    End Sub

    ' code for show password
    Private Sub chkshowpass_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowpass.CheckedChanged
        If chkShowpass.Checked = True Then
            txtPassword1.UseSystemPasswordChar = False
        Else
            txtPassword1.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class
