var subjectsconfig = {
    pageSize: 5,
    pageIndex: 1,
}
var subjectsController = {
    init: function () {
        subjectsController.GetSubjects();
        subjectsController.registerEvent();
    },
    registerEvent: function () {
        $(document).stop().on('click', '.btn-delete-Subjects', function (e) {
            console.log(e)
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
                if (result) {
                    subjectsController.Delete(id);
                }
            });
        })
        $(document).stop().on('click', '.btn-edit-Subjects', function () {
            var id = $(this).data('id');
            $('#SubjectsUpdateDetail').modal('show');
            subjectsController.Detail(id);
        })
        $(document).stop().on('click', '#btnSave-Subjects', function () {
            if ($('#frmSaveData-Subjects').valid()) {
                subjectsController.Save();
            }
        })
        $(document).stop().on('click', '#btnAdd-Subjects', function () {
            $('#SubjectsUpdateDetail').modal('show');
            subjectsController.Reset();
        })
        $(document).stop().on('click', '.btn-info-Subjects', function () {
            var id = $(this).data('id');
            $('#InfoSubjects').modal('show');
            subjectsController.GetCourseInSubject(id);
        })
        $(document).stop().on('click', '.btn-delete-Course', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc muốn xóa?", function (result) {
                if (result) {
                    subjectsController.DeleteCourse(id);
                    $('#InfoSubjects').modal('hide');
                }
            })
        })
        $(document).stop().on('click', '#btnSearch', function () {
            subjectsController.GetSubjects(true);
        })
        $(document).stop().on('keypress', '#txtSearch', function (e) {
            if (e.which == 13) {
                subjectsController.GetSubjects(true);
            }
        })
    },
    GetSubjects: function (changePageSize)
    {
        var name = $('#txtSearch').val();
        $.ajax({
            url: '/Subjects/GetSubjects',
            type: 'GET',
            dataType: 'json',
            data: {
                name: name,
                page: subjectsconfig.pageIndex,
                pageSize: subjectsconfig.pageSize
            },
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var a = [];
                    var template = $('#data-Subjects').html();
                    if (data != '') {
                        $.each(data, function (i, item) {
                            html += Mustache.render(template, {
                                ID: item.ID,
                                NAME: item.NAME,
                                DESCRIPTION: item.DESCRIPTION,
                            });
                        });
                        $('#tblData-Subjects').html(html);
                        subjectsController.paging(response.total, function () {
                            subjectsController.GetSubjects();
                        }, changePageSize);
                    }
                    else {
                        alert("không có thông tin");
                    }
                }
            },
        });
    },
    Delete: function (id) {
        $.ajax({
            url: '/Subjects/Delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Xóa thành công!", function () {
                        console.log(response.total)
                        subjectsController.GetSubjects(true);
                    })
                }
            }
        });
    },
    Detail: function (id) {
        $.ajax({
            url: '/Subjects/Detail',
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
    Save: function () {
        var id = $('#IDsub').val();
        var name = $('#name').val();
        var desciption = $('#description').val();
        var subjects = {
            ID: id,
            NAME: name,
            DESCRIPTION: desciption,
        }
        $.ajax({
            url: '/Subjects/Save',
            data: subjects,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Lưu thành công", function () {
                        $('#SubjectsUpdateDetail').modal('hide');
                        subjectsController.GetSubjects(true);
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
    Reset: function () {
        $('#IDsub').val('0');
        $('#name').val('');
        $('#description').val('');
    },
    GetCourseInSubject: function (id) {
        $.ajax({
            url: '/Admin/Subjects/GetCourseInSubject',
            data: { idsub: id },
            dataType: 'json',
            type: 'POST',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-InfoCourse').html();
                    var subjects = $('#NameSubjects').val(data[0].NAME);
                    $.each(data[0].COURSE, function (i, item) {
                        html += Mustache.render(template, {
                            IDCOURSE: item.IDCOURSE,
                            NAMECOURSE: item.NAMECOURSE,
                            NAMETEACHER: item.TEACH.C_USER.LAST_NAME + ' ' + item.TEACH.C_USER.MIDDLE_NAME + ' ' + item.TEACH.C_USER.FIRST_NAME,
                        });
                    });
                    $('#tblData-InfoCourse').html(html);
                }
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
    paging: function (totalRow, callback, changePageSize) {
        var totalPage = Math.ceil(totalRow / subjectsconfig.pageSize);

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }
        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                subjectsconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
}
subjectsController.init();