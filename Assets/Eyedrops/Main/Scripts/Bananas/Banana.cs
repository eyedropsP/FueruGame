using KanKikuchi.AudioManager;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Eyedrops.Main.Scripts.Bananas
{
	public class Banana : PoolObj<Banana>, IBanana
	{
		private readonly ReactiveProperty<bool> isActive = new ReactiveProperty<bool>(false);
		private readonly ReactiveProperty<bool> isEaten = new ReactiveProperty<bool>();
		
		public IReadOnlyReactiveProperty<bool> IsActive => isActive;
		public IReadOnlyReactiveProperty<bool> IsEaten => isEaten;

		private string[] sePaths = new[] {SEPath.GUCHA1, SEPath.GUCHA2};
		
		private void Start()
		{
			Observable.EveryUpdate()
				.Select(_ => gameObject.activeInHierarchy)
				.Subscribe(x => isActive.Value = x)
				.AddTo(this);
		}
		
		public void Spawn(Vector3 pos, Quaternion rotation)
		{
			transform.position = pos;
			// ReSharper disable once Unity.InefficientPropertyAccess
			transform.rotation = rotation;
		}

		public void Eat()
		{
			if (!gameObject.activeSelf)
			{
				return;
			}
			PlaySE();
			isEaten.Value = true;
			Pool(this);
		}

		protected override void Init()
		{
			gameObject.SetActive(true);
			isEaten.Value = false;
		}

		protected override void Sleep()
		{
			gameObject.SetActive(false);
			isEaten.Value = false;
		}

		private void PlaySE()
		{
			var sePath = sePaths[Random.Range(0, 2)];
			SEManager.Instance.Play(sePath);
		}
	}
}