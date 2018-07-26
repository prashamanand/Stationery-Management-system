
Imports System.Data.SqlClient

Public Class Request

    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")

    Public Sub ItemsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim command As New SqlCommand("select * from ItemsList", connection)

        Dim adapter As New SqlDataAdapter(command)

        Dim table As New DataTable()

        adapter.Fill(table)

        ComboBox1.DataSource = table

        ComboBox1.DisplayMember = "Item Name"
        ComboBox1.ValueMember = "Item Code"

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim command As New SqlCommand("insert into Requests([EmpID],[Item Code], [Item Name], Quantity) values(@id,@ic,@in,@quantity)", connection)

        command.Parameters.Add("@id", SqlDbType.Int).Value = TextBox3.Text
        command.Parameters.Add("@ic", SqlDbType.Int).Value = TextBox1.Text
        command.Parameters.Add("@in", SqlDbType.VarChar).Value = ComboBox1.Text
        command.Parameters.Add("@quantity", SqlDbType.Int).Value = TextBox2.Text

        connection.Open()
        If command.ExecuteNonQuery() = 1 Then

            MessageBox.Show("New Request Added")

        Else

            MessageBox.Show("Request Not Added")

        End If

        connection.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From Requests WHERE [EmpID] =" & TextBox3.Text

        Dim command As New SqlCommand(searchQuery, connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        DataGridView1.DataSource = table
    End Sub

    Private Sub BTN_FILTER_Click(sender As Object, e As EventArgs) Handles BTN_FILTER.Click

        FilterData(TextBox4.Text)

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        FilterData(TextBox4.Text)

    End Sub

    Public Sub ExecuteQuery(query As String)

        Dim command As New SqlCommand(query, connection)

        connection.Open()

        command.ExecuteNonQuery()

        connection.Close()

    End Sub

End Class