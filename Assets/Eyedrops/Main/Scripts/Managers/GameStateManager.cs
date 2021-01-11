using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;
using Zenject;

namespace Eyedrops.Main.Scripts.Managers
{
	public class GameStateManager : MonoBehaviour
	{
		public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;

		private readonly ReactiveProperty<GameState> currentState = new ReactiveProperty<GameState>();

		[Inject] private TimeManager timeManager = default;

		private void Start()
		{
			timeManager.ReadyTime
				.Where(x => x == 0)
				.Take(1)
				.Subscribe(x => { currentState.Value = GameState.EAT; })
				.AddTo(this);

			timeManager.RemainingTime
				.Where(x => x == 0)
				.Take(1)
				.Subscribe(_ => { currentState.Value = GameState.FINISHED; });
		}
	}
}