using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]private Transform m_FollowPlayer;

    private void Update()
    {
        transform.position = m_FollowPlayer.position;
    }
}
