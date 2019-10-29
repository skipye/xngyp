

//仓库生产操作
function INVWork(obj, id) {
    var PostUrl = $(obj).attr("data-url");
    var Title = $(obj).attr("title");
    var ListId = "";
    if (id > 0) {
        ListId = id + "$";
    } else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            var StrStatus = $(this).attr("ref");//获取当前状态,0,是未确认，1是确认，
            if (StrStatus == 1) { ListId += $(this).val() + "$";}
        });
    }
    if (ListId != "" && ListId != undefined) {
        layer.confirm('您确定要重新生产吗？', function (index) {
            $.post(PostUrl, { ListId: ListId }, function (d) {
                if (d == "1") {
                    var MSG = Title + "操作，操作ID：" + ListId + "，操作网址：" + PostUrl;
                    AddWorkLogs(MSG, 3);
                    layer.msg('操作成功!', { icon: 1, time: 1000 });
                    ResetWindow();
                }
                else { layer.msg('服务器错误!', { icon: 2, time: 1000 }); }
            });
        });
    }
    else { layer.alert("请先去选中！"); }
}
/*移库操作*/
function MoveINV(obj, id) {
    var PostUrl = $(obj).attr("data-url");
    var title = $(obj).attr("title");
    var ListId = "";
    if (id > 0) {
        ListId = id + "$";
    } else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            var StrStatus = $(this).attr("ref");//只有确认之后才可以移库
            if (StrStatus == 1 ) { ListId += $(this).val() + "$"; }
        });
    }
    if (ListId != "" && ListId != undefined) {
        layer.confirm('您确定要移库吗？', function (index) {
            layer_show(title, PostUrl + "?ListId=" + ListId, 600, 300);
        });
    }
    else { layer.alert("请先去选中！"); }
}
//确认入库操作
function ConfirmINV(obj, id) {
    var ListId = "";
    var PostUrl = $(obj).attr("data-url");
    var title = $(obj).attr("title");;
    var ListId = "";
    if (id > 0) {
        ListId = id + "$";
    } else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            ListId += $(this).val() + "$";
        });
    }
    if (ListId != "" && ListId != undefined) {
        layer.confirm('您确定要这样操作吗？', function (index) {
            layer_show(title, PostUrl + "?ListId=" + ListId, 600, 300);
        });
    }
    else { layer.alert("请先去选中！"); }
}

//送货维修操作
function DeliveryINV(obj, id) {
    var PostUrl = $(obj).attr("data-url");
    var Title = $(obj).attr("title");
    var ListId = "";
    if (id > 0) {
        ListId = id + "$";
    } else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            var StrStatus = $(this).attr("ref");//获取当前状态,0,是未确认，1是确认，
            if (StrStatus == 1) { ListId += $(this).val() + "$"; }
        });
    }
    if (ListId != "" && ListId != undefined) {
        layer.confirm('您确定要送货维修吗？', function (index) {
            $.post(PostUrl, { ListId: ListId }, function (d) {
                if (d == "1") {
                    var MSG = Title + "操作，操作ID：" + ListId + "，操作网址：" + PostUrl;
                    AddWorkLogs(MSG, 3);
                    layer.msg('操作成功!', { icon: 1, time: 1000 });
                    ResetWindow();
                }
                else { layer.msg('服务器错误!', { icon: 2, time: 1000 }); }
            });
        });
    }
    else { layer.alert("请先去选中！"); }
}