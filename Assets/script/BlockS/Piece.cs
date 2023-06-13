using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece 
{
    public Databiuck bluckdata { get; private set; }
    public Vector2Int[] cells { get; private set; }

    public int rotationindex { get; private set; }

    public void Initialize(Databiuck data )
    {
        this.bluckdata = data;
        cells = null;
        rotationindex = 0;
        if (data.BType != bluckType.None)
        {
            cells = new Vector2Int[data.typecell.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = (Vector2Int)data.typecell[i];
            }



            for (int i = 0; i < Random.Range(0, 4); i++)
            {
                Rotater(1);
            }
        }
      
    } 

    public void blocksave(Piece piecedat)
    {
         
        this.cells = null;
        this.cells = new Vector2Int[piecedat.bluckdata.typecell.Length];
        

        bluckdata = piecedat.bluckdata;
        cells = piecedat.cells;
        rotationindex = piecedat.rotationindex;
    }


   
    public void Rotater(int direction)
    {
        rotationindex = Wraper(rotationindex + direction, 0, 4);

        ApplyRotationMatrix(direction);
    }

    void ApplyRotationMatrix(int direction)
    {
        float[] matrix = Data.RotationMatrix;

        // Rotate all of the cells using the rotation matrix
        for (int i = 0; i < cells.Length; i++)
        {
            Vector2 cell = cells[i];

            int x, y;
            x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
            y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));

            cells[i] = new Vector2Int(x, y);
        }
    }
    int Wraper(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }

    }
}
