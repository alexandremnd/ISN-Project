using UnityEngine;

// On définis un espace de nom pour éviter d'entrer en confusion avec d'autres espace de nom.
// Il faut considérer les espaces de nom comme un module python tels que Numpy, Pillow.
namespace Utility.Math
{
    // On définis une structure avec des fonctions sur des vecteurs.
    // J'avais besoin de fonction pour faire la valeur absolue d'un vecteur (2D/3D), donc j'ai créer des fonctions pour cela.
    public struct Vector
    {
        public static Vector2 AbsVector(Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }

        public static Vector3 AbsVector(Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
    }
}
