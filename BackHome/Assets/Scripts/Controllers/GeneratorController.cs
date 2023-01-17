using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BackHome
{
public class GeneratorController 
{
    // 1 заполнение карты = FillMap
    //2 сглаживание карты (SmoothMap) - в зависимости от окружения. Если все вокруг 1 - то и текущая позиция 1
    //3 отрисовка тайлов (DrawTiles)


    private Tilemap _tilemap;
    private Tile _tile;

    private int _higthMap; //высота тайловой карты
    private int _widthMap; //ширина карты

    private int _fillPercent; //процент заполнения
    private int _smoothPercent; // процент при котором будем сглаживать карту

    private int[,] _map;
    private bool _borders; //галочка для бурдюра

    public  GeneratorController(GenegatorLevelView view )
    {
            _tilemap = view._tilemap;
            _tile = view._tile;

            _higthMap = view._higthMap;
            _widthMap = view._widthMap;
            _fillPercent = view._fillPercent;
            _smoothPercent = view._smoothPercent;

            _map = new int[_widthMap, _higthMap];
            _borders = view._borders; 
    }

    public void Start()
    {
            FillMap();
            for (int i = 0; i < _smoothPercent; i++)
            {
                SmoothMap();
                
            }
          DrawTiles();
    }

    private void FillMap()
    {
        for (int x = 0; x < _widthMap; x++)
        {
            for (int y = 0; y < _higthMap; y++)
            {
                    if (x == 0 ||x== _widthMap-1||y==0||y== _higthMap - 1)
                    {
                        if(_borders)
                        {
                            _map[x, y] = 1;
                        }
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0; 
                    }
            }
        }
            
    }

    private void SmoothMap()
    {
            for (int x = 0; x < _widthMap; x++)
            {
                for (int y = 0; y < _higthMap; y++)
                {
                    int neighbours = CetNeighbours(x,y);
                    if (neighbours > 4)
                    {
                        _map[x, y] = 1;
                   
                    }
                    if (neighbours < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
          

    }
    private int CetNeighbours(int x, int y)
        {
            int neighbours = 0;
            for (int gridX = x-1; gridX <= x+1; gridX++)
            {
                for (int gridY = y-1; gridY <= y+1; gridY++)
                {
                     if (gridX>=0&& gridX < _widthMap && gridY >= 0 && gridY < _higthMap )
                    {
                        if (gridX!=x|| gridY != y)
                        {
                            neighbours += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighbours++;
                    }

                }
            }

            return neighbours;
        }
        private void DrawTiles()
        {
            if (_map == null) return;

            for (int x = 0; x < _widthMap; x++)
            {
                for (int y = 0; y < _higthMap; y++)
                {
                    if (_map[x, y] == 1)
                    {
                        Vector3Int tilePosition = new Vector3Int (_widthMap/2+x, _higthMap/2+y,0);
                        _tilemap.SetTile(tilePosition, _tile);
                    }
                }
            }
        }
  }
}
