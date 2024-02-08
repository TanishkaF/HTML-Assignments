<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataList.aspx.cs" Inherits="AspWebAppPratice2.DataList1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
    <asp:DataList DataSourceID = "School" runat="server" ID="Student">
        <ItemTemplate>
            <asp:Label ID = "ClassIDLabel" runat="server" Text='<%# Eval("EmployeeID") %>' />
            
            <asp:Label ID = "Label2" runat="server" Text='<%# Eval("EmployeeName") %>' />
          
            <asp:Label ID = "Label3" runat="server" Text='<%# Eval("DepartmentID") %>' />
          
            <br />
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource runat = "server" ID="School" ConnectionString="<%$ConnectionStrings:STUDENTConnectionString2 %>" SelectCommand="SELECT * FROM [employee]"></asp:SqlDataSource>
</div>
        </div>
    </form>
</body>
</html>
