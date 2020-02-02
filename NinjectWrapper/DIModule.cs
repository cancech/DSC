namespace DependencyInjection
{
    public abstract class DIModule : Ninject.Modules.NinjectModule
    {

        protected void BindConstant<I>(I instance, string name)
        {
            Bind<I>().ToConstant(instance).Named(name);
        }

        protected void BindType<I, T>() where T : I
        {
            Bind<I>().To<T>();
        }
    }
}
