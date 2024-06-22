using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsSetup.Policy
{
    public class MultiplePoliciesHandler : AuthorizationHandler<MultiplePoliciesRequirement>
    {
        private readonly IServiceProvider _serviceProvider;

        public MultiplePoliciesHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MultiplePoliciesRequirement requirement)
        {
            var authorizationService = _serviceProvider.GetRequiredService<IAuthorizationService>();

            foreach (var policy in requirement.Policies)
            {
                var result = await authorizationService.AuthorizeAsync(context.User, policy);
                if (result.Succeeded)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }
    }
}
