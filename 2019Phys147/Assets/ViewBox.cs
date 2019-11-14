using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;


public class ViewBox : MonoBehaviour
{
    // object designed to be the parent to the n^3 unit cells to provide a defined space for each potentially different unit cell
    // and their orientation in space seperate from each other

    // could try using the inside-out sphere, might look nice and make it easier to distinguish the atoms.

    // keeping in mind the hololens 2 and the possible adjustability of this using hands

    private  int[] cells;
    private  float[] boxSize;
    private  float[] origin;
    private float[] rotations;

    public static ViewBox ViewBoxPreFab;


    public static ViewBox CreateViewBox(int cellsLong, int cellsHigh, int cellsWide, float boxSize, float rotX, float rotY, float rotZ, string filepath, Vector3 initPos)
    {

        Vector3 middlePoint = initPos;

        //making the object that will be copied
        GameObject boxObject = Instantiate(ViewBoxPreFab.gameObject, middlePoint, Quaternion.Euler(rotX,rotY,rotZ));

        ViewBox viewBox = boxObject.GetComponent<ViewBox>();

        //using square scales for proper display
        float boxLength = boxSize;
        float boxHeight = boxSize;
        float boxWidth = boxSize;

        viewBox.cells = new int[] { cellsLong, cellsHigh, cellsWide };
        viewBox.boxSize = new float[] { boxLength, boxHeight, boxWidth };
        Vector3 boxNum = new Vector3(boxLength, boxHeight, boxWidth);
        viewBox.origin = new float[] { -boxLength / 2f, -boxHeight / 2f, -boxWidth / 2f };
        viewBox.rotations = new float[] { rotX, rotY, rotZ };

        boxObject.transform.localScale = boxNum;

        //could also detect if there are other view boxes and try to line them up
        
        // this is the unit cell that is copied; it gets disabled later
        UnitCell primeUnitCell = UnitCell.NewUnitCell(filepath, boxObject.GetComponent<ViewBox>());

        for (int i = 0; i < cellsLong; i++)
        {
            for(int j = 0; j < cellsHigh; j++)
            {
                for (int k = 0; k < cellsWide; k++)
                {
                    Vector3 xposVec = primeUnitCell.vecA * (float)i;
                    Vector3 yposVec = primeUnitCell.vecB * (float)j;
                    Vector3 zposVec = primeUnitCell.vecC * (float)k;

                    float xpos = xposVec.x + yposVec.x + zposVec.x;

                    float ypos = xposVec.y + yposVec.y + zposVec.y;

                    float zpos = xposVec.z + yposVec.z + zposVec.z;

                    Vector3 newPos = new Vector3(xpos, ypos, zpos);

                    newPos += new Vector3(viewBox.origin[0], viewBox.origin[1], viewBox.origin[2]);

                    GameObject iterUnitCell = Instantiate(primeUnitCell.gameObject, boxObject.transform);
                    iterUnitCell.transform.localPosition = newPos;
                }
            }
        }
     
        //disables this instance but keeps all of the other ones that are corectly positioned wrt eachother
        primeUnitCell.gameObject.SetActive(false);

        return (viewBox);


    }

    public int[] GetCells()
    {
        return(cells);
    }
    public float[] GetBoxSize()
    {
        return (boxSize);
    }
    public float[] GetOrigin()
    {
        return (origin);
    }


    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
