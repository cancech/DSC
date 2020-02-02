using Ninject;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DependencyInjection
{
    public class DIKernel
    {
        private readonly StandardKernel kernel;

        public DIKernel()
        {
            List<DIModule> list = new List<DIModule>();
            foreach (System.Type t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
            .Where(x => typeof(DIModule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
            {
                if (t.IsSubclassOf(typeof(DIModule)))
                {
                    list.Add((DIModule) Activator.CreateInstance(t));
                }
            }

            kernel = new StandardKernel(list.ToArray());
        }

        public T Get<T>(string name)
        {
            return kernel.Get<T>(name);
        }
    }
}
