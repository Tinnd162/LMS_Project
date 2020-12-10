using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Topic
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubjectID { get; set; }
        public List<Document> Documents { get; set; }
        public List<Event> Events { get; set; }

    }
}