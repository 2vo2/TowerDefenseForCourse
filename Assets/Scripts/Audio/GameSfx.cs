using UnityEngine;

public class GameSfx : ISfxEmitter
{
    public void PlaySfx(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
