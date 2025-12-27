using System.Collections;
using UnityEngine;

public class ClickFX : MonoBehaviour
{
    [SerializeField] ParticleSystem fx;
    float duration => fx.main.duration;


    private void Start()
    {
        StartCoroutine(KillFX());
    }

    IEnumerator KillFX()
    {
        yield return new WaitForSeconds(duration);

        Destroy(fx.gameObject);
    }
}
