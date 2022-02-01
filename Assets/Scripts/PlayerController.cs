using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject[] GunMass;
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Transform[] wayPoints;
    private Animator animator;
    private PoolerObj _poolerBullet;
    private int indexPoint = 0;

    public static bool isMovable = true;
    public static bool isShootable;

    private KeyCode[] keysWeapon =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
    };

    void Start()
    {
        wayPoints = FindObjectOfType<GameManager>().wayPoints;
        _poolerBullet = PoolerObj.Instance;
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapon();
        PCControl();
#if UNITY_ANDROID
        MobileControl();
#endif
        if (Vector3.Distance(transform.position, wayPoints[indexPoint].position) < 0.3f)
        {
            animator.SetTrigger("Idle");
            isMovable = false;
            isShootable = true;
            if (indexPoint < 2)
                ++indexPoint;
            Debug.Log(indexPoint);
        }
    }
        
    private void PCControl()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (isMovable)
            {
                agent.SetDestination(wayPoints[indexPoint].position);
                animator.SetTrigger("Run");
            }
            if (isShootable)
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    RotatePlayerToShoot(hit.point);
                    Shoot();
                }
            }
        }
    }

    private void MobileControl()
    {
        if (Input.touchCount > 0)
        {

            if (isMovable)
            {
                agent.SetDestination(wayPoints[indexPoint].position);
                animator.SetTrigger("Run");
            }
            if (isShootable)
            {
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.GetTouch(0).position), out hit))
                {
                    RotatePlayerToShoot(hit.point);
                    Shoot();
                }
            }
        }
    }
    private void ChangeWeapon()
    {
        for(int i = 0; i< GunMass.Length; i++)
        {
            if(Input.GetKeyDown(keysWeapon[i]))
            {
                GunMass[i].SetActive(true);
                DeactivWeaponsBesides(GunMass[i]);
            }
        }
    }
    private void DeactivWeaponsBesides(GameObject weapon)
    {
        GunMass.ToList().Where(x => x != weapon).ToList().ForEach(x => x.gameObject.SetActive(false));
    }

    private void RotatePlayerToShoot(Vector3 hit)
    {
        Vector3 mouseWorld = new Vector3(hit.x, transform.position.y, hit.z);                           
        Vector3 forward = mouseWorld - transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    private void Shoot() 
    {
        foreach(GameObject gun in GunMass)
        {
            if(gun.activeInHierarchy)
                _poolerBullet.SpawnFromPool("Bullet", gun.transform.Find("ShootPoint").transform.position, gun.transform.rotation);
        }
    }

}
