using CAPIAuth.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace CAPIAuth.Services
{
  public class APIService
  {
    private static string _baseUrl;
    private static string _username;
    private static string _password;
    private static string _token;

    public APIService()
    {
      _baseUrl = "https://api.escuelajs.co/api/v1";
      _username = "john@mail.com";
      _password = "changeme";
      _token = Login().access_token; // maybe u need to delete ".access_token"
    }

    public static LoginResponse Login()
    {
      var credentials = new Credentials();
      credentials.email = _username; // Or username
      credentials.password = _password;

      var options = new RestClientOptions(_baseUrl);
      var client = new RestClient(options);

      try
      {
        // auth/login
        var request = new RestRequest("auth/login").AddJsonBody(credentials);
        var response = client.PostAsync(request).Result.Content;

        var result = JsonConvert.DeserializeObject<LoginResponse>(response);

        return result;

      }
      catch (Exception)
      {
        Console.WriteLine("\nCredenciales incorrectas\n");
        throw;
      }
    }

    public static RestClient Authenticate()
    {
      // Console.WriteLine($"\n{_token}\n Tipo: {_token.GetType()}");
      var authenticator = new JwtAuthenticator(_token);
      var options = new RestClientOptions(_baseUrl)
      {
        Authenticator = authenticator
      };
      var client = new RestClient(options);
      return client;
    }

    public User GetUser()
    {
      // auth/profile
      var request = new RestRequest("auth/profile");
      var client = Authenticate();

      try
      {
        var response = client.GetAsync(request).Result.Content;
        var user = JsonConvert.DeserializeObject<User>(response);

        return user;
      }
      catch (Exception)
      {
        Console.WriteLine("\nLa petición no tuvo éxito.\n");
        throw;
      }
    }
  }
}