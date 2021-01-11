using Eyedrops.Main.Scripts.Bananas;
using Eyedrops.Scripts;
using JetBrains.Annotations;
using KanKikuchi.AudioManager;
using UniRx;
using UnityEngine;
using Zenject;

namespace Eyedrops.Main.Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
		// [SerializeField] private Slider gorillaPowerSlider = default;

		public IReactiveProperty<bool> IsResultShowing => isResultShowing;
		private readonly ReactiveProperty<bool> isResultShowing = new ReactiveProperty<bool>();

		[Inject] private GameStateManager gameStateManager = default;

		private void Start()
		{
			BGMManager.Instance.Play(BGMPath.JANGLE_LOOP, .5f);

			gameStateManager.CurrentState
				.Where(x => x == GameState.FINISHED)
				.Subscribe(_ => isResultShowing.Value = true)
				.AddTo(this);
			
			// ゴリラのパワー(スキル発動なんかで使う予定)
			// gorillaManager.Power
			// 	.DistinctUntilChanged()
			// 	.Subscribe(x => gorillaPowerSlider.value = x)
			// 	.AddTo(this);
		}

		[UsedImplicitly]
		public void GoToResultAsync()
		{
			BGMManager.Instance.FadeOut(1f, () =>
			{
				Banana.Clear();
				BananaJuiceEffect.Clear();
				SceneLoader.LoadScene(GameScene.ResultScene.ToString());
			});
		}
	}
}