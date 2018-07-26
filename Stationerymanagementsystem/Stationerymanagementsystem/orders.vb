Imports System.Data.SqlClient
Public Class orders
    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")
    Private Sub orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From Orders WHERE CONCAT([OrderCode],[VendorName],[VendorId],[ItemCode],[ItemName],[Quantity],[Status]) like '%" & valueToSearch & "%'"

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

    Private Sub BTN_UPDATE_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        Dim updateQuery As String = "Update Orders Set [Status] = '" & TextBox3.Text & "' WHERE OrderCode=" & TextBox2.Text & ""
        ExecuteQuery(updateQuery)
        MessageBox.Show("Status Updated")
    End Sub

    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click
        Dim deleteQuery As String = "delete from Orders Where OrderCode = " & TextBox2.Text
        ExecuteQuery(deleteQuery)
        MessageBox.Show("Order deleted")

    End Sub
End Class