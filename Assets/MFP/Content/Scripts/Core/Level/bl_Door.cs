using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PhotonView))]
public class bl_Door : bl_PhotonHelper {

    public Transform Door = null;
    public Vector3 From = Vector3.zero;
    public Vector3 To = Vector3.zero;
    [Range(0.1f, 50f)]
    public float Lerp = 8f;
    [Separator("Text")]
    [TextArea(1,3)]public string OpenText;
    [TextArea(1,3)]public string CloseText;
    [Separator("References")]
    public AudioClip SoundEffect = null;
    public GameObject DoorText = null;

    private bool Open = false;
    private bool m_Into = false;
    private PhotonView view = null;
    private Text m_Text = null;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        view = PhotonView.Get(this);
        if (DoorText != null) { m_Text = DoorText.GetComponent<Text>(); }
    }
    /// <summary>
    /// 
    /// </summary>
    [PunRPC]
    public void DoorEvent()
    {
        Open = !Open;
        if (SoundEffect != null) { Source.clip = SoundEffect; Source.Play(); }
        if (Open) { if (m_Text != null) { m_Text.text = CloseText; } }
        else { if (m_Text != null) { m_Text.text = OpenText; } }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == bl_PlayerPhoton.PlayerTag && isMine)
        {
            m_Into = true;
            DoorText.SetActive(true);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == bl_PlayerPhoton.PlayerTag && isMine)
        {
            m_Into = false;
            DoorText.SetActive(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    void SyncDoor()
    {
        //Use OthersBuffered to save some bandwidth, as well we call the function locally.
        view.RPC("DoorEvent", PhotonTargets.OthersBuffered);
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Door == null)
        {
            Debug.LogError("The door transform not yet been assigned to: "+ gameObject.transform.parent.name +"/"+gameObject.name);
            this.enabled = false;
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.F) && m_Into)
        {
            SyncDoor();
            DoorEvent();
        }
        //Get The current Position of door.
        Vector3 pos = Door.transform.position;

        
        if (Open)
        {
            pos = Vector3.Lerp(pos, To, Time.deltaTime * Lerp);
        }
        else
        {
            pos = Vector3.Lerp(pos, From, Time.deltaTime * Lerp);
        }

        Door.transform.position = pos;
    }
    /// <summary>
    /// 
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = bl_ColorHelper.MFPColorWihtAlpha(0.4f);
        Gizmos.DrawCube(this.GetComponent<Transform>().position, this.GetComponent<Transform>().localScale);
        Gizmos.color = bl_ColorHelper.MFPColorWihtAlpha(0.8f);
        Gizmos.DrawWireSphere(From, 1.7f);
        Gizmos.DrawWireSphere(To, 1.7f);
        Gizmos.color = bl_ColorHelper.MFPColorWihtAlpha(0.3f);
        Gizmos.DrawSphere(From, 1.7f);
        Gizmos.DrawSphere(To, 1.7f);
        Gizmos.color = bl_ColorHelper.MFPColorWihtAlpha(1);
        Gizmos.DrawLine(From, To);
    }


    [ContextMenu("Get From")]
    void GetFrom() { From = Door.transform.position; }
    [ContextMenu("Get To")]
    void GetTo() { To= Door.transform.position; }
}