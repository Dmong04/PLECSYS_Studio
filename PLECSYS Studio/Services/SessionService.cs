using PLECSYS_Studio.Wrappers.Users;
using System.Text.Json;

namespace PLECSYS_Studio.Services
{
    public class SessionService
    {
        private const string AccessTokenKey = "access_token";
        private const string EmailKey = "email";
        private const string CompanyIdKey = "selected_company_id";
        private const string CompanyNameKey = "selected_company_name";
        private const string LinkedProcessesKey = "linked_processes";

        public void SaveSession(LoginResponse login, int companyId, string companyName)
        {
            Preferences.Set(AccessTokenKey, login.access_token ?? string.Empty);
            Preferences.Set(EmailKey, login.email ?? string.Empty);
            Preferences.Set(CompanyIdKey, companyId);
            Preferences.Set(CompanyNameKey, companyName);

            var processes = JsonSerializer.Serialize(login.linked_processes ?? new List<SmartFlowOption>());
            Preferences.Set(LinkedProcessesKey, processes);
        }

        public void SaveSelectedCompany(int companyId, string companyName)
        {
            Preferences.Set(CompanyIdKey, companyId);
            Preferences.Set(CompanyNameKey, companyName);
        }

        public string GetAccessToken() => Preferences.Get(AccessTokenKey, string.Empty);
        public string GetEmail() => Preferences.Get(EmailKey, string.Empty);
        public int GetCompanyId() => Preferences.Get(CompanyIdKey, 0);
        public string GetCompanyName() => Preferences.Get(CompanyNameKey, string.Empty);

        public List<SmartFlowOption> GetLinkedProcesses()
        {
            var raw = Preferences.Get(LinkedProcessesKey, string.Empty);
            if (string.IsNullOrEmpty(raw)) return new List<SmartFlowOption>();
            return JsonSerializer.Deserialize<List<SmartFlowOption>>(raw) ?? new List<SmartFlowOption>();
        }

        public bool HasCompany() => Preferences.ContainsKey(CompanyIdKey);

        public void Clear()
        {
            Preferences.Remove(AccessTokenKey);
            Preferences.Remove(EmailKey);
            Preferences.Remove(CompanyIdKey);
            Preferences.Remove(CompanyNameKey);
            Preferences.Remove(LinkedProcessesKey);
        }
    }
}