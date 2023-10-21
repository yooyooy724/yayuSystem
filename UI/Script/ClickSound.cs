using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] string audioSourceTag;
    
    BUTTON btn;
    AudioSource audio;
    private void OnEnable()
    {
        audio = GameObject.FindWithTag(audioSourceTag).GetComponent<AudioSource>();
        if (audio == null) { Debug.LogWarning("audio source is null"); return; }
        if (audio.clip == null) { Debug.LogWarning("audio clip is null"); return; }

        btn = GetComponent<BUTTON>();
        btn.AddListener_onClick(() => 
        { 
            audio.Play(); 
            //Debug.Log("played");
        });
    }
}
