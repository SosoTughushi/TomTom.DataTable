using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Demo.Models;
using TomTom.Functional;
using FF = TomTom.Functional.Functional;

namespace TomTom.DataTable.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Simple()
        {
            return PartialView(new List<Demo1Model>()
            {
                new Demo1Model()
                {
                    Date = DateTime.Now,
                    Int = 3,
                    String = "Some String",
                    NestedProperty = new Demo1NestedModel()
                    {
                        String = "I'm Nested Class String"
                    }
                }
            });
        }

        public ActionResult RowTypes()
        {
            int count = 0;
            var create = FF.Parse(
                (int amount,bool hasCustomClass,bool isCorrupted,bool isDisabled,bool isImportant,bool isSuccessfull) =>
                    new Demo2ModelViewModel()
                    {
                        Model = new Demo2Model()
                        {
                            Amount = amount,
                            IsCorrupted = isCorrupted,
                            IsDisabled = isDisabled,
                            IsImportant = isImportant,
                            IsSuccess = isSuccessfull,
                            HasCustomClass = hasCustomClass,
                            Id = ++count
                        }
                    });

            return PartialView(new List<Demo2ModelViewModel>()
            {
                create(51,false,false,false,false,false),
                create(51,false,true,false,false,false),
                create(51,false,false,true,false,false),
                create(51,false,false,false,true,false),
                create(51,false,false,false,false,true),
                create(15,false,false,false,false,false),
                create(15,true,false,false,false,false),
            });
        }
        
    }
}