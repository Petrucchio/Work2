using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work2.Model;

namespace Work2.Controller
{
    internal class AccountController
    {
        public IOrganizationService ServiceClient { get; set; }
        public Account Conta { get; set; }

        public AccountController(IOrganizationService crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Account(ServiceClient);
        }
        public AccountController(CrmServiceClient crmServiceCliente)
        {
            ServiceClient = crmServiceCliente;
            this.Conta = new Account(ServiceClient);
        }
        public Entity GetAccountById(Guid id, string[] collumns)
        {
            return Conta.GetAccountById(id, collumns);
        }
        public void IncrementOrDecrementNumberOfOpportunities(Entity opportunityAccount, bool? Increment)
        {
            Conta.IncrementOrDecrementNumberOfOpportunities(opportunityAccount, Increment);
        }


    }
}
