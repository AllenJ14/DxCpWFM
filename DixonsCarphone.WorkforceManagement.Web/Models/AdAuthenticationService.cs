using DixonsCarphone.WorkforceManagement.Business.Managers;
using DixonsCarphone.WorkforceManagement.Web.App_Start;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DixonsCarphone.WorkforceManagement.Web.Models
{
    public class AdAuthenticationService
    {
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager authenticationManager;
        private readonly IStoreManager storeManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
            this.storeManager = new StoreManager();
        }


        /// <summary>
        /// Check if username and password matches existing account in AD. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticationResult SignIn(String username, String password)
        {

            // authenticates against your Domain AD
            var authenticationType = ContextType.Domain;
            //string domain = "CPWPLC";

            if (username.StartsWith("DSG\\"))
            {
                //domain = "DSG";
                username = username.Replace("DSG\\", "");
            }

            var principalContext = new PrincipalContext(authenticationType, "CPWPLC");
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                
                if (isAuthenticated)
                {
                    userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                    System.Web.HttpContext.Current.Session.Add("_UserName", userPrincipal.SamAccountName);
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                userPrincipal = null;
            }

            if (!isAuthenticated)
            {
                principalContext = new PrincipalContext(authenticationType, "DSG");
                try
                {
                    isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                    if (isAuthenticated)
                    {
                        userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                        System.Web.HttpContext.Current.Session.Add("_UserName", userPrincipal.SamAccountName);
                    }
                }
                catch (Exception)
                {
                    isAuthenticated = false;
                    userPrincipal = null;
                }
            }

            if (!isAuthenticated || userPrincipal == null)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                // here can be a security related discussion weather it is worth 
                // revealing this information
                return new AuthenticationResult("Your account is disabled");
            }
            if(principalContext.Name != "DSG")
            {
                if (userPrincipal.IsMemberOf(principalContext, IdentityType.Name, ConfigurationManager.AppSettings["BranchManagerGroup"]) || userPrincipal.IsMemberOf(principalContext, IdentityType.Name, ConfigurationManager.AppSettings["IEBranchManagerGroup"]))
                {
                    HttpContext.Current.Session["_AccessLevel"] = "BM";
                }
            }
            else
            {
                //string[] server = principalContext.ConnectedServer.Split('.');
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + principalContext.ConnectedServer + "/" + userPrincipal.DistinguishedName, username, password);
                string test1 = entry.Properties["workforceID"].Value.ToString();
                try
                {
                    int authCheck = storeManager.CheckCPWCAuth(entry.Properties["workforceID"].Value.ToString());
                    if (authCheck == 1)
                    {
                        HttpContext.Current.Session["_AccessLevel"] = "BM";
                    }
                }
                catch (Exception)
                {
                }                
            }

            var identity = CreateIdentity(userPrincipal);

            authenticationManager.SignOut(CpwWfmAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            return new AuthenticationResult();
        }

        private static bool CheckRoles(PrincipalSearchResult<Principal> groups, string roles = null)
        {
            var ls = new List<GroupPrincipal>();

            var iterGroup = groups.GetEnumerator();
            using (iterGroup)
            {
            
                while (iterGroup.MoveNext())
                {
                    try
                    {
                        Principal p = iterGroup.Current;
                        ls.Add((GroupPrincipal)p);
                    }
                    catch (PrincipalOperationException)
                    {
                        continue;
                    }
                }
            }

            if (!string.IsNullOrEmpty(roles))
            {
                var rolesToBeIn = roles.Split(',');
                return ls.Any(x => rolesToBeIn.Contains(x.Name));
            }

            return true;
        }


        private static void StoreUserGroups(PrincipalSearchResult<Principal> groups)
        {
            var ls = new List<GroupPrincipal>();

            var iterGroup = groups.GetEnumerator();
            using (iterGroup)
            {
                while (iterGroup.MoveNext())
                {
                    try
                    {
                        Principal p = iterGroup.Current;
                        ls.Add((GroupPrincipal)p);
                    }
                    catch (PrincipalOperationException)
                    {
                        continue;
                    }
                }
            }

            HttpContext.Current.Session.Add("_UserGroups", ls);
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(CpwWfmAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie

            return identity;
        }
    }
}