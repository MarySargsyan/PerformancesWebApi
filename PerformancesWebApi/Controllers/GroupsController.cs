using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformancesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformancesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
       GroupsContext _context;

        public GroupsController(GroupsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupsWithDepNames>>> Get()
        {
            List<GroupsWithDepNames> groups = new List<GroupsWithDepNames>();
            foreach (Groups group in _context.Groups.Include(g=> g.Department).ToList())
            {
                GroupsWithDepNames g = new GroupsWithDepNames();
                g.id = group.id;
                g.Department = group.Department.Name;
                g.Group = group.Name;
                groups.Add(g);

            }
            return groups;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {         
            Groups group = await _context.Groups.FirstOrDefaultAsync(x => x.id == id);
            if (group == null)
                return NotFound();

            return new ObjectResult(group);
        }
        public class GroupsWithDepNames
        {
            public int id { get; set; }
            public string Department { get; set; }
            public string Group { get; set; }

        }
    }
}
