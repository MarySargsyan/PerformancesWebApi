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
    public class UsersController : ControllerBase
    {
        GroupsContext _context;

        public UsersController(GroupsContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserNames>>> Get(int id)
        {
            List<UserNames> userNames = new List<UserNames>();
            Groups groups = _context.Groups.Find(id);
            Departments departments = _context.Departments.Find(groups.DepartmentId);

            foreach( UserParamEval userParamEval in _context.UserParamEval.Where(upe=> upe.User.DepartmentId == departments.Id).ToList())
            {
                foreach(ParametersGroup parametersGroup in _context.ParametersGroups.Where(pg=> pg.GroupId == groups.id).ToList())
                {
                   if(userParamEval.ParameterId == parametersGroup.ParameterId & userParamEval.Mark >= parametersGroup.Mark)
                    {
                       UserNames user = new UserNames
                       {
                          Name = _context.User.Find(userParamEval.UserId).Name,
                          SourName = _context.User.Find(userParamEval.UserId).SourName
                       };
                       userNames.Add(user);
                    }

                }
            }
            return userNames;
        }
        public class UserNames
        {
            public string Name { get; set; }
            public string SourName { get; set; }

        }
    }
}