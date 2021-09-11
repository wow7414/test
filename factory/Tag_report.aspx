<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_report.aspx.cs" Inherits="factory.Tag_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>日報表</title>
    <meta http-equiv="refresh" content="1800">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:55%">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" DataKeyNames="DataDateTime,SourceServer,TagName" DataSourceID="SDS1" PageSize="20">
            <Columns>
                <asp:BoundField DataField="DataDateTime" HeaderText="DataDateTime" ReadOnly="True" SortExpression="DataDateTime" />
                <asp:BoundField DataField="SourceServer" HeaderText="SourceServer" ReadOnly="True" SortExpression="SourceServer" />
                <asp:BoundField DataField="TagName" HeaderText="TagName" ReadOnly="True" SortExpression="TagName" />
                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
            </Columns>

        </asp:GridView>
        <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Value_Hour] WHERE DataDateTime >= DATEADD(DAY,DATEDIFF(DAY,0,getdate()),0)"></asp:SqlDataSource>
</div>
   
    </asp:Content>
