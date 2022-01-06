using System;
using System.Collections.Generic;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.DataContracts.IRepositoryProvider;

namespace TrisvagoHotels.Providers.RepositoryProvider;

public class RepositoryProvider : IRepositoryProvider {
    private readonly IRepositoryFactory repositoryFactory;
    private Dictionary<Type, object> Repositories { get; set; }

    public RepositoryProvider(IRepositoryFactory repositoryFactory) {
        this.repositoryFactory = repositoryFactory;
        Repositories = new Dictionary<Type, object>();
    }

    public MyDataContext Context { get; set; }

    public virtual T GetRepository<T>(Func<MyDataContext, object> factory = null) where T : class {
        Repositories.TryGetValue(typeof(T), out var repoObj);
        if (repoObj != null)
            return (T) repoObj;
        return MakeRepository<T>(factory, Context);
    }

    private T MakeRepository<T>(Func<MyDataContext, object> factory, MyDataContext context) where T : class {
        var f = factory ?? repositoryFactory.GetRepositoryFactory<T>();
        if (f == null)
            throw new NotImplementedException();
        var repo = (T) f(context);
        SetRepository(repo);
        return repo;
    }

    public IRepository<T> GetRepositoryForEntityType<T>() where T : class {
        return GetRepository<IRepository<T>>(repositoryFactory.GetRepositoryFactoryForEntityType<T>());
    }

    public void SetRepository<T>(T repository) {
        Repositories[typeof(T)] = repository;
    }
}