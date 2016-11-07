
using System.Text;
//using System.ServiceModel.Description;
//using System.ServiceModel;
using System.Net;

//using Microsoft.Xrm;
//using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Messages;
//using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Sdk.Client;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
//using Microsoft.Crm.Sdk.Messages;
using System;
using Microsoft.Xrm.Sdk.Query;

namespace plugins
{
    public class pre_validation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {


            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));


            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {

                Entity entity = (Entity)context.InputParameters["Target"];


                if (entity.LogicalName != "contact")
                {
                    return;
                }


                entity["new_all_phones"] = entity["telephone1"] + "," + entity["mobilephone"];

                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);
                Entity ent = new Entity("contact");
                Entity entity = service.Retrieve("account", new Guid("2BEBB5DF-6C38-E611-80C9-000C29251C6C"),
                    new ColumnSet(new string[] { "primarycontactid" }));
                EntityReference er = (EntityReference)entity["parentcustomerid"];
                EntityReference updateER = new EntityReference();
                er.LogicalName = "account";
                er.Id = new Guid("2DEBB5DF-6C38-E611-80C9-000C29251C6C");
                if




            }

        }

    }
}

