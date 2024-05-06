using DAL.Repositories;
using Entity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly IMongoDatabase _database;
        private bool disposed = false;
        private Context _context;


        private BaseRepository<CheckTask> _checkTaskRepository;
        private BaseRepository<CountTask> _countTaskRepository;
        private BaseRepository<Event> _eventRepository;
        public UnitOfWork(IConfiguration configuration, string databaseName)
        {
            _context = new Context(configuration.GetConnectionString("MongoDB"), databaseName);
        }

        public BaseRepository<CheckTask> CheckTaskRepository 
        {  
            get 
            {
                if(this._checkTaskRepository == null)
                {
                    _checkTaskRepository = new BaseRepository<CheckTask>(_context, "tasks");
                }
                return _checkTaskRepository;
            }
        }

        public BaseRepository<CountTask> CountTaskRepository
        {
            get
            {
                if(this._countTaskRepository == null)
                {
                    _countTaskRepository = new BaseRepository<CountTask>(_context, "tasks");
                }
                return _countTaskRepository;
            }
        }

        public BaseRepository<Event> EventRepository
        {
            get
            {
                if (this._eventRepository == null)
                {
                    _eventRepository = new BaseRepository<Event>(_context, "events");
                }
                return _eventRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    /*_database.Client.Cluster.Dispose();*/
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
