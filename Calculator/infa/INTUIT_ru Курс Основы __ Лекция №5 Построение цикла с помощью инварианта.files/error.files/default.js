function contentOperation(form)
{
    this.contlen = 10;
    this.dialog = null;
    this.form = form;

    this.window = self;

    // keyCode, ctrlKey, shiftKey, altKey
    this.event = function (p) { this.eventKeys = p; };

    var obj = this;

    this.init = function ()
    {
        this.window.document.onkeypress = function(e)
        {
            var pressed = 0;
            var we = obj.window.event ? obj.window.event : null;
            var i;
            for (i=0; i<obj.eventKeys.keyCode.length && pressed==0; i++)
            {
                if (we)
                    pressed = we.keyCode == obj.eventKeys.keyCode[i] && (obj.eventKeys.ctrlKey ? we.ctrlKey : true) && (obj.eventKeys.altKey ? we.altKey : true) && (obj.eventKeys.shiftKey ? we.shiftKey : true);
                else if (e)
                    pressed = 
                        (e.keyCode == obj.eventKeys.keyCode[i] && (obj.eventKeys.ctrlKey ? e.ctrlKey : 1) && (obj.eventKeys.altKey ? e.altKey : 1) && (obj.eventKeys.shiftKey ? e.shiftKey : 1)) ||
                        (e.which == obj.eventKeys.keyCode[i] && (obj.eventKeys.ctrlKey ? e.modifiers & Event.CTRL_MASK : 1) && (obj.eventKeys.altKey ? e.modifiers & Event.ALT_MASK : 1) && (obj.eventKeys.shiftKey ? e.modifiers & Event.SHIFT_MASK : 1));
            }
            if (pressed) obj.doSend();
        };
    };

    // ***** функция построения текста *****
    this.stripSlashn = function(text)
    {
        text = ""+text;
        return text.replace("\r", "").replace("\n", "").replace(new RegExp("^\\s+|\\s+$", "g"), "");
    };

    this.doSend = function()
    {
        var text = null;
        if (navigator.appName.indexOf("Netscape")!=-1 && eval(navigator.appVersion.substring(0,1))<5) {
            alert("Корявый браузер");
            return;
        }
        var selection = null;
        if (this.window.getSelection) {
            text = this.window.getSelection();
        } else if (this.window.document.getSelection) {
            text = this.window.document.getSelection();
        } else {
            selection = this.window.document.selection;
        }
        var context = null;
        if (selection) {
            var r = selection.createRange(); if (!r) return;
            text = r.text;
            var s = 0; 
            while (text.charAt(s)==" " || text.charAt(s)=="\n") s++;
            var e = 0; 
            while (text.charAt(text.length-e-1)==" " || text.charAt(text.length-e-1)=="\n") e++;
            var rngA = selection.createRange();
            rngA.moveStart("word", -this.contlen);
            rngA.moveEnd("character", -text.length+s);
            var rngB = selection.createRange();
            rngB.moveEnd("word", this.contlen);
            rngB.moveStart("character", text.length-e);
            text     = text.substring(s, text.length-e);
            context  = [rngA.text,  text,  rngB.text, 0];
        } else {
            context = ["", text, "", -1];
        }

        if (text == null)
        { 
            alert("Корявый браузер");
            return; 
        }
        if (context[1] == "") return;
        var visCont = this.stripSlashn(context[0]+">>>"+context[1]+"<<<"+context[2]);

        var url = this.window.document.location.href;
        if (this.dialog && this.dialog.confirm)
        {
            var question = this.dialog.confirm.replace(new RegExp('%s'), visCont);
            if (!this.window.confirm(question))
                return;
        }

        var param = '';
        if (this.dialog && this.dialog.prompt)
            param = this.window.prompt(this.dialog.prompt, '');

//        var form = this.window.document.forms[this.form];
        var form = self.document.forms[this.form];
        if (!form) return;
        // Characters are in UNICODE while copying to the form - we do not care of parent charset!
        form.charset.value = document.charset || "windows-1251"; 
        form.url.value = url;
        form.param.value = param;
        form.text.value = visCont;

        form.submit();
    };
}
