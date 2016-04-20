<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>RestSharp</Namespace>
</Query>

/*
Utility for validating the current mastery.json file and coordinates file look correct. Only needed if you need debugging help

The Url to download the masteries will change with each new version and must be updated - see "LOL Static Data" https://developer.riotgames.com/docs/static-data
*/

const bool inChampionSelect = true;
const string champSelectMode = inChampionSelect ? "mastery_coordinates_champion_select" : "mastery_coordinates_menu";

const string lolCdnDomain = "http://ddragon.leagueoflegends.com";
const string masteriesUrl = "cdn/6.8.1/data/en_US/mastery.json";
string lolMasteryImagePath = "http://ddragon.leagueoflegends.com/cdn/6.8.1/img/mastery/{0}.png";
string coordinatesJsonFilePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Coordinates.json"); 


void Main()
{
	champSelectMode.Dump("Mastery Mode");
	
	var officialMasteryIds = GetOfficialMasteryIds();
	var coordinateMasteryIds = GetCoordinateMasteryIds();
	
	var missingMasteries = officialMasteryIds.Except(coordinateMasteryIds);
	var surplusMasteries = coordinateMasteryIds.Except(officialMasteryIds);
	
	missingMasteries.Dump("missingMasteries");
	surplusMasteries.Dump("surplusMasteries");

//	ShowMasteryDetails(6111);
//	ShowMasteryDetails(6123);

	var ferocityMasteryIds = ParseMasteryTree(masteriesjobj, "Ferocity");
	var cunningMasteryIds = ParseMasteryTree(masteriesjobj, "Cunning");
	var resolveMasteryIds = ParseMasteryTree(masteriesjobj, "Resolve");
	
	foreach (var mast in ferocityMasteryIds.Union(cunningMasteryIds).Union(resolveMasteryIds))
	{
		ShowMasteryDetails(mast);
	}
}

// Define other methods and classes here
JObject masteriesjobj;

List<int> GetOfficialMasteryIds()
{
	
	var client = new RestClient(lolCdnDomain);
	var request = new RestRequest(masteriesUrl, Method.GET);
	var response = client.Execute(request);
	
	masteriesjobj = JObject.Parse(response.Content);
	
	var masteryIds = new List<int>();
	
	var masteriesElements = masteriesjobj["data"].Children<JProperty>();
	
	foreach(var prop in masteriesElements){
		masteryIds.Add(Int32.Parse(prop.Name));
	}
	
	return masteryIds;
}

JObject coordinatesjobj;
List<int> GetCoordinateMasteryIds()
{
	coordinatesjobj = JObject.Parse(File.ReadAllText(coordinatesJsonFilePath));
	
	var masteryCoords = coordinatesjobj[champSelectMode].Children<JProperty>();
	
	var masteryIds = new List<int>();
	
	foreach (var mcoord in masteryCoords)
	{
		masteryIds.Add(Int32.Parse(mcoord.Name));
	}
	
	return masteryIds;
}

void ShowMasteryDetails(int masteryId)
{
	var masteryName = masteriesjobj["data"][masteryId.ToString()]["name"].ToString();

	var masteryTree = string.Empty;
	var ferocityMasteryIds = ParseMasteryTree(masteriesjobj, "Ferocity");
	if (ferocityMasteryIds.Contains(masteryId)){masteryTree = "Ferocity";}
	
	var cunningMasteryIds = ParseMasteryTree(masteriesjobj, "Cunning");
	if (cunningMasteryIds.Contains(masteryId)){masteryTree = "Cunning";}
	
	var resolveMasteryIds = ParseMasteryTree(masteriesjobj, "Resolve");
	if (resolveMasteryIds.Contains(masteryId)){masteryTree = "FeroResolvecity";}
	
	var masteryImage = Util.Image(string.Format(lolMasteryImagePath, masteryId));
	
	var masteryCoordinates = coordinatesjobj[champSelectMode][masteryId.ToString()].ToString();
	
	masteryImage.Dump(masteryId + " " + masteryName + " - " + masteryTree + ": " + masteryCoordinates);
}

List<int> ParseMasteryTree(JObject jObject, string tree)
{
	var myMasteryIds = new List<int>();
	var myMasteryJson = jObject["tree"][tree];
	var myMasteryGroupJson = myMasteryJson.Children();
	foreach (var grp in myMasteryGroupJson)
	{
		foreach (var leafMastery in grp.Children<JObject>())
		{
			var masteryIdStr = leafMastery["masteryId"].ToString();
			var masteryId = Int32.Parse(masteryIdStr);
			myMasteryIds.Add(masteryId);
		}
	}
	
	return myMasteryIds;
}