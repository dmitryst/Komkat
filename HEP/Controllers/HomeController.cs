using HEPDataLayer;
using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HEP.Controllers
{


    public class HomeController : BaseController
    {
        private UnitOfWork unitOfWork;
        private int langInUse = 2;
        private IEnumerable<GetItemListProcedure_Result> itemsList;
        private const int LangId = 1;


        public HomeController()
        {
            unitOfWork = new UnitOfWork();
            itemsList = unitOfWork.ItemRepository.GetItemList(LangId);
        }


        public ActionResult Index()
        {

            var types = unitOfWork.MachineTypeRepository.GetAllMachineTypeByLang(langInUse).OrderBy(m => m.Text);
            ViewBag.Types = types;

            var brands = unitOfWork.BrandRepository.GetAllBrands().OrderBy(p => p.BrandName);
            ViewBag.Brands = brands;

            return View();
        }

        public JsonResult LoadBrands(string firstValue)
        {
            StaticClass._machineTypeEnum = (MachineTypeEnum)Enum.Parse(typeof(MachineTypeEnum), firstValue);
            var result = unitOfWork.BrandRepository.GetBrandByType(StaticClass._machineTypeEnum);

            IList<SelectListItem> Data = new List<SelectListItem>();
            foreach (Brand brand in result)
            {
                Data.Add(new SelectListItem()
                    {
                        Text = brand.BrandName,
                        Value = brand.Id.ToString(),
                    });
            }
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ShowBrand()
        {
            return PartialView("_ShowBrand");
        }

        public JsonResult LoadModels(string fstValue)
        {


            int Id = 0;
            if (fstValue != "")
                Id = Convert.ToInt32(fstValue);

            var result = unitOfWork.ModelRepository.GetModelListByTypeAndBrand(StaticClass._machineTypeEnum, Convert.ToInt32(Id));

            IList<SelectListItem> Data = new List<SelectListItem>();
            foreach (Model model in result)
            {
                Data.Add(new SelectListItem()
                {
                    Text = model.ModelName,
                    Value = model.Id.ToString(),
                });
            }
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ChooseModel()
        {
            return PartialView("_ChooseModel");
        }
          
        public ActionResult SearchByNumber(string search, int? modelId)
        {
            if (modelId != null)
                return View("Products", itemsList.Where(x => x.ModelId == modelId).ToList());
            else
                if (search == null || search == string.Empty)
                    return View("Products", itemsList.ToList());
                else
                    return View("Products", itemsList.Where(x => x.CatalogNumber == search));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}