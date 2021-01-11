using Cysharp.Threading.Tasks;
using Eyedrops.Main.Scripts.Managers;
using Eyedrops.Scripts.Extension;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Eyedrops.Main.Scripts.UI
{
	public class ResultPresenter : MonoBehaviour
	{
		[Inject] private GorillaManager gorillaManager = default;

		[SerializeField] private GameManager gameManager = default;
		[SerializeField] private GameObject resultPanelObj = default;
		[SerializeField] private Image resultPanelBackGroundImage = default;
		[SerializeField] private TextMeshProUGUI eatenBananaText = default;
		[SerializeField] private float fade = .5f;

		private RectTransform resultPanel;
		
		private void Start()
		{
			resultPanelObj.SetActive(true);
			resultPanel = resultPanelObj.GetComponent<RectTransform>();
			
			gameManager.IsResultShowing
				.Where(x => x)
				.Take(1)
				.Subscribe(_ =>
				{
					ShowResultPanelAsync().Forget();
				}).AddTo(this);
		}

		private async UniTaskVoid ShowResultPanelAsync()
		{
			await UniTask.Delay(800);

			var process1 = .0f;
			while (process1 < 0.5f)
			{
				process1 += Time.deltaTime / fade;
				resultPanelBackGroundImage.color = Color.black.SetA(process1);
				await UniTask.Yield();
			}
			
			var process2 = .0f;
			while (process2 < 1.0f)
			{
				process2 += Time.deltaTime / fade;
				resultPanel.localScale = new Vector3(process2, process2, process2);
				await UniTask.Yield();
			}

			await UniTask.Delay(1000);
			
			var process3 = 0;
			while (process3 < gorillaManager.EatenBananaQuantity.Value)
			{
				process3++;
				eatenBananaText.text = $"{process3}";
				await UniTask.Yield();
			}

			await UniTask.Delay(3000);
			gameManager.GoToResultAsync();
		}
	}
}