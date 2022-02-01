using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class NPSList
    {
        public List<GameObject> NPSs;
    }

    [SerializeField] private List<NPSList> NPSGroope;
    public Transform[] wayPoints;

    private int i = 0;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (i == NPSGroope.Count) Finish();
        if (!PlayerController.isMovable && NPSGroope[i].NPSs.All(s => s == null))
        {
            if (i < NPSGroope.Count)
            {
                Debug.Log("NE soderzhit");
                PlayerController.isMovable = true;
                PlayerController.isShootable = false;
                i++;
            }
        }
    }
    private void Finish()
    {
        SceneManager.LoadScene(0);
    }

}
