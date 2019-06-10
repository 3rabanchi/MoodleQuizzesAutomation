using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeachingTool.Models;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Serialization;
using TeachingTool.Models.ModelsToSerialize;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Text;

namespace TeachingTool.Controllers
{
    [Authorize]
    //[Area("Questions")]
  //  [Route("Questions")]
    public class QuestionsController : Controller
    {
        private readonly TeachingToolDBContext _context;

        public QuestionsController(TeachingToolDBContext context)
        {
            _context = context;
        }

        // GET: Questions
        [Route("Questions")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }


        // GET: Questions/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }
        [HttpGet]
        [Route("Create")]
        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            string strTypeValue = Request.Form["selectType"].ToString();
            TempData["Type"] = strTypeValue;
            ViewData["Type"] = strTypeValue;
            question.type = strTypeValue;
            if (strTypeValue == "Językowy")
                return View("Type", question);
            else if (strTypeValue == "Developerski")
                return View("ECOARMemory");
            return View();
        }
        // [Route("Create/x86")]
        // public IActionResult X86(string id)
        //  {
        //    Question question = new Question();
        //    question.type = "x86";
        //      return View(question);
        //  }
       // [HttpGet("[controller]/[action]/{rowCount}")]
        [HttpPost]
        public async Task<JsonResult> X86(string cellValues,string title)
        {
           var rows = JsonConvert.DeserializeObject<List<Row>>(cellValues);
            title = title.Replace("\"","");
            // Console.WriteLine(ss[0]);
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
            System.Diagnostics.Debug.WriteLine(title);
            System.Diagnostics.Debug.WriteLine("-----------------------------------");

            StringBuilder sb = new StringBuilder("<table><tbody>", 10000);
            int i = 0;
            while (i < rows.Count())
            {
                StringBuilder tempsb = new StringBuilder("<tr>", 1000);
                if (i < 3) {          
                    tempsb.AppendFormat("<td><span style:\"width:150px;\">{0}</span></td><td><span>{1}</span></td><td><span>{2}</span></td></tr>", rows[i].FirstCell, rows[i].SecondCell, rows[i].ThirdCell);
                    sb.Append(tempsb);
                }
                else
                {
                    string second = "{1:SA:%100%"+rows[i].SecondCell+"~%0%0000000000}";
                    string third = "{1:SA:="+ rows[i].ThirdCell + "~%0%0000000000}";
                    tempsb.AppendFormat("<td><span>{0}</span  style:\"width:150px;\"></td><td><span>{1}</span></td><td><span>{2}</span></td></tr>", rows[i].FirstCell, second, third);
                    sb.Append(tempsb);
                }
                i++;
            }
            sb.Append("</tbody></ table > ");
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
            System.Diagnostics.Debug.WriteLine(sb);
            System.Diagnostics.Debug.WriteLine("-----------------------------------");
            System.Diagnostics.Debug.WriteLine(title);
            System.Diagnostics.Debug.WriteLine("-----------------------------------");

            string strTitleValue = Request.Form["questionTitle"].ToString();
            string type = Convert.ToString(TempData["Type"]);
            Question question = new Question();
            question.title = title;
            question.type = "x86";
            question.content = sb.ToString();
           _context.Add(question);
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            return new JsonResult("DUPA");
        }


        // GET: Questions/Edit/5
        [Route("/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionID,type,content,title")] Question question)
        {
            if (id != question.QuestionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        [Route("/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionID == id);
        }
     
        public async Task<IActionResult> Download(int id)
        {

            var _question = await _context.Questions.FindAsync(id);

            if (_question == null)
            {
                return NotFound();
            }

            string filename = _question.title+".xml";
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();

            XmlDocument doc = new XmlDocument();

                var XML = new XmlSerializer(typeof(quiz));

                  quiz quiz = new quiz();
                question question = new question();
                name name = new name();
                questiontext questiontext = new questiontext();
                generalfeedback generalfeedback = new generalfeedback();

                name.text = _question.title;
                questiontext.text = _question.content;
                questiontext.format = "html";
                generalfeedback.text = "";
                generalfeedback.format = "html";

                question.name = name;
                question.questiontext = questiontext;
                question.generalfeedback = generalfeedback;

                
                question.penalty = "0.333333";
                question.hidden = "0";
                question.idnumber = "";
                 question.type = "cloze";

                quiz.question = question;

                XML.Serialize(memory, quiz);
    
               
                memory.Position = 0;

          
           
            return File(memory, GetContentType(path), Path.GetFileName(path));
            //var net = new System.Net.WebClient();
            //var data = net.DownloadData(link);
            //var content = new System.IO.MemoryStream(data);
            //var contentType = "APPLICATION/octet-stream";
            //var fileName = "something.bin";
            //return File(content, contentType, fileName);
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".xml", "xml/test"}
            };
        }
    }
}
