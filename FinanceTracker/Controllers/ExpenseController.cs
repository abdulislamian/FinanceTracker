using FinanceTracker.Models;
using FinanceTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Controllers
{
    public class ExpenseController : Controller
    {
        public readonly ApplicationDBContext _db;
        public ExpenseController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> itemlist = _db.Expenses;
            foreach(var obj in itemlist)
            {
                obj.ExpenseCategory = _db.ExpenseCategories.FirstOrDefault(x => x.Id == obj.ExpenseTypeId);
            }
            return View(itemlist);
            //return View();
        }
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDD = _db.ExpenseCategories.Select(x => new
            //SelectListItem
            //{
            //    Text =x.CategoryType,
            //    Value=x.Id.ToString()
            //});
            //ViewBag.ExpenseTypeDD = TypeDD;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
                {
                    Text = i.CategoryType,
                    Value = i.Id.ToString()
                })
            };
            return View(expenseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //DeleteGet
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Delete Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Expenses.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Get Update
        public IActionResult Update(int? Id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
                {
                    Text = i.CategoryType,
                    Value = i.Id.ToString()
                })
            };
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense = _db.Expenses.Find(Id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            //obj.TypeDropDown = null;
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
