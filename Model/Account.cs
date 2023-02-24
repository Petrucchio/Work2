using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work2.Model
{
    internal class Account
    {
        public IOrganizationService ServiceClient { get; set; }

        public string Logicalname { get; set; }

        public Account(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname = "account";
        }
        public Account(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.Logicalname = "account";
        }
        public Entity GetAccountById(Guid id, string[] collumns)
        {
            return ServiceClient.Retrieve(this.Logicalname, id, new ColumnSet(collumns));
        }
        public void IncrementOrDecrementNumberOfOpportunities(Entity opportunityAccount, bool? Increment)
        {
            int numberOfOpportunities = opportunityAccount.Contains("new_numerototaldeoportunidades") ? (int)opportunityAccount["new_numerototaldeoportunidades"] : 0;
            if (Convert.ToBoolean(Increment))
            {
                numberOfOpportunities += 1;
            }
            else
            {
                numberOfOpportunities -= 1;
            }
            opportunityAccount["new_numerototaldeoportunidades"] = numberOfOpportunities;
            ServiceClient.Update(opportunityAccount);
        }

    }
}
