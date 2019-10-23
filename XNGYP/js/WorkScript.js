

//销售安排生产操作
function SaleWork(id) {
    var url = "/WorkOrder/Add";
    var ListId = "";
    if (id > 0) { ListId = id + "s$"; }
    else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            ListId += $(this).val() + "s$";
        });
    }
    if (ListId == "" || ListId == undefined) {
        layer.alert("请先去选中！");
        return false;
    }
    var MSG = "安排生产操作，操作ID：" + ListId + "，网址：" + url;
    AddWorkLogs(MSG, 8);
    layer.confirm('您确定要这么操作吗？', function (index) {
        $.post(url, { ListId: ListId }, function (d) {
            if (d == "1") {
                AddWorkLogs(MSG, 8);
                layer.msg('操作成功!', { icon: 1, time: 1000 });
                ResetWindow();
            }
            else { layer.msg('服务器错误!', { icon: 2, time: 1000 }); }
        });
    });
}

//绑定库存产品操作
function BindSaleOrder(WoodId, ProductId, CRMId, Qty) {
    var PostUrl = "/Labels/WorkLabels";
    var title = "绑定库存产品";
    layer_show(title, PostUrl + "?WoodId=" + WoodId + "&ProductId=" + ProductId + "&CRMId=" + CRMId + "&Qty=" + Qty, 650, 400);

}
//
function workorderFrom(obj, url, id, w, h) {
    var ListId = "";
    var title = $(obj).attr("title");
    if (id > 0) { ListId = id + "$"; }
    else {
        var NewObj = $(obj).parent().parent().siblings("div.checkmodel").find("table.table");
        NewObj.find("input[type='checkbox']:checked").each(function () {
            ListId += $(this).val() + "$";
        });
    }
    if (ListId == "" || ListId == undefined) {
        layer.alert("请先去选中！");
        return false;
    }
    layer_show(title, url + "?ListId=" + ListId, w, h);
}
