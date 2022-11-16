namespace AccountManagement.Infrastructure.EFCore.Security
{
    public interface IPermissionChecker
    {
        bool CheckPermission(int permissionId, string username);

        bool CheckUserHasColleagueRole(string username);
    }
}
