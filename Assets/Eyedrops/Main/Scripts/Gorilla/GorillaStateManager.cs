using System;
using Eyedrops.Main.Scripts.Managers;
using UniRx;
using UnityEngine;
using Zenject;

namespace Eyedrops.Main.Scripts.Gorilla
{
	public class GorillaStateManager : MonoBehaviour
	{
		public IReadOnlyReactiveProperty<GorillaState> CurrentState => currentState;
		
		private readonly ReactiveProperty<GorillaState> currentState = new ReactiveProperty<GorillaState>(GorillaState.CanEat);

		[SerializeField] private GorillaAnimator gorillaAnimator = default;

		private void Start()
		{
			gorillaAnimator.IsSuffer
				.Subscribe(x =>
				{
					if (x)
					{
						currentState.Value = GorillaState.Suffer;
					}
					else
					{
						currentState.Value = GorillaState.CanEat;
					}
				}).AddTo(this);
			
			// gorillaAnimator.IsThrowUp
			// 	.Subscribe(x =>
			// 	{
			// 		if (x)
			// 		{
			// 			currentState.Value = GorillaState.ThrowUp;
			// 		}
			// 		else
			// 		{
			// 			currentState.Value = GorillaState.ThrowUp;
			// 		}
			// 	}).AddTo(this);
		}
	}
}