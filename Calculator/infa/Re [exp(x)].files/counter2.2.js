// (c) 1999,2006 Spylog

var Md=window.document;
var sp_prop = Md.getElementById('spylog_code');
var sp_counter = sp_prop.getAttribute('counter');
var sp_part = sp_prop.getAttribute('part');
var undefined;

spylog_tracker(sp_counter,sp_part);


function spylog_tracker(counter,part)
{
	spylog_fix_js();
	first =Math.floor(counter / 100);
	second = counter - first * 100;
	first = spylog_str_pad(first.toString(), 3, '0');
	second = spylog_str_pad(second.toString(), 2, '0');
	Mu = "u" + first + "." + second + ".spylog.com";
	Mnv = navigator;
	Mp = 0;
	Md.cookie = "b=b";
	Mc = 0;
	if (Md.cookie)
	Mc = 1;
	Mrn = Math.random();
	Mn = (Mnv.appName.substring(0, 2) == "Mi") ? 0 : 1;
	Mt = (new Date()).getTimezoneOffset();
	Mz = "p=" + Mp + "&rn=" + Mrn + "&c=" + Mc + "&t=" + Mt;
	Mz += '&title=' + escape(Md.title.substring(0,100));
	if ((part != undefined) && (part != '')) {
		part = part.toString();
		Mz += '&partname=' + spylog_escape(part.replace(/^\s+/, '').replace(/\s+$/, ''));
	}
	if (self != top)
	Mfr = 1;
	else
	Mfr = 0;

	if ((navigator.appVersion.indexOf("Mac") != -1) && (navigator.userAgent.indexOf("MSIE") != -1)
	&& (parseInt(navigator.appVersion) == 3))
	Msl = "1";
	else {
		Msl = spylog_js;
	}
	var Mct='';
	Mfl = spylog_fix_flash();
	if(!Mct)Mct = 'lan';
	if ((Mfl != undefined) && (Mfl != '') && (Mfl.replace(/\s/ig, '') != '')) {
		Mz += '&fl=' + Mfl + '&ct=' + Mct;
	}
	switch (Msl) {
		default:
		case '1.2':
		Ms = screen;
		Mpx = (Mn == 0) ? Ms.colorDepth : Ms.pixelDepth;
		Mz += "&wh=" + Ms.width + 'x' + Ms.height + "&px=" + Mpx;
		case '1.1':
		Mpl = "";
		Mj = (Mnv.javaEnabled()? "Y" : "N");
		Mz += '&j=' + Mj;
		case '1':
		case '1.0':
	}
	if (parent != window)
	Mfm = "&r1=" + escape(Md.referrer) + "&r=" + escape(parent.document.referrer);
	else
	Mfm = "&r=" + escape(Md.referrer);

	var a_url = "http://" + Mu + "/cnt?cid=" + counter + "&f=3&p=" + Mp + "&rn=" + Mrn;

	if (document.location.protocol == 'https:')
	Mu = "s://sec01-hits.spylog.com/cnt.cgi";
	else
	Mu = "://" + Mu + "/cnt";
	var counter_url = "http" + Mu + "?cid=" + counter + "&" + Mz + "&sl=" + Msl + Mfm + "&fr=" + Mfr + "&pg=" +
	escape(window.location.href);
	My = "";
	My += "<a href='" + a_url + "' target='_blank'>";
	My +=  "<img src='"+counter_url;
	My += "' border=0 alt='SpyLOG'>";
	My += "</a>";
	Md.write(My);
}
function spylog_fix_js()
{
	var SCRIPT= '<script language="javascript">var spylog_js=1;</script>';
	SCRIPT += '<script language="javascript1.1">spylog_js=1.1;</script>';
	SCRIPT += '<script language="javascript1.2">spylog_js=1.2;</script>';
	SCRIPT += '<script language="javascript1.3">spylog_js=1.3;</script>';
	SCRIPT += '<script language="javascript1.4">spylog_js=1.4;</script>';
	SCRIPT += '<script language="javascript1.5">spylog_js=1.5;</script>';
	SCRIPT += '<script language="javascript1.6">spylog_js=1.6;</script>';
	SCRIPT += '<script language="javascript"></script>';
	Md.write(SCRIPT);
}
function spylog_escape(str)
{
	var unicod = '';
	var len = str.length;

	for (var i = 0; i < len; i++) {
		var cod = str.charCodeAt(i);

		if (cod < 255) {
			unicod += str.charAt(i);
			continue;
		}
		cod = cod.toString(16);
		unicod += '%u' + spylog_str_pad(cod.toLowerCase(), 4, '0');
	}
	return unicod;
}
function spylog_str_pad(str, len, pad)
{
	var length = str.length;
	if (length >= len)
	return str;
	var count = len - length;
	for (var i = 0; i < count; i++)
	str = pad + str;
	return str;
}
function spylog_fix_flash()
{
	var f="", n=navigator;
	if (n.plugins && n.plugins.length) {
		for (var ii=0;ii<n.plugins.length;ii++) {
			if (n.plugins[ii].name.indexOf('Shockwave Flash')!=-1) {
				f=n.plugins[ii].description.split('Shockwave Flash ')[1];
				break;
			}
		}
	} else if (window.ActiveXObject) {
		for (var ii=10;ii>=2;ii--) {
			try {
				var fl=eval("new ActiveXObject('ShockwaveFlash.ShockwaveFlash."+ii+"');");
				if (fl) { f=ii + '.0'; break; }
			}
			catch(e) {}
		}

		if((f=="")&&!Mn&&(n.appVersion.indexOf("MSIE 5")>-1||n.appVersion.indexOf("MSIE 6")>-1))
		{
			FV=clientInformation.appMinorVersion;
			if(FV.indexOf('SP2') != -1)
			f = '>=7';
		}
	}
	return f;
}

function spylog_testParent (a)
{
	var c=1;
	var res=0;
	while (c != 2)
	{
		if (! a) c=2;
		else
		{
			if (a.nodeName.toUpperCase() == 'BODY') res=1;
			c=1;
			a = a.parentNode
		}
	}
	return res;
}