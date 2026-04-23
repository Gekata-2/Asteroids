using System;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.Services.UI;
using _Project.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverPresenter : IInitializable, IDisposable, IAssetFetcher
    {
        private const string WINDOW_ASSET_NAME = "game_over_ui";

        private readonly AssetsFactory _assetsFactory;
        private readonly GameOverModel _model;
        private readonly UIManager _uiManager;
        private readonly IAdsService _adsService;
        private readonly IInput _input;
        private readonly CursorService _cursorService;

        private GameOverWindow _view;

        public GameOverPresenter(GameOverModel model, UIManager uiManager, IInput input,
            AssetsFactory assetsFactory, IAdsService adsService, CursorService cursorService = null)
        {
            _model = model;
            _uiManager = uiManager;
            _input = input;
            _assetsFactory = assetsFactory;
            _adsService = adsService;
            _cursorService = cursorService;
        }

        public void Initialize()
        {
            _model.GameOver += OnGameOver;

            _input.SubmitPerformed += OnSubmitPerformed;
            _input.CancelPerformed += OnCancelPerformed;
            _input.ContinuePlayingPerformed += OnContinuePlayingPerformed;
        }

        public void FetchAssets()
        {
            _view = _assetsFactory.Create<GameOverWindow>(WINDOW_ASSET_NAME);
        }

        private void OnGameOver()
        {
            _view.Show(_model.Score);
            _uiManager.SetState(UIState.GameOver);
        }

        private void OnSubmitPerformed()
        {
            if (CanPerformActions())
                ShowInterstitialAdAndRestartGame().Forget();
        }

        private void OnCancelPerformed()
        {
            if (CanPerformActions())
                _model.ExitGame();
        }

        private void OnContinuePlayingPerformed()
        {
            if (CanPerformActions())
                ShowRewardedAdAndContinuePlaying().Forget();
        }

        private bool CanPerformActions()
            => _uiManager.CurrentState == UIState.GameOver && !_adsService.IsShowingAd;

        private async UniTask ShowInterstitialAdAndRestartGame()
        {
            _cursorService?.SetCursorVisibility(true);

            if (!_adsService.IsInterstitialAdReady)
            {
                _adsService.LoadInterstitialAd();
                await UniTask.WaitWhile(() => !_adsService.IsInterstitialAdReady);
            }

            _adsService.ShowInterstitialAd(() =>
            {
                _cursorService?.SetCursorVisibility(false);
                _model.RestartGame();
            });
        }


        private async UniTask ShowRewardedAdAndContinuePlaying()
        {
            _cursorService?.SetCursorVisibility(true);

            if (!_adsService.IsRewardedAdReady)
            {
                _adsService.LoadRewardedAd();
                await UniTask.WaitWhile(() => !_adsService.IsRewardedAdReady);
            }

            _adsService.ShowRewardedAd(() =>
            {
                _cursorService?.SetCursorVisibility(false);
                _view.Hide();
                _uiManager.SetState(UIState.None);
                _model.ContinueGame();
            });
        }

        public void Dispose()
        {
            _model.GameOver -= OnGameOver;

            _input.SubmitPerformed -= OnSubmitPerformed;
            _input.CancelPerformed -= OnCancelPerformed;
            _input.ContinuePlayingPerformed -= OnContinuePlayingPerformed;
        }
    }
}