using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ServiceLayerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayerApi.Controllers
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

            if (_context.Items.Count() == 0)
            {
                _context.Items.Add(new UsersData { Name = "Vadim", Surname ="Volkov", Age = 31, IsComplete = true });
                _context.SaveChanges();
            }
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
            var item = _context.Items.Find(id);

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
            var item = "Привет " + name;

            return item;
           
        }

        [HttpPut("Update/{id:long}")]
        public IActionResult Update(long id, UsersData item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var _data = _context.Items.Find(id);

            if (_data == null)
            {
                return NotFound();
            }

            _data.IsComplete = item.IsComplete;
            _data.Name = item.Name;

            _context.Items.Update(_data);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpDelete("Delete/{id:long}")]
        public IActionResult Delete(long id)
        {
            var _data = _context.Items.Find(id);

            if (_data == null)
            {
                return NotFound();
            }

            _context.Items.Remove(_data);
            _context.SaveChanges();

            return NoContent();
        }

    }
}