Imports System.Data.SqlClient


Public Class RequestsList
    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")

    Private Sub RequestsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From Requests WHERE CONCAT([Item Code], [Item Name], Quantity) like '%" & valueToSearch & "%'"

        Dim command As New SqlCommand(searchQuery, connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        DataGridView1.DataSource = table
    End Sub

    Private Sub BTN_FILTER_Click(sender As Object, e As EventArgs) Handles BTN_FILTER.Click

        FilterData(TextBox1.Text)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        FilterData(TextBox1.Text)

    End Sub

    Public Sub ExecuteQuery(query As String)

        Dim command As New SqlCommand(query, connection)

        connection.Open()

        command.ExecuteNonQuery()

        connection.Close()

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        Dim updateQuery As String = "Update Requests Set Status = '" & TextBox6.Text & "' WHERE EmpID =" & TextBox2.Text & ""
        ExecuteQuery(updateQuery)
        MessageBox.Show("Data Updated")

    End Sub

    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click

        Dim deleteQuery As String = "delete from Requests Where EmpID = " & TextBox2.Text
        ExecuteQuery(deleteQuery)
        MessageBox.Show("Request deleted")

    End Sub
End Class