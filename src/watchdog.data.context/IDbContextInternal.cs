using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watchdog.data.context
{
	interface IDbContextInternal : IDbContext
	{
		IDbConnection GetConnection();
		IDbTransaction GetTransaction();
	}
}
