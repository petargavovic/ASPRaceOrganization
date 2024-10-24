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
    public class PlasmaniRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<PlasmanDTO>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var p = await appDbContext.Plasmani.FindAsync(id);
            if (p is null) return NotFound();

            appDbContext.Plasmani.Remove(p);
            await Commit();
            return Success("Uspešno brisanje plasmana!");
        }

        public async Task<List<PlasmanDTO>> GetAll() => await appDbContext.Plasmani.Select(p => PlasmanToDTO(p)).ToListAsync();

        public async Task<PlasmanDTO> GetById(int id)
        {
            var p = await appDbContext.Plasmani.FindAsync(id);
            if (p is null) NotFound();
            return PlasmanToDTO(p);
        }

        public async Task<GeneralResponse> Insert(PlasmanDTO item)
        {
            if (!await Check(item)) return new GeneralResponse(false, "Plasman već postoji!");
            appDbContext.Plasmani.Add(DTOToPlasman(item));
            await Commit();
            return Success("Uspešno dodavanje plasmana!");
        }

        public async Task<GeneralResponse> Update(PlasmanDTO item)
        {
            var p = await appDbContext.Plasmani.FindAsync(item.Id);
            if (p is null) return NotFound();
            p.BrojPoena = item.BrojPoena;
            await Commit();
            return Success("Uspešna izmena plasmana!");
        }
        private static GeneralResponse NotFound() => new(false, "Žao nam je, plasman nije pronađen!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> Check(PlasmanDTO p)
        {
            var item = await appDbContext.Plasmani.FirstOrDefaultAsync(x => x.VozacID == p.VozacID && x.RangListaID == p.RangListaID);
            return item is null;
        }

        static Plasman DTOToPlasman(PlasmanDTO plasmanDTO)
        {
            return new Plasman
            {
                Id = plasmanDTO.Id,
                BrojPoena = plasmanDTO.BrojPoena,
                RangListaID = plasmanDTO.RangListaID,
                VozacID = plasmanDTO.VozacID
            };
        }

        static PlasmanDTO PlasmanToDTO(Plasman plasman)
        {
            return new PlasmanDTO
            {
                Id = plasman.Id,
                BrojPoena = plasman.BrojPoena,
                RangListaID = plasman.RangListaID,
                VozacID = plasman.VozacID
            };
        }
    }
}
