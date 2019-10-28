using System;

public static class GlobalMembers
{
    //Multi-dimensional array for warehouse and taken spaces

    public static bool[][][] warehouse = RectangularArrays.RectangularBoolArray(10, 10, 5); //true means the slot is available
                                                                                            //ADD HEIGHT

    public static void initWarehouse()
    {
        //Sets all values in warehouse to open - clearWarehouse?
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++) //ADD HEIGHT
            {
                for (int k = 0; k < 5; k++)
                {
                    warehouse[i][j][k] = true;
                }
            }
        }
    }

    //Prints a representation of the warehouse
    public static void printWarehouse()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                for (int j = 0; j < 10; j++) //ADD HEIGHT
                {
                    if (warehouse[i][j][k] == true)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write("X");
                    }
                }
                Console.Write(" ");
            }
            Console.Write("\n");
        }
    }

    //returns true if there is space for the package in the area
    public static bool checkForSpace(int length, int width, int height, int x, int y, int z)
    {
        //std::cout << length << " " << width << " " << x << " " << y << std::endl;

        //in case the package's dimensions are too long to fit in warehouse altogether
        if (x + length > 10)
        {
            return false;
        }
        if (y + width > 10)
        {
            return false;
        }
        if (z + height > 5)
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
                    if (warehouse[w][a][b] == false)
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
                else if (warehouse[w][a][z - 1] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (z + height >= 5)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (warehouse[w][a][z + height] == false)
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
                else if (warehouse[w][y - 1][b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (y + width >= 10)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (warehouse[w][y + width][b] == false)
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
                else if (warehouse[x - 1][a][b] == false)
                {
                    //means there is some surface area touching a package
                    sa += 1;
                }

                if (x + length >= 10)
                {
                    //Means there is some surface area touching a warehouse bound
                    sa += 1;
                }
                else if (warehouse[x + length][a][b] == false)
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
                    warehouse[w][a][b] = false;
                }
            }
        }
    }

    //Function to add item into warehouse
    public static void addItem(int length, int width, int height)
    {
        //The cordinates of the corner of the best fit
        int[] bestFit = { -1, -1, -1 };
        //The best surface area fit = how much wall/other package can the new touch?
        int bestSAFit = -1;
        //For rotation, store the order of the length, width, and height
        int[] bestOrientation = { -1, -1, -1 };

        //Create the rotations using the length, width, and height
        int[][] orientations = RectangularArrays.RectangularIntArray(6, 3);

		orientations[0][0] = length;
		orientations[0][1] = width;
		orientations[0][2] = height;

		orientations[1][0] = length;
		orientations[1][1] = height;
		orientations[1][2] = width;

		orientations[2][0] = width;
		orientations[2][1] = length;
		orientations[2][2] = height;

		orientations[3][0] = width;
		orientations[3][1] = height;
		orientations[3][2] = length;

		orientations[4][0] = height;
		orientations[4][1] = width;
		orientations[4][2] = length;

		orientations[5][0] = height;
		orientations[5][1] = length;
		orientations[5][2] = width;

		//Checks if any of the slots are open
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					if (warehouse[i][j][k] == true)
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
									Console.Write("New bestSAFit");
									Console.Write("\n");
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
			Console.Write("The new item does not fit");
			Console.Write("\n");
		}

		Console.Write("best sa fit ");
		Console.Write(bestSAFit);
		Console.Write("\n");
		//Add item dimensions (in the rotation the item was places), coordinate, and name to list.
		return;
	}


	static int Main()
	{
		int l;
		int w;
		int h;
		initWarehouse();
		while (true)
		{
			//Prompts user for length and width
			Console.Write("please enter length ");
			l = int.Parse(ConsoleInput.ReadToWhiteSpace(true));
			if (l == -1)
			{
				break;
			}

			Console.Write("please enter width ");
			w = int.Parse(ConsoleInput.ReadToWhiteSpace(true));
			if (w == -1)
			{
				break;
			}

			Console.Write("please enter height ");
			h = int.Parse(ConsoleInput.ReadToWhiteSpace(true));
			if (h == -1)
			{
				break;
			}

			addItem(l, w, h);
			printWarehouse();
		}
		return 0;
	}
}