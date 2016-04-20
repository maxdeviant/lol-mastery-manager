<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.InteropServices.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>RestSharp</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
  <Namespace>System.Runtime.InteropServices</Namespace>
</Query>

/*
This file will re-build the coordinates.json file for you based on the 2 bmp images you provide.  Take 2 
screenshots of the lol app, one on the mastery editing page and one in the champ select mastery editing 
page.  Trim them down to 1280x800 file size (the default size of the app), then use a single pixel pencil 
tool with the color Green (red:0 green:255 blue:0, 00FF00) and mark a dot in the upper left corner of every 
mastery (they don't have to be perfectly on the corner, just close).  Save as a bmp and then run this script 
to re-generate the coordinates.json file.

The Url to download the masteries will change with each new version and must be updated - see "LOL Static Data" https://developer.riotgames.com/docs/static-data
*/

//Using single green pixels as the identifier of the upper left corner of each button
const string hexColor = "00FF00";
const byte markedR = 0;
const byte markedG = 255;
const byte markedB = 0;
string markedImagePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "mastery_coordinates_0_255_0_00FF00.bmp");
string markedImagePathInChampSelect = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "mastery_champ_select_coordinates_0_255_0_00FF00.bmp");
string coordinatesJsonFilePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Coordinates.json");

const string lolCdnDomain = "http://ddragon.leagueoflegends.com";
const string masteriesUrl = "cdn/6.8.1/data/en_US/mastery.json";

void Main()
{	
	var coordinatesJson = new CoordinatesJsonSchema
	{
		reference_client_size = "1280, 800",
		mastery_coordinates_menu = GetMasteryCoordsByImage(markedImagePath),
		mastery_coordinates_champion_select = GetMasteryCoordsByImage(markedImagePathInChampSelect),
	};

	var formattedJson = JsonConvert.SerializeObject(coordinatesJson, Newtonsoft.Json.Formatting.Indented);
	
	File.WriteAllText(coordinatesJsonFilePath, formattedJson);
}

Dictionary<string, string> GetMasteryCoordsByImage(string imagePath)
{
	var layout = GetOfficialMasteryLayout();

	var coords = GetMarkedCoordinates(imagePath);

	if (layout.Count() != coords.Count()){
		throw new Exception("mismatched masteries count");
	}
	
	PopulateCoordinates("Ferocity", layout, coords);
	PopulateCoordinates("Cunning", layout, coords);
	PopulateCoordinates("Resolve", layout, coords);
	
	var coordinatesJsonSchema = layout.OrderBy(x => x.MasteryId).ToDictionary(x => x.MasteryId.ToString(), x => x.Coords);

	return coordinatesJsonSchema;
}


class CoordinatesJsonSchema
{
	public string reference_client_size{get;set;}
	public Dictionary<string, string> mastery_coordinates_menu{get;set;}
	public Dictionary<string, string> mastery_coordinates_champion_select{get;set;}
}

// Define other methods and classes here
class Coordinate
{
	public int x { get; set; }
	public int y { get; set; }
	public bool IsAssigned { get; set; }
	public int? RowNumber{get;set;}
}

List<Coordinate> GetMarkedCoordinates(string imagePath)
{
	var bmp = new Bitmap(imagePath);

	// Lock the bitmap's bits.  
	var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
	var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
	
	// Get the address of the first line.
	IntPtr ptr = bmpData.Scan0;
	
	// Declare an array to hold the bytes of the bitmap.
	int bytes = bmpData.Stride * bmp.Height;
	byte[] rgbValues = new byte[bytes];
	byte[] r = new byte[bytes / 3];
	byte[] g = new byte[bytes / 3];
	byte[] b = new byte[bytes / 3];
	
	// Copy the RGB values into the array.
	Marshal.Copy(ptr, rgbValues, 0, bytes);
	
	int count = 0;
	int stride = bmpData.Stride;
	
	for (int column = 0; column < bmpData.Height; column++)
	{
		for (int row = 0; row < bmpData.Width; row++)
		{
			b[count] = (byte)(rgbValues[(column * stride) + (row * 3)]);
			g[count] = (byte)(rgbValues[(column * stride) + (row * 3) + 1]);
			r[count++] = (byte)(rgbValues[(column * stride) + (row * 3) + 2]);
		}
	}
	
	var coords = new List<Coordinate>();
	
	for (var i = 0; i < r.Length; i ++)
	{
		//1280x800
		if (r[i] == markedR && g[i] == markedG && b[i] == markedB)
		{
			var x = i % 1280;
			var y = i / 1280;
			coords.Add(new Coordinate
			{
				x = x,
				y = y,
			});
		}
	}
	
	AnalyzeCoords(coords);
	
	return coords;
}

