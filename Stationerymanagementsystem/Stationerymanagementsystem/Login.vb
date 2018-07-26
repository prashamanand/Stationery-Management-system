Imports System.Data.SqlClient
Public Class Login
    Dim con As New SqlClient.SqlConnection
    Dim da As New SqlClient.SqlDataAdapter
    Dim ds As New DataSet
    Dim sqlquery As String
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=lmbpcfbd0301;Initial Catalog=stationerymanagementsystem;Integrated Security=True"

    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            'checking if the username and password field is null

            If Len(Trim(txtUsername.Text)) = 0 Then
                MessageBox.Show("Enter the user name", "Input Error !", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
                txtUsername.Focus()
            End If
            If Len(Trim(txtPassword.Text)) = 0 Then
                MessageBox.Show("Enter the password", "Input Error !", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
                txtPassword.Focus()
            End If

            'executing SQL Query for retrieving the username and password from the database table

            con.Open()
            sqlquery = "SELECT * FROM Login WHERE Username='" & txtUsername.Text & "' and Password='" & txtPassword.Text & "' "
            da = New SqlClient.SqlDataAdapter(sqlquery, con)
            da.Fill(ds, "Login")
            If ds.Tables("Login").Rows.Count <> 0 Then
                MessageBox.Show("Login succeed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Request.Show()
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
        txtPassword.Clear()
        txtUserName.Clear()
    End Sub

    ' code for show password
    Private Sub chkshowpass_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowpass.CheckedChanged
        If chkshowpass.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

End Class
