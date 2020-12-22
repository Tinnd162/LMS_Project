var courseconfig = {
    pageSize: 5,
    pageIndex: 1,
}
var courseController = {
    init: function () {
        courseController.GetCourse();
        courseController.registerEvent();
        courseController.GetNameSemester();
        courseController.GetNameSubject();
    },
    registerEvent: function () {
        $(document).stop().on('click', '#btnSave-Course', function () {
            if ($('#frmSaveData-Course').valid()) {
                courseController.Save();
            }
        })
        $(document).stop().on('click', '.btn-delete-Course', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn chắc chắn muốn xóa", function (result) {
                if (result) {
                    courseController.Delete(id);
                }
            });
        })
        $(document).stop().on('change', '#title', function (e) {
            var optionSelected1 = $(this).find("option:selected");
            var id1 = optionSelected1.data("idOptionsemester");
            $('#semester_id').val(id1);
        })
        $(document).stop().on('change', '#name', function (e) {
            var optionSelected2 = $(this).find("option:selected");
            var id2 = optionSelected2.data("idOptionsubject");
            $('#subject_id').val(id2);
        })
        $(document).stop().on('click', '.btn-edit-Course', function () {
            var id = $(this).data('id');
            $('#CourseUpdateDetail').modal('show');
            courseController.Detail(id);
        })

        $(document).stop().on('click', '#btnAdd-Course', function () {
            $('#CourseUpdateDetail').modal('show');
            courseController.reset();
        })
        $(document).stop().on('click', '.btn-info-Course', function () {
            var id = $(this).data('id');
            $('#InfoCourse').modal('show');
            courseController.InfoCourse(id);
        })
        $(document).stop().on('click', '.btn-delete-Student', function () {
            var id = $(this).data('id');
            $('#InfoCourse').modal('show');
            courseController.InfoCourse(id);
        })
        $(document).stop().on('click', '.btn-delete-Student', function () {
            var id = $(this).data('id');
            bootbox.confirm("Bạn có chắc muốn xóa?", function (result) {
                if (result) {
                    courseController.DeleteStudent(id);
                    $('#InfoCourse').modal('hide');
                }
            })
        })
    },
    GetCourse: function () {
        $.ajax({
            url: '/Course/GetCourse',
            type: 'GET',
            dataType: 'json',
            data: {
                page: courseconfig.pageIndex,
                pageSize: courseconfig.pageSize
            },
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var a = [];
                    var template = $('#data-Course').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            NAME: item.NAME,
                            DESCRIPTION: item.DESCRIPTION,
                        });
                    });
                    $('#tblData-Course').html(html);
                    courseController.paging(response.total, function () {
                        courseController.GetCourse();
                    });
                    courseController.registerEvent();
                }
            },
        });
    },
    paging: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / courseconfig.pageSize);

        ////Unbind pagination if it existed or click change pagesize
        //if ($('#pagination a').length === 0 || changePageSize === true) {
        //    $('#pagination').empty();
        //    $('#pagination').removeData("twbs-pagination");
        //    $('#pagination').unbind("page");
        //}

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 10,
            onPageClick: function (event, page) {
                courseController.GetCourse();
                courseconfig.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    Delete: function (id) {
        $.ajax({
            url: '/Course/Delete',
            data: { id: id },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Xóa thành công!", function () {
                        courseController.GetCourse(true);
                    })
                }
            }
        });
    },
    Detail: function (id) {
        $.ajax({
            url: '/Course/Detail',
            data: {
                id: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data[0];
                    $('#IDcourse').val(data.IDCOURSE);
                    $('#namecourse').val(data.NAMECOURSE);
                    $('#description').val(data.DESCRIPTION);
                    $('#semester_id').val(data.SEMESTER.ID);
                    $('#title').val(data.SEMESTER.TITLE);
                    $('#subject_id').val(data.SUBJECT.ID);
                    $('#name').val(data.SUBJECT.NAME);
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
        var idcourse=$('#IDcourse').val();
        var namecourse=$('#namecourse').val();
        var description=$('#description').val();
        var semester_id=$('#semester_id').val();
        var title=$('#title').val();
        var subject_id=$('#subject_id').val();
        var name=$('#name').val();
        var Course = {
            ID :idcourse,
            NAME: namecourse,
            DESCRIPTION: description,
            SEMESTER_ID: semester_id,
            SUBJECT_ID: subject_id,
        }
        $.ajax({
            url: '/Course/Save',
            data: Course,
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    bootbox.alert("Lưu thành công.", function () {
                        $('#CourseUpdateDetail').modal('hide');
                        courseController.GetCourse(true);
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
    GetNameSemester: function () {
        $.ajax({
            url: '/Admin/Course/GetNameSemester',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                for (var i = 0; i < data.length; i++) {
                    var optsemester = new Option(data[i].TITLE);
                    $(optsemester).data('idOptionsemester', data[i].ID);
                    $('#title').append(optsemester);
                }
     
            }
        })
    },
    GetNameSubject: function () {
        $.ajax({
            url: '/Admin/Course/GetNameSubject',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                for (var i = 0; i < data.length; i++) {
                    var optsubject = new Option(data[i].NAME);
                    $(optsubject).data('idOptionsubject', data[i].ID);
                    $('#name').append(optsubject);
                }

            }
        })
    },
    reset: function () {
        $('#IDcourse').val('0');
        $('#namecourse').val();
        $('#description').val();
        $('#semester_id').val();
        $('#title').val();
        $('#subject_id').val();
        $('#name').val();
    },
    InfoCourse: function (id) {
        $.ajax({
            url: '/Admin/Course/InfoCourse',
            data: { idcourse:id },
            dataType: 'json',
            type: 'POST',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-InfoStudent').html();
                    var teacher = $('#NameTeacher').val(data[0].TEACH.C_USER.LAST_NAME + ' ' + data[0].TEACH.C_USER.MIDDLE_NAME + ' ' + data[0].TEACH.C_USER.FIRST_NAME);
                    var course = $('#NameCourse').val(data[0].NAME);
                    $.each(data[0].C_USER, function (i, item) {
                        html += Mustache.render(template, {
                                ID: item.ID,
                                NAMESTUDENT: item.LAST_NAME + ' ' + item.MIDDLE_NAME + ' ' + item.FIRST_NAME,
                            });
                        });
                    $('#tblData-InfoStudent').html(html);
                }
            }
        })
    },
    DeleteStudent: function (id) {
        $.ajax({
            url: '/Course/DeleteStudent',
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
}
courseController.init();