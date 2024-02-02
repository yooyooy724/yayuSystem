using UnityEngine;
using yayu.UI;

public class ClickSound : MonoBehaviour
{
    [SerializeField] string audioSourceTag;
    
    UIButtonMono btn;
    AudioSource audio;
    private void OnEnable()
    {
        audio = GameObject.FindWithTag(audioSourceTag).GetComponent<AudioSource>();
        if (audio == null) { Debug.LogWarning("audio source is null"); return; }
        if (audio.clip == null) { Debug.LogWarning("audio clip is null"); return; }

        btn = GetComponent<UIButtonMono>();
        btn.AddListener_Click(() => 
        { 
            audio.Play(); 
            //Debug.Log("played");
        });
    }
}
