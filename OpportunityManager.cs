using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work2.Controller;

namespace Work2
{
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            //quando precisar fazer conexão com outro dynamics vou suar isso:
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity opportunity = new Entity();
            bool? Increment = null;
            SetVariables(context, out opportunity, out Increment);
            ExecuteOpportunityProcess(context, tracingService, opportunity, Increment);
        }

        private void ExecuteOpportunityProcess(IPluginExecutionContext context, ITracingService tracingService, Entity opportunity, bool? Increment)
        {
            EntityReference accountReference = opportunity.Contains("parentaccountid") ? (EntityReference)opportunity["parentaccountid"] : null; //Referencia o campo de conta, se houver algum ele o referencia, se não, ele é nulo

            tracingService.Trace("Iniciou processo de Plugin");
            if (accountReference != null)
            {
                tracingService.Trace("Referencia de conta encontrada");
                Entity opportunityAccount = UpdateAccount(Increment, accountReference);
                tracingService.Trace("Conta atualizada");

                if (context.MessageName == "Update")
                {
                    Entity opportunityPostImage = (Entity)context.PostEntityImages["PostImage"];
                    EntityReference postAccountReference = (EntityReference)opportunityPostImage["parentaccountid"];
                    UpdateAccount(true, postAccountReference);
                }
            }
        }

        private Entity UpdateAccount(bool? Increment, EntityReference accountReference)
        {
            AccountController accountController = new AccountController(this.service);
            Entity opportunityAccount = accountController.GetAccountById(accountReference.Id, new string[] { "new_numerototaldeoportunidades" });
            accountController.IncrementOrDecrementNumberOfOpportunities(opportunityAccount, Increment);
            return opportunityAccount;
        }

        private void SetVariables(IPluginExecutionContext context, out Entity opportunity, out bool? Increment)
        {
            if (context.MessageName == "Create")
            {
                opportunity = (Entity)context.InputParameters["Target"];
                Increment = true;
            }
            else
            {
                opportunity = (Entity)context.PreEntityImages["PreImage"];
                Increment = false;
            }
        }

    }
}
