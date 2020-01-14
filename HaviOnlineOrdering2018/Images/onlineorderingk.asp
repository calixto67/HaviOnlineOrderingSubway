<!--#INCLUDE FILE = "conn/connect.asp"-->
<html>
<!--Alchemy Solutions-->
<!--Designed and authored by Alchemy Solutions-->
<!--A division of Tridel Technologies-->
<!--All graphics and designs remain the property of Alchemy Solutions-->
<!--Tridel Technologies Incorporated-->
<!--7/F YL Hanston Building, Emerald Ave. Ortigas Center, Pasig City, Philippines 1605-->
<!--Tel. (63-2) 634-5140 to 45-->
<!--Fax. (63-2) 634-5139-->
<head>
  <title>Welcome to HAVI</title>
  <link rel="stylesheet" href="havi.css" type="text/css">
  <meta name="title" content="   ">
  <meta name="author" content="Alchemy Solutions">
  <meta http-equiv="content-type" content="text/html;charset=iso-8859-1">
  <meta name="description" content="        ">
  <meta name="keywords" content="  ">
  <meta name="copyright" content="copyright (c) 2002 ">
  <meta name="robot" content="all">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<script language="JavaScript">
<!--
// BROWSER CHECK
browser = navigator.appName;
appVer = parseInt(navigator.appVersion);
ie = "Microsoft Internet Explorer";
ns = "Netscape";
if (navigator.appVersion.indexOf("Macintosh") != -1) {
	isnotie = true;
	} else {
	isnotie = false;
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>
</head>
<body bgcolor="#FFFFFF" marginheight="0" marginwidth="0" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0" onLoad="MM_preloadImages('intra/images/info1_05.jpg','intra/images/stock1_06.jpg','intra/images/memo1_07.jpg','intra/images/product1_08.jpg','intra/images/service1.jpg','intra/images/customer1_09.jpg','intra/images/online1.jpg','intra/images/download1_10.jpg','intra/images/supplier1_12.jpg','intra/images/product1.jpg','intra/images/change1.jpg','intra/images/logout1.jpg','intra/images/sbmain1.jpg','intra/images/sborderingform1.jpg','intra/images/sbform1.jpg','intra/images/officeicon1.jpg','intra/images/operatingicon1.jpg','intra/images/papericon1.jpg','intra/images/promoicon1.jpg','intra/images/trainingicon1.jpg','intra/images/allicon1.jpg','intra/images/foodicon2.jpg','intra/images/invpromicon1.jpg','intra/images/linenicon1.jpg','intra/images/msmicon1.jpg','intra/images/itemicon1.jpg')">
<%'if session("auth") = 1 and session("session") <> "" Then%>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" height="430">
        <tr> 
          <td align="center" bgcolor="#FFFFFF" height="588" valign="top"> 
            <!--#include file = "header.asp"-->
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr> 
                <td align="left" valign="top" bordercolor="#CCCCCC" height="509"> 
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr> 
                      <td width="160" bgcolor="F0F0F0" align="center" height="511" valign="top"><br>
                        <!--#include file = "leftlink.asp"-->
                      </td>
                      <td valign="top" height="511"> 
                        <p><br>
                          &nbsp;&nbsp;&nbsp;&nbsp;<img src="intra/images/onlinebar.jpg" width="423" height="29"><br>
                          <br>
                        </p>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                          <tr> 
                            <td align="center" bgcolor="#003366"> 
                              <table width="95%" border="0" cellspacing="0" cellpadding="0">
                                <tr> 
                                  <td align="center" valign="top" height="25" bgcolor="#003366"> 
                                    <table width="90%" border="0" cellspacing="0" cellpadding="0" height="25">
                                      <tr> 
                                        <td bgcolor="#003366" height="27" align="right">
										<a href="onlineordering.asp" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image19','','intra/images/sbmain1.jpg',1)">
										<img name="Image19" border="0" src="intra/images/sbmain.jpg" width="51" height="23"></a>
										<%If Mid(Session("custrights"), 1, 1) = "X" Then %>
										<a href="orderingform.asp" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image20','','intra/images/sborderingform1.jpg',1)">
										<img name="Image20" border="0" src="intra/images/sborderingform.jpg" width="107" height="23"></a>
										<%End If%>
										<%If Mid(Session("custrights"), 2, 1) = "X" Then %>
										<a href="orderingstatus.asp" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image21','','intra/images/sbform1.jpg',1)">
										<img name="Image21" border="0" src="intra/images/sbform.jpg" width="109" height="23"></a>
										<%End If%>
										</td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                                              <br>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
						 <tr>
                            <td align="center"> <br>
                              <table width="90%" border="0" cellspacing="0" cellpadding="5">
						<%
						'list all categories
						set RScategory = haviconn.Execute("Select kennycatid, kennysplitcode, kennycatdesc from tbl_kennycat")
						If NOT RScategory.eof Then
							N = 4					'no of columns
							I = 1					'used for the display of images
							While NOT RScategory.eof
								catid = RScategory("kennycatid")
								splitcode = cstr(RScategory("kennysplitcode"))
								catname = RScategory("kennycatdesc")
								Select Case splitcode
									Case "BD"
										image1 = "bread.jpg"
										image2 = "bread1.jpg"
										imagename = "Image191"
									Case "BF"
										image1 = "beef.jpg"
										image2 = "beef1.jpg"
										imagename = "Image201"
									Case "BM"
										image1 = "breading.jpg"
										image2 = "breading1.jpg"
										imagename = "Image211"
									Case "BV"
										image1 = "beverage.jpg"
										image2 = "beverage1.jpg"
										imagename = "Image221"
									Case "CS"
										image1 = "cafe.jpg"
										image2 = "cafe1.jpg"
										imagename = "Image23"
									Case "DR"
										image1 = "dairy.jpg"
										image2 = "dairy1.jpg"
										imagename = "Image251"
									Case "DS"
										image1 = "desserts.jpg"
										image2 = "desserts1.jpg"
										imagename = "Image26"
									Case "FA"
										image1 = "fixedassets.jpg"
										image2 = "fixedassets1.jpg"
										imagename = "Image27"
									Case "FD"
										image1 = "food.jpg"
										image2 = "food1.jpg"
										imagename = "Image28"
									Case "FD"
										image1 = "food.jpg"
										image2 = "food1.jpg"
										imagename = "Image28"
									Case "HM"
										image1 = "ham.jpg"
										image2 = "ham1.jpg"
										imagename = "Image29"
									Case "ME"
										image1 = "maintenance.jpg"
										image2 = "maintenance1.jpg"
										imagename = "Image30"
									Case "MS"
										image1 = "merchandise.jpg"
										image2 = "merchandise1.jpg"
										imagename = "Image31"
									Case "OF"
										image1 = "otherfoods.jpg"
										image2 = "otherfoods1.jpg"
										imagename = "Image32"
									Case "OS"
										image1 = "officesupplies.jpg"
										image2 = "officesupplies1.jpg"
										imagename = "Image33"
									Case "PC"
										image1 = "portioncontrol.jpg"
										image2 = "portioncontrol1.jpg"
										imagename = "Image34"
									Case "PF"
										image1 = "processedfood.jpg"
										image2 = "processedfood1.jpg"
										imagename = "Image35"
									Case "PK"
										image1 = "packaging.jpg"
										image2 = "packaging1.jpg"
										imagename = "Image36"
									Case "PL"
										image1 = "poultry.jpg"
										image2 = "poultry1.jpg"
										imagename = "Image37"
									Case "PO"
										image1 = "pork.jpg"
										image2 = "pork1.jpg"
										imagename = "Image38"
									Case "PP"
										image1 = "processedproduce.jpg"
										image2 = "processedproduce1.jpg"
										imagename = "Image39"
									Case "RB"
										image1 = "ribs.jpg"
										image2 = "ribs1.jpg"
										imagename = "Image40"
									Case "RC"
										image1 = "container.jpg"
										image2 = "container1.jpg"
										imagename = "Image41"
									Case "SP"
										image1 = "spareparts.jpg"
										image2 = "spareparts1.jpg"
										imagename = "Image42"
									Case "SS"
										image1 = "seasonings.jpg"
										image2 = "seasonings1.jpg"
										imagename = "Image43"
									Case "ST"
										image1 = "storesupplies.jpg"
										image2 = "storesupplies1.jpg"
										imagename = "Image44"
									Case "SU"
										image1 = "supplies.jpg"
										image2 = "supplies1.jpg"
										imagename = "Image45"
									Case "SW"
										image1 = "smallwares.jpg"
										image2 = "smallwares1.jpg"
										imagename = "Image46"
									Case "UF"
										image1 = "uniforms.jpg"
										image2 = "uniforms1.jpg"
										imagename = "Image47"
								End Select

								Response.write image1 & " is image" &I &"<br>"
								If I <= N Then
									tdval1 = tdval1 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval2 = tdval2 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval3 = tdval3 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval4 = tdval4 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval5 = tdval5 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval6 = tdval6 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval7 = tdval7 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								ElseIf I <= (N+4) Then
									tdval8 = tdval8 & "<td width=""26%"" align=""center""><a href=""ordercontent.asp?cat="&catid&""" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('"&imagename&"','','intra/images/kennyimages/"&image2&"',1)""><img name="""&imagename&""" border=""0"" src=""intra/images/kennyimages/"&image1&""" width=""30"" height=""20""></a><div>"&catname&"</div></td>"
								End If
								'manual = "<td width=""26%"" align=""center""><a href=""orderinput.asp"" onMouseOut=""MM_swapImgRestore()"" onMouseOver=""MM_swapImage('image30','','intra/images/itemicon1.jpg',1)""><img name=""image30"" border=""0"" src=""intra/images/itemicon.jpg"" width=""30"" height=""20""></a><div>Input Item Code</div></td>"
								%>
								<%
								I = I + 1
								RScategory.MoveNext
							Wend
							
							trval = "<tr>" & tdval1 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval2 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval3 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval4 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval5 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval6 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval7 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf &_
									"<tr>" & tdval8 & "</tr><tr><td>&nbsp;</td></tr>" & vbcrlf
							Response.write trval
						'Else
						'	Response.write "<tr><td width=""26%"" align=""center"" class=""successmed"">Kenny Roger's Products not available yet!<br><br>Available Soon!</td></tr>"
						End If
						%>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <p>&nbsp;</p>
                      </td>
                      <td width="160" height="511" align="center" valign="top" bgcolor="f0f0f0"> 
                        <p>&nbsp;</p>
                        <p>&nbsp;</p>
                        </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr> 
		<td height="3" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr><td height="1" valign="middle" bgcolor="#000000"><img src="spacer.gif" width="1" height="1"></td></tr>
	</table>
      <p><font size="1" color="#000000" face="Arial, Helvetica, sans-serif">&nbsp;&nbsp;<font color="#999999">Copyright:<a href="http://www.alchemy.com.ph" class="alchemy1"> 
        Alchemy Solutions</a> 2002</font></font><font size="1" color="#000000" face="Arial, Helvetica, sans-serif"><font color="#999999"> 
        </font></font></p>
      </td>
  </tr>
</table>
</body>
</html>