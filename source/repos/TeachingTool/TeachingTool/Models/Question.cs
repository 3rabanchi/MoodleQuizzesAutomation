using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeachingTool.Models
{
    public class Question
    {
       public int QuestionID { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public string userToken { get; set; }
    }
    public enum QuestionTypes
    {
        Językowy,
        Assembly
    }
}
