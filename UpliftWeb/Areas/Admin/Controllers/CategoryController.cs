using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpLift.DataAccess.Data.Repository.IRepository;
using UpLift.Models;

namespace UpliftWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork _unit)
        {
            unitOfWork = _unit;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Category category = new Category();

            if (id == null)
            {
                return View(category);
            }

            category = unitOfWork.Category.Get(id.GetValueOrDefault());

            if (category == null)
            {
                return NotFound();
            }

            return View(category);


        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unitOfWork.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var onjFrmDb = unitOfWork.Category.Get(id);

            if (onjFrmDb == null)
            {
                return Json(new { success = false,message="Error while deleting." });
            }

            unitOfWork.Category.Remove(onjFrmDb);

            unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully" });
        }

        #endregion
    }
}