using AutoMapper;

namespace CleanArc.Application.Profiles;

public interface ICreateMapper<TSource>
{
    public virtual void Map(Profile profile)
    {
        profile.CreateMap(typeof(TSource), GetType()).ReverseMap();
    }
}