namespace Arena.Models
{
    public class Competitor : BindableBase
    {
        private string _name;
        private string _avatarUrl;
        private bool _stilInGame;
        public string Url { get; set; }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }

        public delegate void DuelFinishedDelegete(object sender);
        public event DuelFinishedDelegete DuelFinished;

        public bool StilInGame
        {
            get { return _stilInGame; }
            set
            {
                SetProperty(ref _stilInGame, value);
                DuelFinishedInformer();
            }
        }

        private void DuelFinishedInformer()
        {
            if (DuelFinished != null)
            {
                DuelFinished(this);
            }
        }
    }
}
