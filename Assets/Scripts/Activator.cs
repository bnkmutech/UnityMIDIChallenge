using System.Collections;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note,gm;
    Color old ;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    
    void Start()
    {
        gm = GameObject.Find("GameManager");
        old=sr.color;
    }

    // Update is called once per frame
    // เลือกปุ่มสำหรับกดเมื่อโน๊ตมาหา A,S,D,F,G,H
    void Update()
    {
        if(Input.GetKeyDown(key))
         StartCoroutine(Pressed());

        // ถ้าคลิ๊กโดนให้ทำลายและนับคะแนน
        if(Input.GetKeyDown(key)&&active){
            Destroy(note);
            AddScore();
            active=false;
        }
    }

    //เลือกเฉพาะ gamobject ที่แท๊ก Note
    void OnTriggerEnter2D(Collider2D col)
    {
        active=true;
        if(col.gameObject.tag=="Note")
        note = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        active=false;
    }

    //ใส่คะแนนใน Text ที่ชื่อ Score
    void AddScore()
    {
        PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+gm.GetComponent<GameManager>().GetScore());
    }

    // เมื่อคลิ๊กปุ่ม A,S,D,F,G,H ให้เปลี่ยนสี
    IEnumerator Pressed()
    {
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.1f);
        sr.color = old;
    }
}
