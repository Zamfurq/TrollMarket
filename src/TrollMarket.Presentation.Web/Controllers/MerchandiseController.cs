using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    [Route("Merchandise")]
    public class MerchandiseController : Controller
    {
        private readonly MerchandiseService _service;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MerchandiseController(MerchandiseService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("TrollMarketConnection");
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            DataTable merchs = new DataTable();
            int pageSize = 10;
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetProductFromSeller", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SellerName", username);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(merchs);
                    }
                }
            }

            
            var vm = new MerchIndexViewModel
            {
                Merchs = merchs
            };
            return View(vm);
        }

        [HttpGet("Insert")]
        public IActionResult Insert() {
            return View("Upsert");
        }

        [HttpPost("Insert")]
        public IActionResult Insert(MerchViewModel vm) {
            
            if (!ModelState.IsValid)
            {
                vm.SellerName = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
                return View("Upsert", vm);
            }
            _service.Insert(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("Update/{id}")]
        public IActionResult Update(int id)
        {
            MerchViewModel vm = _service.GetMerchById(id);
            return View("Upsert",vm);
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update(MerchViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.SellerName = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
                return View("Upsert", vm);
            }
            _service.Update(vm);
            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cart = _service.GetCart(id);
            var transaction = _service.GetTransactions(id);
            if (cart.Count != 0 || transaction.Count != 0)
            {
                return View("Error");
            }
            _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("Discontinue/{id}")]
        public IActionResult Discontinued(int id)
        {
            _service.Discontinue(id);
            return RedirectToAction("Index");
        }

        
    }
}
