using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Eyedrops.Scripts.Helper
{
	public class SliderHelper : MonoBehaviour, IPointerUpHandler
	{
		private Slider slider;
		
		protected readonly Subject<float> changeValueSubject = new Subject<float>();

		private void Awake()
		{
			slider = GetComponent<Slider>();
		}

		public IObservable<float> OnChangeValue => changeValueSubject;

		public void OnPointerUp(PointerEventData eventData)
		{
			changeValueSubject.OnNext(slider.normalizedValue);
		}
	}
}