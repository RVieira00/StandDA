using Stand.Infrastructure;
using Stand.Domain.Models;
using System;
using System.Threading.Tasks;


namespace StandConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Stand");

            using var uow = new UnitOfWork();

            await uow.MarcaRepository.UpsertAsync(
                new Marca("Audi"));
            await uow.MarcaRepository.UpsertAsync(
                new Marca("Tesla"));


            var lstMarcas = await uow.MarcaRepository.FindAllAsync();

            Console.WriteLine("\n\tMarcas: ");
            foreach (var item in lstMarcas)
            {
                Console.WriteLine("\t - " + item);
            }
        }
    }
}
