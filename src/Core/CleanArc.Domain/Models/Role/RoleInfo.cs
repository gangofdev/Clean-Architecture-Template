using CleanArc.Domain.Entities.User;
using CleanArc.Domain.Profiles;

namespace CleanArc.Domain.Models.Identity;

public class RoleInfo : ICreateDomainMapper<Entities.User.Role>
{
    public string Id { get; set; }
    public string Name { get; set; }
}