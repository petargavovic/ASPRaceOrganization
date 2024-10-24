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
    public class VozaciRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<VozacDTO>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var v = await appDbContext.Vozaci.FindAsync(id);
            if (v is null) return NotFound();

            appDbContext.Vozaci.Remove(v);
            await Commit();
            return Success("Uspešno brisanje vozača!");
        }

        public async Task<List<VozacDTO>> GetAll()
        {
            var vozaci = await appDbContext.Vozaci.ToListAsync();
            var vozaciDTO = vozaci.Select(t => VozacToDTO(t)).ToList();
            return vozaciDTO;
        }

        public async Task<VozacDTO> GetById(int id)
        {
            var vozac = await appDbContext.Vozaci.Include(v => v.Ucinak).FirstOrDefaultAsync(v => v.Id == id);
            if (vozac is null) NotFound();
            return VozacToDTO(vozac);
        }

        public async Task<GeneralResponse> Insert(VozacDTO item)
        {
            //if (!await CheckId(item.Id!)) return new GeneralResponse(false, "Vozac vec dodat");
            appDbContext.Vozaci.Add(DTOToVozac(item));
            await Commit();
            return Success("Uspešno dodavanje vozača!");
        }

        public async Task<GeneralResponse> Update(VozacDTO item)
        {
            var v = await appDbContext.Vozaci.FindAsync(item.Id);
            if (v is null) return NotFound();
            v.Ime = item.Ime;
            v.Prezime = item.Prezime;
            v.DatumRodjenja = item.DatumRodjenja;
            v.Drzava = item.Drzava;
            await Commit();
            return Success("Uspešna izmena vozača!");
        }

        private static GeneralResponse NotFound() => new(false, "Žao nam je, vozač nije pronađen!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        static Vozac DTOToVozac(VozacDTO vozacDTO)
        {
            return new Vozac
            {
                Id = vozacDTO.Id,
                Ime = vozacDTO.Ime,
                Prezime = vozacDTO.Prezime,
                Drzava = vozacDTO.Drzava,
                DatumRodjenja = vozacDTO.DatumRodjenja
            };
        }

        static VozacDTO VozacToDTO(Vozac vozac)
        {
            VozacDTO vozacDTO = new VozacDTO
            {
                Id = vozac.Id,
                Ime = vozac.Ime,
                Prezime = vozac.Prezime,
                Drzava = vozac.Drzava,
                DatumRodjenja = vozac.DatumRodjenja
            };
            if (vozac.Ucinak?.Count > 0)
            {
                vozacDTO.Ucinak = new List<UcinakDTO>();
                foreach (Ucinak ucinak in vozac.Ucinak)
                {
                    vozacDTO.Ucinak.Add(new UcinakDTO
                    {
                        Id = ucinak.Id,
                        Plasman = ucinak.Plasman,
                        StartnaPozicija = ucinak.StartnaPozicija,
                        Vremena = ucinak.Vremena
                    });
                }
            }
            return vozacDTO;
        }
    }
}
