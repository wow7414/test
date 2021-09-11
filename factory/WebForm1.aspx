<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="factory.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:CheckBox ID="CheckBox1" runat="server"  onclick="xd()" />
        <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" DataKeyNames="FactoryID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="FactoryID" HeaderText="FactoryID" ReadOnly="True" SortExpression="FactoryID" />
                <asp:BoundField DataField="FactoryName" HeaderText="FactoryName" SortExpression="FactoryName" />
                <asp:BoundField DataField="aOrder" HeaderText="aOrder" SortExpression="aOrder" />
                <asp:BoundField DataField="ServerIP" HeaderText="ServerIP" SortExpression="ServerIP" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory]"></asp:SqlDataSource>
    </form>
</body>
    <script>
        function xd() {
            var ck = document.getElementById("CheckBox1");
            if (ck.checked == true) {
                alert("xd");
                console.log(ck.checked);
            }
            else {
                alert("ff");
            }
        }
        var c = document.getElementById("GV1").rows.length; 
        for (var i = 1; i < c; i++){
            var x = document.getElementsByTagName("tr")[i].cells[1].textContent;
            console.log(x);
        }
    </script>
</html>
