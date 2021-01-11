using System;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Eyedrops.Scripts
{
	public static class SceneLoader
	{
		private static TransitionComponent component;

		private static TransitionComponent TransitionComponent
		{
			get
			{
				if (component != null) return component;
				var p = Resources.Load("TransitionManager");
				var go = Object.Instantiate(p) as GameObject;
				if (go != null) component = go.GetComponent<TransitionComponent>();
				return component;
			}
		}

		public static IObservable<Unit> OnTransitionFinished
		{
			get
			{
				if (!TransitionComponent.IsTransition.Value)
				{
					return Observable.Return(Unit.Default);
				}
				else
				{
					return TransitionComponent
						.IsTransition
						.FirstOrDefault(x => !x).AsUnitObservable();
				}
			}
		}

		public static void LoadScene(string nextScene, Action<DiContainer> action = null)
		{
			TransitionComponent.Transition(nextScene, action);
		}
	}
}