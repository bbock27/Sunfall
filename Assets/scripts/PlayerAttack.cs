using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public GameObject shield;

    private bool IsSlashing = false;

    private CapsuleCollider2D swordCollider;

    public void OnSlash()
    {
        if (!IsSlashing)
        {
            IsSlashing = true;
            //parameter is the amount of time for the swing
            StartCoroutine(Swing(0.25f));
        }
        
        
    }

    private IEnumerator Swing(float time)
    {
        float elapsedTime = 0.0f;
        swordCollider.enabled = true;
        Quaternion startingRotation = Quaternion.Euler ( new Vector3 ( 0.0f, 0.0f, 0.0f ) );
        Quaternion targetRotation =  Quaternion.Euler ( new Vector3 ( 0.0f, 0.0f, 100f ) );
        while (elapsedTime < time) {
            print(elapsedTime);
            elapsedTime += Time.deltaTime;
            // Rotations
            sword.transform.localRotation = Quaternion.Lerp(startingRotation, targetRotation,  elapsedTime / time  );
            yield return new WaitForEndOfFrame();
        }
        //reset everything back for next time
        sword.transform.localRotation = startingRotation;
        IsSlashing = false;
        swordCollider.enabled = false;
    }

    public void Awake()
    {
        swordCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){

        if(this.GameObject.tag == "Player" && other.GameObject.tag == "Enemy"){
            //do something lol
        }
    }
}
