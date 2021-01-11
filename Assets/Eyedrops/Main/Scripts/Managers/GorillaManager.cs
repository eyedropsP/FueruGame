using System;
using System.Collections.Generic;
using System.Linq;
using Eyedrops.Main.Scripts.Bananas;
using Eyedrops.Main.Scripts.Gorilla;
using Eyedrops.Scripts.Input;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Eyedrops.Main.Scripts.Managers
{
    public class GorillaManager : MonoBehaviour
    {
        [SerializeField] private GorillaStateManager gorillaStateManager = default;
        [SerializeField] private BananaEffectManager effectManager = default;
        
        private List<IBanana> bananas = new List<IBanana>();
        // ちから 力 パワー 破壊
        // private readonly ReactiveProperty<float> power = new ReactiveProperty<float>();
        // 満腹度
        private readonly ReactiveProperty<float> satiety = new ReactiveProperty<float>(0f);
        // バナナの数
        private readonly ReactiveProperty<int> bananaQuantity = new ReactiveProperty<int>();
        // 食べたバナナの数
        private readonly ReactiveProperty<int> eatenBananaQuantity = new ReactiveProperty<int>(0);
        // 食べられる状態フラグ
        private readonly ReactiveProperty<bool> canEat = new ReactiveProperty<bool>();
        // 嘔吐フラグ
        private readonly ReactiveProperty<bool> isSuffer = new ReactiveProperty<bool>();

        // public IReadOnlyReactiveProperty<float> Power => power;
        public IReadOnlyReactiveProperty<float> Satiety => satiety;
        public IReadOnlyReactiveProperty<int> BananaQuantity => bananaQuantity;
        public IReadOnlyReactiveProperty<int> EatenBananaQuantity => eatenBananaQuantity;
        public IReadOnlyReactiveProperty<bool> CanEat => canEat;
        
        [Inject] private IInputEventProvider inputEventProvider = default;
        [Inject] private GameStateManager gameStateManager = default;

        private void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(x => x.CompareTag("Banana"))
                .Subscribe(x =>
                {
                    x.TryGetComponent(out IBanana banana);
                    bananas.Add(banana);
                }).AddTo(this);
            
            this.OnTriggerExitAsObservable()
                .Where(x => x.CompareTag("Banana"))
                .Subscribe(x =>
                {
                    x.TryGetComponent(out IBanana banana);
                    bananas.Remove(banana);
                }).AddTo(this);
            
            inputEventProvider.SelectButton
                .Where(x => x && gameStateManager.CurrentState.Value == GameState.EAT)
                .Subscribe(_ =>
                {
                    if (gorillaStateManager.CurrentState.Value == GorillaState.Suffer || !canEat.Value)
                    {
                        return;
                    }
                    if (satiety.Value >= 100.0f)
                    {
                        satiety.Value = 100.0f;
                    }
                    else
                    {
                        var poolTarget = bananas.FirstOrDefault(x => x.IsActive.Value);
                        // ReSharper disable once PossibleNullReferenceException
                        poolTarget.Eat();            // 食う
                        effectManager.Play();        // エフェクト発生
                        bananas.Remove(poolTarget);  // 食ったIBananaはListから除外
                        // power.Value += 2.0f;         // ちからを2増やす
                        satiety.Value += 4.0f;       // 満腹度を4増やす
                        eatenBananaQuantity.Value++; // 食ったバナナの数を1増やす
                    }
                }).AddTo(this);
            
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    canEat.Value = bananas.Select(x => x).Any(x => x.IsActive.Value);
                    bananaQuantity.Value = bananas.Where(x => x.IsActive.Value).Select(x => x).Count();

                    // if (power.Value >= 100.0f)
                    // {
                    //     power.Value = .0f;
                    // }
                })
                .AddTo(this);
            
            Observable.Interval(TimeSpan.FromMilliseconds(300))
                .Where(_ => satiety.Value >= 0)
                .Subscribe(_ => satiety.Value -= 2.0f)
                .AddTo(this);

            // power.Where(x => x >= 100.0f)
            //     .Subscribe(_ => satiety.Value = .0f).AddTo(this);
            
            satiety.Where(x => x >= 100.0f)
                .Subscribe(_ =>
                {
                    DecreaseValue();
                }).AddTo(this);
        }

        /// <summary>
        /// 嘔吐(食べたバナナと満腹度が減少する)
        /// </summary>
        private void DecreaseValue()
        {
            Observable
                .Interval(TimeSpan.FromMilliseconds(200))
                .TakeWhile(_ => satiety.Value >= 0)
                .Subscribe(_ =>
                {
                    satiety.Value -= 10;
                    eatenBananaQuantity.Value--;
                }).AddTo(this);
        }
    }
}