using System;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly MainMenuWindow _view;
        private readonly MainMenuModel _model;
        private readonly IAdsService _adsService;
        
        private bool _isStartingGame;

        public MainMenuPresenter(MainMenuWindow view, MainMenuModel model, IAdsService adsService)
        {
            _view = view;
            _model = model;
            _adsService = adsService;
        }

        public void Initialize()
        {
            _view.PlayClicked += OnPlayClicked;
            _view.ExitClicked += OnExitClicked;
        }

        private void OnExitClicked()
        {
            if (!_isStartingGame) _model.ExitGame();
        }

        private void OnPlayClicked()
        {
            if (!_isStartingGame) StartGameTask().Forget();
        }

        private async UniTask StartGameTask()
        {
            _isStartingGame = true;
            if (_adsService.IsBannerShown)
            {
                _adsService.HideBanner();
                await UniTask.WaitUntil(() => !_adsService.IsBannerShown);
            }

            _model.StartGame();
        }

        public void Dispose()
        {
            _view.PlayClicked -= OnPlayClicked;
            _view.ExitClicked -= OnExitClicked;
        }
    }
}