JObject masteriesjobj;

List<MasteryLayout> GetOfficialMasteryLayout()
{	
	var client = new RestClient(lolCdnDomain);
	var request = new RestRequest(masteriesUrl, Method.GET);
	var response = client.Execute(request);
	
	masteriesjobj = JObject.Parse(response.Content);
	
	var masteryLayout = new List<MasteryLayout>();
	
	
	masteryLayout.AddRange(GetOfficialMasteryLayoutTree(masteriesjobj, "Ferocity"));
	masteryLayout.AddRange(GetOfficialMasteryLayoutTree(masteriesjobj, "Cunning"));
	masteryLayout.AddRange(GetOfficialMasteryLayoutTree(masteriesjobj, "Resolve"));
	
	
	return masteryLayout;
}

List<MasteryLayout> GetOfficialMasteryLayoutTree(JObject masteriesObj, string tree)
{
	var masteryLayout = new List<MasteryLayout>();
	
	var ferocityBranch = masteriesObj["tree"][tree];
	
	var rowNum = 0;
	foreach (var row in ferocityBranch)
	{
		var colNum = 0;
		var columns = row.Children<JObject>();
		foreach (var col in columns)
		{
			var masteryId = Int32.Parse(col["masteryId"].ToString());
			
			masteryLayout.Add(new MasteryLayout
			{
				Tree = tree,
				MasteryId = masteryId,
				Row = rowNum,
				Column = colNum,
			});
			
			colNum++;
		}

		rowNum++;
	}
	
	return masteryLayout;
}

public class MasteryLayout
{
	public string Tree {get;set;}
	public int Row {get;set;}
	public int Column{get;set;}
	public int MasteryId{get;set;}
	public string Coords{get;set;}
}

void PopulateCoordinates(string tree, List<MasteryLayout> layout, List<Coordinate> coords){
	var ferocityLayout = layout.Where(x => x.Tree == tree);
	
	for (var row = 0; row <= ferocityLayout.Max(x => x.Row); row++) {
		var cols = ferocityLayout.Where(x => x.Row == row);
		for(var col = 0; col <= cols.Max(x => x.Column); col++) {
		
			var cell = cols.SingleOrDefault	(x => x.Column == col);
			if (cell == null) continue;
			
			//Get left most unassiged coord for this row
			var coord = coords.Where(x => x.IsAssigned == false && x.RowNumber == cell.Row).OrderBy(c => c.x).First();
			
			cell.Coords = string.Format("{0}, {1}", coord.x, coord.y);
			coord.IsAssigned = true;
		}
	}
}

void AnalyzeCoords(List<Coordinate> coords)
{
	//Get row numbers (0-5)
	int minY;
	
	const int yTolerance = 15;
	var rowNumber = 0;
	
	while(coords.Any(x => x.RowNumber == null))
	{
		minY = coords.Where(c => c.RowNumber == null).Min(c => c.y);
	
		var thisRow = coords.Where(c => c.RowNumber == null && c.y < (minY + yTolerance));
		foreach (var r in thisRow){
			r.RowNumber = rowNumber;
		}
		
		rowNumber++;
	}
}