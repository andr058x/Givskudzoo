using System;
using System.Web.Mvc;
using GivskudZoo.Bll;
using GivskudZoo.Model;
using GivskudZoo.Web.Models;

namespace GivskudZoo.Web.Controllers   //Here we have the ActionResult that they are public methods
{
    public class ActivityController : Controller   
    {
        private readonly ActivityManager _manager;

        public ActivityController()
        {
            _manager = new ActivityManager();
        }

        // GET: Activity
        public ActionResult Index(ActivityDateModel model)
        {
            var activityModel = new ActivityModel
            {
                Date = model.Date == default(DateTime) ? DateTime.Today : model.Date
            };

            var l = _manager.GetActivityList(activityModel.Date);

            activityModel.List = l;

            return View(activityModel);
        }

        // GET: Activity/Details/5
        public ActionResult Details(int id)
        {
            var e = _manager.GetActivity(id);

            var model = new ActivityDetailsModel
            {
                CreationDate = e.CreationDate,
                LastUpdateDate = e.LastUpdateDate,
                BeginDate = e.BeginDate,
                EndDate = e.EndDate,
                Title = e.Title,
                Location = e.Location,
                Description = e.Description
            };

            return PartialView("_Details", model);
        }

        // GET: Activity/Create
        public ActionResult Create()
        {
            var activityModel = new ActivityModelInput
            {
                Date = DateTime.Today
            };

            return PartialView("_Create", activityModel);
        }

        // POST: Activity/Create
        [HttpPost]
        public ActionResult Create(ActivityModelInput model)
        {
            try
            {
                var eventsDto = GenerateDto(model);

                var res = _manager.AddActivity(eventsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Error = "Error during the save process!" });
            }
        }

        // GET: Activity/Edit/5
        public ActionResult Edit(int id)
        {
            var e = _manager.GetActivity(id);

            var model = new ActivityModelOutput
            {
                Id = e.Id,
                Title = e.Title,
                Location = e.Location,
                Description = e.Description,
                Date = e.BeginDate.Date,
                BeginHour = e.BeginDate.Hour,
                BeginMin = e.BeginDate.Minute,
                EndHour = e.EndDate.Hour,
                EndMin = e.EndDate.Minute,
            };

            return PartialView("_Edit", model);
        }

        // PUT: Activity/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, ActivityModelInput model)
        {
            try
            {
                var eventsDto = GenerateDto(model);
                eventsDto.Id = id;

                var res = _manager.PutActivity(eventsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Error = "Error during the save process!" });
            }
        }

        // GET: Activity/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _manager.DeleteActivity(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Activity/Delete/5
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

        private static ActivityDto GenerateDto(ActivityModelInput model)
        {
            var eventsDto = new ActivityDto
            {
                Title = model.Title,
                Description = model.Description,
                Location = model.Location,
                BeginDate = model.Date.AddHours(model.BeginHour).AddMinutes(model.BeginMin),
                EndDate = model.Date.AddHours(model.EndHour).AddMinutes(model.EndMin)
            };

            return eventsDto;
        }
    }
}
