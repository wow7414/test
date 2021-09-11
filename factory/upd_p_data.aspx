<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="upd_p_data.aspx.cs" Inherits="factory.upd_p_data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>修改個人資料</title>
    <script src="js/jquery-3.5.1.slim.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top:120px">
        <div class="row align-items-center">
            <div class="col-lg-5 col-sm-6 col-3 text-right" style="padding-right:0px">舊密碼:</div>
            <div class="col-7 col-sm-6">
                <asp:TextBox ID="tb_pwd" runat="server" class="form-control w-auto" TextMode="Password" required></asp:TextBox>
            </div>
        </div>
        <br/>
        <div class="row align-items-center">
            <div class="col-lg-5 col-sm-6 col-3 text-right" style="padding-right:0px">新密碼:</div>
            <div class="col-7 col-sm-6">
                <asp:TextBox ID="tb_npwd" runat="server" class="form-control w-auto" TextMode="Password" required></asp:TextBox>
            </div>
        </div>
        <br/>
        <div class="row align-items-center">
            <div class="col-lg-5 col-sm-6 col-3 text-right" style="padding-right:0px">確認密碼:</div>
            <div class="col-7 col-sm-6">
                <asp:TextBox ID="tb_cfpwd" runat="server" class="form-control w-auto" TextMode="Password" required></asp:TextBox>
            </div>
        </div>
        <div class="row" style="height:24px">
            <div class="col-lg-5 col-sm-6 col-3"></div>
            <div class="text-left">
                <strong>
                <asp:Label ID="lb_err" runat="server" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-5 col-sm-6 col-3"></div>
            <div class="text-left">
                <asp:Button ID="btn_confirm" runat="server" Text="確認" class="btn btn-primary" style="margin-left: 70px;" OnClick="confirm_Click" />
            </div>
        </div>

    </div>
</asp:Content>
