using AutoMapper;

namespace CleanArc.Domain.Profiles;

public interface ICreateDomainMapper<TSource>
{
    void Map(Profile profile)
    {
        profile.CreateMap(typeof(TSource), GetType()).ReverseMap();
    }
}