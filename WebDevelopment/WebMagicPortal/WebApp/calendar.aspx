<script language="vb" runat="server">
   Private Sub Calendar1_SelectionChanged(sender As Object, e As System.EventArgs)
        Dim strjscript as string = "<script language=""javascript"">"
        strjscript = strjscript & "window.opener.addbpo.value='a';window.close();"
        strjscript = strjscript & "</script" & ">" 'Don't Ask, Tool Bug
        'response.write(strjscript)
        Literal1.text = strjscript
    End Sub

    
    Private Sub Calendar1_DayRender(sender As Object, e As System.Web.UI.WebControls.DayRenderEventArgs)
       If e.Day.Date = datetime.now().tostring("d") Then
       e.Cell.BackColor = System.Drawing.Color.LightGray
       End If
    End Sub
</script>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"><html>
<head>
    <title>Choose a Date</title> 
</head>
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
    <form runat="server">
        <asp:Calendar id="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_dayrender" showtitle="true" DayNameFormat="FirstTwoLetters" SelectionMode="Day" BackColor="#ffffff" FirstDayOfWeek="Monday" BorderColor="#000000" ForeColor="#00000" Height="60" Width="170">
            <TitleStyle backcolor="#000080" forecolor="#ffffff" />
            <NextPrevStyle backcolor="#000080" forecolor="#ffffff" />
            <OtherMonthDayStyle forecolor="#c0c0c0" />
        </asp:Calendar>
        <asp:Literal id="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>

