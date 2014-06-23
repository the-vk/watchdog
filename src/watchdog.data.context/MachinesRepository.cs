using System;
using System.Linq;

using watchdog.data.model;

namespace watchdog.data.context
{
	class MachinesRepository : DbRepository<Machine>
	{
		public MachinesRepository(IDbContextInternal context) : base(context, "Machines")
		{
		}

		public override Machine Add(Machine @object)
		{
			var id = Query<int>(String.Format(@"
				set nocount on
				insert {0} (
					Name
				) values (
					@Name
				)
				select cast(scope_identity() as int)
			", TableName), @object).Single();
			@object.Id = id;
			return @object;
		}

		public override Machine Update(Machine @object)
		{
			Execute(String.Format(@"
				update {0} set 
					Name = @Name
				where Id = @Id
			", TableName), @object);
			return @object;
		}
	}
}
