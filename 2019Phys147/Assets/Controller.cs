using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /*
     *  This code has this hierarchy:
     *  Controller -> ViewBox -> UnitCell(s) -> Atom(s) -> Bonds (not yet implemented)
     *  each atom has an element attached to it
     *  
     *  View box allows proper scaling to view as many unit cells as desired
     * 
     * /

    /*
        TO DO:
        1. Make multiple unit cells (check) 
        2. Color atoms by element (check)
        2a. Draw bounds of unit cell (check)
<<<<<<< HEAD

=======
>>>>>>> e8637966ed6a22d7c411cb6234b609b78dcfecbc
        3. Rotate unit cells (check)
            3b. Rotate along crystallographic indices
                3bi. Store crystallographic indices in unit cell structure somewhere
        3.5 Make sure importing from MaterialsProject still works easily.
        4. Draw bonds between atoms (needs PyMatGen <python library>)
        5. Select atoms and get information
            5a. Measure distance between atoms
            5b. Measure angles between bonds
        6. Display and move a plane defined by crystalographic planes to highlight coplanar atoms
        7. Try to make an options menu for each crystal structure
        8. Update crystal structures on the fly, within hololens
        9. Change # of atoms (i guess keep the size) from within hololens

        KNOWN BUGS:
<<<<<<< HEAD
        1.
=======
        Only unknown bugs at the moment.
>>>>>>> e8637966ed6a22d7c411cb6234b609b78dcfecbc

    */

    // Start is called before the first frame update
    [SerializeField]
    private int cellsLong;
    [SerializeField]
    private int cellsHigh;
    [SerializeField]
    private int cellsWide;
    [SerializeField]
    private float boxSize;
    [SerializeField]
    private float rotX;
    [SerializeField]
    private float rotY;
    [SerializeField]
    private float rotZ;

    [SerializeField]
    private float posX;
    [SerializeField]
    private float posY;
    [SerializeField]
    private float posZ;

    public string filepathGeneral;

    public ViewBox ViewBoxPreFaberino;

    public UnitCell UnitCellPreFaberino;

    public Atom AtomPreFaberino;

    public GameObject BoundaryPreFaberino;

    public Vector3 initPos;

    public bool isBoundaryVisible = true;

    void Start()
    {
        // something else above controller makes a controller instance (prefab with gameobject & controller
            //setting up the Initialize function things
            // 

    }

    // Update is called once per frame
    void Update()
    {
        // can set up trigger 
    
    }

    void Initialize()
    {
        Element.TryMakePTable();
        ViewBox.ViewBoxPreFab = ViewBoxPreFaberino;
        UnitCell.UnitCellPreFab = UnitCellPreFaberino;
        UnitCell.BoundaryPreFab = BoundaryPreFaberino;
        Atom.AtomPreFab = AtomPreFaberino;   
        initPos = new Vector3(posX, posY, posZ);

        ViewBox.CreateViewBox(cellsLong, cellsHigh, cellsWide, boxSize, rotX, rotY, rotZ, filepathGeneral, initPos);
    }
}
