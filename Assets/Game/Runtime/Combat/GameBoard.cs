#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Spellbrandt
{
    public class GameBoard : Singleton<GameBoard>
    {
        [SerializeField]
        private Tilemap LHSTilemap;

        [SerializeField]
        private Tilemap RHSTilemap;

        private readonly Dictionary<Vector3, CustomTileData> _tiles = new Dictionary<Vector3, CustomTileData>();

        protected override void Initialise()
        {
            CreateBoardLayout(LHSTilemap);
            CreateBoardLayout(RHSTilemap);
        }

        private void CreateBoardLayout(Tilemap tilemap)
        {
            foreach (var cellPosition in tilemap.cellBounds.allPositionsWithin)
            {
                if (tilemap.HasTile(cellPosition))
                {
                    var tile = tilemap.GetTile(cellPosition) as GameBoardTile;

                    var worldPosition = tilemap.GetCellCenterWorld(cellPosition);

                    CustomTileData tileData = new CustomTileData()
                    {
                        Affinity = tile.Affinity,

                        CellPosition = cellPosition,
                        WorldPosition = worldPosition
                    };

                    _tiles.Add(cellPosition, tileData);
                }
            }
        }

        public void AddGameObjectToTile(Vector3 cellPosition, GameObject gameObject)
        {
            if (_tiles.TryGetValue(cellPosition, out CustomTileData tileData) && gameObject != null)
            {
                tileData.GameObject = gameObject;
            }
        }

        public void RemoveGameObjectFromTile(Vector3 cellPosition)
        {
            if (_tiles.TryGetValue(cellPosition, out CustomTileData tileData))
            {
                tileData.GameObject = null;
            }
        }

        public bool HasTile(Vector3Int cellPosition)
        {
            if (_tiles.TryGetValue(cellPosition, out CustomTileData tileData))
            {
                return tileData.Affinity != Affinity.None;
            }

            return false;
        }

        public bool CanMoveToTile(Vector3Int cellPosition, Affinity affinity)
        {
            if (_tiles.TryGetValue(cellPosition, out CustomTileData tileData))
            {
                if (tileData.GameObject == null && tileData.Affinity == affinity)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryGetTileGameObject(Vector3Int cellPosition, out GameObject gameObject)
        {
            gameObject = null;

            if (_tiles.TryGetValue(cellPosition, out CustomTileData tileData))
            {
                if (tileData.GameObject != null)
                {
                    gameObject = tileData.GameObject;

                    return true;
                }
            }

            return false;
        }

        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return LHSTilemap.WorldToCell(worldPosition);
        }

        public Vector3 GetCellCenterWorld(Vector3Int cellPosition)
        {
            return LHSTilemap.GetCellCenterWorld(cellPosition);
        }
    }
}
