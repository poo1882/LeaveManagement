using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Models
{
    public class UserInfoModel
    {


        
            public string kind { get; set; }
            public string id { get; set; }
            public string etag { get; set; }
            public string primaryEmail { get; set; }
            public Name name { get; set; }
            public bool isAdmin { get; set; }
            public bool isDelegatedAdmin { get; set; }
            public DateTime lastLoginTime { get; set; }
            public DateTime creationTime { get; set; }
            public bool agreedToTerms { get; set; }
            public bool suspended { get; set; }
            public bool changePasswordAtNextLogin { get; set; }
            public bool ipWhitelisted { get; set; }
            public Email[] emails { get; set; }
            public Externalid[] externalIds { get; set; }
            public Relation[] relations { get; set; }
            public Organization[] organizations { get; set; }
            public string[] nonEditableAliases { get; set; }
            public string customerId { get; set; }
            public string orgUnitPath { get; set; }
            public bool isMailboxSetup { get; set; }
            public bool isEnrolledIn2Sv { get; set; }
            public bool isEnforcedIn2Sv { get; set; }
            public bool includeInGlobalAddressList { get; set; }
        

        public class Name
        {
            public string givenName { get; set; }
            public string familyName { get; set; }
            public string fullName { get; set; }
        }

        public class Email
        {
            public string address { get; set; }
            public bool primary { get; set; }
        }

        public class Externalid
        {
            public string value { get; set; }
            public string type { get; set; }
        }

        public class Relation
        {
            public string value { get; set; }
            public string type { get; set; }
        }

        public class Organization
        {
            public bool primary { get; set; }
            public string customType { get; set; }
            public string department { get; set; }
            public string description { get; set; }
        }

    }
}
