Imports System.Data.SqlClient
Public Class ItemsList
    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")


    Private Sub ItemsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From ItemsList WHERE CONCAT([Item Code], [Item Name], [Available],[Unit],[Issued],[Remaining]) like '%" & valueToSearch & "%'"

        Dim command As New SqlCommand(searchQuery, connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        DataGridView1.DataSource = table
    End Sub

    Private Sub BTN_FILTER_Click(sender As Object, e As EventArgs) Handles BTN_FILTER.Click

        FilterData(TextBox1.Text)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

        FilterData(TextBox1.Text)

    End Sub

    Public Sub ExecuteQuery(query As String)

        Dim command As New SqlCommand(query, connection)

        connection.Open()

        command.ExecuteNonQuery()

        connection.Close()

    End Sub

   
    Private Sub BTN_INSERT_Click(sender As Object, e As EventArgs) Handles BTN_INSERT.Click
        Dim command As New SqlCommand("insert into ItemsList([Item Code],[Item Name], [Available], [Unit]) values(@ic,@in,@available,@unit)", connection)

        command.Parameters.Add("@ic", SqlDbType.Int).Value = TextBox2.Text
        command.Parameters.Add("@in", SqlDbType.VarChar).Value = TextBox3.Text
        command.Parameters.Add("@available", SqlDbType.Int).Value = TextBox4.Text
        command.Parameters.Add("@unit", SqlDbType.VarChar).Value = TextBox5.Text
        

        connection.Open()
        If command.ExecuteNonQuery() = 1 Then

            MessageBox.Show("New Item Added")

        Else

            MessageBox.Show("Item Not Added")

        End If

        connection.Close()
    End Sub

    Private Sub BTN_UPDATE_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        Dim updateQuery As String = "Update ItemsList Set Issued = '" & TextBox6.Text & "' WHERE [Item Code] =" & TextBox2.Text & ""
        ExecuteQuery(updateQuery)
        MessageBox.Show("Item Updated")

    End Sub


    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click
        Dim deleteQuery As String = "delete from ItemsList Where [Item Code] = " & TextBox2.Text
        ExecuteQuery(deleteQuery)
        MessageBox.Show("Item deleted")

    End Sub
End Class