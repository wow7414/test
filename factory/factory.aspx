<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="factory.aspx.cs" Inherits="factory.factory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工廠</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:55%;float:left">
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
                <asp:Label ID="Label1" runat="server" Text="廠區:" CssClass="auto-style1"></asp:Label>
            <asp:DropDownList ID="ddl_factory" runat="server" DataSourceID="SDS1" DataTextField="FactoryName" DataValueField="FactoryID" AutoPostBack="True" OnSelectedIndexChanged="ddl_factory_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Description:" CssClass="auto-style1"></asp:Label>
                <asp:TextBox ID="tb_Desc" runat="server" Width="147px"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" Text="SourceTag:" CssClass="auto-style1"></asp:Label>
            <asp:TextBox ID="tb_source" runat="server" Width="147px"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="不顯示:" CssClass="auto-style1"></asp:Label>

            <asp:CheckBoxList ID="cbl_check" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="1" Selected="True">$</asp:ListItem>
            </asp:CheckBoxList>

            <asp:Button ID="btn_select" runat="server" Text="查詢" class="btn btn-primary" Height="32px" OnClick="btn_select_Click"/> 
            <asp:Label ID="Label6" runat="server" Text="顯示:" CssClass="auto-style1"></asp:Label>
            <asp:DropDownList ID="ddl_display" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_display_SelectedIndexChanged">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem Selected="True">20</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div style="float:left;padding-left:20%" class="text-start">
                <asp:Button ID="btn_confirm" runat="server" Text="送出" class="btn btn-primary" Height="32px" Visible="False"  />  
           </div>
        <div>
            <div style="width:52%;float:left">
            <asp:GridView ID="GV1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SDS2" PageSize="20" CssClass="table table-sm table-hover table-bordered" border="1" DataKeyNames="TagName"  OnPageIndexChanging="GV1_PageIndexChanging" OnSelectedIndexChanged="GV1_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:TemplateField HeaderText="TagName" SortExpression="TagName" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TagName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_TagName" runat="server" Text='<%# Bind("TagName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="True" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="SourceTag" HeaderText="SourceTag" SortExpression="SourceTag" />
                    <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" />
                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                </Columns>
                    <PagerTemplate>
            <table>
                <tr>
                    <td style="text-align: right">   
                                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>
                                 <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>
                                / <asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            </PagerTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:RuntimeConnStr %>" SelectCommand="select T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName order by L.DateTime desc"></asp:SqlDataSource>
        </div>
        <div style="width:48%;float:left">
            <asp:GridView ID="GV2" runat="server" AutoGenerateColumns="False" DataKeyNames="ServerName,TagName" CssClass="table table-sm table-hover table-bordered" PageSize="20" OnSelectedIndexChanged="GV2_SelectedIndexChanged" style="margin-top: 0px">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="刪除"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ServerName" HeaderText="ServerName" ReadOnly="True" SortExpression="ServerName" />
                    <asp:BoundField DataField="TagName" HeaderText="TagName" ReadOnly="True" SortExpression="TagName" />
                    <asp:BoundField DataField="SourceTag" HeaderText="SourceTag" SortExpression="SourceTag" />
                    <asp:BoundField DataField="TagDesc" HeaderText="TagDesc" SortExpression="TagDesc" />
                    <asp:TemplateField HeaderText="Rep_Live" SortExpression="Rep_Live">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Rep_Live") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cb_Rep_Live" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rep_Hour" SortExpression="Rep_Hour">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cb_Rep_Hour" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rep_Min" SortExpression="Rep_Min">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox3" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cb_Rep_Min" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>
