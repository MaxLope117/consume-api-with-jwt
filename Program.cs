using CAPIAuth.Services;

namespace CAPIAuth;
class Program
{
    static void Main(string[] args)
    {

        APIService API = new APIService();

        var user = API.GetUser();

        Console.WriteLine(user.name);

    }
}
