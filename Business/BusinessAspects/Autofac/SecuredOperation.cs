using System.Security.Claims;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac;

public class SecuredOperation : MethodInterception
{
    private readonly string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;
     
    public SecuredOperation(string roles)
    {
        _roles = roles.Split(',');
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>() 
                               ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
    }
    
    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _httpContextAccessor.HttpContext.User.Claims.Where
            (c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new Exception(Messages.AuthorizationDenied);
    }
}