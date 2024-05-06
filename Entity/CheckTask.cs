using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity
{
    public class CheckTask : BaseTask
    {
        public bool IsCompleted { get; set; }

        [JsonConstructor]
        public CheckTask() { }
    }
}
