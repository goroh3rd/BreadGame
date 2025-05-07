using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
