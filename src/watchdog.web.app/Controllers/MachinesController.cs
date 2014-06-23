using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using watchdog.data.context;
using watchdog.data.model;

namespace watchdog.web.app.Controllers
{
    public class MachinesController : ApiController
    {
	    private readonly IDbContext _ctx;

	    public MachinesController(IDbContext ctx)
	    {
		    _ctx = ctx;
	    }

        // GET api/machines
        public IEnumerable<Machine> Get()
        {
	        return _ctx.Machines.Get();
        }

        // GET api/machines/5
        public Machine Get(int id)
        {
            return _ctx.Machines.Get(id);
        }

        // POST api/machines
        public void Post([FromBody]Machine value)
        {
			_ctx.BeginTransaction();
			_ctx.Machines.Add(value);
	        _ctx.CommitTransaction();
        }

        // PUT api/machines/5
        public void Put(int id, [FromBody]Machine value)
        {
	        value.Id = id;
			_ctx.BeginTransaction();
			_ctx.Machines.Update(value);
			_ctx.CommitTransaction();
        }

        // DELETE api/machines/5
        public void Delete(int id)
        {
			_ctx.BeginTransaction();
			_ctx.Machines.Delete(id);
			_ctx.CommitTransaction();
        }
    }
}
