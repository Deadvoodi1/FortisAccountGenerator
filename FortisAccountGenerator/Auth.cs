using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace FortisAccountGenerator;

public class Auth
{
    public static string TokenGet(string username, string password)
    {
        Dictionary<string, string> body = new Dictionary<string, string>();

        body.Add("grant_type", "password");
        body.Add("client_id", "fortis-onboard");
        body.Add("username", username);
        body.Add("password", password);

        RestResponse result = SingIn(body: body).Result;
        JObject jobject = (JObject) null;
        try
        {
            jobject = JObject.Parse(result.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Message: {ex.Message} \r\n StackTrace:{ex.StackTrace}");
            throw;
        }
        
        var accessToken = jobject["access_token"].ToString();
        return accessToken;
    }

    private static async Task<RestResponse> SingIn(
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> headerParams = null,
        Dictionary<string, string> queryParams = null,
        Dictionary<string, string> body = null)
    {
        string devApiHostName = "https://keycloak-dev.http.dev.fortisds.tech";
        string endpoint = "/realms/fortis/protocol/openid-connect/token";

        RestClient client = new RestClient(devApiHostName);
        RestRequest request = new RestRequest(endpoint);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("application/x-www-form-urlencoded",
            $"grant_type={body["grant_type"]}&client_id={body["client_id"]}&username={body["username"]}&password={body["password"]}", ParameterType.RequestBody);

        RestResponse response = await client.ExecutePostAsync(request);

        devApiHostName = (string) null;
        endpoint = (string) null;
        client = (RestClient) null;
        request = (RestRequest) null;

        return response;
    }

    public static async Task<RestResponse> Post(
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> headerParams = null,
        Dictionary<string, string> queryParams = null,
        string jsonBody = null)
    {
        string bearerToken = headerParams["Authorization"];
        
        string devApiHostName = "https://api-dev.http.dev.fortisds.tech";
        string endpoint = "/web/current/onboarding/register";

        RestClient client = new RestClient(new RestClientOptions(devApiHostName)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(bearerToken, "Bearer")
        });
        RestRequest request = new RestRequest(endpoint);
        request.AddStringBody(jsonBody, DataFormat.Json);

        RestResponse response = await client.ExecutePostAsync(request);

        bearerToken = null;
        devApiHostName = null;
        endpoint = null;
        client = null;
        request = null;

        return response;
    }
}