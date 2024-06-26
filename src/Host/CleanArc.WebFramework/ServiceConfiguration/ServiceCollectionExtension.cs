﻿using Asp.Versioning;
using CleanArc.SharedKernel.Context;
using CleanArc.WebFramework.Context;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CleanArc.WebFramework.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebFrameworkServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            //url segment => {version}
            options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
            options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1
            options.ReportApiVersions = true;

            //ApiVersion.TryParse("1.0", out var version10);
            //ApiVersion.TryParse("1", out var version1);
            //var a = version10 == version1;

            //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            // api/posts?api-version=1

            //options.ApiVersionReader = new UrlSegmentApiVersionReader();
            // api/v1/posts

            //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
            // header => Api-Version : 1

            //options.ApiVersionReader = new MediaTypeApiVersionReader()

            //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
            // combine of [querystring] & [urlsegment]
        });
        services.AddTransient<IRequestContext>(sp =>
        {

            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var context = httpContextAccessor.HttpContext;
            if (context != null && context.User.Identity.IsAuthenticated)
            {
                var userIdentity = context.User.Identity as ClaimsIdentity;

                return new RequestContext() { UserName = userIdentity.Name, DisplayName = userIdentity.GetUserName(), UserId = userIdentity.GetUserId<int>() };
            }
            return new RequestContext();

        });
        return services;


    }
}