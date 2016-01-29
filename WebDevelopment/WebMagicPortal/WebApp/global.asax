<script runat="server">
	sub AbandonSession()
		   HttpContext.Current.Session.Abandon()
	end sub
</script>