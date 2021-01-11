using System;
using Eyedrops.Main.Scripts.Managers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Eyedrops.Main.Scripts.UI
{
	public class UIPresenter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI countDownTimerText = default;
		[SerializeField] private TextMeshProUGUI startCountDownTimerText = default;
		[SerializeField] private TextMeshProUGUI bananaQuantityText = default;
		[SerializeField] private TextMeshProUGUI eatenBananaQuantityText = default;
		[SerializeField] private Slider satietySlider = default;
		
		[Inject] private TimeManager timeManager = default;
		[Inject] private GorillaManager gorillaManager = default;
		
		private void Start()
		{
			timeManager.ReadyTime
				.Subscribe(x =>
				{
					if (x == 0)
					{
						startCountDownTimerText.text = "ウホ！(はじめ！)";
						countDownTimerText.text = timeManager.RemainingTime.Value.ToString();
					}
					else
					{
						startCountDownTimerText.text = $"{x}";
					}
				}).AddTo(this);

			timeManager.RemainingTime
				.SkipLatestValueOnSubscribe()
				.Subscribe(x =>
				{
					startCountDownTimerText.text = "";

					if (x == 0)
					{
						countDownTimerText.text = "";
						startCountDownTimerText.text = "ウホ！(おわり！)";
					}
					else
					{
						countDownTimerText.text = $"{x}";
					}

					if (x == 10)
					{
						countDownTimerText.color = Color.red;
					}
				}).AddTo(this);

			gorillaManager.BananaQuantity
				.Subscribe(x =>
				{
					bananaQuantityText.text = $"{x}";
				}).AddTo(this);

			gorillaManager.EatenBananaQuantity
				.Subscribe(x =>
				{
					eatenBananaQuantityText.text = $"{x}";
				}).AddTo(this);
			
			gorillaManager.Satiety
				.Subscribe(x =>
				{
					satietySlider.value = x;
				}).AddTo(this);
		}
	}
}