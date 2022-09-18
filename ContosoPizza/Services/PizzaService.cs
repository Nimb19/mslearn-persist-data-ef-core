using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    public PizzaService(PizzaContext pizzaContext)
    {
        _context = pizzaContext;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(x => x.Sauce)
            .Include(x => x.Toppings)
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();
        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
        var toppingToAdd = _context.Toppings.Find(ToppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if (pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();

    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizzasToUpd = _context.Pizzas.Find(PizzaId) ?? throw new Exception($"Pizza with id '{PizzaId}' not found");
        var sauceToUpd = _context.Sauces.Find(SauceId) ?? throw new Exception($"Sauce with id '{SauceId}' not found");

        pizzasToUpd.Sauce = sauceToUpd;
        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }
}