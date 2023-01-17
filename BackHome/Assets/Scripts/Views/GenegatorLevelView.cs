using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BackHome
{
    public class GenegatorLevelView : MonoBehaviour
    {
       public  Tilemap _tilemap;
        public Tile _tile;

        public int _higthMap;
        public int _widthMap; 

       [Range (0,100)] public int _fillPercent;
       [Range(0, 100)] public int _smoothPercent;

        public int[,] _map;
        public bool _borders; 
    }
}

