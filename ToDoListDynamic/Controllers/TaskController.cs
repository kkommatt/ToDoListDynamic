using Microsoft.AspNetCore.Mvc;

namespace ToDoListDynamic.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            var tasksCookie = Request.Cookies["tasks"];
            var tasks = tasksCookie != null ? tasksCookie.Split(',') : Array.Empty<string>();

            return View("Index", tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string taskName)
        {
            var tasksCookie = Request.Cookies["tasks"];

            tasksCookie = string.IsNullOrEmpty(tasksCookie) ? taskName : $"{tasksCookie},{taskName}";

            Response.Cookies.Append("tasks", tasksCookie, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7)
            });

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult DeleteTask()
        {
            Response.Cookies.Delete("tasks");

            return Json(new { success = true });
        }
    }
}
