<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeFile="ImageLoader.aspx.cs" Inherits="Demos_ImageLoader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1
        {
            height: 485px;
            width: 727px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
    
        <asp:TextBox ID="TextBox1" runat="server" Width="449px">http://web.6park.com/bbs/first1.shtml</asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    </div>
    <p>
        &nbsp;</p>
    </form>
    <p>
        <textarea id="TextArea1" width="100%" runat="server" name="S1" readonly="readonly" rows="2"></textarea></p>
</body>
</html>
