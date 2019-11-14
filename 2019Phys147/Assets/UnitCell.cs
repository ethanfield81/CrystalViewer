using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Linq;

public class UnitCell : MonoBehaviour
{
    // properties of the unit cell 
    public double cell_length_a;
    public double cell_length_b;
    public double cell_length_c;
    public double cell_angle_alpha;
    public double cell_angle_beta;
    public double cell_angle_gamma;
    public double originX;
    public double originY;
    public double originZ;

    //corrects the transform 0,0 to the corner of the unit cell <000>
    public Vector3 originVec;

    //points along a b and c
    public Vector3 vecA;
    public Vector3 vecB;
    public Vector3 vecC;

    //cell lengths to fit within the viewbox
    public double scaled_length_a;
    public double scaled_length_b;
    public double scaled_length_c;

    public float unitCellScale;

    //used in construction
    private ViewBox parentViewBox;

    public static UnitCell UnitCellPreFab;
    public static GameObject BoundaryPreFab;

    // transforms to be able to draw bounds of each unit cell
    [SerializeField] private Transform bot1;
    [SerializeField] private Transform bot2;
    [SerializeField] private Transform bot3;
    [SerializeField] private Transform bot4;
    [SerializeField] private Transform top1;
    [SerializeField] private Transform top2;
    [SerializeField] private Transform top3;
    [SerializeField] private Transform top4;

    // list of different elements and all atoms that are within the unit cell
    public List<Element> allElems = new List<Element>();
    public List<Atom> atomList = new List<Atom>();

    // functions within UnitCell
    public static UnitCell NewUnitCell(string filepath, ViewBox parentViewBox)
    { //this is the order these functions should be run in, otherwise there won't be the correct things in place
        GameObject unitCellObject = Instantiate(UnitCellPreFab.gameObject, parentViewBox.transform);
        UnitCell unitCell = unitCellObject.GetComponent<UnitCell>();

        string[] atomsCIF = unitCell.ReadCif(filepath);

        unitCell.parentViewBox = parentViewBox;
        
        unitCell.unitCellScale = unitCell.SetScaleSize();

        unitCell.SetCorners();

        unitCell.atomList = unitCell.MakeAtoms(atomsCIF);

        unitCell.SetAtomColors();

        unitCell.DrawAllBoundaries();

        return ( (unitCell )); ;
    }

    public string[] ReadCif(string filePath)
    {
        // Instead of :  string[] lines = File.ReadAllLines(filePath);
        // place .txt version of the cif file in the Assests/Resources folder
        string[] lines;
        TextAsset cifAsset = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
        lines = cifAsset.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        // setting a, b, and c
        string[] splitLine = lines[3].Split(' ');
        this.cell_length_a = double.Parse(splitLine[3]);
        splitLine = lines[4].Split(' ');
        this.cell_length_b = double.Parse(splitLine[3]);
        splitLine = lines[5].Split(' ');
        this.cell_length_c = double.Parse(splitLine[3]);

        // setting alpha, beta, and gamma
        splitLine = lines[6].Split(' ');
        this.cell_angle_alpha = double.Parse(splitLine[3]);
        splitLine = lines[7].Split(' ');
        this.cell_angle_beta = double.Parse(splitLine[3]);
        splitLine = lines[8].Split(' ');
        this.cell_angle_gamma = double.Parse(splitLine[3]);

        // all lines of atoms with 'columns' = ElementSymbol, AtomLabel, SymmetryMultiplicity, X pos, Y pos, Z pos, occupancy
        string[] atomList = lines.Skip(26).ToArray();

        return (atomList);

    }

    public float SetScaleSize()
    {

        // this part calculates a scale by which to multiply all the size values by to ensure that everything fits in the box right depending on how many unit cells there are
        float[] viewBoxSize = parentViewBox.GetBoxSize();
        int[] viewBoxCells = parentViewBox.GetCells();
        float[] boxSizesArray = new float[] { viewBoxSize[0] / (float)viewBoxCells[0], viewBoxSize[1] / (float)viewBoxCells[1], viewBoxSize[2] / (float)viewBoxCells[2] };
        float minViewerDim = boxSizesArray.Min();
        double maxDim = cell_length_a;

   
        if (cell_length_a <= cell_length_b && cell_length_a <= cell_length_c)
        {
            if (cell_length_b <= cell_length_c)
            {
                maxDim = cell_length_c;
            }
            else
            {
                maxDim = cell_length_b;
            }
        }
        else
        {
            maxDim = cell_length_a;
        }

        // setting the scale to be the ratio of the smallest dimension with reference to the viewer to the largest dimension with reference to the unit cell and an extra 10%
        float thisScale = minViewerDim / (float)maxDim;
        thisScale = thisScale / (float)1.1;
        
        scaled_length_a = cell_length_a * thisScale;
        scaled_length_b = cell_length_b * thisScale;
        scaled_length_c = cell_length_c * thisScale;

      
        return (thisScale);
    }

