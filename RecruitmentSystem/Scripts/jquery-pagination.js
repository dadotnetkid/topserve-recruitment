$(document).ready(function () {
    page = parseInt($('div.pagination').attr('data-page'));
    pagesize = $('div.pagination').attr('data-pagesize');
    pagesize = parseInt(pagesize);
    image = $('div.pagination').attr('data-image-loading');
    col = $('div.pagination').attr('data-col');
    url = $('div.pagination').attr('data-url');
    container = $('div.pagination').attr('data-container')
    var pagination = $('ul.pagination');
    pagination.empty();
    pagination.append("<li><a onclick='previous(this,container,url)'>Previous</a></li>");
    var ellipsis = 0;
    var _page = pagesize - 9;
    console.log(_page);
    var cur = 9 + page;
    for (var i = 0; i <=( pagesize) ; i++) {

        if (page <= 9 && i<=9) {
            pagination.append("<li id=page-" + (i + 1) + "><a onclick='paginate(this,container,url)'>" + (i + 1) + "</a></li>")
            console.log('l');
        }
        else if (page > 9) {
            console.log('9');
            if (_page <= 9 && i>_page)
            {
                if (ellipsis == 0)
                {
                    pagination.append("<li><a href='#'>...</li>")
                }
                ellipsis++;
                pagination.append("<li id=page-" + (i + 1) + "><a onclick='paginate(this,container,url)'>" + (i + 1) + "</a></li>")
            }
              
            
        }
    }
    
    pagination.append("<li><a onclick='next(this,container,url)'>Next</a></li>")

    $('li').removeClass('page-active');
    $('li').removeClass('active');
    $('#page-' + (page)).addClass('active');
    $('#page-' + (page)).addClass('page-active');

});
var pagesize;
var page; var container; var url
var origin;
var col;
var image;
function paginate(li, container, url) {

    var page_ = parseInt($(li).text());
    var loading = "<td colspan='" + countColumn() + "' align='center'><img src='" + image + "' width='32'/></td>";

    $(container).html(loading);
    $.ajax({
        data: { page: page_ },
        url: url,
        success: function (e) {
            $(container).html(e);

        },
        error: function (e) {
            alert(JSON.stringify(e));
        }

    });
}
function previous(li, container, url) {
    page = $('div.pagination').attr('data-page');

    if (page >= 1) {
        var loading = "<td colspan='" + countColumn() + "' align='center'><img src='" + image + "' width='32'/></td>"
        $(container).html(loading);
        $.ajax({

            data: { page: page - 1 },
            url: url,
            success: function (e) {
                $(container).html(e);

            }
        });
    }

}
function next(li, container, url) {
    page = $('div.pagination').attr('data-page');

    var page_ = parseInt(page);
    if (page_ <= pagesize) {
        var loading = "<td colspan='" + countColumn() + "' align='center'><img src='" + image + "' width='32'/></td>"
        $(container).html(loading);
        $.ajax({
            data: { page: page_ + 1 },
            url: url,
            success: function (e) {
                $(container).html(e);

            }
        });
    }

}
function countColumn() {

    var cols = $("table>thead>tr:first>th");
    var count = cols.length;
    return count;
}