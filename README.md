# DI-Container

Features:
- Lifetime cycles
- Factories
- Local scopes

Global DI:
``` csharp
DIContainer.Register<IUserRepository, UserRepository>(Lifetime.Singleton);
DIContainer.Register<IUserService>(() => new UserRepostory(DIContainer.Resolve<IUserRepository>());
DIContainer.Register<IUserExtendedService, UserExtendedService>();

var concreteRepository = DIContainer.Resolve<UserRepository>();
var service = DIContainer.Resolve<IUserService>();
var extendedService = DIContainer.Resolve<IUserExtendedService>();
```


Local Scope DI:
``` csharp
DIContainer.Register<IUserRepository, UserRepository>(Lifetime.Singleton);
IContainerScope scope = DIContainer.GetLocalScope();
scope.Register<IUserService>(() => new UserRepostory(DIContainer.Resolve<IUserRepository>());
scope.Register<IUserExtendedService, UserExtendedService>();

var concreteRepository = scope.Resolve<UserRepository>();
var service = scope.Resolve<IUserService>();
var extendedService = scope.Resolve<IUserExtendedService>();
```
