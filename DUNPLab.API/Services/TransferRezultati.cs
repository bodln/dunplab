using DUNPLab.API.Infrastructure;
using DUNPLab.API.Migrations;
using DUNPLab.API.Models;
using DUNPLab.API.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class TransferRezultati : ITransferRezultati
    {
        private readonly DunpContext context;
        private readonly ILogger<TransferRezultati> logger;

        public TransferRezultati(DunpContext context,
            ILogger<TransferRezultati> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Transfer()
        {
            logger.LogInformation("Beginning transfer. " + DateTime.UtcNow);
            List<RezultatOdMasine> rezultatiOdMasine = context.RezultatiOdMasine
                .Include(rm => rm.SupstanceRezultati)
                .Where(rm => rm.JesuLiPrebaceni == false)
                .ToList();

            foreach (RezultatOdMasine rm in rezultatiOdMasine)
            {
                foreach (SupstancaRezultat sr in rm.SupstanceRezultati)
                {
                    Rezultat rezultat = new Rezultat();

                    rezultat.JeLiUGranicama = sr.JeLiBiloGreske;
                    rezultat.Vrednost = sr.Vrednost;
                    rezultat.IdUzorka = sr.Id;
                    rezultat.Uzorak = context.Uzorci.Where(u => u.Id == 2).FirstOrDefault();
                    if (rezultat.Uzorak != null)
                    {
                        rezultat.Uzorak.KodEpruvete = rm.KodEpruvete;
                    }
                    else
                    {
                        logger.LogWarning("Uzorak is null for Rezultat with IdUzorka: " + rezultat.IdUzorka);
                    }
                    rezultat.IdSupstance = sr.Id;
                    rezultat.Supstanca = context.Supstance.Where(s => s.Id == 2).FirstOrDefault();
                    logger.LogInformation("adding rez " + sr.Id + " from " + rm.Id);
                    context.Add(rezultat);
                }

                rm.JesuLiPrebaceni = true;
            }

            context.SaveChanges();

            logger.LogInformation("Transfer ended. " + DateTime.UtcNow);
        }
    }
}
