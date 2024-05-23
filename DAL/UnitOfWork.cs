using DAL.Repositories;
using Entity.Chat;
using Entity.Event;
using Entity.Identity;
using Entity.Task;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

        private BaseRepository<BaseTask> _baseTaskRepository;
        private BaseRepository<ContentCreationTask> _contentCreationTaskRepository;
        private BaseRepository<LogisticsTask> _logisticsTaskRepository;
        private BaseRepository<MarketingTask> _marketingTaskRepository;
        private BaseRepository<PlanningTask> _planningTaskRepository;
        private BaseRepository<QuotaTask> _quotaTaskRepository;
        private BaseRepository<RegistrationTask> _registrationTaskRepository;
        private BaseRepository<VendorCoordinationTask> _vendorCoordinationTaskRepository;

        private BaseRepository<Event> _eventRepository;
        private BaseRepository<EventRole> _eventRoleRepository;
        private BaseRepository<Phase> _phaseRepository;
        private BaseRepository<ChatBox> _chatBoxRepository;
        private BaseRepository<ApplicationMessage> _messageRepository;

        private BaseRepository<ApplicationUser> _userRepository;
        public UnitOfWork(IConfiguration configuration, string databaseName)
        {
            _context = new Context(configuration.GetConnectionString("MongoDB"), databaseName);
        }
        #region task
        public BaseRepository<BaseTask> BaseTaskRepository
        {
            get
            {
                if (this._baseTaskRepository == null)
                {
                    _baseTaskRepository = new BaseRepository<BaseTask>(_context, "tasks");
                }
                return _baseTaskRepository;
            }
        }
        public BaseRepository<ContentCreationTask> ContentCreationTaskRepository
        {
            get
            {
                if (this._contentCreationTaskRepository == null)
                {
                    _contentCreationTaskRepository = new BaseRepository<ContentCreationTask>(_context, "tasks");
                }
                return _contentCreationTaskRepository;
            }
        }
        public BaseRepository<LogisticsTask> LogisticsTaskRepository
        {
            get
            {
                if (this._logisticsTaskRepository == null)
                {
                    _logisticsTaskRepository = new BaseRepository<LogisticsTask>(_context, "tasks");
                }
                return _logisticsTaskRepository;
            }
        }
        public BaseRepository<MarketingTask> MarketingTaskRepository
        {
            get
            {
                if (this._marketingTaskRepository == null)
                {
                    _marketingTaskRepository = new BaseRepository<MarketingTask>(_context, "tasks");
                }
                return _marketingTaskRepository;
            }
        }
        public BaseRepository<PlanningTask> PlanningTaskRepository
        {
            get
            {
                if (this._planningTaskRepository == null)
                {
                    _planningTaskRepository = new BaseRepository<PlanningTask>(_context, "tasks");
                }
                return _planningTaskRepository;
            }
        }
        public BaseRepository<QuotaTask> QuotaTaskRepository
        {
            get
            {
                if (this._quotaTaskRepository == null)
                {
                    _quotaTaskRepository = new BaseRepository<QuotaTask>(_context, "tasks");
                }
                return _quotaTaskRepository;
            }
        }
        public BaseRepository<RegistrationTask> RegistrationTaskRepository
        {
            get
            {
                if (this._registrationTaskRepository == null)
                {
                    _registrationTaskRepository = new BaseRepository<RegistrationTask>(_context, "tasks");
                }
                return _registrationTaskRepository;
            }
        }
        public BaseRepository<VendorCoordinationTask> VendorCoordinationTaskRepository
        {
            get
            {
                if (this._vendorCoordinationTaskRepository == null)
                {
                    _vendorCoordinationTaskRepository = new BaseRepository<VendorCoordinationTask>(_context, "tasks");
                }
                return _vendorCoordinationTaskRepository;
            }
        }
        #endregion

        #region event
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

        public BaseRepository<EventRole> EventRoleRepository
        {
            get
            {
                if (this._eventRoleRepository == null)
                {
                    _eventRoleRepository = new BaseRepository<EventRole>(_context, "eventroles");
                }
                return _eventRoleRepository;
            }
        }

        public BaseRepository<Phase> PhaseRepository
        {
            get
            {
                if (this._phaseRepository == null)
                {
                    _phaseRepository = new BaseRepository<Phase>(_context, "phases");
                }
                return _phaseRepository;
            }
        }
        #endregion

        #region chat
        public BaseRepository<ChatBox> ChatBoxRepository
        {
            get
            {
                if (this._chatBoxRepository == null)
                {
                    _chatBoxRepository = new BaseRepository<ChatBox>(_context, "chatboxes");
                }
                return _chatBoxRepository;
            }
        }

        public BaseRepository<ApplicationMessage> MessageRepository
        {
            get
            {
                if (this._messageRepository == null)
                {
                    _messageRepository = new BaseRepository<ApplicationMessage>(_context, "messages");
                }
                return _messageRepository;
            }
        }
        #endregion

        public BaseRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    _userRepository = new BaseRepository<ApplicationUser>(_context, "users");
                }
                return _userRepository;
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
