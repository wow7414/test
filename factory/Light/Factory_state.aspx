<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Factory_state.aspx.cs" Inherits="factory.Light.Factory_state" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>
        工廠狀態
    </title>
    <style>
        .b {
            border:1px solid rgba(0, 0, 0, 0.125)
        }
        .b:hover{
            background:gainsboro;
        }
    </style>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top:5px">
        <div class="row justify-content-around">
            <div class="col-auto b">
                <div>
                    <asp:ImageButton ID="imgb_KY" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示觀音廠燈號資料"/>
                </div>
                <div>
                    <strong>觀音廠</strong>
                </div>
            </div>
            <div class="col-auto b">
                <asp:ImageButton ID="imgb_BL" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示八里廠燈號資料"/>
                <div>
                    <strong>八里廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_QX" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示全興廠燈號資料"/>
                <div>
                    <strong>全興廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_ZB" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示彰濱廠燈號資料"/>
                <div>
                    <strong>彰濱廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_KH" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示高雄廠燈號資料"/>
                <div>
                    <strong>高雄廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_LD" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示龍德廠燈號資料"/>
                <div>
                    <strong>龍德廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_LZ" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" />
                <div>
                    <strong>利澤廠</strong>
                </div>
            </div>

            <div class="col-auto b">
                <asp:ImageButton ID="imgb_HL" runat="server" ImageUrl="~/img/disconnected.png" Width="50px" OnClick="imgb_Click" data-placement="right" data-toggle="tooltip" title="顯示花蓮廠燈號資料"/>
                <div>
                    <strong>花蓮廠</strong>
                </div>
            </div>
        </div>
        <div style="padding-top:3%">
            <div class="row justify-content-center">
                <div class="col-auto col-sm-10 col-lg-8">
                    <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" DataKeyNames="DTime,FactoryID" DataSourceID="SDS1" AllowPaging="True" AllowSorting="True" PageSize="15" OnPageIndexChanging="GV1_PageIndexChanging" OnDataBound="GV1_DataBound">
                        <Columns>
                            <asp:BoundField DataField="DTime" HeaderText="時間" ReadOnly="True" SortExpression="DTime" />
                            <asp:BoundField DataField="FactoryID" HeaderText="工廠代號" ReadOnly="True" SortExpression="FactoryID" />
                            <asp:TemplateField HeaderText="燈號" SortExpression="Light" HeaderStyle-CssClass="text-center">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Light") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <center>
                                        <asp:Label ID="lb_light" runat="server" Text='<%# Bind("Light") %>' Visible="False"></asp:Label>
                                        <asp:Image ID="img_light" runat="server" Width="20px" />
                                    </center>
                                </ItemTemplate>

<HeaderStyle CssClass="text-center"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
            <PagerTemplate>
                <div style="text-align:right">
                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>
                            <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>
                           /<asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>
                            <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                    </div>
            </PagerTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory_State] ORDER BY [DTime] DESC"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
