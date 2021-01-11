using UniRx;

namespace Eyedrops.Main.Scripts.Bananas
{
	public interface IBanana
	{
		IReadOnlyReactiveProperty<bool> IsActive { get; }
		void Eat();
	}
}