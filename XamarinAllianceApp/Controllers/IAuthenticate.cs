using System.Threading.Tasks;

namespace XamarinAllianceApp.Controllers
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
