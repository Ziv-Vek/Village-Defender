using System.Threading.Tasks;

namespace VillageDefender.Services
{
    public interface ILocalDataService
    {
        Task<bool> SaveToFileAsync<T>(T data, string path, bool encrypt = false);
        Task<T> LoadFromFileAsync<T>(string path, bool decrypt = false);
        bool DeleteFile(string path);
        bool IsFileExists(string path);
    }
}