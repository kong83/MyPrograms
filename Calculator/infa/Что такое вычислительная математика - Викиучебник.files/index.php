/* generated javascript */
var skin = 'monobook';
var stylepath = '/skins-1.5';

/* MediaWiki:Common.js */
function pickUpText(aParentElement) {var str = "";

function pickUpTextInternal(aElement) {var child = aElement.firstChild;
while (child) {if (child.nodeType == 1)          // ELEMENT_NODE 
pickUpTextInternal(child);
else if (child.nodeType == 3)     // TEXT_NODE
str += child.nodeValue;

child = child.nextSibling;}}
pickUpTextInternal(aParentElement);

return str;}
var disableRealTitle = 0;               // users can disable this by making this true from their monobook.js
addOnloadHook(function() {try {var realTitleBanner = document.getElementById("RealTitleBanner");
if (realTitleBanner && !document.getElementById("DisableRealTitle") && !disableRealTitle) {var realTitle = document.getElementById("RealTitle");
if (realTitle) {var realTitleHTML = realTitle.innerHTML;
realTitleText = pickUpText(realTitle);

var isPasteable = 0;
//var containsHTML = /</.test(realTitleHTML);        // contains ANY HTML
var containsTooMuchHTML = /</.test( realTitleHTML.replace(/<\/?(sub|sup|small|big)>/gi, "") ); // contains HTML that will be ignored when cut-n-pasted as a wikilink
// calculate whether the title is pasteable
var verifyTitle = realTitleText.replace(/^ +/, "");             // trim left spaces
verifyTitle = verifyTitle.charAt(0).toUpperCase() + verifyTitle.substring(1, verifyTitle.length);       // uppercase first character

// if the namespace prefix is there, remove it on our verification copy.  If it isn't there, add it to the original realValue copy.
if (wgNamespaceNumber != 0) {if (wgCanonicalNamespace == verifyTitle.substr(0, wgCanonicalNamespace.length).replace(/ /g, "_") && verifyTitle.charAt(wgCanonicalNamespace.length) == ":") {verifyTitle = verifyTitle.substr(wgCanonicalNamespace.length + 1);} else {realTitleText = wgCanonicalNamespace.replace(/_/g, " ") + ":" + realTitleText;
realTitleHTML = wgCanonicalNamespace.replace(/_/g, " ") + ":" + realTitleHTML;}}
// verify whether wgTitle matches
verifyTitle = verifyTitle.replace(/^ +/, "").replace(/ +$/, "");                // trim left and right spaces
verifyTitle = verifyTitle.replace(/_/g, " ");           // underscores to spaces
verifyTitle = verifyTitle.charAt(0).toUpperCase() + verifyTitle.substring(1, verifyTitle.length);       // uppercase first character
isPasteable = (verifyTitle == wgTitle);

var h1 = document.getElementsByTagName("h1")[0];
if (h1 && isPasteable) {h1.innerHTML = containsTooMuchHTML ? realTitleText : realTitleHTML;
if (!containsTooMuchHTML)
realTitleBanner.style.display = "none";}document.title = realTitleText + " — Викиучебник";}}} catch (e) {/* Something went wrong. */}});

/* Замена неправильного заголовка правильным by SergV */

// Все неправильные заголовки
title_restr_alerts = ["trestrictions_replace", "trestrictions_alert"];

// Только шаблон title
//title_restr_alerts = ["trestrictions_replace"];

function display_correct_title () {var title_restr_alert1, a1;
if (document.getElementsByTagName && document.getElementById) {  
for(var i=0; i < title_restr_alerts.length; i++) { 
title_restr_alert1 = title_restr_alerts[i];
a1 = document.getElementById(title_restr_alert1);
if(a1) {ct = document.getElementById("trestrictions_correct");
if(ct) {document.getElementsByTagName("h1")[0].innerHTML  = ct.innerHTML;
a1.style.display = "none";
document.getElementById("trestrictions_replaced").style.display = "block";}break;}}}}
addOnloadHook(display_correct_title);

/* MediaWiki:Monobook.js (deprecated; migrate to Common.js!) */
var isMainPage = (document.title.indexOf("Заглавная страница") > -1);
var isDiff = (document.location.search && (document.location.search.indexOf("diff=") > -1 || document.location.search.indexOf("oldid=") > -1));
if (isMainPage && !isDiff){document.write('<style type="text/css">/*<![CDATA[*/ #lastmod,#siteSub, #contentSub, h1.firstHeading { display: none !important; } /*]]>*/</style>');
}