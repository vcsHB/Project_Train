using Project_Train.ViewControl;
namespace Project_Train.BuildSystem
{

    public class SubReactor : Building
    {
        private ViewAnchorPoint _viewAnchorPointCompo;
        public ViewAnchorPoint ViewAnchorPoint => _viewAnchorPointCompo;

        protected override void Awake()
        {
            base.Awake();
            _viewAnchorPointCompo = GetComponentInChildren<ViewAnchorPoint>();
        }

        // TODO : ....

    }
}