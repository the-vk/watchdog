using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Dapper;

namespace watchdog.data.context
{
	abstract class DbRepository<T> : IDbRepository<T>
	{
		private readonly IDbContextInternal _ctx;

		public readonly string TableName;

		protected DbRepository(IDbContextInternal context, string tableName)
		{
			_ctx = context;
			TableName = tableName;
		}

		public IEnumerable<TResult> Query<TResult>(string sql, object parameter = null)
		{
			return _ctx.GetConnection().Query<TResult>(sql, parameter, _ctx.GetTransaction());
		}

		public void Execute(string sql, object parameter = null)
		{
			_ctx.GetConnection().Execute(sql, parameter, _ctx.GetTransaction());
		}

		public virtual T Get(int id)
		{
			return Query<T>(String.Format("select * from {0} where Id = @id", TableName), new {id}).FirstOrDefault();
		}

		public virtual IEnumerable<T> Get()
		{
			return Query<T>(String.Format("select * from {0}", TableName));
		}
		public abstract T Add(T @object);
		public abstract T Update(T @object);

		public virtual void Delete(int id)
		{
			Execute(String.Format("delete from {0} where Id = @Id", TableName), new {Id = id});
		}

		public virtual void Delete(T @object)
		{
			Execute(String.Format("delete from {0} where Id = @Id", TableName), @object);
		}
	}
}
