using UnityEngine;

public class separate_animators_test : MonoBehaviour
{
    
    


    public Animator legs_animator;
    public Animator arms_animator;
    public Animator torso_animator;
    private Animator[] animators;

    public bool walking = false;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animators = GetComponentsInChildren<Animator>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartedWalking()
    {
        
        // animators[1].Play("run", 0, 0f);
        // animators[2].Play("run", 0, 0f);
        // animators[0].Play("run", 0, 0f);
        
        animators[1].SetBool("isWalking", true);
        animators[2].SetBool("isWalking", true);
        animators[0].SetBool("isWalking", true);
        walking = true;
        // for (int i = 0; i < animators.Length; i++)
        // {
        //     Debug.Log("walking");
        //     animators[i].SetBool("isWalking", true);
        //     walking = true;
        // }
    }

    public void StoppedWalking()
    {
    //     animators[1].Play("idle", 0, 0f);
    //     animators[2].Play("idle", 0, 0f);
    //     animators[0].Play("idle", 0, 0f);
        animators[1].SetBool("isWalking", false);
        animators[2].SetBool("isWalking", false);
        animators[0].SetBool("isWalking", false);
        walking = false;
        // for (int i = 0; i < animators.Length; i++)
        // {
        //     Debug.Log("not walking");
        //     animators[i].SetBool("isWalking", false);
        //     walking = false;
        // }
    }
}
