using UnityEngine;

public class Foot_placement : MonoBehaviour
{
    public Animator animator;
    public LayerMask checkditection;
    [Range(0f,1f)]
    public float distanceToGround;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            //left
            RaycastHit hit;
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            Debug.DrawRay(ray.origin, Vector3.down, Color.red);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, checkditection))
            {
                if (hit.transform.tag == "Walkable")
                {
                    
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation( transform.forward,hit.normal));

                }
             
            }

            //right
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            Debug.DrawRay(ray.origin, Vector3.down, Color.red);
            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, checkditection))
            {
                if (hit.transform.tag == "Walkable")
                {

                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

                }

            }

        }
        
    }
}
