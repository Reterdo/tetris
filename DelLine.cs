using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets
{
    internal class DelLine : Rule
    {
        private void Start()
        {
            score = 0;
        }

        public UnityEngine.UI.Text UIscore;
        public int score
        {
            get { return int.Parse(UIscore.text); }
            set { UIscore.text = value.ToString(); }

        } 
        
        public static Vector2 stPoint = new Vector2(-4, -6);
        public static  int height = 16;//высота
        public static int width = 9;//ширина
        private static Transform[,] grid = new Transform[width, height];//массив всего поля
        //реализовать удаление линии на одной высоте
        //Выложить на GitHub

        public override void ExecuteRule()
        {
            UpdateGrid();
            CheckLine();
        }

        void CheckLine() //проверяет линию. если линия заполнена,удалить и остатки передвинуть вниз
        {
            for(int i = height-1; i >= 0; i--)
            {
                if (HasLine(i))
                {
                    DeleteLine(i);
                    score += 100;
                    RawDown(i);
                }
            }
        }

        bool HasLine(int i)//проверяет есть ли заполненная линия
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j, i] == null)
                    return false;
            }
            return true;
        }
        void DeleteLine(int i) //удаляет линию.
        {
            for(int j = 0; j < width; j++)
            {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
            }
        }
        void UpdateGrid()
        {
            foreach(Part part in FindObjectsOfType<Part>())
            {
                if (!ContainsPart(part))
                {
                    Vector2 position = (Vector2) part.transform.position - stPoint;
                    grid[(int)position.x,(int) position.y] = part.transform;

                }
            }
        }

        bool ContainsPart(Part part)
        {
            for (int y = 0; y < height; y++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[j, y] == part)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void RawDown(int i)//передвигает остатки вниз
        {
            for(int y = i; y < height; y++)
            {
                for(int j = 0; j < width; j++)
                {
                    if (grid[j,y] != null)
                    {
                        grid[j, y - 1] = grid[j, y];
                        grid[j, y] = null;
                        grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                    }
                }
            }
        }
    }
}
