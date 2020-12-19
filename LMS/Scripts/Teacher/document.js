

function showCardAddDocInCardAddTopic() {
    renderFrmAddDoc('0', '#cardAddTopic')
}


function renderFrmAddDoc(topicId, position) {

    var template = $('#data-template-addDoc').html()
    var html = Mustache.render(template, {
        topicid: topicId
    });

    $(position + ' div[data-id="' + topicId + '"] div[name="frmAddDoc"]').html(html)

    CKEDITOR.replace("fullDesDoc" + topicId);

    $(position + ' div[data-id="' + topicId + '"] .AddTopic_btnAddDoc').click(function () {
        $(position + ' div[data-id="' + topicId + '"] div[name="frmAddDoc"] .frmAddDoc').toggleClass('d-none')
        $(position + ' div[data-id="' + topicId + '"] .divBtnAddDoc').toggleClass('d-none d-inline-flex')
    });
}



//////////////////////////////////////////////////////////////////////////

function hideCardAddDocInCardAddTopic() {
    hideCardAddDoc('#cardAddTopic')
}

function hideCardAddDocInCardTopicAdded() {
    hideCardAddDoc('#listTopic')
}


function hideCardAddDoc(position) {
    $(position).on('click', 'div[name="frmAddDoc"] button[name="btn-CloseDoc"]', function () {
        $(this).closest('.frmAddDoc').toggleClass('d-none')
        $(this).parents('.card-body').children('form').children('.divBtnAddDoc').toggleClass('d-inline-flex d-none ')
    })
}




//////////////////////////////////////////////////////////////////////////////////
function addDocInCardAddTopic() {
    addDoc('#cardAddTopic')
}

function addDocInCardTopicAdded() {
    addDoc('#listTopic')
}

function addDoc(position) {
    
    $(position).on('click', 'div[name="frmAddDoc"] button[name="btn-AddDoc"]', function () {
        let topicid = $(this).closest('.card-body').data('id')
        $(position + ' div[data-id="' + topicid +'"] .frmAddDoc div.text-danger').remove()

        let title = $(position + ' div[data-id="'+ topicid +'"] div[name="frmAddDoc"] .frmAddDoc input[name="AddDoc_title"]')
        let link = $(position + ' div[data-id="' + topicid +'"] div[name="frmAddDoc"] .frmAddDoc input[name="AddDoc_link"]')
        if (title.val() == '' && link.val() == '') {
            $('<div class="text-danger">* Phải nhập tiêu đề</div>').insertAfter(title)
            $('<div class="text-danger">* Phải thêm link tài liệu</div>').insertAfter(link)
            return false;
        }
        else if (title.val() == '') {
            $('<div class="text-danger">* Phải nhập tiêu đề</div>').insertAfter(title)
            return false;
        }
        else if (link.val() == '') {
            $('<div class="text-danger">* Phải thêm link tài liệu</div>').insertAfter(link)
            return false;
        }
        else {

            var valDesCKE = CKEDITOR.instances['fullDesDoc' + topicid + ''].getData().slice(3, -4);
            let flag = false; 

            if (position == '#listTopic') {
                $.ajax({
                    url: '/Teacher/Subject/PostDoc',
                    type: 'POST',
                    data: { ID: generateID('doc'), TITLE: title.val(), DESCRIPTION: valDesCKE, LINK: link.val(), TOPIC_ID: topicid },
                    dataType: 'json',
                    async: false,
                    success: function (msg) {
                        if (msg.success) {
                            flag = true;
 
                            alert('Thêm thành công !')
                        } else {
                            alert('Đã xảy ra lỗi !')
                        }
                    }
                });
            } else {// trong cardAddTopic
                flag = true;
            }

            if (flag) {
                var templateDoc = $('#data-template-doc-item').html()
                var htmlItemDoc = Mustache.render(templateDoc, {
                    IdDoc: 'null',
                    TitleDoc: title.val(),
                    LinkDoc: link.val(),
                    DesDoc: valDesCKE
                });
                $(position + ' div[data-id="' + topicid + '"]  div[name="listDoc"]').append(htmlItemDoc)
                title.val('')
                CKEDITOR.instances['fullDesDoc' + topicid + ''].setData('')
                link.val('')
            }

            $(this).closest('.frmAddDoc').toggleClass('d-none')
            $(this).parents('.card-body').children('form').children('.divBtnAddDoc').toggleClass('d-inline-flex d-none ')
        }

    });
}


//////////////////////////////////////////////////////////////////////////////

