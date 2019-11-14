using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Diagnostics;
using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class APIManager
{   /* this function will be used to interface with the Materials API on the materials project website.

    https://materialsproject.org/docs/api

    GET https://www.materialsproject.org/rest/v2/materials/ {material id, formula, or chemical system} /vasp/ {property}

    specifically:

    GET https://www.materialsproject.org/rest/v2/materials/mp-13/vasp/{property}?API_KEY=YOUR_API_KEY

    will return a string containing the structure [of mp-13] in the CIF format

    https://stackoverflow.com/questions/9620278/how-do-i-make-calls-to-a-rest-api-using-c

    */

    static HttpClient client = new HttpClient();

    private static string baseURL = "https://www.materialsproject.org/rest/v2/materials/";

    [SerializeField]
    private static string apiMaterialID = "";

    private static string baseURLb = "/vasp/";

    [SerializeField]
    private static string apiProperty = "";

    private static string apiKey = "?API_KEY=" + GetAPIKey();

    private static ResponseObject vaspResponse;

    static async Task RunAsync()
    {
        client.BaseAddress = new Uri("http://localhost:64199");

        // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //update these values from a menu within the hololens rather than accepting them at start-up

        string thisurl = baseURL + apiMaterialID + baseURLb + apiProperty + apiKey;

        try
        {
            vaspResponse = await GetResponseObjectAsync(thisurl);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    static async Task<ResponseObject> GetResponseObjectAsync(string URL)
    {
        // List data response.
        HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body
            ResponseObject dataObjects = await response.Content.ReadAsAsync<ResponseObject>(); // figure out how to interpret this data based off the properties that are being asked for by the program?
            //foreach (ResponseObject d in dataObjects)
            //{
            //    Console.WriteLine("{0}", d.Name);
            //}
            return dataObjects;
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            return (null);
        }

        //  url paramters {property} + ?API_KEY=YOUR_API_KEY
    

    }

    public static string GetAPIKey()
    { //once this project becomes open-source, update to pull the apikey from a file or prompt
        return ("wOCm5LMnQStA5e2U54");
    }

    void Start()
    {
        APIManager.apiMaterialID = "mp-13";
        APIManager.apiProperty = "vasp";
        APIManager.RunAsync();
        Debug.Log(vaspResponse);
    }
    //add methods to set and get the url parameters
}
public class ResponseObject
{
    public bool valid_response { get; set; }
    public VersionObject version { get; set; }
    public string created_at { get; set; }
    public string copyright { get; set; }
    public ResponseContentObject response {get; set;}
}

public class VersionObject
{
    public string pymatgen { get; set; }
    public string db { get; set; }
    public string rest { get; set; }
}

public class ResponseContentObject
{ // so I'm gonna take everything in from vasp, get the material ID, then get the cif
    public double formation_energy_per_atom { get; set; }
    public string[] elements { get; set; }
    public BandGapObject band_gap { get; set; }
    public float e_above_hull { get; set; }
    public int nelements { get; set; }
    public string pretty_formula { get; set; }
    public double energy { get; set; }
    public bool is_hubbard { get; set; }
    public int nsites { get; set; }
    public int material_id { get; set; }
    public Dictionary<string, double> unit_cell_formula { get; set; }
    public double volume { get; set; }
    public bool is_compatible { get; set; }
    public Dictionary<string, double> hubbards { get; set; }
    public int icsd_id { get; set; }
    public SpaceGroupObject spacegroup { get; set; }
    public double energy_per_atom { get; set; }
    public string full_formula { get; set; }
    public double density { get; set; }

}

public class SpaceGroupObject
{
    public string symbol { get; set; }
    public int number { get; set; }
    public string point_group { get; set; }
    public string source { get; set; }
    public string crystal_system { get; set;}
    public string hall { get; set; }
}

public class BandGapObject
{
    public double energy { get; set; }
    public string transition { get; set; }
    public bool direct { get; set; }
}

