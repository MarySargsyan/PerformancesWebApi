using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformancesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformancesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersGroupsController : ControllerBase
    {
        GroupsContext _context;

        public ParametersGroupsController(GroupsContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PGWithNames>>> Get(int id)
        {
            List<PGWithNames> newlist = new List<PGWithNames>();
            foreach (ParametersGroup paramGroup in _context.ParametersGroups.Where(pg => pg.GroupId == id).ToList())
            {
                PGWithNames pG = new PGWithNames();
                pG.ParameterName =_context.Parameters.Find(paramGroup.ParameterId).Name;
                pG.mark = paramGroup.Mark;
                newlist.Add(pG);
            
            }
            return newlist;
        }
        public class PGWithNames
        {
            public string ParameterName { get; set; }
            public double mark { get; set; }

        }
    }
}
