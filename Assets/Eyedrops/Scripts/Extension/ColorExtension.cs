using UnityEngine;

namespace Eyedrops.Scripts.Extension
{
	public static class ColorExtension
	{
		public static Color SetA(this Color color, float alpha)
		{
			return new Color(color.r, color.g, color.b, alpha);
		}
	}
}