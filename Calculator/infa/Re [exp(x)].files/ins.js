function x () {
return;
}
		
function DoSmilie(addSmilie) {
	var revisedMessage;
	var currentMessage = document.board.body.value;
	revisedMessage = currentMessage+addSmilie;
	document.board.body.value=revisedMessage;
	document.board.body.focus();
	return;
}
	


var form=document.forms["board"];
tags_stack=new Array();
var undoeng=new Array();

function emoticon(smiley){insertCode(getText()+smiley)}

function emo(smiley){emoticon(smiley);form['body'].focus();}

function insertCode(code){
if(form["body"].createTextRange && form["body"].caretPos)
{form["body"].caretPos.body=code}
else{form["body"].value+=code}
}



function storeCaret(el) {
if(el.createTextRange){el.caretPos=document.selection.createRange().duplicate();}
}

function getText() {
if(document.all && form["body"].createTextRange && form["body"].caretPos)return form["body"].caretPos.body;
return "";
}


var decoder_type='kw';
function DoTableWin(str, Type)
{

var aTable1 = "������������������������������������������������������������������"; // Windows-1251
var aTable2 = "���������������������������������������������������������������ڨ�"; // ���-8
var aTable6 = "ABVGDE�ZIJKLMNOPRSTUFHC���QY'W��abvgde�zijklmnoprstufhc���qy'w����"; // ��������
var strTable1, strTable2;
if ("kw" == Type)
{
strTable1 = aTable2;
strTable2 = aTable1;
}
else if ("wk" == Type)
{
strTable1 = aTable1;
strTable2 = aTable2;
}
else if ("dw" == Type)
{
strTable1 = aTable3;
strTable2 = aTable1;
}
else if ("wd" == Type)
{
strTable1 = aTable1;
strTable2 = aTable3;
}
else if ("mw" == Type)
{
strTable1 = aTable4;
strTable2 = aTable1;
}
else if ("wm" == Type)
{
strTable1 = aTable1;
strTable2 = aTable4;
}
else if ("iw" == Type)
{
strTable1 = aTable5;
strTable2 = aTable1;
}
else if ("wi" == Type)
{
strTable1 = aTable1;
strTable2 = aTable5;
}
else if ("lw" == Type)
{
strTable1 = aTable6;
strTable2 = aTable1;
}
else if ("wl" == Type)
{
strTable1 = aTable1;
strTable2 = aTable6;
}
else
{
alert("ASSERT!");
return str;
}
var strNew = "";
var i, len = str.length;
for (i = 0; i < len; i++)
{
if ("lw" == Type)
{
if ("'" == str.substr(i, 1))
{
strNew = strNew + "�";
continue;
}
if (i < (len - 2))
{
var c = str.substr(i, 3);
if ("Sch" == c)
{
strNew = strNew + "�";
i = i + 2;
continue;
}
else if ("sch" == c)
{
strNew = strNew + "�";
i = i + 2;
continue;
}
}
if (i < (len - 1))
{
var c = str.substr(i, 2);
if (("Zh" == c) || ("ZH" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Ch" == c) || ("CH" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Sh" == c) || ("SH" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Ju" == c) || ("JU" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Ja" == c) || ("JA" == c))
{
strNew = strNew + "�";
i++;
continue;
}
if ("zh" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if ("ch" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if ("sh" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if ("ju" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if ("ja" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Jo" == c) || ("JO" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if ("jo" == c)
{
strNew = strNew + "�";
i++;
continue;
}
else if (("W" == c) || ("W" == c))
{
strNew = strNew + "�";
i++;
continue;
}
else if (("Q" == c) || ("Q" == c))
{
strNew = strNew + "�";
i++;
continue;
}
}
}
var c = str.substr(i, 1);
var pos = strTable1.indexOf(c);
if (pos < 0)
strNew = strNew + c;
else
{
var d = strTable2.substr(pos, 1);
if ("�" == d)
{
if ("wl" == Type)
{
if ("�" == c)
d = "Zh";
else if ("�" == c)
d = "Ch";
else if ("�" == c)
d = "Sh";
else if ("�" == c)
d = "Sch";
else if ("�" == c)
d = "Ju";
else if ("�" == c)
d = "Ja";
else if ("�" == c)
d = "zh";
else if ("�" == c)
d = "ch";
else if ("�" == c)
d = "sh";
else if ("�" == c)
d = "sch";
else if ("�" == c)
d = "ju";
else if ("�" == c)
d = "ja";
else if ("�" == c)
d = "Jo";
else if ("�" == c)
d = "jo";
}
}
strNew = strNew + d;
}
}
return strNew;
}

function DoQuotedWin(str, ToWin)
{
var aTable = "0123456789ABCDEF";
var strNew = ""
if (ToWin)
{
str.toUpperCase();
while (str.length > 0)
{
var c = str.charAt(0);
if (c != "=")
{
strNew += c;
str = str.substr(1);
}
else
{
var i1 = aTable.indexOf(str.charAt(1));
var i2 = aTable.indexOf(str.charAt(2));
if ((i1 >= 0) || (i2 >= 0))
{
var c = i1 * 16 + i2;
if (c >= 0x00C0)
c += 0x0350;
strNew += String.fromCharCode(c);
}
str = str.substr(3);
}
}
}
else
{
var i, len = str.length;
for (i = 0; i < len; i++)
{
var c = str.charCodeAt(i);
if ((c >= 0x0410) && (c <= 0x044F))
c -= 0x0350;
var i1 = Math.floor(c / 16);
var i2 = c % 16;
strNew += "=" + aTable.charAt(i1) + aTable.charAt(i2);
}
}
return strNew;
}

function Decode2(td)
{
decoder_type=td
var strText = document.forms.board.body.value;
if (0 == strText.length)
{
//alert("������� ����� ��� ��������������!");
document.forms.board.body.focus();
return strText;
}
if ("qw" == decoder_type)
strText = DoQuotedWin(strText, true);
else if ("wq" == decoder_type)
strText = DoQuotedWin(strText, false);
else
strText = DoTableWin(strText, decoder_type);
document.forms.board.body.value = strText;
return strText;
}


function Symbol(action) {
	var revisedMessage;
	var currentMessage = document.board.body.value;
	document.board.body.value=currentMessage+'<font face="Symbol">'+action+'</font>';
	document.board.body.focus();
}

function Greek(action) {
	var revisedMessage;
	var currentMessage = document.board.body.value;
	document.board.body.value=currentMessage+action;
	document.board.body.focus();
}

function DoPrompt(action) {
	var revisedMessage;
	var currentMessage = document.board.body.value;

	if (action == "url") {
	var thisURL = prompt("������� URL ����� ��� ���������, ������� �� ������ �������� � ���� ���������", "http://");
	var thisTitle = prompt("������ ������� �������� ������ �������� ��� �������� URL", thisURL);
	var urlUBBCode = "<a href=\""+thisURL+"\">"+thisTitle+"</a>";
	revisedMessage = currentMessage+urlUBBCode;
	document.board.body.value=revisedMessage;
	document.board.body.focus();
	return;
	}
	
	if (action == "email") {
	var thisEmail = prompt("������� E-mail, ������� �� ������ �������� � ���� ���������", "");
	var emailUBBCode = "<a href=\"mailto:"+thisEmail+"\">"+thisEmail+"</a>";
	revisedMessage = currentMessage+emailUBBCode;
	if(thisEmail) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}
	
	if (action == "bold") {
	var thisBold = prompt("������� �����, ������� �� ������ ������� ������ �������.", "");
	var boldUBBCode = "<b>"+thisBold+"</b>";
	revisedMessage = currentMessage+boldUBBCode;
	if(thisBold) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}
	
	if (action == "italics") {
	var thisItal = prompt("������� �����, ������� �� ������ ������� ��������", "");
	var italUBBCode = "<i>"+thisItal+"</i>";
	revisedMessage = currentMessage+italUBBCode;
	if(thisItal) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}

	if (action == "sqrt") {
	var thisSqrt = prompt("������� �����, ������� ����� ������ ��� ���������� ������", "");
	var sqrtUBBCode = "&radic;<span style=\"text-decoration: overline\">"+thisSqrt+"</span>";
	revisedMessage = currentMessage+sqrtUBBCode;
	if(thisSqrt) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}

	if (action == "sup") {
	var thisSup = prompt("������� ������� ������", "");
	var supUBBCode = "<sup><font size=\"-1\">"+thisSup+"</font></sup>";
	revisedMessage = currentMessage+supUBBCode;
	if(thisSup) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}

	if (action == "sub") {
	var thisSub = prompt("������� ������ ������", "");
	var subUBBCode = "<sub><font size=\"-1\">"+thisSub+"</font></sub>";
	revisedMessage = currentMessage+subUBBCode;
	if(thisSub) {document.board.body.value=revisedMessage;}
	document.board.body.focus();
	return;
	}
	
	if (action == "image") {
	var thisImage = prompt("������� ������ URL �������", "http://");
	var imageUBBCode = "<img border=\"0\" src=\""+thisImage+"\">";
	revisedMessage = currentMessage+imageUBBCode;
	document.board.body.value=revisedMessage;
	document.board.body.focus();
	return;
	}
	
	if (action == "code") {
	var thisCode = prompt("����� ����� <xmp> � </xmp> ����� ������������ ��� ����, ��� html �������������. ������ ����� ��� ������� Ok", " ");
	var codeUBBCode = "<xmp>"+thisCode+"</xmp>";
	revisedMessage = currentMessage+codeUBBCode;
	document.board.body.value=revisedMessage;
	document.board.body.focus();
	return;
	}

	if (action == "pre") {
	var thisPre = prompt("����� ����� ������ <pre> � </pre> ����� ������������ c ����������� �������� � ��������� �����. ��� ������ ��� ����� �������������.", " ");
	var preUBBCode = "<pre>"+thisPre+"</pre>";
	revisedMessage = currentMessage+preUBBCode;
	document.board.body.value=revisedMessage;
	document.board.body.focus();
	return;
	}

	if (action == "lt") {
	document.board.body.value=currentMessage+"&lt;";
	document.board.body.focus();
	return;
	}

	if (action == "gt") {
	document.board.body.value=currentMessage+"&gt;";
	document.board.body.focus();
	return;
	}
}