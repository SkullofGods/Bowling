using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Butilki
    {
        public Vector3 position;
        public Rigidbody obj;
    }
    [SerializeField] Image _force;
    private Vector2 start;
    [SerializeField]
    private Rigidbody ball;   
    private Vector3 StartPosition;
    [SerializeField]
    private Butilki[] butilki;
    [SerializeField]
    private Transform butilkiParent;
    [SerializeField]
    private TMP_Text  scor_text;
    private bool ball_active = true;
    private int scor;
    private float velocity;
    private float dir;
    [SerializeField] private AudioSource _roll;
    private AudioSource _ball;
    [SerializeField]
    private AudioClip _ballClip;
    [SerializeField]
    private AudioClip _butilkiClip;
    [SerializeField]
    private GameObject _arrow;
   void Start()
    {
        _ball = gameObject.AddComponent<AudioSource>();
        _ball.playOnAwake = false;
        StartPosition = ball.transform.position;
        Finish.getParams = GetData;
        for(int i = 0; i < butilki.Length; i++)
        {
            butilki[i].position = butilki[i].obj.transform.localPosition;
          
        }
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Drag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MoveBall();
        }
    }
    void Drag()
    {
        if (!ball_active)
            return;
        velocity = Mathf.Clamp((start.y - Input.mousePosition.y) / 10f, 5f, 40f);
        _force.fillAmount = 1 - (35 - velocity) / 35;
        dir = Mathf.Clamp((start.x - Input.mousePosition.x) / 10, -5f, 5f);
        ball.transform.eulerAngles = new Vector3(0, dir, 0);
        _arrow.transform.eulerAngles = new Vector3(0, dir, 0);
    }
    void MoveBall()
    {
        if(!ball_active)
            return;
        _arrow.SetActive(false);
        _ball.PlayOneShot(_ballClip);
        _roll.Play();
        scor = 0;
        scor_text.text = scor.ToString();
        ball_active = false;
        ball.isKinematic = false;
        ball.velocity = ball.transform.forward * velocity;
        StartCoroutine(CheckRbMagnitude());
    }
   
    void GetData()
    {
        _ball.PlayOneShot(_butilkiClip);
        _roll.Stop();
        
       
        ball.isKinematic = true;
        ball.transform.position = Vector3.down * 10;
    }
    IEnumerator CheckRbMagnitude()
    {
        while(ball.velocity.magnitude > 0.5f)
        {
            
            yield return null;  
        }
        _roll.Stop();
      
        ball.isKinematic = true;
        ball.transform.position = Vector3.down * 10;
        StartCoroutine(ResetButilki());
    }
    IEnumerator ResetButilki()
    {
        
        yield return new WaitForSeconds(1);
        for (int i = 0; i < butilki.Length; i++)
        {

            if (butilki[i].obj.transform.eulerAngles.x > 2f)
            {
                scor++;
            }
        }
        scor_text.text = scor.ToString();
            yield return new WaitForSeconds(1);
        float y = 3f;
        butilkiParent.position = new Vector3(0, 3, 0);
       
        for (int i = 0; i < butilki.Length; i++)
        {          
            butilki[i].obj.isKinematic = true;
            butilki[i].obj.transform.rotation = Quaternion.identity;
            butilki[i].obj.transform.localPosition = butilki[i].position;                  
        }
        while (y > 0)
        {
            y -= Time.deltaTime * 2;
            butilkiParent.position = new Vector3(0, y, 0);
            yield return null;
        }
        y = 0;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < butilki.Length; i++)
        {          
            butilki[i].obj.isKinematic = false;
        }
        _arrow.SetActive(true);
        ball.transform.rotation = Quaternion.identity;
        ball.transform.position = StartPosition;
        _arrow.transform.rotation = Quaternion.identity;
        ball_active = true;
    }
}
