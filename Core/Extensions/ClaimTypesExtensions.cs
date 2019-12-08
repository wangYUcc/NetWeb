using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Extensions
{
  public class ClaimTypesExtensions
  {

    // void fuck (string fieldName)
    //{
    //  Type type = typeof(ClaimTypes);
    //  var filedInfo= type.GetField(fieldName);
    //  filedInfo.GetValue(type);
    //}
  public const string Permission = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permission";
  /// <summary>http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor.</summary>
  /// <returns></returns>
  public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";

  /// <summary>The URI for a claim that specifies the anonymous user; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous.</summary>
  /// <returns></returns>
  public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

  /// <summary>The URI for a claim that specifies details about whether an identity is authenticated, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticated.</summary>
  /// <returns></returns>
  public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

  /// <summary>The URI for a claim that specifies the instant at which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant.</summary>
  /// <returns></returns>
  public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";

  /// <summary>The URI for a claim that specifies the method with which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod.</summary>
  /// <returns></returns>
  public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

  /// <summary>The URI for a claim that specifies an authorization decision on an entity; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision.</summary>
  /// <returns></returns>
  public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";

  /// <summary>The URI for a claim that specifies the cookie path; http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath.</summary>
  /// <returns></returns>
  public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";

  /// <summary>The URI for a claim that specifies the country/region in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country.</summary>
  /// <returns></returns>
  public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

  /// <summary>The URI for a claim that specifies the date of birth of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth.</summary>
  /// <returns></returns>
  public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

  /// <summary>The URI for a claim that specifies the deny-only primary group SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid. A deny-only SID denies the specified entity to a securable object.</summary>
  /// <returns></returns>
  public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";

  /// <summary>The URI for a claim that specifies the deny-only primary SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid. A deny-only SID denies the specified entity to a securable object.</summary>
  /// <returns></returns>
  public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";

  /// <summary>The URI for a claim that specifies a deny-only security identifier (SID) for an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid. A deny-only SID denies the specified entity to a securable object.</summary>
  /// <returns></returns>
  public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup.</summary>
  /// <returns></returns>
  public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";

  /// <summary>The URI for a claim that specifies the DNS name associated with the computer name or with the alternative name of either the subject or issuer of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns.</summary>
  /// <returns></returns>
  public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa.</summary>
  /// <returns></returns>
  public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";

  /// <summary>The URI for a claim that specifies the email address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/email.</summary>
  /// <returns></returns>
  public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration.</summary>
  /// <returns></returns>
  public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expired.</summary>
  /// <returns></returns>
  public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";

  /// <summary>The URI for a claim that specifies the gender of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender.</summary>
  /// <returns></returns>
  public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

  /// <summary>The URI for a claim that specifies the given name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname.</summary>
  /// <returns></returns>
  public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

  /// <summary>The URI for a claim that specifies the SID for the group of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid.</summary>
  /// <returns></returns>
  public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";

  /// <summary>The URI for a claim that specifies a hash value, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash.</summary>
  /// <returns></returns>
  public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

  /// <summary>The URI for a claim that specifies the home phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone.</summary>
  /// <returns></returns>
  public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent.</summary>
  /// <returns></returns>
  public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";

  /// <summary>The URI for a claim that specifies the locale in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality.</summary>
  /// <returns></returns>
  public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

  /// <summary>The URI for a claim that specifies the mobile phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone.</summary>
  /// <returns></returns>
  public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

  /// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name.</summary>
  /// <returns></returns>
  public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

  /// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier.</summary>
  /// <returns></returns>
  public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

  /// <summary>The URI for a claim that specifies the alternative phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone.</summary>
  /// <returns></returns>
  public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

  /// <summary>The URI for a claim that specifies the postal code of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode.</summary>
  /// <returns></returns>
  public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

  /// <summary>The URI for a claim that specifies the primary group SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid.</summary>
  /// <returns></returns>
  public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";

  /// <summary>The URI for a claim that specifies the primary SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid.</summary>
  /// <returns></returns>
  public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";

  /// <summary>The URI for a claim that specifies the role of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/role.</summary>
  /// <returns></returns>
  public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

  /// <summary>The URI for a claim that specifies an RSA key, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa.</summary>
  /// <returns></returns>
  public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

  /// <summary>The URI for a claim that specifies a serial number, http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber.</summary>
  /// <returns></returns>
  public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

  /// <summary>The URI for a claim that specifies a security identifier (SID), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid.</summary>
  /// <returns></returns>
  public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

  /// <summary>The URI for a claim that specifies a service principal name (SPN) claim, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn.</summary>
  /// <returns></returns>
  public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

  /// <summary>The URI for a claim that specifies the state or province in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince.</summary>
  /// <returns></returns>
  public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

  /// <summary>The URI for a claim that specifies the street address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress.</summary>
  /// <returns></returns>
  public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

  /// <summary>The URI for a claim that specifies the surname of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname.</summary>
  /// <returns></returns>
  public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

  /// <summary>The URI for a claim that identifies the system entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system.</summary>
  /// <returns></returns>
  public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

  /// <summary>The URI for a claim that specifies a thumbprint, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint. A thumbprint is a globally unique SHA-1 hash of an X.509 certificate.</summary>
  /// <returns></returns>
  public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

  /// <summary>The URI for a claim that specifies a user principal name (UPN), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn.</summary>
  /// <returns></returns>
  public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

  /// <summary>The URI for a claim that specifies a URI, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri.</summary>
  /// <returns></returns>
  public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata.</summary>
  /// <returns></returns>
  public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/version.</summary>
  /// <returns></returns>
  public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";

  /// <summary>The URI for a claim that specifies the webpage of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage.</summary>
  /// <returns></returns>
  public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

  /// <summary>The URI for a claim that specifies the Windows domain account name of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname.</summary>
  /// <returns></returns>
  public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim.</summary>
  /// <returns></returns>
  public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup.</summary>
  /// <returns></returns>
  public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion.</summary>
  /// <returns></returns>
  public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority.</summary>
  /// <returns></returns>
  public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";

  /// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim.</summary>
  /// <returns></returns>
  public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

  /// <summary>The URI for a distinguished name claim of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname. The X.500 standard defines the methodology for defining distinguished names that are used by X.509 certificates.</summary>
  /// <returns></returns>
  public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

}
}
