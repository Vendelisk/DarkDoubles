using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject fogPlane;
    public Transform playerT;
    public float radius = 5f;
    public LayerMask fogLayer;
    private float sqrRad { get { return radius * radius; } }

    private Mesh fogMesh;
    private Vector3[] fogVert;
    private Color[] fogColor;

    // Use this for initialization
    //void Awake()
    //{
    //    Initialize();
    //    if (GameObject.FindGameObjectWithTag("Player") != null)
    //        playerT = GameObject.FindGameObjectWithTag("Player").transform;
    //}

    #region Singleton

    private static Minimap instance;
    public static Minimap Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        Initialize();

        if (PersistentValues.Instance.fogColor.Length > 0)
            this.fogColor = PersistentValues.Instance.fogColor;

        if (GameObject.FindGameObjectWithTag("Player") != null)
            playerT = GameObject.FindGameObjectWithTag("Player").transform;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        if (playerT == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
                playerT = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        //Debug.DrawLine(transform.position, playerT.position, Color.red);
        Vector3 vertPlayerRay = new Vector3(playerT.position.x, transform.position.y, playerT.position.z);
        Debug.DrawRay(vertPlayerRay, playerT.position - vertPlayerRay, Color.green);
        Ray r = new Ray(vertPlayerRay, playerT.position - vertPlayerRay);
        //Ray r = new Ray(transform.position, playerT.position - transform.position); // THIS IS THE UNIVERSAL WAY (but draws angled lines which sucks)
        if (Physics.Raycast(r, out RaycastHit hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            for (int i = 0; i < fogVert.Length; i++)
            {
                Vector3 v = fogPlane.transform.TransformPoint(fogVert[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < sqrRad)
                {
                    float alpha = Mathf.Min(fogColor[i].a, dist / sqrRad);
                    fogColor[i].a = alpha;
                    PersistentValues.Instance.fogColor = fogColor;
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        fogLayer = LayerMask.GetMask("FogOfWar", "Terrain");
        fogMesh = fogPlane.GetComponent<MeshFilter>().mesh;
        fogVert = fogMesh.vertices;
        fogColor = new Color[fogVert.Length];
        for (int i = 0; i < fogColor.Length; i++)
        {
            fogColor[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        fogMesh.colors = fogColor;
    }
}