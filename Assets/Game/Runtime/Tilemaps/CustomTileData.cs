using UnityEngine;

namespace Spellbrandt
{
    public class CustomTileData
    {
        public GameObject GameObject { get; set; }

        public Affinity Affinity { get; set; }

        public Vector3 WorldPosition { get; set; }
        public Vector3Int CellPosition { get; set; }
    }
}
