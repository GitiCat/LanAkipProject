namespace Akip
{
    public class ViewModelLocation
    {
        public static ViewModelLocation Instance { get; private set; } = new ViewModelLocation();

        public static CollectionViewModels CollectionViewModels => IoC.Get<CollectionViewModels>();
    }
}
