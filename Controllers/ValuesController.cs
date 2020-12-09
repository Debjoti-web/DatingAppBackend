using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
   // [Route("api/{controller}")]
   // [ApiController]
    public class ValuesController: ControllerBase
    {
         public readonly DataContext _context ;
        public ValuesController(DataContext context)
        {
            _context=context;
        }
        [Authorize]
        [HttpGet]
        [Route("api/v1/value/get")]
        public  async Task<IActionResult> Get()
        {
            //throw new System.Exception("jio");
            var t= await _context.Values.ToListAsync();
            return Ok(t);
        }
        [HttpGet]
        [Route("api/v1/value/getvalue")]
        public IActionResult Get([FromQuery]int id)
        {
            //throw new System.Exception("jio");
            var t=_context.Values.FirstOrDefault(x=>x.Id==id);
            return Ok(t);
        }
    }
}