using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MTGDeck.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MTGDeck.Controllers
{
  [Authorize]
  public class CardsController : Controller
  {
    private readonly MTGDeckContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CardsController(UserManager<ApplicationUser> userManager, MTGDeckContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public IActionResult Search(string name, string color, string type)
    {
    List<Card> result = Card.SearchCards(name, color, type);
    storeCardListImages(result);
    return View(result);
    }
    public IActionResult GetCard(string name)
    {
      Card result = Card.GetCard(name);
      return View(result);
    }

    [AllowAnonymous]
    public IActionResult Index()
      {
        List<Card> result = Card.SearchCards("", "", "elf");
        storeCardListImages(result);
        return View(result);
      }
    [AllowAnonymous]
    public IActionResult Details(string name)
    {
      Card card = Card.GetCard(name);
      storeCardInfo(name);
      ViewBag.DeckId = new SelectList(_db.Decks, "DeckId", "Name");
      return View(card);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCard = _db.Cards.FirstOrDefault(card => card.CardId == id);
      _db.Cards.Remove(thisCard);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCard(int joinId)
    {
      var joinEntry = _db.CardDecks.FirstOrDefault(entry => entry.CardDeckId == joinId);
      _db.CardDecks.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // Grabs the extra info stored in ScryfallCard and assignes passes them to the ViewBag
    public void storeCardInfo(string cardName)
        {
            string cardImages = Card.GetCardImage(cardName);
            ViewBag.CardImages = Url.Content(cardImages);
            string[] cardLegalities = Card.GetLegalities(cardName).Replace("_", " ").Split(", ");
            ViewBag.CardLegalities = cardLegalities;
            string[] cardPrices = Card.GetPrices(cardName).Replace("_", " ").Split(", ");
            ViewBag.CardPrices = cardPrices;
            string cardOracle = Card.GetOracle(cardName);
            ViewBag.CardOracle = cardOracle;
            string cardFlavor = Card.GetFlavor(cardName);
            ViewBag.CardFlavor = cardFlavor;
        }
    public void storeCardListImages(List<Card> cardList)
    {
      List<string> urlList =  new List<string>{};
      ViewBag.result = cardList[0].Name;
      foreach(Card card in cardList)
      {
        string cardImage = Card.GetCardImage(card.Name);
        urlList.Add(cardImage);
      }
      ViewBag.CardListImages = urlList;
    }
  }
}