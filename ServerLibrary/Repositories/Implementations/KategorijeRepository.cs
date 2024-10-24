using BaseLibrary.DTOs.EntityDTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class KategorijeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<Kategorija>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var k = await appDbContext.Kategorije.FindAsync(id);
            if (k is null) return NotFound();

            appDbContext.Kategorije.Remove(k);
            await Commit();
            return Success("Uspešno brsanje kategorije!");
        }

        public async Task<List<Kategorija>> GetAll() => await appDbContext.Kategorije.ToListAsync();

        public async Task<Kategorija> GetById(int id) => await appDbContext.Kategorije.FindAsync(id);

        public async Task<GeneralResponse> Insert(Kategorija item)
        {
            if (!await CheckNaziv(item.NazivKategorije!)) return new GeneralResponse(false, "Kategorija je već dodata");
            appDbContext.Kategorije.Add(item);
            await Commit();
            return Success("Uspešno dodavanje kategorije!");
        }

        public async Task<GeneralResponse> Update(Kategorija item)
        {
            var k = await appDbContext.Kategorije.FindAsync(item.Id);
            if (k is null) return NotFound();
            k.NazivKategorije = item.NazivKategorije;
            await Commit();
            return Success("Uspešna izmena kategorije!");
        }

        private static GeneralResponse NotFound() => new(false, "Žao nam je, kategorija nije pronađena!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();
        private async Task<bool> CheckNaziv(string naziv)
        {
            var item = await appDbContext.Kategorije.FirstOrDefaultAsync(x => x.NazivKategorije!.ToLower().Equals(naziv.ToLower()));
            return item is null;
        }

        static Kategorija DTOToKategorija(KategorijaDTO kategorijaDTO)
        {
            return new Kategorija
            {
                Id = kategorijaDTO.Id,
                NazivKategorije = kategorijaDTO.NazivKategorije
            };
        }

        static KategorijaDTO KategorijaToDTO(Kategorija kategorija)
        {
            return new KategorijaDTO
            {
                Id = kategorija.Id,
                NazivKategorije = kategorija.NazivKategorije
            };
        }
    }
}
