using System;
using System.Web.Mvc;
using ChatterBotAPI;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public string Index(string s)
		{
			ChatterBotFactory factory = new ChatterBotFactory();
	
			ChatterBot bot2 = factory.Create(ChatterBotType.PANDORABOTS, "b0dafd24ee35a477");
			ChatterBotSession bot2session = bot2.CreateSession();

			Console.WriteLine("bot1> " + s);

			Console.WriteLine("bot2> " + s);

			var s2 = bot2session.Think(s);

			return s2;
		}

		public class HomeModel
		{
			public string Bot1Answer { get; set; }
			public string Bot2Answer { get; set; }

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