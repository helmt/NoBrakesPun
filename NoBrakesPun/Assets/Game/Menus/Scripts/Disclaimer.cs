using UnityEngine;

public class Disclaimer : MonoBehaviour
{
    float countDown;
    public GameObject startScreen;
    public AudioSource music;
    public GameObject text;

    void Start()
    {
        music.Stop();
        countDown = 7f;
    } 

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 2f)
        {
            text.SetActive(false);
        }
        if (countDown <= 0f)
        {
            gameObject.SetActive(false);
            startScreen.SetActive(true);
            music.Play();
        }
    }
}
