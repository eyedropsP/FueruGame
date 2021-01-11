using System;
using System.Collections;
using Eyedrops.Main.Scripts.Managers;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Eyedrops.Main.Scripts.Bananas
{
	public class BananaSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject bananaPrefab = default;
		[SerializeField] private GameStateManager gameStateManager = default;
		
		[SerializeField] private float spawnIntervalTime = 500;

		private void Awake()
		{
			Bananas.Banana.SetOriginal(bananaPrefab);
		}
		
		private void Start()
		{
			gameStateManager.CurrentState
				.Where(x => x == GameState.EAT)
				.Subscribe(x =>
				{
					Spawn();
				});
		}

		private void Spawn()
		{
			Observable
				.Interval(TimeSpan.FromSeconds(3))
				.TakeWhile(_ => gameStateManager.CurrentState.Value == GameState.EAT)
				.Subscribe(_ => { spawnIntervalTime -= 30f; })
				.AddTo(this);

			StartCoroutine(SpawnCoroutine());
		}

		private IEnumerator SpawnCoroutine()
		{
			while (gameStateManager.CurrentState.Value == GameState.EAT)
			{
				var pos = transform.position;
				var x = pos.x + Random.Range(-2.0f, 2.0f);
				var z = pos.z + Random.Range(-2.0f, 2.0f);
				
				var spawnPos = new Vector3(x, pos.y, z);
				transform.Rotate(.5f, .5f, .5f);

				var banana = Bananas.Banana.Create();
				banana.Spawn(spawnPos, transform.rotation);
				yield return new WaitForSeconds(spawnIntervalTime / 1000f);
			}
		}
	}
}