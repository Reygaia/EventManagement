using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Event
{
    public class EventUser
    {
        public Guid UserId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public List<int>? RolesId { get; set; } = new List<int>();
    }
}
