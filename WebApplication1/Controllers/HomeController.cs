using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ChatterBotAPI;
using Newtonsoft.Json;
using System.IO;//File, Directory, Path

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public string Index(string s)
		{
			var webClient = GetWebClient();
			var answer = webClient.UploadValues(new Uri($"https://api.wit.ai/message?v=20160714&q={s}"), "POST", "");
			var serializedAnswer = JsonConvert.DeserializeObject<AiNlipResponse>(answer);
			var intent = GetIntent(serializedAnswer);
			var nlpResonse = SetResponseAccordingToIntent(intent, serializedAnswer);

			if (nlpResonse != null)
				return nlpResonse;

			ChatterBotFactory factory = new ChatterBotFactory();

			ChatterBot bot2 = factory.Create(ChatterBotType.PANDORABOTS, "b0dafd24ee35a477");
			ChatterBotSession bot2session = bot2.CreateSession();

			Console.WriteLine("bot1> " + s);

			Console.WriteLine("bot2> " + s);

			var s2 = bot2session.Think(s);

			return s2;
		}

		private string SetResponseAccordingToIntent(string intent, AiNlipResponse response)
		{
			if (intent == null)
				return null;
			switch (intent)
			{
				case "greeting":
					return ReturnGrettingMessage(response);
				case "seeking gift":
					return SeekingGift(response);
				case "age_detection":
					return AgeDetection();
				case "money_amount":
					return MoneyAcount();
			}
			return null;
		}

		private string MoneyAcount()
		{
			string statment = System.IO.File.ReadAllText(Server.MapPath("~/Views/statment.txt"));

			if (statment == "third")
			{
				System.IO.File.WriteAllText(Server.MapPath("~/Views/statment.txt"), "first");
				return "ok i will search a gift for your nience for the ammount of 50 dollars";
			}
			return null;
		}

		private string AgeDetection()
		{
			string statment = System.IO.File.ReadAllText(Server.MapPath("~/Views/statment.txt"));

			if (statment == "second")
			{
				System.IO.File.WriteAllText(Server.MapPath("~/Views/statment.txt"), "third");
				return "how much would you like to spend?";
			}

			return null;
		}

		private static string ReturnGrettingMessage(AiNlipResponse response)
		{
			if (response._text.Contains(" you doing")) return "Iam a robot, you pervert";
			return null;
		}

		private string SeekingGift(AiNlipResponse response)
		{
			var statment = System.IO.File.ReadAllText(Server.MapPath("~/Views/statment.txt"));

			if (statment == "" || statment == "first")
			{
				System.IO.File.WriteAllText(Server.MapPath("~/Views/statment.txt"),"second");
				return $"how old is your {response.entities.recipient.FirstOrDefault().value}?";
			}
			return null;
		}

		private string GetIntent(AiNlipResponse serializedAnswer)
		{
			return serializedAnswer.entities.intent?.FirstOrDefault()?.value;
		}

		public class HomeModel
		{
			public string Bot1Answer { get; set; }
			public string Bot2Answer { get; set; }

		}

		public class AiNlipResponse
		{
			public string _text { get; set; }
			public Entities entities { get; set; }

		}

		public class Entities
		{
			public IntentResponse[] intent { get; set; }
			public IntentResponse[] recipient { get; set; }

		}

		public class IntentResponse
		{
			public string value { get; set; }
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


}