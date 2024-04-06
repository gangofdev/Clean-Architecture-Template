namespace CleanArc.Domain.Models.Role;

public class RolePermissionItem
{
    public int RoleId { get; set; }
    public List<string> Permissions { get; set; }
}