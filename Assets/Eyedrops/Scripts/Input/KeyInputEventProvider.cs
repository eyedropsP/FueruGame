using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Eyedrops.Scripts.Input
{
	public class KeyInputEventProvider : MonoBehaviour, IInputEventProvider
	{
		private readonly ReactiveProperty<bool> selectButton = new ReactiveProperty<bool>();
		private readonly ReactiveProperty<bool> backButton = new ReactiveProperty<bool>();
		private readonly  ReactiveProperty<bool> subButton = new ReactiveProperty<bool>();
		
		public IReadOnlyReactiveProperty<bool> SelectButton => selectButton;
		public IReadOnlyReactiveProperty<bool> BackButton => backButton;
		public IReadOnlyReactiveProperty<bool> SubButton => subButton;

		private void Start()
		{
			// 決定ボタン & 食べるボタン
			this.UpdateAsObservable()
				.Select(_ => UnityEngine.Input.GetButtonDown("Submit"))
				.DistinctUntilChanged()
				.Subscribe(x => selectButton.Value = x);
			
			// キャンセルボタン
			this.UpdateAsObservable()
				.Select(_ => UnityEngine.Input.GetButtonDown("Cancel"))
				.DistinctUntilChanged()
				.Subscribe(x => backButton.Value = x);

			// サブボタン
			this.UpdateAsObservable()
				.Select(_ => UnityEngine.Input.GetButtonDown("Sub"))
				.DistinctUntilChanged()
				.Subscribe(x => subButton.Value = x);

			selectButton.AddTo(this);
			backButton.AddTo(this);
			subButton.AddTo(this);
		}
	}
}