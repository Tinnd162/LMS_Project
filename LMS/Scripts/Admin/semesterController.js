var semesterconfig = {
    pageSize: 5,
    pageIndex: 1,
}
var semesterController = {
    init: function () {
        semesterController.GetSemester();
        semesterController.registerEvent();
    },
    registerEvent: function () {

        $(document).stop().on('click', '#btnSave-Semester', function () {
            if ($('#frmSaveData-Semester').valid()) {
                semesterController.Save();
                $('#InfoSemester').modal('hide');
            }
        })
        $(document).stop().on('click', '.btn-edit-Semester', function () {
            var id = $(this).data('id');
            $('#InfoSemester').modal('show');
            semesterController.Detail(id);
        })
        $(document).stop().on('click', '.btn-delete-Semester', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
                if (result) {
                    semesterController.Delete(id);
                }
            })
        })
        $(document).stop().on('click', '.btn-info-Semester', function () {
            var id = $(this).data('id');
            $('#InfoCourseInSemester').modal('show');
            semesterController.CourseInSemester(id);
        })
        $(document).stop().on('click', '.btn-delete-Course', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc muốn xóa?", function (result) {
                if (result) {
                    semesterController.DeleteCourse(id);
                    $('#InfoCourseInSemester').modal('hide');
                }
            })
        })
        $(document).stop().on('click', '#btnAdd-Semester', function () {
            $('#InfoSemester').modal('show');
            semesterController.reset();
        })
    },

    GetSemester: function () {
        $.ajax({
            url: '/Semester/GetSemester',
            type: 'GET',
            dataType: 'json',
            data: {
                page: semesterconfig.pageIndex,
                pageSize: semesterconfig.pageSize
            },
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-Semester').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            TITLE: item.TITLE,
                            DESCRIPTION: item.DESCRIPTION,
                        });
                    });
                    $('#tblData-Semester').html(html);
                    semesterController.paging(response.total, function () {
                        semesterController.GetSemester();
                    });
                }
            },
        });
    },
    paging: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / semesterconfig.pageSize);

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                semesterController.GetSemester();
                semesterconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    Detail: function (id) {
        $.ajax({
            url: '/Semester/Detail',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#IDsemester').val(data.ID);
                    $('#title').val(data.TITLE);
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
    Delete: function (id) {
        $.ajax({
            url: '/Semester/Delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Xóa thành công", function () {
                        semesterController.GetSemester(true);
                    })
                }
            }
        });
    },
    Save: function () {
        var id = $('#IDsemester').val();
        var title = $('#title').val();
        var description = $('#description').val();
        var semester = {
            ID: id,
            TITLE: title,
            DESCRIPTION: description,
        };
        $.ajax({
            url: '/Semester/Save',
            data: semester,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Thêm thành công!", function () {
                        $('#InfoSemester').modal('hide');
                        semesterController.GetSemester(true);
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
    DeleteCourse: function (id) {
        $.ajax({
            url: '/Semester/DeleteCourseInSemester',
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
    CourseInSemester: function (id) {
        $.ajax({
            url: '/Semester/CourseInSemester',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-CourseInSemester').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            NAME: item.NAME,
                            DESCRIPTION: item.DESCRIPTION,
                            NAMETEACHER: item.TEACH.C_USER.LAST_NAME + ' ' + item.TEACH.C_USER.MIDDLE_NAME + ' ' + item.TEACH.C_USER.FIRST_NAME,
                        });
                    });
                    $('#tblData-CourseInSemester').html(html);
                }
            }
        });
    },
    reset: function () {
        $('#IDsemester').val('0');
        $('#title').val('');
        $('#description').val('');
    },
}
semesterController.init();