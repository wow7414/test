<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mill_report.aspx.cs" Inherits="factory.Mill.Mill_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/Chart.min.js"></script>
    <script src="../js/hammerjs@2.0.8.js"></script>  
    <script src="../js/chartjs-plugin-zoom.js"></script>
    <script src="../js/freeze-table.js"></script>
    <script>
            $(function () {
                $("#<%=tb_SDATE.ClientID%>").datepicker({
            closeText: "關閉",
            prevText: "&#x3C;上個月",
            nextText: "下個月&#x3E;",
            currentText: "今天",
            monthNames: ["一月", "二月", "三月", "四月", "五月", "六月",
                "七月", "八月", "九月", "十月", "十一月", "十二月"],
            monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月",
                "七月", "八月", "九月", "十月", "十一月", "十二月"],
            dayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
            dayNamesShort: ["週日", "週一", "週二", "週三", "週四", "週五", "週六"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            weekHeader: "週",
            dateFormat: "yy-mm-dd",
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: true,
            yearSuffix: "年",
        });
    });
    </script>
        <style type="text/css">
        
        .auto-style1 {
            font-size: large;
        }
        .mar-r {
            margin-right:10px;
        }
        #ContentPlaceHolder1_imgb_p:hover,#ContentPlaceHolder1_imgb_n:hover{
            background:gainsboro;
        }
            .auto-style2 {
                font-size: large;
                color: #FF0000;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
            <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:Label>
                </strong> 
            </div>
            <div class="row">
                <div class="col-auto" id="name">
                    <strong>
                        <asp:Label ID="lb_factory" runat="server" CssClass="auto-style1"></asp:Label>
                    </strong>
                </div>
                <div class="col-auto">
                    <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged" ></asp:TextBox>
                    <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一天" CssClass="mar-r" OnClick="imgb_p_Click" />
                    <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一天" CssClass="mar-r" OnClick="imgb_n_Click" />
                    <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_add" runat="server" Class="btn btn-primary" Text="新增檢測數據" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_add_Click" />
                </div>
            </div>
        </div>

        <div class="row" style="margin-top:10px">
            <div class="col-12"> 
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Power" runat="server" class="nav-link">電流/功率</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Temp" runat="server" class="nav-link">溫度</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Wind" runat="server" class="nav-link">風壓/轉速</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Fd" runat="server" class="nav-link">秤飼機</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Quality" runat="server" class="nav-link">成品品質</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>

        <div id="G1" runat="server" class="xd table-responsive" style="margin-top:5px">
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="my table table-sm table-hover table-bordered text-center" DataKeyNames="FactoryID,Mill_ID,DDate,DataTime" DataSourceID="SDS1" OnRowCreated="GV1_RowCreated">
                <Columns>
                    <asp:BoundField DataField="FactoryID" HeaderText="FactoryID" ReadOnly="True" SortExpression="FactoryID" Visible="False" />
                    <asp:BoundField DataField="Mill_ID" HeaderText="Mill_ID" ReadOnly="True" SortExpression="Mill_ID" Visible="False" />
                    <asp:BoundField DataField="DDate" HeaderText="DDate" ReadOnly="True" SortExpression="DDate" Visible="False" />
                    <asp:BoundField DataField="DataTime" HeaderText="DataTime" ReadOnly="True" SortExpression="DataTime" Visible="False" />
                    <asp:BoundField DataField="DTime" HeaderText="時間" SortExpression="DTime" >
                    <ItemStyle Width="45px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Motor_Current_A" HeaderText="磨機電流A" SortExpression="Motor_Current_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Motor_Current_B" HeaderText="磨機電流B" SortExpression="Motor_Current_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Motor_PowerKW_A" HeaderText="磨機功率A" SortExpression="Motor_PowerKW_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Motor_PowerKW_B" HeaderText="磨機功率B" SortExpression="Motor_PowerKW_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Bucket_Elevator_A" HeaderText="循環提運機" SortExpression="Bucket_Elevator_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Bucket_Elevator_B" HeaderText="入庫提運機" SortExpression="Bucket_Elevator_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="OSEPA_Current" HeaderText="O-SEPA選粉機電流" SortExpression="OSEPA_Current" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="OSEPA_RPM" HeaderText="O-SEPA選粉機轉速" SortExpression="OSEPA_RPM" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Bag_Colletcot_M1" HeaderText="袋式收塵機M1" SortExpression="Bag_Colletcot_M1" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Bag_Colletcot_M2" HeaderText="袋式收塵機M2" SortExpression="Bag_Colletcot_M2" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="Bag_Colletcot_S" HeaderText="袋式收塵機S" SortExpression="Bag_Colletcot_S" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_In_A" HeaderText="耳軸承#1入口" SortExpression="TE_Mill_Bearing_In_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Out_A" HeaderText="耳軸承#1出口" SortExpression="TE_Mill_Bearing_Out_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_In_A" HeaderText="潤滑油#1入口" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_Out_A" HeaderText="潤滑油#1出口" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_In_B" HeaderText="耳軸承#2入口" SortExpression="TE_Mill_Bearing_In_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Out_B" HeaderText="耳軸承#2出口" SortExpression="TE_Mill_Bearing_Out_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_In_B" HeaderText="潤滑油#2入口" NullDisplayText="0.00"></asp:BoundField>
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_Out_B" HeaderText="潤滑油#2出口" NullDisplayText="0.00"></asp:BoundField>
                    <asp:BoundField DataField="TE_Mill_RAW_A" HeaderText="#1&lt;br&gt;料溫" SortExpression="TE_Mill_RAW_A" DataFormatString="{0:N2}" NullDisplayText="0.00" HtmlEncode="False" />
                    <asp:BoundField DataField="TE_Mill_Air_A" HeaderText="#1&lt;br&gt;氣溫" SortExpression="TE_Mill_Air_A" DataFormatString="{0:N2}" NullDisplayText="0.00" HtmlEncode="False" />
                    <asp:BoundField DataField="TE_Mill_RAW_B" HeaderText="#2&lt;br&gt;料溫" SortExpression="TE_Mill_RAW_B" DataFormatString="{0:N2}" NullDisplayText="0.00" HtmlEncode="False" />
                    <asp:BoundField DataField="TE_Mill_Air_B" HeaderText="#2&lt;br&gt;氣溫" SortExpression="TE_Mill_Air_B" DataFormatString="{0:N2}" NullDisplayText="0.00" HtmlEncode="False" />
                    <asp:BoundField DataField="TE_S_In" HeaderText="風析機出口" SortExpression="TE_S_In" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Product" HeaderText="成品入庫料溫" SortExpression="TE_Product" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_1_A" HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_2_A" HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_3_A" HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_4_A" HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_5_A" HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_6_A" HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_1_B" HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_2_B" HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_3_B" HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_4_B" HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_5_B" HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="TE_Motor_6_B" HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="WP_Mill_A" HeaderText="磨機出口#A" SortExpression="WP_Mill_A" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="WP_Mill_B" HeaderText="磨機出口#B" SortExpression="WP_Mill_B" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="WP_OSEPA" HeaderText="風析機出口" SortExpression="WP_OSEPA" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="WP_BC_S_IN" HeaderText="S系排風機入口" SortExpression="WP_BC_S_IN" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="WP_BC_S_Diff" HeaderText="S系排風機壓差" SortExpression="WP_BC_S_Diff" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="RPM_BC_M1" HeaderText="收塵排風機M系#1" SortExpression="RPM_BC_M1" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="RPM_BC_M2" HeaderText="收塵排風機M系#2" SortExpression="RPM_BC_M2" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="RPM_BC_S" HeaderText="收塵排風機 S" SortExpression="RPM_BC_S" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="OP_BC_M1" HeaderText="收塵排風機M系#1" SortExpression="OP_BC_M1" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="OP_BC_M2" HeaderText="收塵排風機M系#2" SortExpression="OP_BC_M2" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="OP_BC_S" HeaderText="收塵排風機S" SortExpression="OP_BC_S" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_A_P" HeaderText="#1A飼量調節器" SortExpression="W_M1_A_P" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_A_C" HeaderText="#1A積數器" SortExpression="W_M1_A_C" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_A_Q" HeaderText="#1A飼料量" SortExpression="W_M1_A_Q" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_B_P" HeaderText="#1B飼量調節器" SortExpression="W_M1_B_P" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_B_C" HeaderText="#1B積數器" SortExpression="W_M1_B_C" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M1_B_Q" HeaderText="#1B飼料量" SortExpression="W_M1_B_Q" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_A_P" HeaderText="#2A飼量調節器" SortExpression="W_M2_A_P" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_A_C" HeaderText="#2A積數器" SortExpression="W_M2_A_C" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_A_Q" HeaderText="#2A飼料量" SortExpression="W_M2_A_Q" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_B_P" HeaderText="#2B飼量調節器" SortExpression="W_M2_B_P" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_B_C" HeaderText="#2B積數器" SortExpression="W_M2_B_C" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                    <asp:BoundField DataField="W_M2_B_Q" HeaderText="#2B飼料量" SortExpression="W_M2_B_Q" DataFormatString="{0:N2}" NullDisplayText="0.00" />
                </Columns>
                <EmptyDataTemplate>
                    <strong>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                    </strong>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [G_Milling_Machine]"></asp:SqlDataSource>
        </div>
        <div id="G2" runat="server" class="row">
            <div class="col-xl-6 table-responsive">
                <asp:GridView ID="GV2" runat="server" AutoGenerateColumns="False" DataSourceID="SDS2" CssClass="table table-sm table-hover table-bordered text-center">
                <Columns>
                    <asp:BoundField DataField="DateTime" HeaderText="時間" SortExpression="DateTime" DataFormatString="{0:HH:mm}" />
                    <asp:BoundField DataField="Mill_ID" HeaderText="磨機" SortExpression="Mill_ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="成品" SortExpression="ProductName" />
                    <asp:BoundField DataField="Moisture" HeaderText="水份%" SortExpression="Moisture" />
                    <asp:BoundField DataField="Specific_Surface" HeaderText="比表面積㎡/kg" SortExpression="Specific_Surface" />
                    <asp:BoundField DataField="Residual_On_Sieve" HeaderText="篩餘%" SortExpression="Residual_On_Sieve" />
                </Columns>
                    <EmptyDataTemplate>
                        <strong>
                        <asp:Label ID="Label2" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                        </strong>
                    </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve FROM A_Product_Quality AS Q LEFT OUTER JOIN A_Product AS P ON Q.ProductID = P.ProductID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="tb_SDATE" Name="newparameter" PropertyName="Text" />
                </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>

    <script>
        var d = document.getElementById('ContentPlaceHolder1_GV1');
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
            document.getElementById("name").style.display = "none";
            //d.style.width = "320%";
            $(document).ready(function () {
                //抓取GV下的DIV賦予CLASS名稱
                var x = document.querySelector('div.xd div');
                if (x != null) {
                    x.className = "basic";
                    $(".basic").freezeTable();
                }
            });
        }
    </script>

</asp:Content>
