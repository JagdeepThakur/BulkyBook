using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticData.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            //Create
            if (id == null)
            {
                return View(coverType);
            }
            //edit
            var param = new DynamicParameters();
            param.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticData.Proc_CoverType_Get, param);

            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var param = new DynamicParameters();
                param.Add("@Name", coverType.Name);
                if (coverType.ID == 0)
                {
                    _unitOfWork.SP_Call.Execute(StaticData.Proc_CoverType_Create, param);
                }
                else
                {
                    param.Add("@Id", coverType.ID);
                    _unitOfWork.SP_Call.Execute(StaticData.Proc_CoverType_Update, param);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var coverTypes = _unitOfWork.SP_Call.List<CoverType>(StaticData.Proc_CoverType_GetAll, null);
            return Json(new { data = coverTypes });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id",id);
            var objFromdb = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticData.Proc_CoverType_Get,param);
            if (objFromdb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SP_Call.Execute(StaticData.Proc_CoverType_Delete,param);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
