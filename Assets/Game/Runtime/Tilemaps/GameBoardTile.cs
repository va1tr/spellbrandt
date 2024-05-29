using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Spellbrandt
{
    [System.Serializable]
    public class GameBoardTile : Tile
    {
        [SerializeField]
        private Affinity affinity;

        public Affinity Affinity
        {
            get => affinity;
            set => affinity = value;
        } 

#if UNITY_EDITOR
        [MenuItem("Board Tile", menuItem = "Assets/Create/Custom Tile/Board Tile", priority = 380)]
        public static void CreateBoardTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Board Tile", "New Board Tile", "Asset", "Save Board Tile", "Assets/Tilemaps/Custom Tiles");

            if (path == string.Empty)
            {
                return;
            }

            AssetDatabase.CreateAsset(CreateInstance<GameBoardTile>(), path);
        }
#endif

    }

#if UNITY_EDITOR

    [CustomEditor(typeof(GameBoardTile))]
    public class BoardTileEditor : Editor
    {
        private SerializedProperty spriteProperty;

        private SerializedProperty colorProperty;
        private SerializedProperty colliderTypeProperty;

        private SerializedProperty affinityProperty;

        private GameBoardTile tile { get { return target as GameBoardTile; } }

        private void OnEnable()
        {
            spriteProperty = serializedObject.FindProperty("m_Sprite");

            colorProperty = serializedObject.FindProperty("m_Color");
            colliderTypeProperty = serializedObject.FindProperty("m_ColliderType");

            affinityProperty = serializedObject.FindProperty("affinity");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(spriteProperty);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(colorProperty);
            EditorGUILayout.PropertyField(colliderTypeProperty);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(affinityProperty);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(tile);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

#endif
}
