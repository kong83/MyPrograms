function buddy_pop() {
 window.open('index.php?act=buddy&s=' + session_id, 'BrowserBuddy','width=250,height=500,resizable=yes,scrollbars=yes');
}
function chat_pop(cw,ch) {
 window.open('index.php?s=' + session_id + '&act=chat&pop=1','Chat','width='+cw+',height='+ch+',resizable=yes,scrollbars=yes');
}
function multi_page_jump( url_bit, total_posts, per_page ){
 pages = 1; cur_st = parseInt(st); cur_page  = 1;
 if ( total_posts % per_page == 0 ) { pages = total_posts / per_page; }
 else { pages = Math.ceil( total_posts / per_page ); }
 msg = tpl_q1 + " " + pages;
 if ( cur_st > 0 ) { cur_page = cur_st / per_page; cur_page = cur_page -1; }
 show_page = 1;
 if ( cur_page < pages )  { show_page = cur_page + 1; }
 if ( cur_page >= pages ) { show_page = cur_page - 1; }
 else { show_page = cur_page + 1; }
 userPage = prompt( msg, show_page );
 if ( userPage > 0  ) {
  if ( userPage < 1 )     {    userPage = 1;  }
  if ( userPage > pages ) { userPage = pages; }
  if ( userPage == 1 )    {     start = 0;    }
  else { start = (userPage - 1) * per_page; }
  window.location = url_bit + "&st=" + start;
 }
}
function contact_admin(admin_email_one, admin_email_two) {
  window.location = 'mailto:' + admin_email_one + '@' + admin_email_two + '?subject=Error on the forums';
}
function do_url(pid) { window.location = base_url + '&act=SF&pid=' + pid + '&st=0&f=' + document.forummenu.f.value + '#List'; }
function pm_popup() { window.open('index.php?act=Msg&CODE=99&s=' + session_id,'NewPM','width=500,height=250,resizable=yes,scrollbars=yes'); }
