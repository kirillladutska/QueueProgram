using System.Text.Json;

namespace QueueProgram.Admin
{
    public interface IAutorisationService
    {
        bool AutorisationValidation(string username, string password, string option);
    }

    public class AutorisationService: IAutorisationService

    {
    private List<AuthUser> _users;

    public AutorisationService()
    {
        _users = new List<AuthUser>();
        LoadAutorisationLog();
    }

    public class AuthUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Option { get; set; }
    }

    private void LoadAutorisationLog()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "autorisation.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _users = JsonSerializer.Deserialize<List<AuthUser>>(json);
            if (_users == null) throw new Exception("Autorisation data is null");
        }
        else
        {
            throw new FileNotFoundException("File autorisation.json not found.");
        }
    }

    public bool AutorisationValidation(string Username, string Password, string Option)
    {
        bool result;
        foreach (var cred in _users)
        {
            if (cred.Username == Username && cred.Password == Password && cred.Option == Option)
            {
                result = true;
                return result;
            }

        }
        
        result = false;
        return result;
    }
    }
}
