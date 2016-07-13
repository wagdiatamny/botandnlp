using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ChatterBotAPI;
using SearsIL.ShopYourWay.Framework.WebClient;

namespace WebApplication1.Controllers
{
	public class LandingController : Controller
	{


		[Route("/landing")]
		public ActionResult Landing ()
		{
			ChatterBotFactory factory = new ChatterBotFactory();
			ChatterBot bot1 = factory.Create(ChatterBotType.CLEVERBOT);
			ChatterBotSession bot1session = bot1.CreateSession();

			ChatterBot bot2 = factory.Create(ChatterBotType.PANDORABOTS, "b0dafd24ee35a477");
			ChatterBotSession bot2session = bot2.CreateSession();

			string s = "Hi, how are you?";
			while (true)
			{

				Console.WriteLine("bot1> " + s);

				s = bot2session.Think(s);
				Console.WriteLine("bot2> " + s);

				s = bot1session.Think(s);
			}
			return Content("ok");
		}
	}
}