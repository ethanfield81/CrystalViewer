﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class Element
{
    public static Dictionary<string, Element> pTable = new Dictionary<string, Element>();

    public static void TryMakePTable()
    {
        if(pTable.Count == 0)
        {
            MakePTable();
        }
    }


    private static void MakePTable()
    {
        // mam switch to using a TextAsset and storing the file in the Assests/Resources folder
        //Instead of : string filePath1 = "c:/users/ethan/desktop/phys147/ptoe.csv";
        //             A comma separated file ptoe.txt has been placed in in the Assests/Resources folder
        string ptoeFileName = "ptoe"; // Note that the .txt extension is not included
        TextAsset ptoeAsset = (TextAsset)Resources.Load(ptoeFileName, typeof(TextAsset));
       
        // read all of the lines
        string[] all_rows;
        string[] rows;
        all_rows = ptoeAsset.text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        // remove header row;
        rows = all_rows.Skip(1).ToArray();

        foreach (string line in rows)
        {

            string[] splitline = line.Split(',');
            Element currentElement = new Element(splitline);
            pTable.Add(splitline[2], currentElement);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int AtomicNumber;
    private string ElementName;
    private string Symbol;
    private double AtomicMass;
    private int NumberofNeutrons;
    private int NumberofProtons;
    private int NumberofElectons;
    private int Period;
    private int Group;
    private string Phase;
    private bool Radioactive;
    private bool Natural;
    private bool Metal;
    private bool Nonmetal;
    private bool Metalloid;
    private string ElementType;
    private double AtomicRadius;
    private double Electronegativity;
    private double FirstIonization;
    private double Density;
    private double MeltingPoint;
    private double BoilingPoint;
    private int NumberOfIsotopes;
    private string Discoverer;
    private int Year;
    private double SpecificHeat;
    private int NumberofShells;
    private int NumberofValence;

    public Element(string[] splitLine)
    {

        // if the integer is empty it won't be able to parse it.
        // if parsing succeeds, then it assigns it as the number otherwise to 0, I guess
        if (int.TryParse(splitLine[0], out int num1)) { this.AtomicNumber = num1; } else { this.AtomicNumber = 0; };
        this.ElementName = splitLine[1];
        this.Symbol = splitLine[2];
        if (double.TryParse(splitLine[3], out double dbl1)) { this.AtomicMass = dbl1; } else { this.AtomicMass = 0; };
        if (int.TryParse(splitLine[4], out int num2)) { this.NumberofNeutrons = num2; } else { this.NumberofNeutrons = 0; };
        if (int.TryParse(splitLine[5], out int num3)) { this.NumberofProtons = num3; } else { this.NumberofProtons = 0; };
        if (int.TryParse(splitLine[6], out int num4)) { this.NumberofElectons = num4; } else { this.NumberofElectons = 0; };
        if (int.TryParse(splitLine[7], out int num5)) { this.Period = num5; } else { this.Period = 0; };
        if (int.TryParse(splitLine[8], out int num6)) { this.Group = num6; } else { this.Group = 0; };
        this.Phase = splitLine[9];
        this.Radioactive = "yes".Equals(splitLine[10]);
        this.Natural = "yes".Equals(splitLine[11]);
        this.Metal = "yes".Equals(splitLine[12]);
        this.Nonmetal = "yes".Equals(splitLine[13]);
        this.Metalloid = "yes".Equals(splitLine[14]);
        this.ElementType = splitLine[15];
        if (double.TryParse(splitLine[16], out double dbl2)) { this.AtomicRadius = dbl2; } else { this.AtomicRadius = 0; };
        if (double.TryParse(splitLine[17], out double dbl3)) { this.Electronegativity = dbl3; } else { this.Electronegativity = 0; };
        if (double.TryParse(splitLine[18], out double dbl4)) { this.FirstIonization = dbl4; } else { this.FirstIonization = 0; };
        if (double.TryParse(splitLine[19], out double dbl5)) { this.Density = dbl5; } else { this.Density = 0; };
        if (double.TryParse(splitLine[20], out double dbl6)) { this.MeltingPoint = dbl6; } else { this.MeltingPoint = 0; };
        if (double.TryParse(splitLine[21], out double dbl7)) { this.BoilingPoint = dbl7; } else { this.BoilingPoint = 0; };
        if (int.TryParse(splitLine[22], out int num7)) { this.NumberOfIsotopes = num7; } else { this.NumberOfIsotopes = 0; };
        this.Discoverer = splitLine[23];
        if (int.TryParse(splitLine[24], out int num8)) { this.Year = num8; } else { this.Year = 0; };
        if (double.TryParse(splitLine[25], out double dbl8)) { this.SpecificHeat = dbl8; } else { this.SpecificHeat = 0; };
        if (int.TryParse(splitLine[26], out int num9)) { this.NumberofShells = num9; } else { this.NumberofShells = 0; };
        if (int.TryParse(splitLine[27], out int num10)) { this.NumberofValence = num10; } else { this.NumberofValence = 0; };
    }

    public double GetAtomicNumber()
    {
        return AtomicNumber;
    }
    public string GetElementName()
    {
        return ElementName;
    }
    public string GetSymbol()
    {
        return Symbol;
    }
    public double GetAtomicMass()
    {
        return AtomicMass;
    }
    public int GetNumberofNeutrons()
    {
        return NumberofNeutrons;
    }
    public int GetNumberofProtons()
    {
        return NumberofProtons;
    }
    public int GetNumberofElectons()
    {
        return NumberofElectons;
    }
    public int GetPeriod()
    {
        return Period;
    }
    public int GetGroup()
    {
        return Group;
    }
    public string GetPhase()
    {
        return Phase;
    }
    public bool GetRadioactive()
    {
        return Radioactive;
    }
    public bool GetNatural()
    {
        return Natural;
    }
    public bool GetMetal()
    {
        return Metal;
    }
    public bool GetNonmetal()
    {
        return Nonmetal;
    }
    public bool GetMetalloid()
    {
        return Metalloid;
    }
    public string GetElementType()
    {
        return ElementType;
    }
    public double GetAtomicRadius()
    {
        return AtomicRadius;
    }
    public double GetElectronegativity()
    {
        return Electronegativity;
    }
    public double GetFirstIonization()
    {
        return FirstIonization;
    }
    public double GetDensity()
    {
        return Density;
    }
    public double GetMeltingPoint()
    {
        return MeltingPoint;
    }
    public double GetBoilingPoint()
    {
        return BoilingPoint;
    }
    public int GetNumberOfIsotopes()
    {
        return NumberOfIsotopes;
    }
    public string GetDiscoverer()
    {
        return Discoverer;
    }
    public int GetYear()
    {
        return Year;
    }
    public double GetSpecificHeat()
    {
        return SpecificHeat;
    }
    public int GetNumberofShells()
    {
        return NumberofShells;
    }
    public int GetNumberofValence()
    {
        return NumberofValence;
    }
}
