using UnityEngine;
using UnityEngine.Serialization;

namespace SO
{
    [CreateAssetMenu(fileName = "New GameAudio", menuName = "GameAudio", order = 0)]
    public class GameAudioScriptableObject : ScriptableObject
    {
        [SerializeField] private AudioClip _music;
        [SerializeField] private AudioClip _clickSfx;
        
        public AudioClip Music => _music;
        public AudioClip ClickSfx => _clickSfx;
    }
}