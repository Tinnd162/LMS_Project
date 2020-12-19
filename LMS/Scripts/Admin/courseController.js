var courseController = {
    init: function () {
        courseController.getcourse();
        courseController.registerEvent();
    },
    registerEvent: function () {
        $(document).stop().on('click', '#btnSave-course', function () {
            if ($('#frmSaveData-course').valid()) {
                courseController.save();
                $('#infocourse').modal('hide');
            }
        })
        $(document).stop().on('click', '.btn-edit-course', function () {
            var id = $(this).data('id');
            $('#infocourse').modal('show');
            courseController.detail(id);
        })
        $(document).stop().on('click', '.btn-delete-course', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
                if (result) {
                    courseController.delete(id);
                }
            })
        })
        $(document).stop().on('click', '.btn-info-course', function () {
            var id = $(this).data('id');
            $('#infosubjectIncourse').modal('show');
            courseController.subjectIncourse(id);
        })
        $(document).stop().on('click', '.btn-delete-subject', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc muốn xóa?", function (result) {
                if (result) {
                    courseController.deletesubject(id);
                    $('#infosubjectIncourse').modal('hide');
                }
            })
        })
        $(document).stop().on('click', '#btnAdd-course', function () {
            $('#infocourse').modal('show');
            courseController.reset();
        })
    },
    save: function () {
        var id = $('#IDcourse').val();
        var tilte = $('#tilte').val();
        var description = $('#description').val();
        var course = {
            ID: id,
            TILTE: tilte,
            DESCRIPTION: description,
        };
        $.ajax({
            url: '/Courses/save',
            data: {
                strCourse: JSON.stringify(course)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Save success", function () {
                        $('#infocourse').modal('hide');
                        courseController.getcourse(true);
                    })
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        })
    },
    deletesubject: function (id) {
        $.ajax({
            url: '/Courses/deleteSubjectInCourse',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Xóa thành công")
                }
            }
        });
    },
    delete: function (id) {
        $.ajax({
            url: '/Courses/delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Delete success", function () {
                        courseController.getcourse(true);
                    })
                }
            }
        });
    },
    detail: function (id) {
        $.ajax({
            url: '/Courses/detail',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data[0];
                    $('#IDcourse').val(data.ID);
                    $('#tilte').val(data.TILTE);
                    $('#description').val(data.DESCRIPTION);
                }
                else {
                    alert(response.message);
                }
            },
            error: function (err) {
                console.log(err)
            }
        });
    },
    getcourse: function () {
        $.ajax({
            url: '/Courses/getcourse',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-course').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            TILTE: item.TILTE,
                            DESCRIPTION: item.DESCRIPTION,
                        });
                    });
                    $('#tblData-course').html(html);
                }
            },
        });
    },
    subjectIncourse: function (id) {
        $.ajax({
            url: '/Courses/subjectIncourse',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-subjectIncourse').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            NAME: item.NAME,
                            DESCRIPTION: item.DESCRIPTION,
                            NAMETEACHER: item.C_USER1[0].LAST_NAME + ' ' + item.C_USER1[0].MIDDLE_NAME + ' ' + item.C_USER1[0].FIRST_NAME,
                        });
                    });
                    $('#tblData-subjectIncourse').html(html);
                }
            }
        });
    },
    reset: function () {
        $('#ID').val('0');
        $('#tilte').val('');
        $('#description').val('');
    },
}
courseController.init();