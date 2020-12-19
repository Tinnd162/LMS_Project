var teacherController = {
	init: function () {
		teacherController.getteacher();
		teacherController.registerEvent();
		teacherController.getfacultyID_NAME();
	},
	registerEvent: function () {
		$(document).stop().on('click', '.btn-delete-teacher', function () {
			var id = $(this).data('id');
			bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
				if (result) {
					teacherController.delete(id);
				}
			})
		})
		$(document).stop().on('click', '.btn-edit-teacher', function () {
			var id = $(this).data('id');
			$('#update-teacher-info').modal('show');
			teacherController.detail(id);
		})
		$(document).stop().on('click', '#btnSave-infoteacher', function () {
			if ($('#frmSaveDataTeacher').valid()) {
				teacherController.save();
			}
		})
		$(document).stop().on('click', '#facultyname', function (e) {
			var optionSelected = $(this).find("option:selected");
			var id = optionSelected.data("idOption");
			$('#IDfacl').val(id);
		})
		$(document).stop().on('click', '.btn-info-teacher', function () {
			var id = $(this).data('id');
			$('#infoteacher').modal('show');
			teacherController.getsubbyID(id);
		})
		$(document).stop().on('click', '.btn-delete-subject', function () {
			var idsub = $(this).data('id');
			var idcourse = $(this).data('course');
			bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
				if (result) {
					teacherController.deletesubbyID(idsub, idcourse);
                }
            })
        })
	},
	save: function () {
		var id = $('#ID').val();
		var first = $('#first_name').val();
		var last = $('#last_name').val();
		var middle = $('#middle_name').val();
		var phone = $('#phone_no').val();
		var sex = $('#sex').val();
		var dob = $('#dob').val();
		var mail = $('#mail').val();
		var password = $('#password').val();
		var lastvisited = $('#lastvisited').val();
		var facultyid = $('#IDfacl').val();
		var infoteacher = {
			ID: id,
			FIRST_NAME: first,
			LAST_NAME: last,
			MIDDLE_NAME: middle,
			PHONE_NO: phone,
			SEX: sex,
			DoB: dob,
			MAIL: mail,
			PASSWORD: password,
			LASTVISITDATE: lastvisited,
			FACULTY_ID: facultyid,
		}
		$.ajax({
			url: '/Admin/Teacher/save',
			data: infoteacher,
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					bootbox.alert("Save success", function () {
						$('#frmSaveDataTeacher').modal('hide');
						teacherController.getteacher(true);
					})
				} else {
					bootbox.log(response.message);
				}
			},
			error: function (err) {
				console.log(err);
			}
		})
	},
	getteacher: function () {
		$.ajax({
			url: '/Admin/Teacher/getteacher',
			type: 'GET',
			dataType: 'json',
			success: function (response) {
				if (response.status) {
					var data = response.data;
					var html = '';
					var template = $('#data-teacher').html();
					$.each(data, function (i, item) {
						html += Mustache.render(template, {
							IDNAMEFACULTY: item.FACULTY.ID,
							NAMEFACULTY: item.FACULTY.NAME,
							ID: item.ID,
							FIRST_NAME: item.LAST_NAME + ' ' + item.MIDDLE_NAME + ' ' + item.FIRST_NAME,
							PHONE_NO: item.PHONE_NO,
							MAIL: item.MAIL,
						});
						$('#tblData-Teacher').html(html);
					});
				}
			}
		})
	},
	delete: function (id) {
		$.ajax({
			url: '/Admin/Teacher/delete',
			data: {
				id: id
			},
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					bootbox.alert("Xóa thành công", function () {
						teacherController.getteacher(true);
					})
				}
			}
		});
	},
	detail: function (id) {
		$.ajax({
			url: '/Admin/Teacher/detail',
			data: {
				idnameteacher: id,
			},
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					var data = response.data[0];
					var datetime = teacherController.convert(data.DoB);
					$('#ID').val(data.ID);
					$('#first_name').val(data.FIRST_NAME);
					$('#last_name').val(data.LAST_NAME);
					$('#middle_name').val(data.MIDDLE_NAME);
					$('#phone_no').val(data.PHONE_NO);
					$('#sex').val(data.SEX);
					$('#dob').val(datetime);
					$('#mail').val(data.MAIL);
					$('#password').val(data.PASSWORD);
					$('#lastvisited').val(data.LASTVISITED);
					$('#facultyname').val(data.FACULTY.NAME);
					$('#IDfacl').val(data.FACULTY.ID);
				}
			}
		})
	},
	convert: function ConvertTimestampJSONToDateTime(timestampJson) {
		let timestamp = timestampJson.slice(6, -2)
		let date = new Date(parseInt(timestamp))
		return date.getFullYear() + '/' + ((date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) +
			'/' + (date.getDate() > 9 ? date.getDate() : ('0' + date.getDate())) +
			' ' + date.toLocaleTimeString('vi');
	},
	getfacultyID_NAME: function () {
		$.ajax({
			url: '/Admin/Teacher/getfacultyID_NAME',
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
	getsubbyID: function (id) {
		$.ajax({
			url: '/Admin/Teacher/getsubbyID',
			data: {idteacher:id},
			type: 'POST',
			datatype: 'json',
			success: function (response) {
				if (response.status == true) {
					var data = response.data;
					var html = '';
					var template = $('#data-infosubject').html();
					var teacher = $('#nameteacher').val(data[0].LAST_NAME + ' ' + data[0].MIDDLE_NAME + ' ' + data[0].FIRST_NAME);
					$.each(data[0].SUBJECTs1, function (i, item) {
						html += Mustache.render(template, {
							IDSUB: item.IDSUB,
							NAMESUB: item.NAMESUB,
							IDCOURSE: item.COURSE.ID,
							COURSE: item.COURSE.TILTE,
						});
					});
					$('#tblData-subject').html(html);
                }
            }
        })
	},
	deletesubbyID: function (idsub, idcourse) {
		$.ajax({
			url: '/Admin/Teacher/deletesubbyID',
			data: {
				idsub: idsub,
				idcourse: idcourse
			},
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					bootbox.alert("Xóa thành công", function () {
						$('#infoteacher').modal('hide');
					})
                }
            }
        })
    }
}
teacherController.init();