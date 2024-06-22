using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsSetup.Policy
{
    public class MultiplePoliciesRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Policies { get; }

        public MultiplePoliciesRequirement(params string[] policies)
        {
            Policies = policies ?? throw new ArgumentNullException(nameof(policies));
        }
    }
}
