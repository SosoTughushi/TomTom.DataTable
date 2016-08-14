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
            int id = 0;
            var create = FF.Parse((string stringParam, string nesterProperty) =>
                new Demo1Model()
                {
                    Date = DateTime.Now,
                    Int = ++id,
                    String = stringParam,
                    NestedProperty = new Demo1NestedModel()
                    {
                        String = nesterProperty
                    }
                });

            return View(new List<Demo1Model>()
            {
                create("Climax Blues Band", "Shopping Bag People"),
                create("PINK FLOYD", "SHEEP"),
                create("Beck", "Gamma Ray"),
                create("Led Zeppelin", "No Quarter"),
                create("Kill It Kid", "Pray On Me"),
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

            return View(new List<Demo2ModelViewModel>()
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

        public ActionResult ActionsDemo()
        {
            var create = FF.Parse((bool hasActions, string name) =>
                new ActionsModel()
                {
                    Name = name,
                    HasActions = hasActions
                }
            );

            return View(new List<ActionsModel>
            {
                create(true, "led zeppelin"),
                create(true,"pink floyd"),
                create(false,"sleepy sun"),
                create(true,"all them witches")
            });
        }

        public ActionResult DetailsDemo()
        {
            return View(_detailsSource());
        }

        public PartialViewResult Details(int id)
        {
            return PartialView(_detailsSource().First(c => c.Id == id));
        }

        private List<DetailsDemoModel> _detailsSource()
        {
            int count = 0;
            var create = FF.Parse((string name, string description) 
                => new DetailsDemoModel()
                {
                    Id = ++count,
                    Name = name,
                    Description = description
                });

            return new List<DetailsDemoModel>()
            {
                create("Sleepy Sun-Golden Artifact","https://www.youtube.com/watch?v=MekyM0C2pNo"),
                create("SHEEP - PINK FLOYD","https://www.youtube.com/watch?v=UqlsVZ1zxMk"),
                create("House Targaryen","https://www.youtube.com/watch?v=b7_e9n-S2t8"),
            };
        } 
    }
}