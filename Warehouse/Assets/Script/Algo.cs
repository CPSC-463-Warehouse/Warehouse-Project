using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Algo : MonoBehaviour
{
    //Multi-dimensional array for warehouse and taken spaces

    static bool[,,] array = new bool[10, 10, 5];
    //static bool[,,] warehouse = new bool[10, 10, 10];
    //public static bool[][][] warehouse = RectangularArrays.RectangularBoolArray(10, 10, 5); //true means the slot is available
    //ADD HEIGHT

    // public int length = WarehouseInstance.instance.length;
    //private int width = WarehouseInstance.instance.width;
    //private int heigth = WarehouseInstance.instance.height;

    public static void initWarehouse(int x, int y, int z)
    {
        
        //Sets all values in warehouse to open - clearWarehouse?
        for (int i = 0; i < WarehouseInstance.instance.length; i++)
        {
            for (int j = 0; j < WarehouseInstance.instance.width; j++) //ADD HEIGHT
            {
                for (int k = 0; k < WarehouseInstance.instance.height; k++)
                {
                    WarehouseInstance.instance.warehouse[i, j, k] = true;

                }
            }
        }
    }

    public class Item
    {
        private string name;
        private int width;
        private int length;
        private int height;
        public string Name
        {
            get { return name; }
            set { name = Name; }
        }
        public int Width
        {
            get { return width; }
            set { width = Width; }
        }
        public int Length
        {
            get { return length; }
            set { length = Length; }
        }
        public int Height
        {
            get { return height; }
            set { height = Height; }
        }
        public Item(string Name, int Width, int Length, int Height)
        {
            name = Name;
            width = Width;
            length = Length;
            height = Height;
        }


    }

    //Prints a representation of the warehouse
    public static string printWarehouse()
    {
        string warehouseText = "";
        for (int i = 0; i < WarehouseInstance.instance.length; i++)
        {
            for (int k = 0; k < WarehouseInstance.instance.height; k++)
            {
                for (int j = 0; j < WarehouseInstance.instance.width; j++) //ADD HEIGHT
                {
                    if (WarehouseInstance.instance.warehouse[i, j, k] == true)
                    {
                        warehouseText+="_";
                    }
                    else
                    {
                        warehouseText+="X";
                    }
                }
                warehouseText+=" ";
            }
            warehouseText+="\n";
        }
        return warehouseText;
    }

    //returns true if there is space for the package in the area
    public static bool checkForSpace(int length, int width, int height, int x, int y, int z)
    {
        //std::cout << length << " " << width << " " << x << " " << y << std::endl;

        //in case the package's dimensions are too long to fit in warehouse altogether
        if (x + length > WarehouseInstance.instance.length)
        {
            return false;
        }
        if (y + width > WarehouseInstance.instance.width)
        {
            return false;
        }
        if (z + height > WarehouseInstance.instance.height)
        {
            return false;
        }

        //Checks each space within the length and width (ADD HEIGHT)
        for (int w = x; w < x + length; w++)
        {
            for (int a = y; a < y + width; a++)
            {
                for (int b = z; b < z + height; b++)
                {
                    //If a space is occupied, the package won't fit there
                    if (WarehouseInstance.instance.warehouse[w, a, b] == false)
                    {
                        return false;
                    }
                }
            }
        }

        //No occupied spaces indicates the package can fit
        return true;
    }

    //checks the neighboring spaces to see how much surface area would be touching the package in this position
    //It's a heuristic
    public static int checkSAHeuristic(int length, int width, int height, int x, int y, int z)
    {
        //The surface area of the new package that would touch a wall or another package
        int sa = 0;

        //Checks all of the spaces that would be adjacent to the package in that position
        //NOTE: x:length:w, y:width:a, z:height:b - Keep this way to avoid confusion
        for (int w = x; w < x + length; w++)
        {
            for (int a = y; a < y + width; a++)
            {
                if (z - 1 < 0)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[w, a, z - 1] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (z + height >= WarehouseInstance.instance.height)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[w, a, z + height] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }
            }
        }

        for (int w = x; w < x + length; w++)
        {
            for (int b = z; b < z + height; b++)
            {
                if (y - 1 < 0)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[w, y - 1, b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (y + width >= WarehouseInstance.instance.width)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[w, y + width, b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }
            }
        }

        for (int a = y; a < y + width; a++)
        {
            for (int b = z; b < z + height; b++)
            {
                if (x - 1 < 0)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[x - 1, a, b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (x + length >= WarehouseInstance.instance.length)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (WarehouseInstance.instance.warehouse[x + length, a, b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }
            }
        }

        return sa;
    }

    //Fills warehouse array to occupy spaces with the new package
    //Will need to take orientation into account, if we do it.
    public static void fillSpaces(int length, int width, int height, int x, int y, int z)
    {
        for (int w = x; w < x + length; w++)
        {
            for (int a = y; a < y + width; a++)
            {
                for (int b = z; b < z + height; b++)
                {
                    WarehouseInstance.instance.warehouse[w, a, b] = false;
                }
            }
        }
    }

    //Function to add item into warehouse
    //public static void addItem(int length, int width, int height)
    public static void addItem(Item item)
    {
        //The cordinates of the corner of the best fit
        int[] bestFit = { -1, -1, -1 };
        //The best surface area fit = how much wall/other package can the new touch?
        int bestSAFit = -1;
        //For rotation, store the order of the length, width, and height
        int[] bestOrientation = { -1, -1, -1 };

        //Create the rotations using the length, width, and height
        int[][] orientations = RectangularArrays.RectangularIntArray(6, 3);

        orientations[0][0] = item.Length;
        orientations[0][1] = item.Width;
        orientations[0][2] = item.Height;

        orientations[1][0] = item.Length;
        orientations[1][1] = item.Height;
        orientations[1][2] = item.Width;

        orientations[2][0] = item.Width;
        orientations[2][1] = item.Length;
        orientations[2][2] = item.Height;

        orientations[3][0] = item.Width;
        orientations[3][1] = item.Height;
        orientations[3][2] = item.Length;

        orientations[4][0] = item.Height;
        orientations[4][1] = item.Width;
        orientations[4][2] = item.Length;

        orientations[5][0] = item.Height;
        orientations[5][1] = item.Length;
        orientations[5][2] = item.Length;

        //Checks if any of the slots are open
        for (int i = 0; i < WarehouseInstance.instance.length; i++)
        {
            for (int j = 0; j < WarehouseInstance.instance.width; j++)
            {
                for (int k = 0; k < WarehouseInstance.instance.height; k++)
                {
                    if (WarehouseInstance.instance.warehouse[i, j, k] == true)
                    {

                        //std::cout << "Checking if item fits..." << std::endl;
                        //for rotation, check with different orders of length, width, and height
                        //would need to keep track of rotation, somehow

                        //Goes through each orientation
                        for (int o = 0; o < 6; o++)
                        {
                            bool fits = checkForSpace(orientations[o][0], orientations[o][1], orientations[o][2], i, j, k);
                            if (fits == true)
                            {
                                //check neighboring spaces for heuristic
                                int newSA = checkSAHeuristic(orientations[o][0], orientations[o][1], orientations[o][2], i, j, k);

                                //Updates if a new best surface area fit is found
                                if (newSA > bestSAFit)
                                {
                                    Debug.Log("New bestSAFit");
                                    Debug.Log("\n");
                                    bestFit[0] = i;
                                    bestFit[1] = j;
                                    bestFit[2] = k;

                                    bestSAFit = newSA;

                                    bestOrientation[0] = orientations[o][0];
                                    bestOrientation[1] = orientations[o][1];
                                    bestOrientation[2] = orientations[o][2];
                                }
                            }
                        }
                    }
                }
            }
        }

        //choose best fit, fill spaces
        if (bestFit[0] != -1 && bestFit[1] != -1 && bestFit[2] != -1)
        {
            fillSpaces(bestOrientation[0], bestOrientation[1], bestOrientation[2], bestFit[0], bestFit[1], bestFit[2]);
        }
        else
        {
            Debug.Log("The new item does not fit");
            Debug.Log("\n");
        }

        Debug.Log("best sa fit ");
        Debug.Log(bestSAFit);
        Debug.Log("\n");
        //Add item dimensions (in the rotation the item was places), coordinate, and name to list.
        return;
    }

    /*
    static void Main(string[] args)
    {
        int l;
        int w;
        int h;
        initWarehouse();
        while (true)
        {
            //Prompts user for length and width
            Console.Write("please enter length ");
            l = int.Parse(Console.ReadLine());
            if (l == -1)
            {
                break;
            }

            Console.Write("please enter width ");
            w = int.Parse(Console.ReadLine());
            if (w == -1)
            {
                break;
            }

            Console.Write("please enter height ");
            h = int.Parse(Console.ReadLine());
            if (h == -1)
            {
                break;
            }
            Console.Write("please enter item name ");
            string item_name = (Console.ReadLine());


            Item new_item = new Item(item_name, w, l, h);
            //new_item.Length = l;
            //new_item.Width = w;
            //new_item.Height = h;
            //new_item.Name = item_name;
            addItem(new_item);
            printWarehouse();
        }
    }
    */
}