    public void SetCorners()
    {   // moves the corners to the correct positions after scaling the unitcell(s) to fit inside the viewbox

        // most importantly this calculates the vectors of A, B, and C
        this.vecA = new Vector3((float)scaled_length_a, 0, 0);
        this.vecB = new Vector3((float)scaled_length_b * (float)Math.Cos((float)cell_angle_gamma*(float)Math.PI/180f),0, (float)scaled_length_b * (float)Math.Sin((float)cell_angle_gamma * (float)Math.PI / 180f));

        // to find the vector C, we take the projections of c onto each orthogonal axis.
        // this represents the projection of C onto X
        float u1 = (float)Math.Cos((float)Math.PI * (float)cell_angle_beta / 180f)*(float)scaled_length_c;
        // projection of C onto Z
        float u2 = ((float)Math.Cos((float)Math.PI * (float)cell_angle_gamma / 180f) * ((float)Math.Cos(Math.PI * (float)cell_angle_alpha / 180f) * (float)scaled_length_c))+
                        ((float)Math.Sin((float)Math.PI * (float)cell_angle_gamma / 180f)* ((float)Math.Cos(Math.PI * (float)cell_angle_alpha / 180f) * (float)scaled_length_c));

        // projection of C onto Y (uses u1 and u2 so needs to be last
        float u3 = (float)Math.Cos(Math.Asin(Math.Sqrt((u1*u1)+(u2*u2))/scaled_length_c)) * (float)scaled_length_c;

        this.vecC = new Vector3(u1, u3, u2);

        //origin corner
        Vector3 bc1 = new Vector3(0, 0, 0);
        //bottom corner #2 (at the end of a)
        Vector3 bc2 = vecA;
        //bottom corner #3 (at the end of b)
        Vector3 bc3 = vecB;
        //top corner #1 (at the end of c)
        Vector3 tc1 = vecC;
        //bottom corner #4 (addition of 2 and 3)
        Vector3 bc4 = vecA + vecB;
        //top corner #2 (tc1 + bc2)
        Vector3 tc2 = vecA + vecC;
        //top corner #3 (tc1 + bc3)
        Vector3 tc3 = vecB + vecC;
        //top corner #4 (tc1 + bc2 + bc3)
        Vector3 tc4 = vecA + vecB + vecC;

       
        this.originVec = -(vecA + vecB + vecC) /2f;

       // moving corner objects into position
        bot1.localPosition = bc1 + originVec;
        bot2.localPosition = bc2 + originVec;
        bot3.localPosition = bc3 + originVec;
        bot4.localPosition = bc4 + originVec;
        top1.localPosition = tc1 + originVec;
        top2.localPosition = tc2 + originVec;
        top3.localPosition = tc3 + originVec;
        top4.localPosition = tc4 + originVec;
    }

