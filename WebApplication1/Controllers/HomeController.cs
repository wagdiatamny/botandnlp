using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChatterBotAPI;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public string Index(string s)
		{
			var webClient = GetWebClient();
			var answer = webClient.UploadValues(new Uri($"https://api.wit.ai/message?v=20160714&q={s}"), "POST", "");
			var serializedAnswer = JsonConvert.DeserializeObject<AiNlipResponse>(answer);
			if (serializedAnswer.intent.value == "greeting" && serializedAnswer._text.Contains(" you doing"))
				return "Iam a robot, you pervert";
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

		public class AiNlipResponse
		{
			public string _text { get; set; }
			public IntentResponse intent{ get; set; }

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

		private IWebClient GetWebClient()
		{
			var webClient = new WebClient();
			webClient.CustomizeRequest =
				request =>
				{
					request.ContentType = "application/json";
					request.Headers["Authorization"] = "Bearer HQ6KPDQXFCBYUQIB54ZPNNWHJ5VEA5N5";
				};

			return webClient;
		}
	}

	public class IntentResponse
	{
		public string value { get; set; }
	}
}