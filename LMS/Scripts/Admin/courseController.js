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
        courseController.GetFacultyID_NAME();
    },
    registerEvent: function () {
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
        $(document).stop().on('change', '#facultyname', function (e) {
            var optionSelected = $(this).find("option:selected");
            var id = optionSelected.data("idOption");
            $('#IDfacl').val(id);
            courseController.GetTeacherInFaculty(id);
        })
        $(document).stop().on('change', '#teachername', function (e) {
            var optionSelected = $(this).find("option:selected");
            var id = optionSelected.data("idOptionteacher");
            $('#IDteacher').val(id);
        })
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
        $(document).stop().on('click', '#btnAdd-Course', function () {
            $('#CourseUpdateDetail').modal('show');
            courseController.reset();
           
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
        $(document).stop().on('click', '.btn-info-Course', function () {
            var id = $(this).data('id');
            $('#InfoCourse').modal('show');
            courseController.InfoCourse(id);
        })
        $(document).stop().on('click', '.btn-edit-Course', function () {
            var id = $(this).data('id');
            var idfacl = $(this).data('facl')
            $('#CourseUpdateDetail').modal('show');
            courseController.GetTeacherInFaculty(idfacl);
            courseController.Detail(id);       
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
                            IDFACULTY: item.TEACH.C_USER.FACULTY.ID,
                        });
                    });
                    $('#tblData-Course').html(html);
                    courseController.paging(response.total, function () {
                        courseController.GetCourse();
                    });
                }
            },
        });
    },
    paging: function (totalRow, callback) {
        var totalPage = Math.ceil(totalRow / courseconfig.pageSize);

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
                    $('#IDfacl').val(data.TEACH.C_USER.FACULTY.ID);
                    $('#facultyname').val(data.TEACH.C_USER.FACULTY.NAME);
                    $('#IDteacher').val(data.TEACH.C_USER.ID);
                    $('#teachername').val(data.TEACH.C_USER.LAST_NAME + ' ' + data.TEACH.C_USER.MIDDLE_NAME + ' ' + data.TEACH.C_USER.FIRST_NAME);
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
        var name = $('#name').val();
        var idfacl =$('#IDfacl').val();
        var namefacl =$('#facultyname').val();
        var idteacher =$('#IDteacher').val();
        var nameteacher =$('#teachername').val();
        var Course = {
            ID: idcourse,
            NAME: namecourse,
            DESCRIPTION: description,
            SEMESTER_ID: semester_id,
            SUBJECT_ID: subject_id,
        };
        var Teach = {
            USER_ID:idteacher,
            COURSE_ID:idcourse,
        };
        $.ajax({
            url: '/Course/Save',
            data: {
                Course:Course,
                Teach:Teach
            },
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
    GetTeacherInFaculty: function (id) {
        $.ajax({
            url: '/Admin/Course/GetTeacherInFaculy',
            type: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (response) {
                var data = response.data[0];
                $('#teachername').html('')
                for (var j = 0; j < data.C_USER.length; j++) {
                    var optteacher = new Option(data.C_USER[j].LAST_NAME + ' ' + data.C_USER[j].MIDDLE_NAME + ' ' + data.C_USER[j].FIRST_NAME);
                    $(optteacher).data('idOptionteacher', data.C_USER[j].IDTEA);
                    $('#teachername').append(optteacher);
                }

            }
        })
    },
    reset: function () {
        $('#IDcourse').val('0');
        $('#namecourse').val('');
        $('#description').val('');
        $('#semester_id').val('');
        $('#title').val('');
        $('#subject_id').val('');
        $('#name').val('');
        $('#IDfacl').val('');
        $('#facultyname').val('');
        $('#IDteacher').val('');
        $('#teachername').val('');
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
    GetFacultyID_NAME: function () {
        $.ajax({
            url: '/Admin/Teacher/GetFacultyID_NAME',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var data = response.data;
                for (var i = 0; i < data.length; i++) {
                    var opt = new Option(data[i].NAME);
                    $(opt).data('idOption', data[i].ID);
                    $('#facultyname').append(opt);
                }

            }
        })
    },
}
courseController.init();