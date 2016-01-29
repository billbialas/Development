//***************THE FOLLOWING FUNCTIONS ARE USED THROUGHOUT************
/*##################################################################
	Function "openPopup" will open the pop-up window
	INPUT PARAMS:	# sURL - URL to open
					# sName - Name of the new window
					# nWidth - Width of the popup
					# nHeight - Height of the popup */
	function openPopup(sURL, bScroll, nWidth, nHeight, windowName, menuOn, locationOn, toolBar)
	{
		if (windowName == null)	windowName = "_blank";
		var temp = window.open(sURL, windowName, "height=" + nHeight + ",width=" + nWidth + ",resizable=yes,status=no,toolbar=" + (toolBar == true ? "yes" : "no") + ",menubar=" + (menuOn == true ? "yes" : "no") + ",location=" + (locationOn == true ? "yes" : "no") + ",scrollbars=" + (bScroll == true ? "yes" : "no"));
		temp.opener = self;
	}

	function openPopupWithHandle(sURL, bScroll, nWidth, nHeight, windowName, menuOn, locationOn, toolBar)
	{
		if (windowName == null)	windowName = "_blank";
		var temp = window.open(sURL, windowName, "height=" + nHeight + ",width=" + nWidth + ",resizable=yes,status=no,toolbar=" + (toolBar == true ? "yes" : "no") + ",menubar=" + (menuOn == true ? "yes" : "no") + ",location=" + (locationOn == true ? "yes" : "no") + ",scrollbars=" + (bScroll == true ? "yes" : "no"));
		if (temp)
		 temp.opener = self;
		return temp;
	}

	function openerHREF(sURL)
	{
		if(!opener.closed) {
			
			opener.location.href = sURL
			opener.focus()
		}
		else {
			
			window.open(sURL)
			
		}
	}
	
	
		
	/*##################################################################
	!!!! IE ONLY !!!!
	Function "openDialog" will open the pop-up modal dialog window
	INPUT PARAMS:	# sURL - URL to open
					# nWidth - Width of the popup
					# nHeight - Height of the popup */
	function openDialog(sURL, nWidth, nHeight)
	{
		var popup_window = window.showModalDialog(sURL, "", "edge:raised;status:no;help:no;dialogWidth:" + nWidth + "px;dialogHeight:" + nHeight + "px;");
	}
	
	//##################################################################






//<script language="javascript" type="text/javascript">
      function readCookie(name){
	    var nameEQ = name + "=";
	    var ca = document.cookie.split(';');
	    for(var i=0;i < ca.length;i++)
	    {
		    var c = ca[i];
		    while (c.charAt(0)==' ') c = c.substring(1,c.length);
		    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	      }
	    return null;
        }
  		
		function eraseCookie(name)
				 {
				 createCookie(name,"",-1);
				 window.location="http://hms/choiceone/index.aspx";
				 }
				 
		function createCookie(name,value,days)
		 {
		  if (days)
		  {
		   var date = new Date();
		   date.setTime(date.getTime()+(days*24*60*60*1000));
		   var expires = "; expires="+date.toGMTString();
		   }
		   else var expires = "";
		   document.cookie = name+"="+value+expires+"; path=/";
		   }

//	     </script>
 
function openWin(url,windowName,options){
	var WindowHandle=window.open(url,windowName,options);
	WindowHandle.focus();
}

var ns6=document.getElementById&&!document.all
var ie=document.all

function showSpan(spanID,thetext){
	if (ie) eval("document.all."+spanID).innerHTML=thetext
	else if (ns6) document.getElementById(spanID).innerHTML=thetext
}
function hideSpan(spanID){
	if (ie) eval("document.all."+spanID).innerHTML=' '
	else if (ns6) document.getElementById(spanID).innerHTML=' '
}

function getElementBy(elemTag){
	var elem = document.getElementById (elemTag);
	if (elem)
		return elem;
	var elems = document.getElementsByName (elemTag);
	if (elems.length > 0)
		return elems[0];
	return null;
}

