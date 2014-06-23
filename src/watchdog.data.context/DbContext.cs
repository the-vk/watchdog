using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using watchdog.data.model;

namespace watchdog.data.context
{
    public class DbContext : IDbContextInternal
    {
	    private IDbConnection _connection;
	    private IDbTransaction _transaction;

	    public DbContext(string connectionStringName)
	    {
		    if (String.IsNullOrEmpty(connectionStringName))
		    {
			    throw new ArgumentNullException("connectionStringName");
		    }
		    var dbConnectionConfiguration = ConfigurationManager.ConnectionStrings[connectionStringName];
		    if (dbConnectionConfiguration == null)
		    {
			    throw new ArgumentException("Connection string was not found.");
		    }
		    var dbProviderFactory = DbProviderFactories.GetFactory(dbConnectionConfiguration.ProviderName);
		    _connection = dbProviderFactory.CreateConnection();
			//TODO: change to a more specific exception class
			if (_connection == null) throw new Exception("Failed to create db connection.");
		    _connection.ConnectionString = dbConnectionConfiguration.ConnectionString;

			Machines = new MachinesRepository(this);
	    }

	    public DbContext(IDbConnection connection)
	    {
		    if (_connection == null)
		    {
			    throw new ArgumentNullException("connection");
		    }
		    _connection = connection;
			Machines = new MachinesRepository(this);
	    }

	    public void Dispose()
	    {
		    Dispose(true);
	    }

	    protected virtual void Dispose(bool disposing)
	    {
		    if (_connection != null)
		    {
			    _connection.Dispose();
			    _connection = null;
		    }
		    if (_transaction != null)
		    {
			    RollbackTransaction();
		    }
	    }

	    public IDbRepository<Machine> Machines { get; private set; }

	    public void BeginTransaction()
	    {
		    BeginTransaction(IsolationLevel.ReadCommitted);
	    }

	    public void BeginTransaction(IsolationLevel isolationLevel)
	    {
		    if (_transaction != null)
		    {
			    throw new InvalidOperationException("There is another transaction in progress associated with db context.");
		    }
			if (_connection.State != ConnectionState.Open) _connection.Open();
		    _transaction = _connection.BeginTransaction(isolationLevel);
	    }

	    public void CommitTransaction()
	    {
		    if (_transaction == null)
		    {
				//TODO: implement warning log here
			    return;
		    }
			_transaction.Commit();
			_transaction.Dispose();
		    _transaction = null;
	    }

	    public void RollbackTransaction()
	    {
		    if (_transaction == null)
		    {
			    //TODO: implement warning log here
			    return;
		    }
			_transaction.Rollback();
			_transaction.Dispose();
		    _transaction = null;
	    }

	    public IDbConnection GetConnection()
	    {
		    return _connection;
	    }

	    public IDbTransaction GetTransaction()
	    {
		    return _transaction;
	    }
    }
}
