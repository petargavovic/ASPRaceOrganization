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
    public class UcinciRepository(AppDbContext appDbContext) : IGenericRepositoryInterface<UcinakDTO>
    {
        readonly int[] brojeviPoena = { 12, 10, 8, 5, 4, 3, 2, 1 };
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var u = await appDbContext.Ucinci.FindAsync(id);
            if (u is null) return NotFound();
            Trka trka = await appDbContext.Trke.Include(t => t.Kategorija).FirstOrDefaultAsync(t => t.Id == u.TrkaID);
            RangLista rl = new RangLista()
            {
                Kategorija = trka.Kategorija,
                KrajSezone = trka.DatumTrke,
                Plasman = new()
            };
            List<RangLista> listaRangListi = await appDbContext.RangListe.Include(rl => rl.Kategorija).ToListAsync();
            listaRangListi = listaRangListi.Where(r => r.Kategorija.Id == rl.Kategorija.Id).ToList();
            RangLista rl2 = null;
            bool hasSeason = false;
            foreach (RangLista rangLista in listaRangListi)
                if (rangLista.KrajSezone.Year == rl.KrajSezone.Year)
                {
                    hasSeason = true;
                    rl2 = rangLista;
                    rl.Id = rangLista.Id;
                }
            if (!hasSeason)
            {
                appDbContext.RangListe.Add(rl);
                await Commit();
            }
            else
            {
                if (rl.KrajSezone > rl2.KrajSezone)
                {
                    var r = await appDbContext.RangListe.FindAsync(rl.Id);
                    if (r is null) return NotFound();
                    r.KrajSezone = rl.KrajSezone;
                    await Commit();
                }
            }
            var v = await appDbContext.Vozaci.FindAsync(u.VozacID);
            Plasman p = new Plasman()
            {
                Vozac = v,
                RangLista = (await appDbContext.RangListe.ToListAsync()).Where(r => r.Kategorija.Id == rl.Kategorija.Id && r.KrajSezone.Year == rl.KrajSezone.Year).First(),
                BrojPoena = brojeviPoena[u.Plasman - 1]
            };
            Plasman plasman = (await appDbContext.Plasmani.ToListAsync()).Where(plasman => plasman.VozacID == p.Vozac.Id && plasman.RangListaID == p.RangLista.Id).FirstOrDefault();
            if (plasman != null)
            {
                var pl = await appDbContext.Plasmani.FindAsync(plasman.Id);
                if (pl is null) return NotFound();
                plasman.BrojPoena -= brojeviPoena[u.Plasman - 1];
                if (plasman.BrojPoena <= 0)
                    appDbContext.Plasmani.Remove(pl);
                else
                    pl.BrojPoena = plasman.BrojPoena;
                await Commit();
            }
            appDbContext.Ucinci.Remove(u);
            await Commit();
            return Success("Uspešno brisanje učinka!");
        }

        public async Task<List<UcinakDTO>> GetAll()
        {
            var ucinci = await appDbContext.Ucinci
        .Include(u => u.Vozac) // Include related Vozac
        .ToListAsync();

            return ucinci.Select(u => new UcinakDTO
            {
                Id = u.Id,
                StartnaPozicija = u.StartnaPozicija,
                Plasman = u.Plasman,
                Vremena = u.Vremena,
                TrkaID = u.TrkaID,
                VozacID = u.VozacID,
                ImeIPrezime = u.Vozac != null ? u.Vozac.Ime + " " + u.Vozac.Prezime : null
            }).ToList();

        }

        public async Task<UcinakDTO> GetById(int id)
        {
            var ucinak = await appDbContext.Ucinci
        .Include(u => u.Vozac)
        .FirstOrDefaultAsync(u => u.Id == id);

            if (ucinak == null)
            {
                NotFound();
                return null;
            }

            return new UcinakDTO
            {
                Id = ucinak.Id,
                StartnaPozicija = ucinak.StartnaPozicija,
                Plasman = ucinak.Plasman,
                Vremena = ucinak.Vremena,
                TrkaID = ucinak.TrkaID,
                VozacID = ucinak.VozacID,
                ImeIPrezime = ucinak.Vozac != null ? $"{ucinak.Vozac.Ime} {ucinak.Vozac.Prezime}" : null
            };
        }

        public async Task<GeneralResponse> Insert(UcinakDTO item)
        {
            appDbContext.Ucinci.Add(DTOToUcinak(item));
            await Commit();


            Trka trka = await appDbContext.Trke.Include(t => t.Kategorija).FirstOrDefaultAsync(t => t.Id == item.TrkaID);
            RangLista rl = new RangLista()
            {
                Kategorija = trka.Kategorija,
                KrajSezone = trka.DatumTrke,
                Plasman = new()
            };
            List<RangLista> listaRangListi = await appDbContext.RangListe.Include(rl => rl.Kategorija).ToListAsync();
            listaRangListi = listaRangListi.Where(r => r.Kategorija.Id == rl.Kategorija.Id).ToList();
            RangLista rl2 = null;
            bool hasSeason = false;
            foreach (RangLista rangLista in listaRangListi)
                if (rangLista.KrajSezone.Year == rl.KrajSezone.Year)
                {
                    hasSeason = true;
                    rl2 = rangLista;
                    rl.Id = rangLista.Id;
                }
            if (!hasSeason)
            {
                appDbContext.RangListe.Add(rl);
                await Commit();
            }
            else
            {
                if (rl.KrajSezone > rl2.KrajSezone)
                {
                    var r = await appDbContext.RangListe.FindAsync(rl.Id);
                    if (r is null) return NotFound();
                    r.KrajSezone = rl.KrajSezone;
                    await Commit();
                }
            }
            var v = await appDbContext.Vozaci.FindAsync(item.VozacID);
            Plasman p = new Plasman()
            {
                Vozac = v,
                RangLista = (await appDbContext.RangListe.ToListAsync()).Where(r => r.Kategorija.Id == rl.Kategorija.Id && r.KrajSezone.Year == rl.KrajSezone.Year).First(),
                BrojPoena = brojeviPoena[item.Plasman - 1]
            };
            Plasman plasman = (await appDbContext.Plasmani.ToListAsync()).Where(plasman => plasman.VozacID == p.Vozac.Id && plasman.RangListaID == p.RangLista.Id).FirstOrDefault();
            if (plasman != null)
            {
                p = plasman;
                p.BrojPoena += brojeviPoena[item.Plasman - 1];
                var pl = await appDbContext.Plasmani.FindAsync(item.Id);
                if (pl is null) return NotFound();
                pl.BrojPoena = p.BrojPoena;
            }
            else
                appDbContext.Plasmani.Add(p);
            await Commit();

            return Success("Uspešno dodavanje učinka!");
        }

        public async Task<GeneralResponse> Update(UcinakDTO item)
        {
            var u = await appDbContext.Ucinci.FindAsync(item.Id);
            if (u is null) return NotFound();
            int plasmanPre = u.Plasman;
            u.StartnaPozicija = item.StartnaPozicija;
            u.Plasman = item.Plasman;
            u.Vremena = item.Vremena;
            await Commit();

            Trka trka = await appDbContext.Trke.Include(t => t.Kategorija).FirstOrDefaultAsync(t => t.Id == item.TrkaID);
            RangLista rl = new RangLista()
            {
                Kategorija = trka.Kategorija,
                KrajSezone = trka.DatumTrke,
                Plasman = new()
            };
            List<RangLista> listaRangListi = await appDbContext.RangListe.Include(rl => rl.Kategorija).ToListAsync();
            listaRangListi = listaRangListi.Where(r => r.Kategorija.Id == rl.Kategorija.Id).ToList();
            RangLista rl2 = null;
            bool hasSeason = false;
            foreach (RangLista rangLista in listaRangListi)
                if (rangLista.KrajSezone.Year == rl.KrajSezone.Year)
                {
                    hasSeason = true;
                    rl2 = rangLista;
                    rl.Id = rangLista.Id;
                }
            if (!hasSeason)
            {
                appDbContext.RangListe.Add(rl);
                await Commit();
            }
            else
            {
                if (rl.KrajSezone > rl2.KrajSezone)
                {
                    var r = await appDbContext.RangListe.FindAsync(rl.Id);
                    if (r is null) return NotFound();
                    r.KrajSezone = rl.KrajSezone;
                    await Commit();
                }
            }
            var v = await appDbContext.Vozaci.FindAsync(item.VozacID);
            Plasman p = new Plasman()
            {
                Vozac = v,
                RangLista = (await appDbContext.RangListe.ToListAsync()).Where(r => r.Kategorija.Id == rl.Kategorija.Id && r.KrajSezone.Year == rl.KrajSezone.Year).First(),
                BrojPoena = brojeviPoena[item.Plasman - 1]
            };
            Plasman plasman = (await appDbContext.Plasmani.ToListAsync()).Where(plasman => plasman.VozacID == p.Vozac.Id && plasman.RangListaID == p.RangLista.Id).FirstOrDefault();
                        if (plasman != null)
                        {
                            p = plasman;
                                p.BrojPoena -= brojeviPoena[plasmanPre - 1];
                                p.BrojPoena += brojeviPoena[item.Plasman - 1];
                var pl = await appDbContext.Plasmani.FindAsync(plasman.Id);
                if (pl is null) return NotFound();
                pl.BrojPoena = p.BrojPoena;
                await Commit();
            }

            return Success("Uspešna izmena učinka!");

        }

        private static GeneralResponse NotFound() => new(true, "Žao nam je, učinak nije pronađen!");
        private static GeneralResponse Success(string poruka) => new(false, poruka);
        private async Task Commit() => await appDbContext.SaveChangesAsync();

        static Ucinak DTOToUcinak(UcinakDTO ucinakDTO)
        {
            return new Ucinak
            {
                Id = ucinakDTO.Id,
                StartnaPozicija = ucinakDTO.StartnaPozicija,
                Plasman = ucinakDTO.Plasman,
                Vremena = ucinakDTO.Vremena,
                TrkaID = ucinakDTO.TrkaID,
                VozacID = ucinakDTO.VozacID
            };
        }

        async Task<UcinakDTO> UcinakToDTOAsync(Ucinak ucinak)
        {
            var vozac = await appDbContext.Vozaci.FindAsync(ucinak.VozacID);
            return new UcinakDTO
            {
                Id = ucinak.Id,
                StartnaPozicija = ucinak.StartnaPozicija,
                Plasman = ucinak.Plasman,
                Vremena = ucinak.Vremena,
                TrkaID = ucinak.TrkaID,
                VozacID = ucinak.VozacID,
                ImeIPrezime = vozac.Ime + " " + vozac.Prezime
                //                Vozac = new Vozac
                //                {
                //                    Ime = ucinak.Vozac.Ime,
                //                    Prezime = ucinak.Vozac.Prezime,
                //                    Drzava = ucinak.Vozac.Drzava,
                //                    DatumRodjenja = ucinak.Vozac.DatumRodjenja
                //               }
            };
        }
    }
}
