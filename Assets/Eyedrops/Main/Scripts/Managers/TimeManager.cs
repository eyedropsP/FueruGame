using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Eyedrops.Scripts;
using UniRx;
using UnityEngine;

namespace Eyedrops.Main.Scripts.Managers
{
	public class TimeManager : MonoBehaviour
	{
		[SerializeField] private IntReactiveProperty readyTime = new IntReactiveProperty();
		[SerializeField] private IntReactiveProperty remainingTime = default;

		public IReadOnlyReactiveProperty<int> ReadyTime => readyTime;
		public IReadOnlyReactiveProperty<int> RemainingTime => remainingTime;

		private void Start()
		{
			readyTime.AddTo(this);
			remainingTime.AddTo(this);
			CountDownAsync(this.GetCancellationTokenOnDestroy()).Forget();
		}

		private async UniTaskVoid CountDownAsync(CancellationToken token)
		{
			await SceneLoader.OnTransitionFinished;
			await CountReadyTimeAsync(token);
			await CountRemainingTimeAsync(token);
		}

		private async Task CountReadyTimeAsync(CancellationToken token)
		{
			await UniTask.Delay(1000, cancellationToken: token);
			readyTime.SetValueAndForceNotify(readyTime.Value);
			while (!token.IsCancellationRequested)
			{
				await UniTask.Delay(1000, cancellationToken: token);
				readyTime.Value--;
				if (readyTime.Value == 0) return;
			}
		}

		private async Task CountRemainingTimeAsync(CancellationToken token)
		{
			await UniTask.Delay(1000, cancellationToken: token);
			remainingTime.SetValueAndForceNotify(remainingTime.Value);
			while (!token.IsCancellationRequested)
			{
				await UniTask.Delay(1000, cancellationToken: token);
				remainingTime.Value--;
				if (remainingTime.Value == 0) return;
			}
		}
	}
}