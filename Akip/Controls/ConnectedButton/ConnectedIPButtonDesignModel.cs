namespace Akip
{
    public class ConnectedIPButtonDesignModel : ConnectedIPButtonViewModel
    {
        public static ConnectedIPButtonDesignModel Instance => new ConnectedIPButtonDesignModel();

        public ConnectedIPButtonDesignModel()
        {
            ConnectedString = "0.0.0.0";
        }
    }
}
