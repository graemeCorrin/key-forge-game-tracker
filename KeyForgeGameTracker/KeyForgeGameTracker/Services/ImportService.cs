using KeyForgeGameTracker.Data;
using KeyForgeGameTracker.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KeyForgeGameTracker.Services
{
    public class ImportService : IImportService
    {

        private readonly string _url = "https://www.keyforgegame.com/api/decks/{0}";
        private readonly string _urlParameters = "?links=cards";

        private readonly KeyForgeContext _context;

        public ImportService(KeyForgeContext context)
        {
            _context = context;
        }

        //Assumes Deck does not yet exist in the system
        //TODO: Allow for decks to be "reimported" in case errata changes them in the future
        public async Task ImportDeckAsync(Deck deck)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(string.Format(_url, deck.KeyForgeId));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(_urlParameters);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonString);
                var data = json.SelectToken("data");
                var _linked = json.SelectToken("_linked");

                deck.Name = data.SelectToken("name").ToString();
                deck.Expansion = Int32.Parse(data.SelectToken("expansion").ToString());
                deck.PowerLevel = Int32.Parse(data.SelectToken("power_level").ToString());
                deck.Chains = Int32.Parse(data.SelectToken("chains").ToString());
                deck.Wins = Int32.Parse(data.SelectToken("wins").ToString());
                deck.Loses = Int32.Parse(data.SelectToken("losses").ToString());

                var houses = _linked.SelectToken("houses") as JArray;
                var houseDictionary = new Dictionary<string, House>();
                foreach (JObject jsonHouse in houses)
                {
                    var house = GetHouseFromJson(jsonHouse);
                    houseDictionary.Add(house.KeyForgeId, house);

                    var deckHouse = new DeckHouse()
                    {
                        Deck = deck,
                        House = house
                    };
                    _context.Add(deckHouse);
                }


                var cards = _linked.SelectToken("cards") as JArray;
                foreach (JObject jsonCard in cards)
                {
                    var card = GetCardFromJson(jsonCard, houseDictionary);
                    var deckCard = new DeckCard()
                    {
                        Deck = deck,
                        Card = card
                    };
                    _context.Add(deckCard);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

        private House GetHouseFromJson(JObject json)
        {
            var keyForgeId = json.SelectToken("id").ToString();
            var house = _context.House.Where(h => h.KeyForgeId == keyForgeId).FirstOrDefault();

            if (house is null)
            {
                house = new House()
                {
                    KeyForgeId = keyForgeId,
                    Name = json.SelectToken("name").ToString(),
                    Image = json.SelectToken("image").ToString()
                };
                _context.Add(house);
            }

            return house;
        }

        private Card GetCardFromJson(JObject json, Dictionary<string, House> houseDictionary)
        {
            var keyForgeId = new Guid(json.SelectToken("id").ToString());
            var card = _context.Card.Where(c => c.KeyForgeId == keyForgeId).FirstOrDefault();

            if (card is null)
            {
                card = new Card()
                {
                    KeyForgeId = keyForgeId,
                    Title = json.SelectToken("card_title").ToString(),
                    House = houseDictionary[json.SelectToken("house").ToString()],
                    CardType = json.SelectToken("card_type").ToString(),
                    FrontImage = json.SelectToken("front_image").ToString(),
                    CardText = json.SelectToken("card_text").ToString(),
                    Traits = json.SelectToken("traits").ToString(),
                    Amber = Int32.TryParse(json.SelectToken("traits").ToString(), out var i) ? i : (int?)null,
                    Power = json.SelectToken("power").ToString(),
                    Armor = json.SelectToken("armor").ToString(),
                    Rarity = json.SelectToken("rarity").ToString(),
                    FlavorText = json.SelectToken("flavor_text").ToString(),
                    CardNumber = json.SelectToken("card_number").ToString(),
                    Expansion = Int32.TryParse(json.SelectToken("expansion").ToString(), out i) ? i : (int?)null,
                    IsMaverick = Boolean.Parse(json.SelectToken("is_maverick").ToString()),
                    IsAnomaly = Boolean.Parse(json.SelectToken("is_anomaly").ToString())
                };
                _context.Add(card);
            }

            return card;
        }
    }
}
