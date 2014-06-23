using System;
using System.Data;

using watchdog.data.model;

namespace watchdog.data.context
{
	public interface IDbContext : IDisposable
	{
		IDbRepository<Machine> Machines { get; }

		void BeginTransaction();
		void BeginTransaction(IsolationLevel isolationLevel);
		void CommitTransaction();
		void RollbackTransaction();
	}
}
