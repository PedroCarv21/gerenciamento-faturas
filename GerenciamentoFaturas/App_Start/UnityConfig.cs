using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Application.Services;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Context;
using GerenciamentoFaturas.Infrastructure.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace GerenciamentoFaturas.API.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<GerenciamentoFaturasContext>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IFaturaRepository, FaturaRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IFaturaService, FaturaService>(
                new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}