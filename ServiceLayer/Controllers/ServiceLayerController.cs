using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceLayerController : ControllerBase
    {
        private readonly ServiceContext _context;
        public ServiceLayerController(ServiceContext context)
        {
            _context = context;

            if (_context.Items.Any()) return;
            _context.Items.Add(new UsersData { Name = "Vadim", Surname ="Volkov", Age = 31, IsComplete = true });
            _context.SaveChanges();
        }
        [Route("GetAll")]
        [HttpGet]
        public ActionResult<List<UsersData>> GetAll()
        {
            return _context.Items.AsNoTracking().ToList();
        }


        [Route("GetById/{id:long}", Name = "GetAction")]
        [HttpGet]
        public ActionResult<UsersData> GetById(long id)
        {
            UsersData item = _context.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<UsersData> Create(UsersData item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAction", new { id = item.Id }, item);
        }
        [Route("About")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<string> About(string name)
        { 
            string item = "Привет " + name;
            return item;
           
        }

        [HttpPut("Update/{id:long}")]
        public IActionResult Update(long id, UsersData item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            UsersData data = _context.Items.Find(id);

            if (data == null)
            {
                return NotFound();
            }

            data.IsComplete = item.IsComplete;
            data.Name = item.Name;

            _context.Items.Update(data);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpDelete("Delete/{id:long}")]
        public IActionResult Delete(long id)
        {
            UsersData data = _context.Items.Find(id);

            if (data == null)
            {
                return NotFound();
            }

            _context.Items.Remove(data);
            _context.SaveChanges();

            return NoContent();
        }

    }
}