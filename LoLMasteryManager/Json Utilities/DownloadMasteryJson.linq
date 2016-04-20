<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>RestSharp</Namespace>
</Query>

/*
This will download the latest version of the masteries.json file. Useful if the mastery file is updated by riot

The Url to download the masteries will change with each new version and must be updated - see "LOL Static Data" https://developer.riotgames.com/docs/static-data
*/
const string lolCdnDomain = "http://ddragon.leagueoflegends.com";
const string masteriesUrl = "cdn/6.8.1/data/en_US/mastery.json";
string masteryJsonFilePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "Masteries.json");
	
void Main()
{	
	var client = new RestClient(lolCdnDomain);
	var request = new RestRequest(masteriesUrl, Method.GET);
	var response = client.Execute(request);
	
	var jObject = JObject.Parse(response.Content);
	
	File.WriteAllText(masteryJsonFilePath, jObject.ToString());
}