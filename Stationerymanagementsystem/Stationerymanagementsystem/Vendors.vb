Imports System.Data.SqlClient


Public Class Vendors
    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")

    Private Sub Vendors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterData("")

        Dim command As New SqlCommand("select * from ItemsList", connection)

        Dim adapter As New SqlDataAdapter(command)

        Dim table As New DataTable()

        adapter.Fill(table)

        ComboBox1.DataSource = table

        ComboBox1.DisplayMember = "Item Name"
        ComboBox1.ValueMember = "Item Code"


    End Sub


    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From Vendors WHERE CONCAT([VendorName], [VendorId], [Contact],[Location]) like '%" & valueToSearch & "%'"

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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim command As New SqlCommand("insert into Vendors([VendorId],[VendorName], [Contact], [Location]) values(@vi,@vn,@contact,@location)", connection)

        command.Parameters.Add("@vi", SqlDbType.Int).Value = TextBox10.Text
        command.Parameters.Add("@vn", SqlDbType.VarChar).Value = TextBox11.Text
        command.Parameters.Add("@contact", SqlDbType.Char).Value = TextBox12.Text
        command.Parameters.Add("@location", SqlDbType.VarChar).Value = TextBox13.Text

        connection.Open()
        If command.ExecuteNonQuery() = 1 Then

            MessageBox.Show("New Vendor Added")

        Else

            MessageBox.Show("Vendor Not Added")

        End If

        connection.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim updateQuery As String = "Update Vendors Set [Contact] = '" & TextBox12.Text & "',[Location]= '" & TextBox13.Text & "' WHERE VendorId=" & TextBox10.Text & ""
        ExecuteQuery(updateQuery)
        MessageBox.Show("Data Updated")
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim deleteQuery As String = "delete from Vendors Where VendorId = " & TextBox10.Text
        ExecuteQuery(deleteQuery)
        MessageBox.Show("Record deleted")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New SqlCommand("insert into Orders([OrderCode],[VendorName], [VendorId], [ItemCode],[ItemName],[Quantity]) values(@oc,@vn,@vi,@ic,@in,@quantity)", connection)

        command.Parameters.Add("@oc", SqlDbType.Int).Value = TextBox2.Text
        command.Parameters.Add("@vn", SqlDbType.VarChar).Value = TextBox3.Text
        command.Parameters.Add("@vi", SqlDbType.Int).Value = TextBox4.Text
        command.Parameters.Add("@ic", SqlDbType.Int).Value = TextBox5.Text
        command.Parameters.Add("@in", SqlDbType.VarChar).Value = ComboBox1.Text
        command.Parameters.Add("@quantity", SqlDbType.Int).Value = TextBox7.Text

        connection.Open()
        If command.ExecuteNonQuery() = 1 Then

            MessageBox.Show("Order Placed")

        Else

            MessageBox.Show("Order could not be placed")

        End If

        connection.Close()
    End Sub
End Class

