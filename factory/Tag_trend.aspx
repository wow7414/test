<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_trend.aspx.cs" Inherits="factory.Tag_trend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>趨勢圖</title>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" crossorigin="anonymous"></script>
      <style type="text/css">
          .auto-style1 {
              font-size: large;
          }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <asp:Label ID="Label2" runat="server" Text="選擇時間:" CssClass="auto-style1"></asp:Label>
    <asp:DropDownList ID="ddl_s" runat="server" Height="30px">
    </asp:DropDownList>
    <asp:Button ID="btn_confrim" runat="server" Text="確定" class="btn btn-primary" Height="30px" OnClick="btn_confrim_Click" />
</div> 
<div>
    <div class="card-body"><canvas id="myAreaChart" width="100%" height="40"></canvas></div>
</div>
<script>
    var ctx = document.getElementById("myAreaChart");
    var color = [ "red" , "blue", "yellow", 'green', 'purple', 'orange', 'black', 'pink',
        'brown', '#4D1F00', '#704214', '#006400'];
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [<%Response.Write(Session["t"]);%>],
            datasets: [ 
                /*
                {
                    label: "1",
                    lineTension: 0.3, //曲線幅度
                    backgroundColor: "red", // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                    borderColor: "red", // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                    data: [10, 30, 39, 20, 25, 34, 1],
                    fill: false, //背景是否要填滿   
                },
                {
                    label: '溫度B',
                    fill: false,
                    backgroundColor: "blue",
                    borderColor: "blue",
                    data: [18, 33, 22, 19, 11, 39, 30],
                },
                */
                ],
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: '每分鐘趨勢圖'
                },
                scales: {
                    xAxes: [{
                        gridLines: {
                            display: false
                        },

                    }],
                    /*
                    yAxes: [{ //Y軸
                        ticks: { //Y軸的刻度
                            min: 0,
                            max: 100,
                            stepSize: 10 //間距
                        },
                    }],
                    */
                },
                legend: { //顯示各標題並且可隱藏
                    display: true
                },
                tooltips: { //滑鼠移到點上顯示資訊
                    enabled: true
                }
            }
    });
    var count_data = <% Response.Write(Session["count_data"]);%>;
    var TagName = <% Response.Write(Session["TagName"]);%>;
    var dats = <% Response.Write(Session["datas"]);%>;
    for (i = 0; i < count_data; i++) {
        const newDataSet = {
            label: TagName[i],
            lineTension: 0.3, //曲線幅度
            backgroundColor: color[i], // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: color[i], // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: dats[i],
            fill: false, //背景是否要填滿  
        }
        myLineChart.data.datasets.push(newDataSet);
    }
    myLineChart.update();
    
</script>
</asp:Content>
