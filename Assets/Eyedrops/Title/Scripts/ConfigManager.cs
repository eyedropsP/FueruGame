using Eyedrops.Scripts.Helper;
using KanKikuchi.AudioManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Eyedrops.Title.Scripts
{
	public class ConfigManager : MonoBehaviour
	{
		[SerializeField] private Slider bgmSlider = default; 
		[SerializeField] private Slider seSlider = default;

		[SerializeField] private SliderHelper sliderHelper = default;
		
		private void Start()
		{
			BGMManager.Instance.ChangeBaseVolume(bgmSlider.normalizedValue);
			SEManager.Instance.ChangeBaseVolume(seSlider.normalizedValue);

			sliderHelper.OnChangeValue
				.Subscribe(value =>
				{
					if (SEManager.Instance.IsPlaying())
					{
						SEManager.Instance.Stop();
					}
					SEManager.Instance.Play(SEPath.GUCHA1, value);
				}).AddTo(this);
		}

		public void BGMBaseVolumeChange()
		{
			BGMManager.Instance.ChangeBaseVolume(bgmSlider.normalizedValue);
		}

		public void SEBaseVolumeChange()
		{
			SEManager.Instance.ChangeBaseVolume(seSlider.normalizedValue);
		}
	}
}