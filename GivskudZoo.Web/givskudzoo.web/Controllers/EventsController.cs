using System;
using GivskudZoo.Web.Models;
using System.Web.Mvc;
using GivskudZoo.Bll;
using GivskudZoo.Bll.Utils;
using GivskudZoo.Model;

namespace GivskudZoo.Web.Controllers
{
    public class EventsController : Controller   //Here we have the ActionResult that they are public methods
    {
        private readonly EventsManager _manager;

        public EventsController()
        {
            _manager = new EventsManager();
        }

        // GET: News
        public ActionResult Index(FilterDto filter)
        {
            var l = _manager.GetEventsList(filter);
            ViewBag.Filter = filter;
            return View(l);
        }

        // POST: News
        [HttpPost]
        public ActionResult Index(ComplexFilterDto filter)
        {
            var l = _manager.GetEventsList(filter);
            ViewBag.Filter = filter;
            return View(l);
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            var e = _manager.GetEvents(id);

            var model = new EventsDetailsModel
            {
                CreationDate = e.CreationDate,
                LastUpdateDate = e.LastUpdateDate,
                Author = e.Author,
                Title = e.Title,
                ShortDescription = e.ShortDescription,
                Description = e.Description
            };

            return PartialView("_Details", model);
        }

        // GET: News/Filter
        public ActionResult Filter()
        {
            return PartialView("_Filters", new ComplexFilterDto());
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(EventsModelInput model)
        {
            try
            {
                var eventsDto = GenerateDto(model);

                var res = _manager.AddEvents(eventsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Error = "Error during the save process!" });
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            var e = _manager.GetEvents(id);

            var model = new EventsModelOutput
            {
                Id = e.Id,
                FileName = e.Image.FileName,
                Title = e.Title,
                ShortDescription = e.ShortDescription,
                Description = e.Description
            };

            return PartialView("_Edit", model);
        }

        // PUT: Events/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, EventsModelInput model)
        {
            try
            {
                var eventsDto = GenerateDto(model);
                eventsDto.Id = id;

                var res = _manager.PutEvents(eventsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Error = "Error during the save process!" });
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _manager.DeleteEvents(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private EventsDto GenerateDto(EventsModelInput model)
        {
            var eventsDto = new EventsDto
            {
                Title = model.Title,
                Author = User.Identity.Name,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
            };

            if (model.ImageFile != null)
                eventsDto.Image = new ImageDto
                {
                    Blob = Base64Manager.ReadFully(model.ImageFile.InputStream),
                    MimeType = model.ImageFile.ContentType,
                    FileName = model.ImageFile.FileName,
                    Size = model.ImageFile.ContentLength
                };
            return eventsDto;
        }

    }
}
