using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//usefull crm libaries
using Microsoft.Xrm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
//framework libaries
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Configuration;

namespace crmExe
{
    class Program
    {
        static void Main(string[] args)
        {
            //read the organization address from config
            string s = ConfigurationSettings.AppSettings["orgName"];
            Uri orgURI = new Uri(s);
            //thr user context for woriking with service
            ClientCredentials credentials = new ClientCredentials();
            //מריץ תחת היוזר שלי
            credentials.Windows.ClientCredential = (NetworkCredential)CredentialCache.DefaultCredentials;

            using (OrganizationServiceProxy _proxy = new OrganizationServiceProxy(orgURI,null, credentials, null)){
                IOrganizationService service = (IOrganizationService)_proxy;
                //get data from record

                //מציג jobtitle  מישות account
                //Entity entity= service.Retrieve("contact", new Guid("89EBB5DF-6C38-E611-80C9-000C29251C6C"),
                //    new ColumnSet(new string[] {"jobtitle", "emailaddress1" }));
                //Console.WriteLine("jobtitle:" + entity["jobtitle"]);
                //Console.ReadLine ("jobtitle:" + entity["jobtitle"]);

                //הצג שדות topic וphone עבור marya
                #region 2
                //Entity entity = service.Retrieve("lead", new Guid("ABECB5DF-6C38-E611-80C9-000C29251C6C"),
                //     new ColumnSet(new string[] { "subject", "telephone1" }));
                // Console.WriteLine("topic:" + entity["subject"]+ "\n"+ "Business Phone:" + entity["telephone1"]);
                // entity["subject"] = "1111";
                // service.Update(entity);
                // Console.ReadLine();
                #endregion

                #region 3
                //עדכון שדה jobtitle ל"cto" עבור אותו contact שמופיע באותו שדה עבור primary contact שלו.

                //Entity entity = service.Retrieve("account", new Guid("2BEBB5DF-6C38-E611-80C9-000C29251C6C"),
                //    new ColumnSet(new string[] { "primarycontactid" }));
                //EntityReference er = (EntityReference)entity["jobtitle"];                
                //EntityReference updateER = new EntityReference();               
                //er.LogicalName = "contact";
                //entity["jobtitle"] = "cto";
                //er.Id =new Guid ("95EBB5DF-6C38-E611-80C9-000C29251C6C") ;
                //entity["contact"] = updateER;
                //service.Update(entity);
                #endregion

                #region 4

                //Entity ent = new Entity("contact");
                //ent["lastname"]="katz";
                //ent["firstname"]="sarit";
                //ent["emailaddress1"] = "test@gmail";
                //Entity entity = service.Retrieve("account", new Guid("2BEBB5DF-6C38-E611-80C9-000C29251C6C"),
                //    new ColumnSet(new string[] { "primarycontactid" }));
                //EntityReference er = (EntityReference)entity["parentcustomerid"];
                //EntityReference updateER = new EntityReference();
                //er.LogicalName = "account";
                //er.Id = new Guid("2DEBB5DF-6C38-E611-80C9-000C29251C6C");

                #endregion

                #region 6
                //service.Create(ent);

                // שולף את כל התיקי לקוחות שהמספר טלפון שלהם מתחיל ב555
                //Entity ent = new Entity("account");
                //OrganizationServiceContext context = new OrganizationServiceContext(service);
                //var result = from acc in context.CreateQuery("account")
                //             where((string)acc["telephone1"]).StartsWith("555")
                //             select acc;

                //    foreach (var item in result)
                //{
                //     ent["telephone1"] = "111";

                //    context.UpdateObject(item);
                //    // Console.WriteLine(item["telephone1"]);
                //}
                //context.SaveChanges();
                //Console.ReadLine();

                #endregion

                #region 7
                //

                // OrganizationServiceContext context = new OrganizationServiceContext(service);
                // var result = from acc in context.CreateQuery("opportunity")
                //              where ((string)acc["name"]).Contains("Product")|| ((string)acc["name"]).Contains("sell")
                //              select acc;

                // foreach (var item in result)
                // {
                //    // ent["telephone1"] = "111";

                //    // context.UpdateObject(item);
                //     Console.WriteLine(item["name"]);
                // }
                //// context.SaveChanges();


                #endregion

                #region 8
                //QueryExpression eq = new QueryExpression("opportunity");
                //eq.ColumnSet = new ColumnSet("subject");
                //LinkEntity le = new LinkEntity();
                //le.LinkFromEntityName = "contact";
                //le.LinkFromAttributeName = "parentcontctid";
                //le.LinkToAttributeName = "opportunity";
                //le.LinkToAttributeName = "opportunityid";
                //le.JoinOperator = JoinOperator.Inner;
                //le.Columns = new ColumnSet("telephone1");
                //le.EntityAlias = "opportunity_contact";

                //ConditionExpression cond = new ConditionExpression("telephone1",ConditionOperator.EndsWith,"7");

                //LinkEntity l = new LinkEntity();
                //l.LinkFromEntityName = "account";
                //l.LinkFromAttributeName = "parentaccountid";
                //l.LinkToAttributeName = "opportunity";
                //l.LinkToAttributeName = "opportunityid";
                //l.JoinOperator = JoinOperator.Inner;
                //l.Columns = new ColumnSet("telephone");
                //l.EntityAlias = "opportunity_account";

                //ConditionExpression con = new ConditionExpression("telephone", ConditionOperator.Contains, "01");


                //FilterExpression filter = new FilterExpression();
                //filter.Conditions.Add(cond);
                //le.LinkCriteria.AddFilter(filter);
                //eq.LinkEntities.Add(le);
                //EntityCollection ent = service.RetrieveMultiple(eq);
                //foreach (var item in ent.Entities)
                //{
                //    Console.WriteLine(item["name"]);
                //}

                #endregion
                #region 9
                

                #endregion

                Console.ReadLine();

               
            }

        }
    }
}
