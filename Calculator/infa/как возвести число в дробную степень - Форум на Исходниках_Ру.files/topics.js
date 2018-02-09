function link_to_post(pid) {
  temp = prompt( tt_prompt, base_url + "showtopic=" + tid + "&view=findpost&p=" + pid );
  return false;
}
function delete_post(theURL) {

  if (confirm(js_del_1)) {
    window.location.href=theURL;
  } else {
    alert(js_del_2);
  } 
}
function PopUp(url, name, width,height,center,resize,scroll,posleft,postop) {
  if (posleft != 0) { x = posleft }
  if (postop  != 0) { y = postop  }
  if (!scroll) { scroll = 1 }
  if (!resize) { resize = 1 }
  if ((parseInt (navigator.appVersion) >= 4 ) && (center)) {
    X = (screen.width  - width ) / 2;
    Y = (screen.height - height) / 2;
  }
  if (scroll != 0) { scroll = 1 }
  var Win = window.open( url, name, 'width='+width+',height='+height+',top='+Y+',left='+X+',resizable='+resize+',scrollbars='+scroll+',location=no,directories=no,status=no,menubar=no,toolbar=no');
}
function keyb_pop() {
  window.open('index.php?act=legends&CODE=keyb&s=' + session_id,'Legends','width=700,height=160,resizable=yes,scrollbars=yes'); 
}
function emo_pop() {
  window.open('index.php?act=legends&CODE=emoticons&s=' + session_id,'Legends','width=250,height=500,resizable=yes,scrollbars=yes');
}
function bbc_pop() {
  window.open('index.php?act=legends&CODE=bbcode&s=' + session_id,'Legends','width=700,height=500,resizable=yes,scrollbars=yes');
}
function ValidateForm(isMsg) {
 MessageLength  = document.REPLIER.Post.value.length;
 errors = "";
 if (isMsg == 1)
 {
	if (document.REPLIER.msg_title.value.length < 2)
	{
		errors = msg_no_title;
	}
 }
 if (MessageLength < 2) {
	 errors = js_no_message;
 }
 if (MessageMax !=0) {
	if (MessageLength > MessageMax) {
		errors = js_max_length + " " + MessageMax + " " + js_characters + ". " + js_current + ": " + MessageLength;
	}
 }
 if (errors != "" && Override == "") {
	alert(errors);
	return false;
 } else {
	document.REPLIER.submit.disabled = true;
	return true;
 }
}
function rusLang() {
var textar = document.REPLIER.Post.value;
if (textar) {
 for (i=0; i<engReg.length; i++)
 { textar = textar.replace(engReg[i], rusLet[i]) }
   document.REPLIER.Post.value = textar; }
}
function expMenu(id) {
  var itm = null;
  if (document.getElementById) {
    itm = document.getElementById(id);
  } else if (document.all){
    itm = document.all[id];
  } else if (document.layers){
    itm = document.layers[id];
  }
  if (!itm) {
    // do nothing
  }
  else if (itm.style) {
    if (itm.style.display == "none") { itm.style.display = ""; }
    else { itm.style.display = "none"; }
  }
  else { itm.visibility = "show"; }
}
function ShowHide(id1, id2) {
  if (id1 != '') expMenu(id1);
  if (id2 != '') expMenu(id2);
}
var D=document;
function toChangeLink(ID,html,url)
{
    ID=D.getElementById(ID)
    ID.innerHTML=html
    if(url)ID.href=url
}
unique_id.c=0
function unique_id(o)
{
    return o.id=!o.id?o.id='so__'+(++unique_id.c):o.id;
}
function JSRequest(url,p)
{
   try{
    var s=D.createElement('script')
    s.type="text/javascript"
            s.src=url+"&linkID="+p+"&random="+Math.random();
    D.body.appendChild(s)
    return false
   }catch(e){return true}
}
function setPostHTML(i,h)
{
   i=D.getElementById(i);
   while(i.tagName!='TBODY' && i.tagName!='TABLE')i=i.parentNode;
   i=i.rows[1].cells[1].getElementsByTagName('DIV')[0]
   i.innerHTML=h
   if(!(is_ie && !Boolean(document.body.contentEditable)))h.replace(new RegExp("<script>(.*?)<\/script>","gi"),function($,s){eval(s)})
}