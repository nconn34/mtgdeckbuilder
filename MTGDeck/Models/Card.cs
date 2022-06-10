using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MTGDeck.Models
{
    public class Card
    {
      public Card()
      {
        this.JoinEntities = new HashSet<CardDeck>();
      }
        public int CardId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public Prices Prices { get; set; }
        [NotMapped]
        public Legalities Legalities { get; set; }
        public string Mana_Cost { get; set; }
        [NotMapped]
        public string[] Colors { get; set; }
        public string ColorsAsString() { 
            string s = "";
            for (int i = 0; i < (Colors?.Length ?? 0); i++) {
              s += Colors[i];
              s += " ";
            }
            return s;
            }
        public string Type_Line { get; set; }
        public string Set_Name { get; set; }
        public virtual ICollection<CardDeck> JoinEntities {get; set; } 

    public static List<Card> SearchCards(string name, string colors, string type)
    {
      var apiCallTask = ApiHelper.Search(name, colors, type);
      var result = apiCallTask.Result;
      if (result == "No cards found.")
      {
          return new List<Card> { };
      }
      else
      { 
        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        List<Card> cardList = JsonConvert.DeserializeObject<List<Card>>(jsonResponse["data"].ToString());
        return cardList;
      }
    }
///////////////////////
    public static List<ScryfallCard> SearchScryFallCards(string name, string colors, string type)
    {
      var apiCallTask = ApiHelper.Search(name, colors, type);
      var result = apiCallTask.Result;
      if (result == "No cards found.")
      {
        return new List<ScryfallCard> { };
      }
      else
      { 
        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        List<ScryfallCard> cardList = JsonConvert.DeserializeObject<List<ScryfallCard>>(jsonResponse["data"].ToString());
        return cardList;
      }
    }
/////////////////////////
    public static Card GetCard(string name)
    {
      Card card = ApiHelper.GetCard(name).Result;
      return card;
    }
    public static string GetCardImage(string name)
    {
      ScryfallCard card = ApiHelper.GetScryfallCard(name).Result;
      string imageUrl = card.image_uris["border_crop"];
      return imageUrl;
    }  
    public static string GetLegalities(string name)
    {
      ScryfallCard card = ApiHelper.GetScryfallCard(name).Result;
      string legalities = "";
      foreach((string format, string legality) in card.legalities)
      {
        legalities += $"{format}: {legality}, ";
      }
      return legalities;
    }  
    public static string GetPrices(string name)
    {
      ScryfallCard card = ApiHelper.GetScryfallCard(name).Result;
      string prices = "";
      foreach((string currency, string value) in card.prices)
      {
        prices += $"{currency}: {value}, ";
      }
      return prices;
    }  
    public static string GetOracle(string name)
    {
      ScryfallCard card = ApiHelper.GetScryfallCard(name).Result;
      string oracle = card.Oracle_Text;
      return oracle;
    }  
    public static string GetFlavor(string name)
    {
      ScryfallCard card = ApiHelper.GetScryfallCard(name).Result;
      string flavor = card.Flavor_Text;
      return flavor;
    }
  }

    public partial class ImageUris
    {
        [JsonProperty("border_crop")]
        public Uri BorderCrop { get; set; }
    }
  public partial class Legalities
  {
      [JsonProperty("standard")]
      public string Standard { get; set; }

      [JsonProperty("future")]
      public string Future { get; set; }

      [JsonProperty("historic")]
      public string Historic { get; set; }

      [JsonProperty("gladiator")]
      public string Gladiator { get; set; }

      [JsonProperty("pioneer")]
      public string Pioneer { get; set; }

      [JsonProperty("explorer")]
      public string Explorer { get; set; }

      [JsonProperty("modern")]
      public string Modern { get; set; }

      [JsonProperty("legacy")]
      public string Legacy { get; set; }

      [JsonProperty("pauper")]
      public string Pauper { get; set; }

      [JsonProperty("vintage")]
      public string Vintage { get; set; }

      [JsonProperty("penny")]
      public string Penny { get; set; }

      [JsonProperty("commander")]
      public string Commander { get; set; }

      [JsonProperty("brawl")]
      public string Brawl { get; set; }

      [JsonProperty("historicbrawl")]
      public string Historicbrawl { get; set; }

      [JsonProperty("alchemy")]
      public string Alchemy { get; set; }

      [JsonProperty("paupercommander")]
      public string Paupercommander { get; set; }

      [JsonProperty("duel")]
      public string Duel { get; set; }

      [JsonProperty("oldschool")]
      public string Oldschool { get; set; }

      [JsonProperty("premodern")]
      public string Premodern { get; set; }
  }
   public partial class Prices
    {
        [DisplayName("USD")]
        [JsonProperty("usd")]
        public string Usd { get; set; }

        [DisplayName("USD Foil")]
        [JsonProperty("usd_foil")]
        public string UsdFoil { get; set; }

        [DisplayName("EUR")]
        [JsonProperty("eur")]
        public string Eur { get; set; }

        [DisplayName("EUR Foil")]
        [JsonProperty("eur_foil")]
        public string EurFoil { get; set; }
    }
}