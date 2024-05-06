using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity
{
    public class CountTask : BaseTask
    {
        public int maxCount { get; set; }
        public int Count { get; set; }

        [JsonConstructor]
        public CountTask() { }

    }
}