function removeDocChild(position) {
   
    $(position).on('click', 'div[name="listDoc"]  .btn_RemoveDoc', function () {
        if (position == '#listTopic') {
            $.ajax({
                url: '/Teacher/Subject/DeleteDoc',
                dataType: 'json',
                type: 'POST',
                data: { id: $(this).closest('.itemDoc').data('id') },
                async: true,
                success: function (msg) {
                    if (msg.success) {
                        alert('Xóa thành công !')
                    } else {
                        alert('Đã xảy ra lỗi !')
                        return;
                    }
                }
            });
        } 
        $(this).closest('.itemDoc').remove()
       
    });
}

function removeDocChildInCardAddtopic() {
    removeDocChild('#cardAddTopic')
}


function removeDocChildInTopicAdded() {
    removeDocChild('#listTopic')
}


////////////////////////////////////////

function editDocChildInCardAddtopic() {
    editDocChild('#cardAddTopic')
}

function editDocChildInTopicAdded() {
    editDocChild('#listTopic')
}

function editDocChild(position) {

    $(position).on('click', 'div[name="listDoc"] .btn_EditDoc', function () {

        var docID = $(this).closest('div.itemDoc').data('id')
        var item = $(this).parents('div[data-id="' + docID + '"].itemDoc').children('.itemDocContent')


        item.toggleClass('d-none d-flex')
        var linkItem = item.children('a').attr('href');
        var titleItem = item.children('h5').text();
        var desItem = item.children('p').text();


        var templateEditDoc = $('#data-template-editDoc-item').html()
        var editDocHtml = Mustache.render(templateEditDoc, {
            titleItem: titleItem,
            desItem: desItem,
            linkItem: linkItem
        });      
        $(this).parents('div[data-id="' + docID +'"].itemDoc').append(editDocHtml)
    });
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function closeFormEditDocChildInCardAddtopic() {
    closeFormEditDocChild('#cardAddTopic')
}

function closeFormEditDocChildInTopicAdded() {
    closeFormEditDocChild('#listTopic')
}

function closeFormEditDocChild(position) {

    $(position).on('click', 'div[name="listDoc"] .btn_close_EditDoc', function () {
        $(this).closest('div.itemDoc').children('.itemDocContent').toggleClass('d-none d-flex')
        $(this).closest(' .formEdit_itemDoc').remove();

    });
}




///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function editDocChildContentInCardAddTopic() {
    editDocChildContent('#cardAddTopic')   
}
function editDocChildContentInTopicAdded() {
    editDocChildContent('#listTopic')
}


function editDocChildContent(position) {
   
    $(position).on('click', 'div[name = "listDoc"] .itemDoc .formEdit_itemDoc .btn_ComfirmEditDoc', function () {

        let docID = $(this).closest('div.itemDoc').data('id')
        alert($(this).closest('div.itemDoc').html())

        $(position + ' .itemDoc[data-id="'+docID+'"] .formEdit_itemDoc div.text-danger').remove()

        var item = $(this).closest('.itemDoc[data-id= "'+docID+'"] .formEdit_itemDoc')
        
        if (item.children('.editDocLink').val() == '' && item.children('.editDocTitle').val() == '') {
            $('<div class="text-danger">* Không để trống</div>').insertAfter(item.children('.editDocTitle'))
            $('<div class="text-danger">* Không để trống</div>').insertAfter(item.children('.editDocLink'))
            return false;
        }
        else if (item.children('.editDocTitle').val() == '') {
            $('<div class="text-danger">* Không để trống</div>').insertAfter(item.children('.editDocTitle'))
            return false;
        }
        else if (item.children('.editDocLink').val() == '') {
            $('<div class="text-danger">* Không để trống</div>').insertAfter(item.children('.editDocLink'))
            return false;
        }
        else {
            var itemContent = $(this).closest('.itemDoc').children('.itemDocContent')
            let flag = false;
            if (position == '#listTopic') {
                $.ajax({
                    url: '/Teacher/Subject/UpdateDoc',
                    type: 'POST',
                    data: {
                        ID: docID,
                        TITLE: item.children('.editDocTitle').val(),
                        DESCRIPTION: item.children('.editDocDes').val(),
                        LINK: item.children('.editDocLink').val(),
                        TOPIC_ID: $(this).closest('.card-body').data('id')
                    },
                    dataType: 'json',
                    async: false,
                    success: function (msg) {
                        if (msg.success) {
                            flag = true;
                            alert('Sửa thành công !')
                        } else {
                            alert('Đã xảy ra lỗi !')
                        }
                    }
                });
            } else {// trong cardAddTopic
                flag = true;
            }

            if (flag) {
                itemContent.children('a').attr('href', item.children('.editDocLink').val())
                itemContent.children('a').children('p').text(item.children('.editDocLink').val())
                itemContent.children('h5').text(item.children('.editDocTitle').val())
                itemContent.children('p').text(item.children('.editDocDes').val())
            }
            item.remove()
            itemContent.toggleClass('d-none d-flex')
        }
    });
}




