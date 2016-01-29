<%@ Page language="vb" Codebehind="pricing.aspx.vb" Inherits="PageTemplate.pricing" Debug="false" trace="false"%>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script type="text/javascript" src="../_include/jquery.js"></script>
<script type="text/javascript" src="../_include/default.js"></script>

<html >

<head runat="server">
 <title>www.webmagicportal.com- Pricing</title>

	
</head>

<body style="background-color: #FFFFFF">

<form id="MAIN" runat="server">
  

<ul id="nav">
	<li id="nav-1"><a href="http://www.webmagicportal.com/features.aspx">Features</a></li>
	<li id="nav-2"><a href="http://www.webmagicportal.com/pricing.aspx">Pricing</a></li>
	<li id="nav-4"><a href="http://www.webmagicportal.com/contact.aspx">Contact</a></li>
	<li id="nav-5"><a href="http://app.webmagicportal.com">Log IN</a></li>
</ul>
<div id="PG_BackGround" >
	<div class="content">
		<div class="sub-heading">
			<img src="../images/pbckg.jpg" />				
		</div>		
		<div class="sub-headingA" >
			<h2>Basic Subscription</h2>
			<h3>The Basic option is an entry level subscription and is designed for individuals/companies who have a limited scope of business.</h3>
			<br>
			<h3>The Basic Subscription includes..</h3>
			<ul class=pricing>
     	 	  <li >Lead Manager</li>              	
             <li >AD Manager</li>
             <li >System Configuration</li>             
           
             </ul>
   			<div id="ss" style="height:87px;">
   			</div>        	    
             <h5>Subscription&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
             $34.95/Monthly</h5>
        	<h3>Choose your Subscription Period:&nbsp<asp:DropDownList ID="ddbasic" runat=server>
                <asp:ListItem Value="1" Text="1 Month"/>
	                <asp:ListItem Value="3" Text="3 Months"/>
	                <asp:ListItem Value="6" Text="6 Months"/>
	                <asp:ListItem Value="9" Text="9 Months"/>
	                <asp:ListItem Value="12" Text="1 Year"/>
                </asp:DropDownList></h3>
                <h3 style="margin-left:100">Then</h3>
                 <div style="margin-left:140"><asp:ImageButton id="imagebutton1" runat="server"  AlternateText="ImageButton 1"  ImageUrl="images/../images/subscribeIMG.jpg" OnClick="AddproductA"  />
            </div>
 
         </div>
		<div class="sub-headingA">
			<h2>Branding Subscription</h2>
			<h3>The Branding option is the next level up from the Basic Subscription and is desigend for individuals/companies who have a broad scope of business. </h3>
			<br>
			<h3>The Branding Subscription includes..</h3>
			<ul class=pricing>  
			 	  <li >Lead Manager</li>              	
             <li >AD Manager</li>
             <li >Branding Manager</li>                 
             <li >System Configuration</li>             
             </ul>      
	             <div id="ss" style="height:47px;">
	   			 </div>        	    
  
        		<h5>Subscription&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 
        		$44.95/Monthly</h5>
        		<h3>Choose your Subscription Period:&nbsp<asp:DropDownList ID="ddbranding" runat=server>
                <asp:ListItem Value="1" Text="1 Month"/>
	                <asp:ListItem Value="3" Text="3 Months"/>
	                <asp:ListItem Value="6" Text="6 Months"/>
	                <asp:ListItem Value="9" Text="9 Months"/>
	                <asp:ListItem Value="12" Text="1 Year"/>
                </asp:DropDownList></h3>
                <h3 style="margin-left:100">Then</h3>
                
            <div style="margin-left:140"><asp:ImageButton id="imagebutton2" runat="server"  AlternateText="ImageButton 1"  ImageUrl="images/../images/subscribeIMG.jpg" OnClick="Addproductb"  />
            </div>
		</div>
		
		
	</div>
</div>

</form>
</body>

</html>
