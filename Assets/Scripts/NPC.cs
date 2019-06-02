using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    public Transform NPCTarget;
    public string Name;
    [TextArea(3, 20)]
    public string[] Sentences;

    private DialougeSystem dialougeSystem;
    
    void Start()
    {
        dialougeSystem = FindObjectOfType<DialougeSystem>();
    }
    
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(NPCTarget.position);
        pos.y += 175;
        ChatBackGround.position = pos;
    }

    public void OnTriggerStay(Collider collider)
    {
        this.gameObject.GetComponent<NPC>().enabled = true;
        FindObjectOfType<DialougeSystem>().EnterRangeOfNPC(this.gameObject);
        if((collider.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<NPC>().enabled = true;
            dialougeSystem.Names = Name;
            dialougeSystem.dialougeLines = Sentences;
            FindObjectOfType<DialougeSystem>().NPCName();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<DialougeSystem>().OutOfRange();
        this.gameObject.GetComponent<NPC>().enabled = false;
    }
}
