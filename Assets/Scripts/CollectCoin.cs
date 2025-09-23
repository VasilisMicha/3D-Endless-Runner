using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFx;
    void OnTriggerEnter(Collider other)
    {
        coinFx.Play();
        MasterInfo.coinCount += 1;
        this.gameObject.SetActive(false);
        if (MasterInfo.coinCount == 20)
        {
            SceneManager.LoadScene(3);
            MasterInfo.coinCount = 0;
        } 
    }

}
