<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_update.aspx.cs" Inherits="factory.Tag_update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>修改Tag</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:25%;float:left">
    <asp:Label ID="Label1" runat="server" Text="顯示:"></asp:Label>
    <asp:DropDownList ID="ddl_display" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_display_SelectedIndexChanged">
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem Selected="True">20</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
    </asp:DropDownList>
    </div>
    <div>
        <asp:Button ID="btn_confirm" runat="server" Text="查看日報表" class="btn btn-primary" Height="32px" OnClick="btn_confirm_Click" />
        <asp:Button ID="btn_Trend" runat="server" Text="查看趨勢圖" class="btn btn btn-success" Height="32px" OnClick="btn_Trend_Click"  />
    </div>
    <div style="width:60%">
    <asp:GridView ID="GV1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" DataKeyNames="ServerName,TagName" DataSourceID="SqlDataSource1" PageSize="20" OnPageIndexChanging="GV1_PageIndexChanging">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Update" ImageUrl="~/img/save.png" Width="20px" />
                    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" CommandName="Cancel" ImageUrl="~/img/error.png" Width="20px" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/img/edit.png" Width="20px" />
                    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/delete.png" Width="20px" CommandName="Delete" OnClientClick="return confirm('確定要刪除嗎?');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ServerName" HeaderText="ServerName" ReadOnly="True" SortExpression="ServerName" />
            <asp:BoundField DataField="TagName" HeaderText="TagName" ReadOnly="True" SortExpression="TagName" />
            <asp:TemplateField HeaderText="SourceTag" SortExpression="SourceTag">
                <EditItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SourceTag") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SourceTag") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TagDesc" SortExpression="TagDesc">
                <EditItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("TagDesc") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("TagDesc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Rep_Live" HeaderText="Rep_Live" SortExpression="Rep_Live" Visible="False" />
            <asp:CheckBoxField DataField="Rep_Hour" HeaderText="Rep_Hour" SortExpression="Rep_Hour" />
            <asp:CheckBoxField DataField="Rep_Min" HeaderText="Rep_Min" SortExpression="Rep_Min" />
        </Columns>
                            <PagerTemplate>
            <table>
                <tr>
                <!--    <td style="text-align: right">  --> 
                                <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>&nbsp;&nbsp;
                                 <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                                / <asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                  <!--  </td>--> 
                </tr>
            </table>
            </PagerTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" DeleteCommand="DELETE FROM [TagList] WHERE [ServerName] = @ServerName AND [TagName] = @TagName" InsertCommand="INSERT INTO [TagList] ([ServerName], [TagName], [SourceTag], [TagDesc], [Rep_Live], [Rep_Hour], [Rep_Min]) VALUES (@ServerName, @TagName, @SourceTag, @TagDesc, @Rep_Live, @Rep_Hour, @Rep_Min)" SelectCommand="SELECT * FROM [TagList]" UpdateCommand="UPDATE [TagList] SET [SourceTag] = @SourceTag, [TagDesc] = @TagDesc, [Rep_Live] = @Rep_Live, [Rep_Hour] = @Rep_Hour, [Rep_Min] = @Rep_Min WHERE [ServerName] = @ServerName AND [TagName] = @TagName">
        <DeleteParameters>
            <asp:Parameter Name="ServerName" Type="String" />
            <asp:Parameter Name="TagName" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ServerName" Type="String" />
            <asp:Parameter Name="TagName" Type="String" />
            <asp:Parameter Name="SourceTag" Type="String" />
            <asp:Parameter Name="TagDesc" Type="String" />
            <asp:Parameter Name="Rep_Live" Type="Boolean" />
            <asp:Parameter Name="Rep_Hour" Type="Boolean" />
            <asp:Parameter Name="Rep_Min" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="SourceTag" Type="String" />
            <asp:Parameter Name="TagDesc" Type="String" />
            <asp:Parameter Name="Rep_Live" Type="Boolean" />
            <asp:Parameter Name="Rep_Hour" Type="Boolean" />
            <asp:Parameter Name="Rep_Min" Type="Boolean" />
            <asp:Parameter Name="ServerName" Type="String" />
            <asp:Parameter Name="TagName" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
        </div>
</asp:Content>
