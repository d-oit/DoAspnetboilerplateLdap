# Sample - AD / OpenLdap

This sample use Novell.Directory.Ldap for the ladp connection as additional option.

Check out https://github.com/dotnet/runtime/issues/23944 - atm .net core 3 has no 
Support System.DirectoryServices.Protocols on Linux/Mac

The sample use the OpenLdap from
https://www.forumsys.com/tutorials/integration-how-to/ldap/online-ldap-test-server/ 

Check out Ldap Queries with:
https://directory.apache.org/studio/

The Difference Between Active Directory and LDAP
https://www.varonis.com/blog/the-difference-between-active-directory-and-ldap/


## Setting

**DefaultSettingsCreator.cs**  

 // Ldap / Ad
AddSettingIfNotExists(LdapSettingNames.IsEnabled, "true", tenantId);
 
 For your AD uncomment and setup your domain:
 // *** AD ***
            // Setup the domain 
            //AddSettingIfNotExists(LdapSettingNames.UserName, "AD_Administrator", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.Password, "password", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.ContextType, "Domain.Context", tenantId);
            //AddSettingIfNotExists(LdapSettingNames.Domain, "test.local");

Note: If you don't define a domain, username and password, LDAP authentication works for the current domain if your application runs in a domain with appropriate privileges.
https://aspnetboilerplate.com/Pages/Documents/Zero/User-Management#settings


// *** LDAP Sever ***

//https://www.forumsys.com/tutorials/integration-how-to/ldap/online-ldap-test-server/

AddSettingIfNotExists(LdapSettingNames.UseNovellLdap, "true", tenantId);
AddSettingIfNotExists(LdapSettingNames.LdapServer, "ldap.forumsys.com", tenantId);
// Ldap admin user
AddSettingIfNotExists(LdapSettingNames.LdapLoginDn, "cn=read-only-admin,dc=example,dc=com", tenantId);
AddSettingIfNotExists(LdapSettingNames.Password, "password", tenantId);

AddSettingIfNotExists(LdapSettingNames.LdapSearchBaseDn, "dc=example,dc=com", tenantId);

**AppLdapAuthenticationSource.cs**

TryAuthenticateAsync check against the server. If the user existis an the server CreateUser or UpdateUser are called


```csharp
public override async Task<User> CreateUserAsync(string userNameOrEmailAddress, Tenant tenant)
{
    if(await _settings.GetUseNovellLdap(tenant?.Id))
    {
        var user = await base.CreateUserAsync(userNameOrEmailAddress, tenant);
        UpdateUser(user);
        return user;
    }

    return await base.CreateUserAsync(userNameOrEmailAddress, tenant);
}
```

 // Update IdentityUser with attributes from  LdapEntries
protected void UpdateUser(User user)

Customize the methode with your requirements 


## Run the test

Run the mvc project and test with:

**User:** riemann

Any password.


# Important

Issues of this repository are tracked on https://github.com/aspnetboilerplate/aspnetboilerplate. Please create your issues on https://github.com/aspnetboilerplate/aspnetboilerplate/issues.

# Introduction

This is a template to create **ASP.NET Core MVC / Angular** based startup projects for [ASP.NET Boilerplate](https://aspnetboilerplate.com/Pages/Documents). It has 2 different versions:

1. [ASP.NET Core MVC & jQuery](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core) (server rendered multi-page application).
2. [ASP.NET Core & Angular](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular) (single page application).
 
User Interface is based on [BSB Admin theme](https://github.com/gurayyarar/AdminBSBMaterialDesign).
 
# Download

Create & download your project from https://aspnetboilerplate.com/Templates

# Screenshots

#### Sample Dashboard Page
![](_screenshots/module-zero-core-template-ui-home.png)

#### User Creation Modal
![](_screenshots/module-zero-core-template-ui-user-create-modal.png)

#### Login Page

![](_screenshots/module-zero-core-template-ui-login.png)

# Documentation

* [ASP.NET Core MVC & jQuery version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Core)
* [ASP.NET Core & Angular  version.](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular)

# License

[MIT](LICENSE).
