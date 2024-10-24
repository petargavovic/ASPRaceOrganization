using BaseLibrary.DTOs.EntityDTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class TrkeRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<TrkaDTO>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var t = await appDbContext.Trke.FindAsync(id);
            if (t is null) return NotFound();

            appDbContext.Trke.Remove(t);
            await Commit();
            return Success("Uspešno brisanje trke!");
        }

        public async Task<List<TrkaDTO>> GetAll()
        {
            var trke = await appDbContext.Trke
        .Include(t => t.Kategorija)
        .ToListAsync();

            var trkeDtoTasks = trke.Select(t => TrkaToDTOAsync(t)).ToList();

            var trkeDto = await Task.WhenAll(trkeDtoTasks);

            return trkeDto.ToList();
        }

        public async Task<TrkaDTO> GetById(int id)
        {
            var trka = await appDbContext.Trke.Include(t => t.Kategorija).Include(t => t.Ucinak).ThenInclude(u => u.Vozac).FirstOrDefaultAsync(t => t.Id == id);
            if (trka is null) NotFound();
            return await TrkaToDTOAsync(trka);
        }

        public async Task<GeneralResponse> Insert(TrkaDTO item)
        {
            if (!await CheckNaziv(item.Naziv!)) return new GeneralResponse(false, "Trka je već dodata");
            appDbContext.Trke.Add(DTOToTrka(item));
            await Commit();
            return Success("Uspešno dodavanje trke!");
        }

        public async Task<GeneralResponse> Update(TrkaDTO item)
        {
            var t = await appDbContext.Trke.FindAsync(item.Id);
            if (t is null) return NotFound();
            t.Naziv = item.Naziv;
            t.BrojKrugova = item.BrojKrugova;
            t.DatumTrke = item.DatumTrke;
            await Commit();
            return Success("Uspešna izmena trke!");
        }

        private static GeneralResponse NotFound() => new(false, "Žao nam je, trka nije pronađena!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        private async Task<bool> CheckNaziv(string naziv)
        {
            var item = await appDbContext.Trke.FirstOrDefaultAsync(x => x.Naziv!.ToLower().Equals(naziv.ToLower()));
            return item is null;
        }

        static Trka DTOToTrka(TrkaDTO trkaDTO)
        {
            return new Trka
            {
                Id = trkaDTO.Id,
                Naziv = trkaDTO.Naziv,
                BrojKrugova = trkaDTO.BrojKrugova,
                DatumTrke = trkaDTO.DatumTrke,
                KategorijaId = trkaDTO.Kategorija.Id
            };
        }

        async Task<TrkaDTO> TrkaToDTOAsync(Trka trka)
        {
            TrkaDTO trkaDTO = new TrkaDTO
            {
                Id = trka.Id,
                Naziv = trka.Naziv,
                BrojKrugova = trka.BrojKrugova,
                DatumTrke = trka.DatumTrke,
                Kategorija = new KategorijaDTO
                {
                    Id = trka.Kategorija.Id,
                    NazivKategorije = trka.Kategorija.NazivKategorije
                },
            };
            if(trka.Ucinak?.Count > 0)
            {
                trkaDTO.Ucinak = new List<UcinakDTO>();
                foreach(Ucinak ucinak in trka.Ucinak)
                {
                    var vozac = await appDbContext.Vozaci.FindAsync(ucinak.VozacID);
                    trkaDTO.Ucinak.Add(new UcinakDTO
                    {
                        Id = ucinak.Id,
                        Plasman = ucinak.Plasman,
                        StartnaPozicija = ucinak.StartnaPozicija,
                        Vremena = ucinak.Vremena,
                        VozacID = ucinak.VozacID,
                        ImeIPrezime = vozac.Ime + " " + vozac.Prezime
                    });
                }
            }
            return trkaDTO;
        }
    }
}
