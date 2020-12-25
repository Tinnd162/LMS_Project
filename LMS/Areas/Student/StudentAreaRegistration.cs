using System.Web.Mvc;

namespace LMS.Areas.Student
{
    public class StudentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Student";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Student_subject",
            //    "Student/Subject/{subject_id}",
            //    new { Controller = "Subject", action = "GetTopicStudent", id = UrlParameter.Optional }
            //);
            //context.MapRoute(
            //    "Student_subjectAssessment",
            //    "Student/Subject-Assessment/{subject_id}",
            //    new { Controller = "Subject", action = "GetSubjectAssessments", id = UrlParameter.Optional }
            //);
            context.MapRoute(
                "Student_default",
                "Student/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}