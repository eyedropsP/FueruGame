using UnityEngine;

namespace Eyedrops.Main.Scripts.Bananas
{
	public class BananaEffectManager : MonoBehaviour
	{
		[SerializeField] private GameObject bananaEffectPrefab = default;
		
		private RenderTexture bananaJuiceTexture;

		private void Awake()
		{
			BananaJuiceEffect.SetOriginal(bananaEffectPrefab);	
		}

		public void Play()
		{
			var effect = BananaJuiceEffect.Create(gameObject);
			effect.PlayEffects();
		}
	}
}