    public List<Atom> MakeAtoms(string[] atomList)
    {
        List < Atom > someAtoms = new List<Atom>();


        foreach(string line in atomList)
        {
            //interprets atom part of the CIF file 
            // ** splits around the total number of spaces and not the white space, finicky implementation **
            string[] splitline = line.Split(' ');
            string atomLabel = splitline[1];
            Element atomElement = null;

            if (Element.pTable.TryGetValue(splitline[2], out Element elem)) // = reference to periodictable dictionary;
            {
                atomElement = elem;
                if (!allElems.Contains(elem))
                {
                    allElems.Add(elem);
                }
            }
            else { Debug.Log("sometings f* with the elements: " + splitline[0]); }
            Debug.Log(atomElement);
            Debug.Log("Atom Element");
            Debug.Log(elem);

            //Vector3 init is the row from the CIF file
            Vector3 latticePosition = new Vector3( float.Parse(splitline[8]), float.Parse(splitline[10]), float.Parse(splitline[12]));
            Vector3 xposVec = this.vecA * latticePosition.x;
            Vector3 yposVec = this.vecC * latticePosition.y;
            Vector3 zposVec = this.vecB * latticePosition.z;


            float xpos = xposVec.x + yposVec.x + zposVec.x;

            float ypos = xposVec.y + yposVec.y + zposVec.y;

            float zpos = xposVec.z + yposVec.z + zposVec.z;

            Vector3 atomPosition = new Vector3(xpos, ypos, zpos);

            atomPosition += this.originVec;

            // makes the atom in reference to this unit cell and adds it to a list of atoms that are within the unit cell

            someAtoms.Add(Atom.NewAtom(atomElement, atomPosition, latticePosition, atomLabel, this));

            
        }
        return (someAtoms);
    }

    public Vector3 ConvertFromLatticePos(Vector3 latticePos)
    {
        Vector3 xposVec = this.vecA * latticePos.x;
        Vector3 yposVec = this.vecC * latticePos.y;
        Vector3 zposVec = this.vecB * latticePos.z;

        float xpos = xposVec.x + yposVec.x + zposVec.x;

        float ypos = xposVec.y + yposVec.y + zposVec.y;

        float zpos = xposVec.z + yposVec.z + zposVec.z;

        Vector3 atomPosition = new Vector3(xpos, ypos, zpos);

        atomPosition += this.originVec;

        return (atomPosition);
    }

    public void SetAtomColors()
    {
        List<Color> colorList = new List<Color>() { Color.blue, Color.red, Color.yellow, Color.green, Color.cyan };

        foreach(Atom thisAtom in atomList)
        {
            //Debug.Log(thisAtom.GetAtomLabel());
            int thisIndex = allElems.IndexOf(thisAtom.GetElement());
            //Debug.Log("Finding element:" + thisAtom.GetElement().GetElementName() + " at index " + allElems.IndexOf(thisAtom.GetElement()));
            thisAtom.gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorList[thisIndex]);
        }
    }

    public void DrawAllBoundaries()
    {   // 12 vertices (point b - point a) created from point a
        // b1 - b2, b1 - b3, b1 - t1
        DrawBoundary(bot1, bot2);
        DrawBoundary(bot1, bot3);
        DrawBoundary(bot1, top1);
        
        // b2 - b4, b2 - t2
        DrawBoundary(bot2, bot4);
        DrawBoundary(bot2, top2);

        // b3 - b4, b3 - t3
        DrawBoundary(bot3, bot4);
        DrawBoundary(bot3, top3);

        // b4 - t4
        DrawBoundary(bot4, top4);

        // t1 - t2, t2 - t3, t3 - t4, t4 - t1
        DrawBoundary(top1, top2);
        DrawBoundary(top2, top4);
        DrawBoundary(top3, top4);
        DrawBoundary(top3, top1); 
        
    }

    public void DrawBoundary(Transform pointA, Transform pointB)
    {   
        // creates a cylinder between pointA and pointB
        Vector3 boundVec = pointA.position - pointB.position;
        GameObject thisBoundary = GameObject.Instantiate(BoundaryPreFab, this.transform);
        
        thisBoundary.transform.localPosition = pointA.localPosition;
        thisBoundary.transform.LookAt(pointB);
        thisBoundary.transform.localScale = new Vector3((float)unitCellScale, (float)unitCellScale, boundVec.magnitude );
        thisBoundary.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.magenta);

    }

    public Vector3 GetCellDim()
    {
        Vector3 cellDim = new Vector3((float)cell_length_a, (float)cell_length_b, (float)cell_length_c);
        return (cellDim);
    }

    public Vector3 GetCellDeg()
    {
        Vector3 cellDeg = new Vector3((float)cell_angle_alpha, (float)cell_angle_beta, (float)cell_angle_gamma);
        return (cellDeg);
    }

// Start is called before the first frame update
void Start()
{

    // calculate bonds between atoms, nearest neightbors? there's a part of pymatgen (CrystalNN) that will return pairs of nearest neighbors for a structure.

    // calculate boundary of unit cell
    // add option to add translucent polygon around the unit cell.

}

// Update is called once per frame
void Update()
    {
        
    }
}