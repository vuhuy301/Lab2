using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System;
using ODataBookStore.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5235/odata/Books";
        }

        
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            string url = ProductApiUrl;
            if (!String.IsNullOrEmpty(searchString))
            {
                url += $"?$filter=contains(Title, '{searchString}')";
            }

            List<Book> allItems = new List<Book>();
            string nextLink = url;

            while (!string.IsNullOrEmpty(nextLink))
            {
                HttpResponseMessage response = await client.GetAsync(nextLink);
                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    dynamic temp = JObject.Parse(strData);
                    var lst = temp.value;

                    allItems.AddRange(((JArray)lst).Select(x => new Book
                    {
                        Id = (int)x["Id"],
                        Author = (string)x["Author"],
                        ISBN = (string)x["ISBN"],
                        Title = (string)x["Title"],
                        Price = (decimal)x["Price"],
                    }));

                    nextLink = temp["@odata.nextLink"]?.ToString();
                }
                else
                {
                    return View("Error");
                }
            }

            return View(allItems);
        }
    }
}
