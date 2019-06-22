using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TafeGrandeGift.Data;
using TafeGrandeGift.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TafeGrandeGift.Controllers
{

    [Route("api/[controller]")]
    
    public class HamperApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HamperApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Hamper> Get()
        {
            return _context.Hamper.ToList();
        }

        //Get Hamper by Category
        [HttpGet("{category}")]
        public IEnumerable<Hamper> GetHamperByCategoty(string category)
        {
            var id = _context.Category.Where(n => n.CategoryName == category).Select(i => i.CategoryId).FirstOrDefault();
            return _context.Hamper.Where(n => n.CategoryId == id).ToList();
        }
        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
