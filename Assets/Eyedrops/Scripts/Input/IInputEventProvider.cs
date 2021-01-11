using UniRx;

namespace Eyedrops.Scripts.Input
{
	public interface IInputEventProvider
	{
		IReadOnlyReactiveProperty<bool> SelectButton { get; }
		IReadOnlyReactiveProperty<bool> BackButton { get; }
		IReadOnlyReactiveProperty<bool> SubButton { get; }
	}
}