using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.StudentView
{
    public class CourseDetailsView
    {
        public string courseID { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public string topicID { get; set; }
        public string topicTitle { get; set; }
        public string topicDescription { get; set; }
        public string documentID { get; set; }
        public string documentTitle { get; set; }
        public string documentDescription { get; set; }
        public string documentLink { get; set; }
        public string eventID { get; set; }
        public string eventTitle { get; set; }
        public DateTime? eventDeadline { get; set; }
    }
}
