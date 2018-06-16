using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

     var authorizationHeader  =  req.Headers.GetValues("Authorization").FirstOrDefault();
     log.Info("Validating token: " + authorizationHeader);

     if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer"))
    {
        string bearerToken = authorizationHeader.Substring("Bearer ".Length).Trim();
        log.Info("Got token: " + bearerToken);
            ClaimsPrincipal principal;
            if ((principal = await AuthenticationService.ValidateTokenAsync(bearerToken, log)) == null)
            {
                log.Info("The authorization token is not valid.");
                return req.CreateResponse(HttpStatusCode.Unauthorized, "The authorization token is not valid.");
            }
    }
    else 
    {
        return req.CreateResponse(HttpStatusCode.Unauthorized, "The authorization header is either empty or isn't Bearer.");
    }


    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    if (name == null)
    {
        dynamic data = await req.Content.ReadAsAsync<object>();
        name = data?.name;
    }

    return name == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Hello " + name + "!" +" Greetings from Azure Function secured with Auth0");
}

public static class AuthenticationService
    {
        private static readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        private static readonly string ISSUER = "https://devisland.eu.auth0.com/";
        private static readonly string AUDIENCE = "devisland";

        static AuthenticationService()
        {
            var documentRetriever = new HttpDocumentRetriever { RequireHttps = ISSUER.StartsWith("https://") };

            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                $"{ISSUER}.well-known/openid-configuration",
                new OpenIdConnectConfigurationRetriever(),
                documentRetriever
            );
        }

        public static async Task<ClaimsPrincipal> ValidateTokenAsync(string bearerToken, TraceWriter log)
        {
            ClaimsPrincipal validationResult = null;
            short retry = 0;
            while(retry <=0 && validationResult == null)
            {
                try
                {
                var openIdConfig = await _configurationManager.GetConfigurationAsync(CancellationToken.None);

                    TokenValidationParameters validationParameters =
                        new TokenValidationParameters
                        {
                            ValidIssuer = ISSUER,
                            ValidAudiences = new[] { AUDIENCE },
                            IssuerSigningKeys = openIdConfig.SigningKeys
                        };

                    SecurityToken validatedToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    validationResult = handler.ValidateToken(bearerToken, validationParameters, out validatedToken);

                    
                    log.Info($"Token is validated. User Id {validationResult.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
                    return validationResult;
                }
                catch (SecurityTokenSignatureKeyNotFoundException)
                {
                    log.Info("SecurityTokenSignatureKeyNotFoundException exception thrown. Refreshing configuration...");
                    _configurationManager.RequestRefresh();
                    retry ++;
                }
                catch (SecurityTokenException)
                {
                    log.Info("SecurityTokenException exception throwns. One more attepmt...");
                     return null;    
                }
            }
            return validationResult;
        }
          
    }