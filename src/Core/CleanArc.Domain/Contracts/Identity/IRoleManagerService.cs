using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Models.Identity;
using CleanArc.Domain.Models.Role;
using Microsoft.AspNetCore.Identity;

namespace CleanArc.Domain.Contracts.Identity;

public interface IRoleManagerService
{
    Task<List<RoleInfo>> GetRolesAsync();
    Task<IdentityResult> CreateRoleAsync(NewRole model);
    Task<bool> DeleteRoleAsync(int roleId);
    Task<List<PermissionActionDescription>> GetPermissionActionsAsync();
    Task<RolePermission> GetRolePermissionsAsync(int roleId);
    Task<bool> ChangeRolePermissionsAsync(RolePermissionItem model);
    Task<Role> GetRoleByIdAsync(int roleId);
}