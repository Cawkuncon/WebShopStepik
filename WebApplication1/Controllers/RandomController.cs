using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RandomController : Controller
	{
		private readonly ICounter randomCounter;
		private readonly CounterService counterService;

		public RandomController(ICounter randomCounter, CounterService counterService)
        {
			this.randomCounter = randomCounter;
			this.counterService = counterService;
		}
        public string Index()
		{
			return $"randomCounter.Value = {randomCounter.Value}, counterSevice.Counter.Value = {counterService.Counter.Value}";
		}
	}
}
