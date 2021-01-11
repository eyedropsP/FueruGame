using System.Collections;
using UnityEngine;

namespace Eyedrops.Main.Scripts.Bananas
{
	public class BananaJuiceEffect : PoolObj<BananaJuiceEffect>
	{
		[SerializeField] private ParticleSystem bananaJuiceParticle = default;
		
		public void PlayEffects()
		{
			StartCoroutine(PlayParticle());
		}

		private IEnumerator PlayParticle()
		{
			bananaJuiceParticle.Play();
			yield return new WaitWhile(() => bananaJuiceParticle.IsAlive(true));
			Pool(this);
		}
		
		protected override void Init()
		{
			gameObject.SetActive(true);
		}

		protected override void Sleep()
		{
			gameObject.SetActive(false);
		}
	}
}