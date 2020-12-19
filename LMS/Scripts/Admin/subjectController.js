var subjectconfig = {
    pageSize:20,
    pageIndex:1,
};
var subjectController = {
    init: function () {
        subjectController.Getsubject();
        subjectController.registerEvent();
        subjectController.getNAMEcourse();
    },
    registerEvent: function () {
        $(document).stop().on('click', '#btnSave-subject', function () {
            if ($('#frmSaveData-subject').valid()) {
                subjectController.Save();
            }
        })
        $(document).stop().on('click', '.btn-delete-subject', function () {
            var id = $(this).data('id');
            bootbox.confirm("Are you sure to delete this employee?", function (result) {
                if (result) {
                    subjectController.Delete(id);
                }
            });
        })
        $(document).stop().on('click', '.btn-edit-subject', function () {
            var id = $(this).data('id');
            $('#update-detail-create-subject').modal('show');
            subjectController.loadDetail(id);
        })
        $(document).stop().on('change', '#title', function (e) {  
            var optionSelected = $(this).find("option:selected");
            var id = optionSelected.data("idOption");
            $('#course-id').val(id);
        })
        $(document).stop().on('click', '#btnAdd-subject', function () {
            $('#update-detail-create-subject').modal('show');
            subjectController.reset();
        })
        $(document).stop().on('click', '.btn-info-subject', function () {
            var id = $(this).data('id');
            $('#infosubject').modal('show');
            subjectController.infosubject(id);
        })
    },
    Save: function () {
        var id = $('#IDsub').val();
        var name = $('#name').val();
        var desciption = $('#description').val();
        var course_id = $('#course-id').val();
        var subject = {
            ID: id,
            NAME: name,
            DESCRIPTION: desciption,
            COURSE_ID: course_id
        }
        $.ajax({
            url: '/Subjects/save',
            data: subject,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Save Success", function () {
                        $('#update-detail-create-subject').modal('hide');
                        subjectController.Getsubject(true);
                    })
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },
    loadDetail: function (id) {
        $.ajax({
            url: '/Subjects/detail',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data[0];
                    $('#IDsub').val(data.ID);
                    $('#name').val(data.NAME);
                    $('#description').val(data.DESCRIPTION);
                    $('#title').val(data.TILTE);
                    $('#course-id').val(data.COURSE_ID);
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    },
    Delete: function (id) {
        $.ajax({
            url: '/Subjects/delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Delete Success", function () {
                        subjectController.Getsubject(true);
                    })
                }
            }
        });
    },
    Getsubject: function () {
        $.ajax({
            url: '/Subjects/getsubject',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var a = [];
                    var template = $('#data-subject').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            NAME: item.NAME,
                            DESCRIPTION: item.DESCRIPTION,
                        });
                    });
                    $('#tblData-subject').html(html);
                }
            },
        });
    },
    paging: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / subjectconfig.pageSize);
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                subjectconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    getNAMEcourse: function () {
        $.ajax({
            url: '/Admin/Subjects/getNAMEcourse',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].TILTE);
                    $(opt).data('idOption', data[i].ID);
                    $('#title').append(opt);
                }
     
            }
        })
    },
    reset: function () {
        $('#IDsub').val('0');
        $('#name').val('');
        $('#description').val('');
        $('#title').val('');
        $('#course-id').val('');
    },
    infosubject: function (id) {
        $.ajax({
            url: '/Admin/Subjects/infosubject',
            data: { idsub:id },
            dataType: 'json',
            type: 'POST',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-infostudent').html();
                    var teacher = $('#nameteacher').val(data[0].C_USER1[0].LAST_NAME + ' ' + data[0].C_USER1[0].MIDDLE_NAME + ' ' + data[0].C_USER1[0].FIRST_NAME);
                    var subject = $('#namesubject').val(data[0].NAME);
                    $.each(data[0].C_USER, function (i, item) {
                        html += Mustache.render(template, {
                                ID: item.ID,
                                NAMESTUDENT: item.LAST_NAME + ' ' + item.MIDDLE_NAME + ' ' + item.FIRST_NAME,
                            });
                        });
                    $('#tblData-infostudent').html(html);
                }
            }
        })
    },
}
subjectController.init();