namespace Client.ApplicationStates
{
    public class AllState
    {
        public event Action? Action;

        public bool ShowWelcome { get; private set; }
        public bool ShowVozaci { get; private set; }
        public bool ShowTrke { get; private set; }
        public bool ShowRangListe { get; private set; }
        public bool ShowUser { get; private set; }

        public void WelcomeClicked()
        {
            ResetAll();
            ShowWelcome = true;
            NotifyStateChanged();
        }

        public void VozaciClicked()
        {
            ResetAll();
            ShowVozaci = true;
            NotifyStateChanged();
        }

        public void TrkeClicked()
        {
            ResetAll();
            ShowTrke = true;
            NotifyStateChanged();
        }

        public void RangListeClicked()
        {
            ResetAll();
            ShowRangListe = true;
            NotifyStateChanged();
        }

        public void UserClicked()
        {
            ResetAll();
            ShowUser = true;
            NotifyStateChanged();
        }

        private void ResetAll()
        {
            ShowVozaci = false;
            ShowTrke = false;
            ShowRangListe = false;
            ShowUser = false;
            ShowWelcome = false;
        }

        private void NotifyStateChanged() => Action?.Invoke();
    }
}
