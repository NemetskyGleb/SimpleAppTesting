using Microsoft.AspNetCore.Mvc;
using Testing.Models;

namespace Testing.Controllers;

public class HomeController : Controller 
{
    public ViewResult Index()
    {
        return View(Product.GetProducts());
    }
}