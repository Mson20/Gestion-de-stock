using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionStock.API.Data;
using GestionStock.API.Models;

namespace GestionStock.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProduitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits(
            [FromQuery] string? search,
            [FromQuery] string? categorie)
        {
            var query = _context.Produits.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Nom.Contains(search) || 
                                        p.Fournisseur!.Contains(search));

            if (!string.IsNullOrEmpty(categorie))
                query = query.Where(p => p.Categorie == categorie);

            return await query.OrderBy(p => p.Nom).ToListAsync();
        }

        // GET /api/produits/alertes
        [HttpGet("alertes")]
        public async Task<ActionResult<IEnumerable<Produit>>> GetAlertes()
        {
            return await _context.Produits
                .Where(p => p.Stock <= p.SeuilAlerte)
                .OrderBy(p => p.Stock)
                .ToListAsync();
        }

        // GET /api/produits/stats
        [HttpGet("stats")]
        public async Task<ActionResult<object>> GetStats()
        {
            var produits = await _context.Produits.ToListAsync();
            return new
            {
                TotalProduits = produits.Count,
                EnAlerte = produits.Count(p => p.Stock <= p.SeuilAlerte),
                EnRupture = produits.Count(p => p.Stock == 0),
                ValeurTotale = produits.Sum(p => p.Stock * p.Prix)
            };
        }

        // GET /api/produits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();
            return produit;
        }

        // POST /api/produits
        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            produit.DateAjout = DateTime.UtcNow;
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduit), new { id = produit.Id }, produit);
        }

        // PUT /api/produits/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            if (id != produit.Id) return BadRequest();

            var existant = await _context.Produits.FindAsync(id);
            if (existant == null) return NotFound();

            existant.Nom = produit.Nom;
            existant.Description = produit.Description;
            existant.Categorie = produit.Categorie;
            existant.Fournisseur = produit.Fournisseur;
            existant.Stock = produit.Stock;
            existant.SeuilAlerte = produit.SeuilAlerte;
            existant.Prix = produit.Prix;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();
            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}