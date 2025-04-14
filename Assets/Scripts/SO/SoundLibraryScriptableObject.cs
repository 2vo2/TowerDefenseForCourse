using UnityEngine;

[CreateAssetMenu(menuName = "SoundLibrary", fileName = "New Sound Library", order = 0)]
public class SoundLibraryScriptableObject : ScriptableObject
{
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _buttonClickSound;
}
