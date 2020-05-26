using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

// Cette classe s'occupe de gérer la "hotbar" du joueur (la barre en bas de l'écran en jeu).
// Elle n'est pas sensé se trouver dans le jeu final, mais elle nous permet de tester et développer
// des fonctionnalités qui doivent intéragir avec l'inventaire, ou alors que l'action dépend de l'outil en main.
// Ainsi, vu que le jeu est actuellement en démo, je laisse ce script.
public class t_BasicInventory : MonoBehaviour
{
    public static t_BasicInventory Instance;

    [Header("Inventory Settings")]
    [SerializeField] private const int m_slotNumber = 7;
    [SerializeField] private Item[] m_hotbarItem = new Item[m_slotNumber];
    [SerializeField] private GameObject m_slotPrefab;

    [Header("References")]
    [SerializeField] private Transform m_hotbar;

    private int m_index = 0;
    private Image[] m_slotImage = new Image[m_slotNumber];

    private Color32 activeColor = new Color32(42, 96, 140, 188);
    private Color32 idleColor = new Color32(0, 0, 0, 107);

    [System.Serializable]
    public class Item
    {
        public string itemInternalName;
        public Sprite itemSprite;
    }

    /// <summary>
    /// On créer ce que l'on appelle un Singleton, c'est à dire que cette classe aura une seule instance, 
    /// et pas une de plus.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Item GetItem()
    {
        return m_hotbarItem[m_index];
    }

    void Start()
    {
        // On itère sur chaque objet contenu dans la variable m_hotbar, on crée un slot (emplacement) sur l'interface graphique
        // On y affiche l'image associé à l'objet du slot en question.
        for (int i = 0; i < m_hotbarItem.Length; i++)
        {
            GameObject slot = Instantiate(m_slotPrefab, m_hotbar);
            Image itemImage = slot.transform.GetChild(0).GetComponent<Image>();
            m_slotImage[i] = slot.GetComponent<Image>();

            itemImage.enabled = false;

            if (m_hotbarItem[i].itemSprite != null)
            {
                itemImage.enabled = true;
                itemImage.sprite = m_hotbarItem[i].itemSprite;
            }
        }
    }

    /// <summary>
    /// Selon le mouvement de la molette de la souris, on déplace l'index pour change l'objet en main.
    /// </summary>
    void Update()
    {
        int oldIndex = m_index;
        if (Input.mouseScrollDelta.y > 0)
        {
            m_index++;
            if (m_index >= m_hotbarItem.Length)
            {
                m_index = 0;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            m_index--;
            if (m_index < 0)
            {
                m_index = m_hotbarItem.Length - 1;
            }
        }
        UpdateHotbar(oldIndex, m_index);
    }

    /// <summary>
    /// On change la couleur de l'emplacement de l'ancien objet et aussi de l'emplacement du nouveau objet pour faire
    /// comprendre au joueur qu'il a changé d'objet en main.
    /// </summary>
    /// <param name="oldActiveIndex">L'ancien objet en main</param>
    /// <param name="newActiveIndex">Le nouveau objet en main</param>
    void UpdateHotbar(int oldActiveIndex, int newActiveIndex)
    {
        m_slotImage[oldActiveIndex].color = idleColor;
        m_slotImage[newActiveIndex].color = activeColor;
    }
}