function toggleOpenCloseElem(elemName){
	var elem = getElementBy (elemName);
	if (!elem)
		return;
	if (elem.style.display == "")
		elem.style.display = "none";
	else
		elem.style.display = "";
}
//***************THE ABOVE FUNCTION IS USED THROUGHOUT************



//***************THE FOLLOWING FUNCTION IS USED FOR FORM VALIDATION************
function confirmPrompt(msg,url){
	if(confirm(msg)) 
		document.location = url;
}

function validateLength(objTB,maxChar){
	if (objTB.value.length > maxChar){return false;}
	return true;
}

function validateEmail(objTB){
	var invalidChars = "*|,\":<> []{}`\';()&$#%";
	if (objTB.value.indexOf('@') < 0 || objTB.value.indexOf('.') < 0 || objTB.value.length < 5){return false;}
	for (var i = 0; i < objTB.value.length; i++){
	   if (invalidChars.indexOf(objTB.value.charAt(i)) != -1){return false;}
	}
	return true;
} 

function validateEmailNonReq(objTB){
	var invalidChars = "*|,\":<> []{}`\';()&$#%";
	if (objTB.value.length > 0){
		if (objTB.value.indexOf('@') < 0 || objTB.value.indexOf('.') < 0 || objTB.value.length < 5){return false;}
		for (var i = 0; i < objTB.value.length; i++){
		   if (invalidChars.indexOf(objTB.value.charAt(i)) != -1){return false;}
		}
		return true;
	}
	return true;
} 

function validateTextBox(objTB){
	if (objTB.value==''){return false;}
	return true;
}

function validateSelectList(objTB){
	if (objTB.selectedIndex==''){return false;}
	return true;
}

function validateNumberTextBoxNonReq(objTB){
	if (isNaN(objTB.value)){return false;}
	return true;
}

function validateNumberTextBox(objTB){
	if (objTB.value=='' || isNaN(objTB.value)){return false;}
	return true;
}

//***************THE ABOVE FUNCTION IS USED FOR FORM VALIDATION************



//***************THE FOLLOWING FUNCTIONS ARE USED BY THE PROPERTY COMPARE FEATURE************
var thecookie = document.cookie;

function getCookie(name) { // use: getCookie("name");
	var index = thecookie.indexOf(name + "=");
	if (index == -1) return null;
	index = thecookie.indexOf("=", index) + 1; // first character
	var endstr = thecookie.indexOf(";", index);
	if (endstr == -1) endstr = thecookie.length; // last character
	return unescape(thecookie.substring(index, endstr));
}

function setCookie(name, value) { // use: setCookie("name", value);
    document.cookie=name + "=" + escape(value) + ";";
}

function commitCompareList(){		
	setCookie("VAR_CompareList",GLOBAL_CompareList);
	setCookie("CompareCount",GLOBAL_CompareCount);
}		

function sendToCompare(){
	if (GLOBAL_CompareCount < 2){
		alert('Please check more than 1 property!');
	}else{
		if (GLOBAL_CompareCount > 4){
			alert('Please check no more than 4 properties!');
		}else{
			window.location = "/property/propertycompare.asp?VAR_CompareList=" + GLOBAL_CompareList + "&CompareCount=" + GLOBAL_CompareCount;
		}
	}
}
		
function compareClear(){
	GLOBAL_CompareList = "";
	GLOBAL_CompareCount = 0;		
	setCookie("VAR_CompareList","");
	setCookie("CompareCount","");
}

function addCompare(obj){
	//the maximum number of listings a user can check to compare
	var Max_Number_Properties = 4;
	
	if (obj.checked == true) 
	{
		if (GLOBAL_CompareCount == Max_Number_Properties) {			
			obj.checked = false;
			alert('You have already chosen ' + Max_Number_Properties + ' properties!');
		}
		else {
			GLOBAL_CompareList = GLOBAL_CompareList + obj.value;
			GLOBAL_CompareCount = parseInt(GLOBAL_CompareCount) + 1;
		}
	}	
	else 
	{
		GLOBAL_CompareCount = parseInt(GLOBAL_CompareCount) - 1
		GLOBAL_CompareList = GLOBAL_CompareList.replace(obj.value,"");
	}
}

