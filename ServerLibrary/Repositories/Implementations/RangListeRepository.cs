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
    public class RangListeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<RangListaDTO>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var rl = await appDbContext.RangListe.FindAsync(id);
            if (rl is null) return NotFound();

            appDbContext.RangListe.Remove(rl);
            await Commit();
            return Success("Uspešno brisanje rang liste!");
        }
        public async Task<List<RangListaDTO>> GetAll() 
        { 

            var rangListe = await appDbContext.RangListe.Include(rl => rl.Kategorija).Include(rl => rl.Plasman).ThenInclude(rl => rl.Vozac).ToListAsync();

            var rangListeDtoTasks = rangListe.Select(rl => RangListaToDTOAsync(rl)).ToList();

            var rangListeDto = await Task.WhenAll(rangListeDtoTasks);

            return rangListeDto.ToList();
        }

        public async Task<RangListaDTO> GetById(int id) {
            var rl = await appDbContext.RangListe.Include(rl => rl.Kategorija).Include(rl => rl.Plasman).ThenInclude(rl => rl.Vozac).FirstOrDefaultAsync(t => t.Id == id);
            if (rl is null) NotFound();
            return await RangListaToDTOAsync(rl);
        }

        public async Task<GeneralResponse> Insert(RangListaDTO item)
        {
            if (!await Check(item)) return new GeneralResponse(false, "Rang lista je već dodata");
            appDbContext.RangListe.Add(DTOToRangLista(item));
            await Commit();
            return Success("Uspešno dodavanje rang liste!");
        }

        public async Task<GeneralResponse> Update(RangListaDTO item)
        {
            var rl = await appDbContext.RangListe.FindAsync(item.Id);
            if (rl is null) return NotFound();
            rl.KrajSezone = item.KrajSezone;
            await Commit();
            return Success("Uspešna izmena rang liste!");
        }

        private static GeneralResponse NotFound() => new(false, "Žao nam je, rang lista nije pronađena!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> Check(RangListaDTO rl)
        {
            var item = await appDbContext.RangListe.FirstOrDefaultAsync(x => x.KrajSezone.Year == rl.KrajSezone.Year && x.KategorijaId == rl.Kategorija.Id);
            return item is null;
        }

        static RangLista DTOToRangLista(RangListaDTO rangListaDTO)
        {
            RangLista rl = new RangLista
            {
                Id = rangListaDTO.Id,
                KrajSezone = rangListaDTO.KrajSezone,
                Kategorija = new Kategorija { Id = rangListaDTO.Kategorija.Id, NazivKategorije = rangListaDTO.Kategorija.NazivKategorije},
               
            };
            List<Plasman> plasmani = new();
            if (rangListaDTO.Plasman != null)
                foreach (PlasmanDTO plasman in rangListaDTO.Plasman)
            {
                plasmani.Add(new Plasman { Id = plasman.Id, BrojPoena = plasman.BrojPoena, VozacID = plasman.VozacID, RangListaID = plasman.RangListaID });
            }
            rl.Plasman = plasmani;
            return rl;
        }

        async Task<RangListaDTO> RangListaToDTOAsync(RangLista rangLista)
        {
            RangListaDTO rlDTO = new RangListaDTO
            {
                Id = rangLista.Id,
                KrajSezone = rangLista.KrajSezone,
                Kategorija = new KategorijaDTO
                {
                    Id = rangLista.Kategorija.Id,
                    NazivKategorije = rangLista.Kategorija.NazivKategorije
                },
            };
            List<PlasmanDTO> plasmani = new();
            if(rangLista.Plasman != null)
            foreach (Plasman plasman in rangLista.Plasman)
            {
                    var vozac = await appDbContext.Vozaci.FindAsync(plasman.VozacID);
                    plasmani.Add(new PlasmanDTO { Id = plasman.Id, BrojPoena = plasman.BrojPoena, VozacID = plasman.VozacID, RangListaID = plasman.RangListaID, ImeIPrezime = vozac.Ime + " " + vozac.Prezime });
            }
            rlDTO.Plasman = plasmani;
            return rlDTO;
        }
    }
}
