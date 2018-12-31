using System;
using GivskudZoo.Web.Models;
using System.Web.Mvc;
using GivskudZoo.Bll;
using GivskudZoo.Bll.Utils;
using GivskudZoo.Model;

namespace GivskudZoo.Web.Controllers  //Here we have the ActionResult that they are public methods
{
    public class NewsController : Controller
    {
        private readonly NewsManager _manager;

        public NewsController()
        {
            _manager = new NewsManager();
        }

        // GET: News
        public ActionResult Index(FilterDto filter)
        {
            var l = _manager.GetNewsList(filter);
            ViewBag.Filter = filter;
            return View(l);
        }

        // POST: News
        [HttpPost]
        public ActionResult Index(ComplexFilterDto filter)
        {
            var l = _manager.GetNewsList(filter);
            ViewBag.Filter = filter;
            return View(l);
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            var e = _manager.GetNews(id);

            var model = new NewsDetailsModel
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

        // GET: News/Create
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: News/Create
        [HttpPost]
        public ActionResult Create(NewsModelInput model)
        {
            try
            {
                var newsDto = GenerateDto(model);

                var res = _manager.AddNews(newsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch(Exception ex)
            {
                return Json(new ResultModel {Error = "Error during the save process!" });
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            var e = _manager.GetNews(id);

            var model = new NewsModelOutput
            {
                Id = e.Id,
                FileName = e.Image.FileName,
                Title = e.Title,
                ShortDescription = e.ShortDescription,
                Description = e.Description
            };

            return PartialView("_Edit", model);
        }

        // PUT: News/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, NewsModelInput model)
        {
            try
            {
                var newsDto = GenerateDto(model);
                newsDto.Id = id;

                var res = _manager.PutNews(newsDto);

                return Json(new ResultModel { Error = res ? string.Empty : "Error during the save in db!" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Error = "Error during the save process!" });
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _manager.DeleteNews(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: News/Delete/5
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

        private NewsDto GenerateDto(NewsModelInput model)
        {
            var newsDto = new NewsDto
            {
                Title = model.Title,
                Author = User.Identity.Name,
                Description = model.Description,
                ShortDescription = model.ShortDescription,
            };

            if (model.ImageFile != null)
                newsDto.Image = new ImageDto
                {
                    Blob = Base64Manager.ReadFully(model.ImageFile.InputStream),
                    MimeType = model.ImageFile.ContentType,
                    FileName = model.ImageFile.FileName,
                    Size = model.ImageFile.ContentLength
                };
            return newsDto;
        }

    }
}
