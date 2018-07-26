Imports System.Data.SqlClient


Public Class Employee
    Dim connection As New SqlConnection("Server= lmbpcfbd0301; Database = stationerymanagementsystem; Integrated Security = true")

    Private Sub Employee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterData("")
    End Sub

    Public Sub FilterData(valueToSearch As String)

        Dim searchQuery As String = "SELECT * From Login WHERE CONCAT([EmployeeName], [EmployeeId], [Username],[Password]) like '%" & valueToSearch & "%'"

        Dim command As New SqlCommand(searchQuery, connection)
        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        DataGridView2.DataSource = table
    End Sub

    Private Sub BTN_FILTER_Click(sender As Object, e As EventArgs) Handles BTN_FILTER.Click

        FilterData(TextBox6.Text)

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

        FilterData(TextBox6.Text)

    End Sub

    Public Sub ExecuteQuery(query As String)

        Dim command As New SqlCommand(query, connection)

        connection.Open()

        command.ExecuteNonQuery()

        connection.Close()

    End Sub



    Private Sub BTN_UPDATE_Click(sender As Object, e As EventArgs) Handles BTN_UPDATE.Click
        Dim updateQuery As String = "Update Login Set [EmployeeName] = '" & TextBox7.Text & "',[Username]= '" & TextBox9.Text & "', [Password]= '" & TextBox10.Text & "' WHERE EmployeeId =" & TextBox8.Text & ""
        ExecuteQuery(updateQuery)
        MessageBox.Show("Data Updated")

    End Sub

    Private Sub BTN_DELETE_Click(sender As Object, e As EventArgs) Handles BTN_DELETE.Click

        Dim deleteQuery As String = "delete from Login Where EmployeeId = " & TextBox8.Text
        ExecuteQuery(deleteQuery)
        MessageBox.Show("Record deleted")

    End Sub

    

   
    
    Private Sub BTN_INSERT_Click(sender As Object, e As EventArgs) Handles BTN_INSERT.Click
        Dim command As New SqlCommand("insert into Login([EmployeeName],[EmployeeId], [Username], [Password]) values(@en,@ei,@un,@pw)", connection)

        command.Parameters.Add("@en", SqlDbType.VarChar).Value = TextBox7.Text
        command.Parameters.Add("@ei", SqlDbType.Int).Value = TextBox8.Text
        command.Parameters.Add("@un", SqlDbType.VarChar).Value = TextBox9.Text
        command.Parameters.Add("@pw", SqlDbType.VarChar).Value = TextBox10.Text

        connection.Open()
        If command.ExecuteNonQuery() = 1 Then

            MessageBox.Show("New Employee Added")

        Else

            MessageBox.Show("Employee Not Added")

        End If

        connection.Close()

    End Sub


End Class