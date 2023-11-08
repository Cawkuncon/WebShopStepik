﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(string id)
        {
            var products = ProductRepository.GetAll();
            var ID = -1;
            if (id != null)
            {
                if (int.TryParse(id, out var productId))
                {
                    ID = productId;
                }
            }
            var product = products.FirstOrDefault(pr => pr.Id == ID);

            return View(product);
        }
    }
}
