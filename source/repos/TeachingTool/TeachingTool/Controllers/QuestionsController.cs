using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeachingTool.Models;
using Microsoft.AspNetCore.Authorization;

namespace TeachingTool.Controllers
{
    [Authorize]
    [Area("Questions")]
    [Route("questions")]
    public class QuestionsController : Controller
    {
        private readonly TeachingToolDBContext _context;

        public QuestionsController(TeachingToolDBContext context)
        {
            _context = context;
        }

        // GET: Questions
        [Route("")]
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
            if (ModelState.IsValid)
            {
                return View("Create2",question);
            }
            return View();
        }
        [Route("/Create/Type/{id}")]
        public IActionResult Type(string id)
        {
            ViewData["Type"] = id;
            var question = new Question();
            question.type = id;
            return View(question);
        }
        [HttpPost]
        [Route("/Create/Type/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Type(string id,[Bind("QuestionID,type,content,title")] Question question)
        {
            string type = Convert.ToString(TempData["Type"]);
            type = question.type;
            ViewData["Type"] = type;
            _context.Add(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
