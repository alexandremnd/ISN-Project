using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameFile : MonoBehaviour
{
    public static GameFile Instance;

    private string m_gameFilePath;

    /// <summary>
    /// On créer les répertoires dans le dossier "Documents" de l'ordinateur afin d'enregister les paramètres du joueur.
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
            DontDestroyOnLoad(this);
        }

        m_gameFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\My Games\\";
        if (!Directory.Exists(m_gameFilePath))
        {
            Directory.CreateDirectory(m_gameFilePath);
        }

        m_gameFilePath += Application.productName + "\\";
        if (!Directory.Exists(m_gameFilePath))
        {
            Directory.CreateDirectory(m_gameFilePath);
        }
    }

    /// <summary>
    /// Retourne le fichier demandé si il existe, sinon, créer le fichier.
    /// </summary>
    /// <param name="fileName">Nom du fichier dans le dossier"My Games/Low-Poly Surival/". Ne pas oublier l'extension</param>
    /// <returns></returns>
    public string ReadFile(string fileName)
    {
        if (File.Exists(m_gameFilePath + fileName))
        {
            return File.ReadAllText(m_gameFilePath + fileName);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Ecrit un fichier à l'emplacement indiqué et avec le contenu fournis.
    /// </summary>
    /// <param name="fileName">Le nom du fichier (avec son chemin)</param>
    /// <param name="content">Le contenu du fichier</param>
    public void WriteFile(string fileName, string content)
    {
        File.WriteAllText(m_gameFilePath + fileName, content);
    }
}
