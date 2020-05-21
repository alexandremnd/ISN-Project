using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;


[RequireComponent(typeof(Interactive), typeof(AudioSource))]
public class OreNode : MonoBehaviour
{
    [Header("Node settings")]
    [SerializeField] private int m_hitBeforeBreak;
    [SerializeField] private GameObject[] m_droppedItem;

    [Space]
    [SerializeField] private AudioClip m_soundOnHit;
    [SerializeField] private AudioClip m_soundOnBreak;

    private AudioSource m_audioSource;
    private int m_hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // On règle un évènement, quand un script capte que le joueur effectue une "attaque" sur lui
        // Il appelle les fonctions enregistrer dans "m_actionOnAttack", et ici on demande à appeler "RegisterHit"
        this.GetComponent<Interactive>().m_actionOnAttack += RegisterHit;

        m_audioSource = GetComponent<AudioSource>();
        if (m_audioSource == null)
        {
            m_audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }

    // Note : Cette fonction est temporaire, pour le moment la fonction n'implémente pas l'inventaire.
    // Donc pour le moment, on fait apparaître les objets au sol (ce qui risque de causer des performances plus faibles)
    private void RegisterHit(Transform playerData)
    {
        // On enregistre un coup supplémentaires, on joue le son en adéquation avec la situation (si l'on tape ou si l'on a cassé l'objet)
        m_hitCount++;
        m_audioSource.clip = m_hitCount >= m_hitBeforeBreak ? m_soundOnBreak : m_soundOnHit;
        m_audioSource.Play();
        
        // On fait apparaître les ressources récoltables après avoir casser l'objet (arbre, rocher par exemple)
        if (m_hitCount >= m_hitBeforeBreak)
        {
            for (int i = 0; i < m_droppedItem.Length; i++)
            {
                GameObject go = Instantiate(m_droppedItem[i], this.transform.position + Vector3.up * i, Random.rotation);
                MeshCollider collider = go.AddComponent<MeshCollider>();
                go.AddComponent<Rigidbody>();
                collider.convex = true;
            }
            Destroy(this.gameObject, m_soundOnBreak.length);
        }
    }
}
