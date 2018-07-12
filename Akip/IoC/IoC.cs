using Ninject;

namespace Akip
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Setup()
        {
            BindViewModel();
        }

        private static void BindViewModel()
        {
            Kernel.Bind<CollectionViewModels>().ToConstant( new CollectionViewModels() );
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
