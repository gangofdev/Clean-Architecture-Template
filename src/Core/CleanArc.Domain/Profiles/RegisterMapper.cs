﻿using System.Reflection;
using AutoMapper;

namespace CleanArc.Domain.Profiles;

public class RegisterMapper : Profile
{
    public RegisterMapper()
    {
        this.ApplyMappingProfiles(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingProfiles(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreateDomainMapper<>)))
            .ToList();

        foreach (var type in types)
        {
            var typeConstructorArgumentLength = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .First().GetParameters().Length;

            var model = Activator.CreateInstance(type, new object[typeConstructorArgumentLength]);

            var methodInfo = type.GetMethod("Map") //get the map method directly by the class
                             ?? type.GetInterface("ICreateDomainMapper`1").GetMethod("Map"); //if null get the interface implementation

            if (model != null)
                methodInfo?.Invoke(model, new object[] { this });
        }
    }
}