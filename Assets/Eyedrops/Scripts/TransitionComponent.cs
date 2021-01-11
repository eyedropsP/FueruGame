using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FadeCamera.Scripts;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Eyedrops.Scripts
{
	public class TransitionComponent : MonoBehaviour
	{
		[SerializeField] private FadeImage fade = default;
		[SerializeField] private float transitionSeconds = 1;
		[Inject] private ZenjectSceneLoader zenjectSceneLoader = default;

		private BoolReactiveProperty isTransition = new BoolReactiveProperty();
		public IReadOnlyReactiveProperty<bool> IsTransition => isTransition;
		
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void Transition(string nextScene, Action<DiContainer> bindAction = null)
		{
			isTransition.Value = false;
			Transition(nextScene, this.GetCancellationTokenOnDestroy(), bindAction).Forget();
		}

		private async UniTaskVoid Transition(string nextScene, CancellationToken token,
			Action<DiContainer> bindAction = null)
		{
			var startTime = Time.time;
			while (Time.time - startTime < transitionSeconds)
			{
				var rate = (Time.time - startTime) / transitionSeconds;
				fade.Range = rate;
				await UniTask.Yield();
			}

			fade.Range = 1;
			
			await zenjectSceneLoader.LoadSceneAsync(nextScene, LoadSceneMode.Single, bindAction);

			startTime = Time.time;
			while (Time.time - startTime < transitionSeconds)
			{
				var rate = 1 - (Time.time - startTime) / transitionSeconds;
				fade.Range = rate;
				await UniTask.Yield();
			}

			fade.Range = 0;
			isTransition.Value = true;
		}
	}
}