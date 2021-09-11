<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="add_data.aspx.cs" Inherits="factory.Check.add_data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>新增數據</title>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <style>
        td {
            padding-right:10px
        }
        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            font-size: x-large;
        }

        .auto-style3 {
            color: #FF0000;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px;border:1px solid rgba(0, 0, 0, 0.125);">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">新增檢測數據</asp:Label>
                </strong> 
        </div>

        <div class="col-xxl-7 card" style="padding:0px;margin-top:20px">
        <div style="margin-bottom:10px;padding-left:10px">
            <strong>
            <asp:Label ID="Label2" runat="server" Text="成品品質紀錄" CssClass="auto-style2"></asp:Label>
            </strong>
        </div>
        <div id="data" runat="server" style="padding-left:10px">
            <strong>
            <asp:RadioButtonList ID="rbl_M" runat="server" RepeatDirection="Horizontal" CssClass="auto-style1">
            </asp:RadioButtonList>
            
            <asp:RadioButtonList ID="rbl_P" runat="server" RepeatDirection="Horizontal" CssClass="auto-style1">
            </asp:RadioButtonList>
            </strong>
        </div>
        <div id="datas" runat="server">
            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-right:10px">
                    <B>水份</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_M" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>%</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-left:0px;padding-right:10px">
                    <B>比表面積</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_S" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>㎡/kg</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-right:10px">
                    <B>篩餘</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_R" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>%</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right"></div>
                <div class="text-left">
                    <strong>
                    <asp:Label ID="lb_error" runat="server" Text="至少填選一個值" CssClass="auto-style3" Visible="False"></asp:Label>
                    </strong>
                    <asp:Button ID="btn_save" runat="server" Class="btn btn-outline-primary" Text="儲存" style="margin-left:143px" OnClick="btn_save_Click"/>
                </div>
            </div>
        </div>
        <div id="tip" runat="server" class="col-xxl-3 col-lg-4 col-sm-5 bg-light">
            <div>
                <asp:Label ID="lb_M1" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <asp:Label ID="lb_M2" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <asp:Label ID="lb_M3" runat="server" Text=""></asp:Label>
            </div>

            <div class="text-right">
                <asp:Label ID="lb_save" runat="server" Text="已經儲存"></asp:Label>
            </div>
        </div>
        </div>
    </div>
    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
        }
    </script>
</asp:Content>
