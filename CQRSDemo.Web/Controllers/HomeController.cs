using CQRSFramework;
using CQRSFramework.Commands;
using CQRSFramework.Exceptions;
using CQRSFramework.Services;
using System;
using System.Web.Mvc;
using CQRSFramework.Services.ApplicationServices;

namespace CQRSDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        // 查询部分
        public ActionResult Index()
        {
            // 直接获得QueryDatabase对象来查询所有日志
            var model = ServiceLocator.QueryStorage.GetItems();
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var item = ServiceLocator.QueryStorage.GetById(id);
            // 发布DeleteCommand命令
            ServiceLocator.CommandBus.Send(new DeleteItemCommand(item.Id, item.Version));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(DiaryItemDto item)
        {
            // 发布CreateItemCommand到CommandBus中
            ServiceLocator.CommandBus.Send(new CreateItemCommand(Guid.NewGuid(), item.Title, item.Description, -1, item.From, item.To));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var item = ServiceLocator.QueryStorage.GetById(id);
            var model = new DiaryItemDto()
            {
                Description = item.Description,
                From = item.From,
                Id = item.Id,
                Title = item.Title,
                To = item.To,
                Version = item.Version
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DiaryItemDto item)
        {
            try
            {
                ServiceLocator.CommandBus.Send(new ChangeItemCommand(item.Id, item.Title, item.Description, item.From, item.To, item.Version));
            }
            catch (ConcurrencyException err)
            {

                ViewBag.error = err.Message;
                ModelState.AddModelError("", err.Message);
                return View();

            }

            return RedirectToAction("Index");
        }
    }
}
