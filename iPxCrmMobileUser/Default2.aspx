<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../assets/css/bootstrap.css" type="text/css" media="all" /> 
    <link rel="stylesheet" href="../assets/css/font-awesome.css">
    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/js/jquery-2.2.3.min.js"></script> 
    <title>Untitled Page</title>
    <script type="text/javascript">
        var myVar = setInterval (function(){ ShowCurrentTime() },1000);
        function ShowCurrentTime() {
            var Possition = '<%=Server.HtmlEncode(Session("sPossition"))%>';
            var ProductCode = '<%=Server.HtmlEncode(Session("sProductCode"))%>';
            $.ajax({
                type: "POST",
                url: "Default2.aspx/GetUser",
                data: '{Possition: "' + Possition + '", ProductCode: "' + ProductCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function(response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "Default2.aspx/GetUnread",
                data: '{Possition: "' + Possition + '", ProductCode: "' + ProductCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessRead,
                failure: function(responseRead) {
                    alert(responseRead.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "Default2.aspx/notifTotalTicketNew",
                data: '{Possition: "' + Possition + '", ProductCode: "' + ProductCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessNotifNew,
                failure: function(responseNotifNew) {
                    alert(responseNotifNew.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "Default2.aspx/notifTotalTicketFollow",
                data: '{Possition: "' + Possition + '", ProductCode: "' + ProductCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessNotifFollow,
                failure: function(responseNotifFollow) {
                    alert(responseNotifFollow.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "Default2.aspx/notifTotalTicketDone",
                data: '{Possition: "' + Possition + '", ProductCode: "' + ProductCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessNotifDone,
                failure: function(responseNotifDone) {
                    alert(responseNotifDone.d);
                }
            });
        }

        function OnSuccessNotifNew(responseNotifNew) {
            data = JSON.parse(responseNotifNew.d);
            var unread = '';
            $(data).each(function(key, value) {
                unread += value.jumlahTiket;
            });
            $('#newTiket').html(unread);
            if (unread == '0') {
                $('#newTiket').css('display', 'none');
            } else {
                $('#newTiket').css('display', 'inline-block');
            }
        }

        function OnSuccessNotifFollow(responseNotifFollow) {
            data = JSON.parse(responseNotifFollow.d);
            var unread = '';
            $(data).each(function(key, value) {
                unread += value.jumlahFollow;
            });
            $('#followTiket').html(unread);
            if (unread == '0') {
                $('#followTiket').css('display', 'none');
            } else {
                $('#followTiket').css('display', 'inline-block');
            }
        }

        function OnSuccessNotifDone(responseNotifDone) {
            data = JSON.parse(responseNotifDone.d);
            var unread = '';
            $(data).each(function(key, value) {
                unread += value.jumlahFollow;
            });
            $('#DoneTiket').html(unread);
            if (unread == '0') {
                $('#DoneTiket').css('display', 'none');
            } else {
                $('#DoneTiket').css('display', 'inline-block');
            }
        }
        
        function OnSuccess(response) {
             data = JSON.parse(response.d);
             var unread = '';
             $(data).each(function(key, value) {
                    unread += '<tr><td>' +
                                '<div id ="new" style="padding:0px;" class="col-lg-12 col-xs-12">'+
                                    '<div class="small-box">'+
                                        '<div class="inner">'+
                                            '<h3>INFO</h3>'+
                                            '<p>' + value.name + '</p>' +
                                            '<p>' + value.email + '</p>' +
                                            '<p>' + value.dept + '</p>' +
                                        '</div>'+
                                        '<div class="icon"  style="top: 10px;">'+
                                            '<i class="fa fa-envelope"></i>'+
                                        '</div>'+
                                        '<div class="small-box-footer">'+
                                            '<i class="fa fa-download"></i>&nbsp;&nbsp;' +
                                            '<i class="fa fa-envelope"></i>&nbsp;&nbsp;' +
                                        '</div>'+
                                    '</div>'+
                                '</div>'+
                              '</td></tr>';  
             });
             $('#show_data').html(unread);
        }

        function OnSuccessRead(responseRead) {
             data = JSON.parse(responseRead.d);
             var unread = '';
             $(data).each(function(key, value) {
                 if (value.FollowUpCode == "H") {
                     if (value.FollowUpSopLink != '') {
                         unread += '<tr><td>' +
                                '<div class="box3 sb14 darker">' +
                                  '<img src="../assets/images/icon/user.png" alt="Avatar" class="" style="width:70%;">' +
                                  value.FollowUpBy + '<br />' +
                                  '<p style="margin-bottom:0; color:"">' + value.FollowUpNote + '</p>' +
                                  '<a target="_blank" href="' + value.FollowUpSopLink + '" class="text-primary">Open Link</a>'+
                                  '<span class="time-right">' +
                                      value.FollowUpDate +
                                  '</span>' +
                                '</div>' +
                              '</td></tr>';
                     }
                     else {
                         unread += '<tr><td>' +
                                '<div class="box3 sb14 darker">' +
                                  '<img src="../assets/images/icon/user.png" alt="Avatar" class="" style="width:70%;">' +
                                  value.FollowUpBy + '<br />' +
                                  '<p style="margin-bottom:0; color:"">' + value.FollowUpNote + '</p>' +
                                  '<span class="time-right">' +
                                      value.FollowUpDate +
                                  '</span>' +
                                '</div>' +
                              '</td></tr>';
                     }
                 } else {
                    if (value.FollowUpSopLink != '') {
                        unread += '<tr><td>' +
                            '<div class="box3 sb13">' +
                              '<img src="../assets/images/icon/userwhite.png" alt="Avatar" class="right" style="width:70%;">' +
                              value.FollowUpBy + '<br />' +
                              '<p style="margin-bottom:0; color:"">' + value.FollowUpNote + '</p>' +
                              '<p style="margin-bottom:0; color:""><button id="unduh" class="btn btn-link">Unduh File</button></p>' +
                              '<span class="time-left">' +
                                  value.FollowUpDate +
                              '</span>' +
                            '</div>' +
                          '</td></tr>';
                    }
                    else {
                         unread += '<tr><td>' +
                            '<div class="box3 sb13">' +
                              '<img src="../assets/images/icon/userwhite.png" alt="Avatar" class="right" style="width:70%;">' +
                              value.FollowUpBy + '<br />' +
                              '<p style="margin-bottom:0; color:"">' + value.FollowUpNote + '</p>' +
                              '<span class="time-left">' +
                                  value.FollowUpDate +
                              '</span>' +
                            '</div>' +
                          '</td></tr>';
                      }
                 }
             });
             $('#show_chat').html(unread);
        }
    </script>
    <script>
        $(function() {
            $("#unduh").click(function() {
                alert('button clicked');
            }
            )
        });
    </script>
    <style>
	    * {
            margin: 0px;
            padding: 0px;
        }
        .box3 {
            width: 90%;
            margin: 5px auto;
            border-radius: 15px;
            background: #00bfb6;
            color: #fff;
            padding-top: 12px;
            padding-bottom:20px;
            padding-left:20px;
            padding-right:20px;
            text-align: left;
            font-size :small;
            font-family: arial;
            position: relative;
        }
        .darker {
            border-color: #ccc;
            background-color: #ddd;
            color : #333;
        }
        .sb13:before {
            content: "";
            width: 0px;
            height: 0px;
            position: absolute;
            border-left: 15px solid #00bfb6;
            border-right: 15px solid transparent;
            border-top: 15px solid #00bfb6;
            border-bottom: 15px solid transparent;
            right: -16px;
            top: 0px;
        }
        .sb14:before {
            content: "";
            width: 0px;
            height: 0px;
            position: absolute;
            border-left: 15px solid transparent;
            border-right: 15px solid #ddd;
            border-top: 15px solid #ddd;
            border-bottom: 15px solid transparent;
            left: -16px;
            top: 0px;
        }
        .box3 img {
            float: left;
            max-width: 40px;
            width: 100%;
            margin-right: 20px;
            border-radius: 50%;
        }
        .box3 img.right {
            float: right;
            margin-left: 20px;
            margin-right:0;
        }   
        .time-right {
            float: right;
            color: #000000;
            font-size :x-small ;
        }
        .time-left {
            float: left;
            color: #ffffff;
            font-size :x-small ;
        }
    </style>
    <style>
        .small-box {
            border-radius: 3px;
            position: relative;
            display: block;
            margin-bottom: 1px;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            color:#25a79f;
            border-style: solid;
            border-color:#25a79f;
            border-width:1px;
        }
        .bg-aqua {
            background-color: #e0e0e0 !important;
        }
        .inner {
            padding: 10px 10px 0 10px;
        }
        .small-box h3 {
            font-size: 12px;
            font-weight: bold;
            margin: 0 0 3px 0;
            white-space: nowrap;
            padding: 0;
        }
        .small-box p {
            font-size: 9px;
            margin: 0 0 0 0;
            color:#25a79f;
        }
        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: 0px;
            right: 13px;
            z-index: 0;
            font-size: 20px;
            color: #25a79f;
        }
        .small-box-footer {
                position: relative;
                text-align: right;
                padding: 3px 0;
                color: #fff;
                color: rgba(255,255,255,0.8);
                display: block;
                z-index: 10;
                background: #25a79f;
                text-decoration: none;
        }
    </style>
</head>
<body style="margin:15px;">
    <form id="form1" runat="server">
    <div>
        <div class="row">
            <div class="col-lg-12">
                <span id="newTiket" style="background-color:Red;" class="badge">0</span>
                <span id="followTiket" style="background-color:Red;" class="badge">0</span>
                <span id="DoneTiket" style="background-color:Red;" class="badge">0</span>
                <table class="table" id="mychat">
                    <tbody id="show_chat">
                        
                    </tbody>
                </table>
                <table class="table" id="mydata">
                    <tbody id="show_data">
                        
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
