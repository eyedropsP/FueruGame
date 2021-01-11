using Eyedrops.Scripts;
using JetBrains.Annotations;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace Eyedrops.Result.Scripts
{
	public class ResultManager : MonoBehaviour
	{
		private void Start()
		{
			BGMManager.Instance.Play(BGMPath.JANGLE_LOOP);
		}

		[UsedImplicitly]
		public void GotoTitleScene()
		{
			SEManager.Instance.Play(SEPath.VSQ_JINGLE_066_ETHNIC, 1.0f, 0f, 3f);
			BGMManager.Instance.FadeOut(BGMPath.JANGLE_LOOP, 1.0f, () =>
			{
				SceneLoader.LoadScene(GameScene.TitleScene.ToString());
			});
		}
		
		[UsedImplicitly]
		public void GotoStageScene()
		{
			SEManager.Instance.Play(SEPath.VSQ_JINGLE_066_ETHNIC, 1.0f, 0f, 3f);
			BGMManager.Instance.FadeOut(BGMPath.JANGLE_LOOP, 1.0f, () =>
			{
				SceneLoader.LoadScene(GameScene.StageScene.ToString());
			});
		}
	}
}