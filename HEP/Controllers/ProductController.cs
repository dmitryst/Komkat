using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HEPDataModel;
using HEPDataLayer;
using PagedList;
using PagedList.Mvc;

namespace HEP.Controllers
{
    public class ProductController : BaseController
    {
        private IEnumerable<GetItemListProcedure_Result> itemsList;
        //private IEnumerable<Model> modelList;
        private UnitOfWork unitOfWork;
        private const int LangId = 1;
        //List<Brand> brandList;

        public ProductController()
        {
            unitOfWork = new UnitOfWork();
            itemsList = unitOfWork.ItemRepository.GetItemList(LangId);
            //modelList = unitOfWork.ModelRepository.GetAllModels();
            //brandList = unitOfWork.BrandRepository.GetAllBrands().ToList();
        }

        public ActionResult Index(string search, int? page, int modelId)
        {
            
            //if (search == null || search == string.Empty)
            //    return View(itemsList.ToList());
            //else
            //    if ( modelId == 0)
            //        return View(itemsList.Where(x => x.CatalogNumber == search).ToList());
            //    else 
            //        return View(itemsList.Where(x => x.ModelId == modelId).ToList());
            return View(itemsList);

        }

        public ActionResult ShowPartsForThisModel(int modelId)
        {
            
            itemsList = itemsList.Where(x => x.ModelId == modelId).ToList();
            return RedirectToAction("Index");
        }

        //{
        //    List<SelectListItem> dropDownData = new List<SelectListItem>();

        //    foreach (Brand brand in brandList)
        //        dropDownData.Add(new SelectListItem { Text = brand.BrandName, Value = brand.Id.ToString() });

        //    ViewData["Brands"] = dropDownData;

        //    return View(itemsList.ToList());
        //}
        //public ActionResult TempDataDemo()
        //{
        //    TempData["SuccessMessage"] = "The Action succeeded!";

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Auction()
        //{


        //    var auction = new HEP.Models.AuctionModel()
        //    {
        //        Title = "Example Auction",
        //        Description = "This is an example Auction",
        //        StartTime = DateTime.Now,
        //        EndTime = DateTime.Now.AddDays(7),
        //        StartPrice = 1.00m,
        //        CurrentPrice = null,
        //    };


        //    return View(auction);
        //}

       
    }
}