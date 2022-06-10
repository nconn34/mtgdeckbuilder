using System.Threading.Tasks;
using RestSharp;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MTGDeck.Models
{
  class ApiHelper
  {
    // Searches for many cards based on name, color, and type. Returns 0 - many cards.
    public static async Task<string> Search(string name, string color, string type)
    {
      if (color == null && type == null)
      {
        RestClient client = new RestClient("https://api.scryfall.com");
        RestRequest request = new RestRequest($"cards/search?q={name}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        if(response.IsSuccessful)
        {
          return response.Content;
        }
        else
        {
          return "No cards found.";
        }
      }
      else if (color == null)
      {
        RestClient client = new RestClient("https://api.scryfall.com");
        RestRequest request = new RestRequest($"cards/search?q={name} t:{type}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        if(response.IsSuccessful)
        {
          return response.Content;
        }
        else
        {
          return "No cards found.";
        }
      }
      else if (type == null)
      {
        RestClient client = new RestClient("https://api.scryfall.com");
        RestRequest request = new RestRequest($"cards/search?q={name} c:{color}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        if(response.IsSuccessful)
        {
          return response.Content;
        }
        else
        {
          return "No cards found.";
        }
      }
        else if (type == null && color == null)
        {
        RestClient client = new RestClient("https://api.scryfall.com");
        RestRequest request = new RestRequest($"cards/search?q={name}",  Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        if (response.IsSuccessful)
        {
        return response.Content;
        }
        else
        { 
        return "No cards found.";
        }
      }
      else
      {
        RestClient client = new RestClient("https://api.scryfall.com");
        RestRequest request = new RestRequest($"cards/search?q={name} c:{color} t:{type}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        if(response.IsSuccessful)
        {
          return response.Content;
        }
        else
        {
          return "No cards found.";
        }
      }

    }
    //Searches for 1 specific card by fuzzy name. == Used for card details page ==
    public static async Task<Card> GetCard(string name)
    {
      RestClient client = new RestClient("https://api.scryfall.com");
      RestRequest request = new RestRequest($"cards/named?fuzzy={name}", Method.GET);

      var response = await client.ExecuteTaskAsync(request);
      
      return JsonConvert.DeserializeObject<Card>(response.Content);

    }
    public static async Task<ScryfallCard> GetScryfallCard(string name)
    {
      RestClient client = new RestClient("https://api.scryfall.com");
      RestRequest request = new RestRequest($"cards/named?fuzzy={name}", Method.GET);

      var response = await client.ExecuteTaskAsync(request);
      if(response.IsSuccessful)
        {
          return JsonConvert.DeserializeObject<ScryfallCard>(response.Content);
        }
        else
        {
          return new ScryfallCard { };
        }
      

    }
  }
  public class ScryfallCard
  {
    public Dictionary<string, string> image_uris { get; set; }
    public Dictionary<string, string> legalities { get; set; }
    public Dictionary<string, string> prices { get; set; }
    public string Oracle_Text { get; set; }
    public string Flavor_Text { get; set; }
  }
}
