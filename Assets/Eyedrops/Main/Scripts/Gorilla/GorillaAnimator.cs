using Eyedrops.Main.Scripts.Managers;
using Eyedrops.Scripts.Input;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Eyedrops.Main.Scripts.Gorilla
{
	public class GorillaAnimator : MonoBehaviour
	{
		public IReadOnlyReactiveProperty<bool> IsSuffer => isSuffer;
		public IReadOnlyReactiveProperty<bool> IsThrowUp => isThrowUp;
		
		[Inject] private IInputEventProvider inputEventProvider = default;

		[SerializeField] private Animator gorillaAnimator = default;
		[SerializeField] private GorillaManager gorillaManager = default;

		private readonly ReactiveProperty<bool> isSuffer = new ReactiveProperty<bool>(false);
		private readonly ReactiveProperty<bool> isThrowUp = new ReactiveProperty<bool>();
		private bool isLeft = true;

		private static readonly int EatRight = Animator.StringToHash("EatRight");
		private static readonly int EatLeft = Animator.StringToHash("EatLeft");
		private static readonly int Suffer = Animator.StringToHash("Suffer");
		private static readonly int Eating = Animator.StringToHash("Eating");

		private void Start()
		{
			var stateMachine = gorillaAnimator.GetBehaviour<ObservableStateMachineTrigger>();
			
			stateMachine.OnStateEnterAsObservable()
				.Where(x => x.StateInfo.IsName("Idle"))
				.Subscribe(_ =>
				{
					gorillaAnimator.ResetTrigger(EatRight);
					gorillaAnimator.ResetTrigger(EatLeft);
					gorillaAnimator.SetBool(Eating, false);
					isThrowUp.Value = false;
					isSuffer.Value = false;
				}).AddTo(this);

			stateMachine.OnStateEnterAsObservable()
				.Where(x => x.StateInfo.IsName("ThrowUp"))
				.Subscribe(_ =>
				{
					isThrowUp.Value = true;
				}).AddTo(this);

			inputEventProvider.SelectButton
				.Where(x => x && gorillaManager.CanEat.Value)
				.Subscribe(_ =>
				{
					if (isLeft)
					{
						gorillaAnimator.SetTrigger(EatLeft);
						isLeft = false;
					}
					else
					{
						gorillaAnimator.SetTrigger(EatRight);
						isLeft = true;
					}
					gorillaAnimator.SetBool(Eating, true);
				}).AddTo(this);
			
			gorillaManager.Satiety
				.Where(x => x >= 100.0f)
				.Subscribe(_ =>
				{
					gorillaAnimator.SetTrigger(Suffer);
					isSuffer.Value = true;
				}).AddTo(this);
		}
	}
}