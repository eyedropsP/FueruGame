using Eyedrops.Scripts;
using JetBrains.Annotations;
using KanKikuchi.AudioManager;
using UnityEngine;

namespace Eyedrops.Title.Scripts
{
    public class TitleManager : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = true;
            BGMManager.Instance.Play(BGMPath.SEA_BIRD, .5f);
        }

        public void DisplayIntroduce(float bgmVolume, float seVolume, float voiceVolume)
        {
            
        }

        [UsedImplicitly]
        public void GotoStageScene()
        {
            SEManager.Instance.Play(SEPath.VSQ_JINGLE_066_ETHNIC, 1.0f, 0f, 3f);
            BGMManager.Instance.FadeOut(BGMPath.SEA_BIRD, 1.0f, () =>
            {
                SceneLoader.LoadScene(GameScene.StageScene.ToString());
            });
        }
    }
}