function copyToClipboard(field) {
    var content = eval("document." + field)
    content.focus()
    content.select()
    range = content.createTextRange()
    range.execCommand("Copy")
    window.status = "Contents copied to clipboard"
    setTimeout("window.status=''", 1800)
}
//***************THE ABOVE FUNCTIONS ARE USED BY THE COMPARE TOOL************
	
	
	
//**************MOUSEOVER MENU IMAGES CODE*******************************

icona = new Image()   
icona.src = "images/bt2_searchproperties2.jpg"  

iconb = new Image()
iconb.src = "images/bt2_searchproperties.jpg"

iconc = new Image()   
iconc.src = "images/bt2_ouragents2.jpg"  

icond = new Image()
icond.src = "images/bt2_ouragents.jpg"

icone = new Image()   
icone.src = "images/bt2_selling2.jpg"  

iconf = new Image()
iconf.src = "images/bt2_selling.jpg"

icong = new Image()   
icong.src = "images/bt2_buying2.jpg"  

iconh = new Image()
iconh.src = "images/bt2_buying.jpg"

iconi = new Image()   
iconi.src = "images/bt2_online2.jpg"  

iconj = new Image()
iconj.src = "images/bt2_online.jpg"

iconk = new Image()   
iconk.src = "images/bt2_contact2.jpg"  

iconl = new Image()
iconl.src = "images/bt2_contact.jpg"

iconm = new Image()   
iconm.src = "images/bt2_myspace2.jpg"  

iconn = new Image()
iconn.src = "images/bt2_myspace.jpg"

icono = new Image()   
icono.src = "/images/icon8a.gif"  

iconp = new Image()
iconp.src = "/images/icon8.gif"

iconq = new Image()   
iconq.src = "/images/icon9a.gif"  

iconr = new Image()
iconr.src = "/images/icon9.gif"



function imageChange(imgName,imgSrc){
document.images[imgName].src = eval(imgSrc +".src")
}






//popup menus
var IE5up = document.getElementById && document.all;
var NS6up = document.getElementById && !document.all;
var NS4 = document.layers;
var IE4 = document.all && !window.print;
var previousLayer = "";
var tm;

function hide(layerid) {
	if (IE5up || NS6up) {
		document.getElementById(layerid).style.visibility = "hidden";
	} else if (NS4) {
		document.layers[layerid].visibility = "hidden";
	} else if (IE4) {
		document.all[layerid].style.visibility = "hidden";
	}
}

function NS4Hide(layerid) {
	if (NS4) {
		delayHide(layerid);
	}
}

function delayHide(layerid) {
var timer;
	if (IE5up || NS6up) {
		timer = "";
	} else if (NS4) {
		timer = "300";
	} else if (IE4) {
		timer = "";
	}
tm = setTimeout("hide('" + layerid + "')", timer);
}

function show(layerid) {
clearTimeout(tm);
	if (previousLayer != "" && previousLayer != layerid) {
		hide(previousLayer);
	}
previousLayer = layerid;

	if (IE5up || NS6up) {
		document.getElementById(layerid).style.visibility = "visible";
	} else if (NS4) {
		document.layers[layerid].visibility = "visible";
	} else if (IE4) {
		document.all[layerid].style.visibility = "visible";
	}
}

//////


//<![CDATA[
  function HandleClose() 
   {
     alert("Killing the session on the server!!");
      PageMethods.AbandonSession();
   }
   //]]>

   <!--
		                var start = 1800;
		                var timer = start;
		                var minutes = 0;
		                function countdown() {
		                    if (timer > 0) {


		                        timer -= 1;
		                        minutes = timer / 60;
		                        setTimeout("countdown()", 1000);
		                        SpnDisp.innerHTML = minutes.toFixed(0);                         
		                   
		                  }
		               		else  	
		                    	{
		                    		
		                        location.href = "logout.aspx";
		                        alert("Your session has timed out");
		                    }
		                }
            //-->

