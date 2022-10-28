using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    internal class DelLine : Rule
    {
        public static  int height = 15;//высота
        public static int width = 8;//ширина
        private static Transform[,] grid = new Transform[width, height];//массив всего поля
        //реализовать удаление линии на одной высоте
        //Выложить на GitHub
        public override void ExecuteRule()
        {
            Part[] parts = FindObjectsOfType<Part>().ToArray();//я забыл зачем мне эта функция
            CheckLine();
        }
        void CheckLine() //проверяет линию. если линия заполнена,удалить и остатки передвинуть вниз
        {
            for(int i = height-1; i< width; i--)
            {
                if (HasLine(i))
                {
                    DeleteLine(i);
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
