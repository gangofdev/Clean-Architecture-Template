using CleanArc.Domain.Models.Identity;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Domain.Models.Role;

public class RolePermission
{
    public List<string> Keys { get; set; } = new List<string>();

    public Entities.User.Role Role { get; set; }

    public int RoleId { get; set; }

    public List<PermissionActionDescription> Actions { get; set; }
}