var studentController = {
	init: function () {
		studentController.getstudent();
		studentController.registerEvent();
		studentController.getfacultyID_NAME();
	},
	registerEvent: function () {
		$(document).stop().on('click', '.btn-delete-student', function () {
			var id = $(this).data('id');
			bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
				if (result) {
					studentController.delete(id);
				}
			})
		})
		$(document).stop().on('click', '.btn-edit-student', function () {
			var id = $(this).data('id');
			var idfac = $(this).data('fac');
			$('#update-student-info').modal('show');
			studentController.detail(id);
			studentController.getclassinfaculty(idfac);
		})
		$(document).stop().on('click', '#btnSave-infostudent', function () {
			if ($('#frmSaveDataStudent').valid()) {
				studentController.save();
			}
		})
		$(document).stop().on('click', '#facultyname', function (e) {
			var optionSelected = $(this).find("option:selected");
			var id = optionSelected.data("idOptionfacul");
			$('#IDfacl').val(id);
			studentController.getclassinfaculty(id);
		})
		$(document).stop().on('click', '#classname', function (e) {
			var optionSelected = $(this).find("option:selected");
			var id = optionSelected.data("idOptionclass");
			$('#IDclass').val(id);
		})
		$(document).stop().on('click', '.btn-info-student', function () {
			var id = $(this).data('id');
			$('#infostudent').modal('show');
			studentController.getsubbyID(id);
		})
		$(document).stop().on('click', '.btn-delete-subject', function () {
			var idsub = $(this).data('id');
			var idcourse = $(this).data('course');
			bootbox.confirm("Bạn có chắc chắn muốn xóa?", function (result) {
				if (result) {
					studentController.deletesubbyID(idsub, idcourse);
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
		var classid = $('#IDclass').val();
		var infostudent = {
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
			CLASS_ID: classid,
		}
		$.ajax({
			url: '/Admin/Student/save',
			data: infostudent,
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					bootbox.alert("Save success", function () {
						$('#frmSaveDataStudent').modal('hide');
						studentController.getstudent(true);
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
	getstudent: function () {
		$.ajax({
			url: "/Admin/Student/getstudent",
			type: 'GET',
			dataType: 'json',
			success: function (response) {
				if (response.status) {
					var data = response.data;
					var html = '';
					var template = $('#data-student').html();
					$.each(data, function (i, item) {
						html += Mustache.render(template, {
							IDFACULTY: item.FACULTY.ID,
							NAMEFACULTY: item.FACULTY.NAME,
							IDCLASS: item.CLASS.ID,
							NAMECLASS: item.CLASS.NAME,
							MAJORCLASS: item.CLASS.MAJOR,
							ID: item.ID,
							FIRST_NAME: item.LAST_NAME + ' ' + item.MIDDLE_NAME + ' ' + item.FIRST_NAME,
							PHONE_NO: item.PHONE_NO,
							MAIL: item.MAIL,
						});
						$('#tblData-student').html(html);
					});
				}
			}
		})
	},
	delete: function (id) {
		$.ajax({
			url: '/Admin/Student/delete',
			data: {
				id: id
			},
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					bootbox.alert("Xóa thành công", function () {
						studentController.getstudent(true);
					})
				}
			}
		});
	},
	detail: function (id) {
		$.ajax({
			url: '/Admin/Student/detail',
			data: {
				idstudent: id,
			},
			type: 'POST',
			dataType: 'json',
			success: function (response) {
				if (response.status == true) {
					var data = response.data[0];
					var datetime = studentController.convert(data.DoB);
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
					$('#facultyname').val(data.FACULTY.NAMEFACULTY);
					$('#IDfacl').val(data.FACULTY.ID);
					$('#classname').val(data.CLASS.NAMECLASS + '--' + data.CLASS.MAJOR);
					$('#IDclass').val(data.CLASS.ID);
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
	getclassinfaculty: function (id) {
		$.ajax({
			url: '/Admin/Student/getclassinfaculty',
			type: 'POST',
			data: {id:id},
			dataType: 'json',
			success: function (response) {
				var data = response.data[0];
				$('#classname').html('')
				for (var j = 0; j < data.CLASSes.length; j++) {		
					var optclass = new Option(data.CLASSes[j].NAMECLASS + '--' + data.CLASSes[j].MAJORCLASS);
					$(optclass).data('idOptionclass', data.CLASSes[j].IDCLASS);
					$('#classname').append(optclass);
				}

			}
		})
	},
	getfacultyID_NAME: function () {
		$.ajax({
			url: '/Admin/Teacher/getfacultyID_NAME',
			type: 'GET',
			dataType: 'json',
			success: function (response) {
				var data = response.data;
				if ($('#facultyname').children('option').length < 1) {
					for (var i = 0; i < data.length; i++) {
						var opt = new Option(data[i].NAME);
						$(opt).data('idOptionfacul', data[i].ID);
						$('#facultyname').append(opt);
					}
                }
		
			}
		})
	},
	getsubbyID: function (id) {
		$.ajax({
			url: '/Admin/Student/getsubbyID',
			data: { idstudent: id },
			type: 'POST',
			datatype: 'json',
			success: function (response) {
				if (response.status == true) {
					var data = response.data;
					var html = '';
					var template = $('#data-infosubject').html();
					var student = $('#namestudent').val(data[0].LAST_NAME + ' ' + data[0].MIDDLE_NAME + ' ' + data[0].FIRST_NAME);
					$.each(data[0].SUBJECTs, function (i, item) {
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
						$('#infostudent').modal('hide');
					})
				}
			}
		})
	},
}
studentController.init();