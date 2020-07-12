using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PlayerCollides : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start()
    {
        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            Destroy(collision.gameObject);
            GameManager.instance.Damage(1);
            var noise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = 5f;
            DOTween.To(() => noise.m_AmplitudeGain, (x) => noise.m_AmplitudeGain = x, 0, 0.5f);
        }
    }
}
