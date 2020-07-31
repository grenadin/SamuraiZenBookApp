using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {

        private static SamuraiContextNoTracking _context = new SamuraiContextNoTracking();
        static void Main()
        {
            //context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            //InsertMultipleSamurais();
            //InsertVariousTypes();
            //GetSamuraisSimpler();
            //QueryFilters();
            //QueryFiltersExtend();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();

            //MultipleDatabaseOperations();
            //RetrieveAndDeleteASamurai();
            //AddSamurai();
            //GetSamurais("After Add:");

            //InsertBattle();
            QueryAndUpdateBattle_Disconnected();
            Console.WriteLine("Press Any Key");
            Console.ReadKey();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            //This finished connected with Database in 1 context
            //The dbcontext will disconnected
            var battle = _context.Battles.AsNoTracking().FirstOrDefault(); //Notrack

            battle.EndDate = new DateTime(1560, 06, 15); //Like the bind 
           
            //This time _context is disconnected
            //now created new context from SamuraiContext
            using (var newContextInstance = new SamuraiContextNoTracking())
            {
                newContextInstance.Battles.Update(battle);//Context will start tracking object and mark it state as modified
                newContextInstance.SaveChanges();

            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }

        private static void RetrieveAndDeleteASamurai()
        {
            var samurai = _context.Samurais.Find(5);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Kuma" }); //Add new Samurai
            _context.Clans.Add(new Clan { ClanName = "Minamoto" }); //Add new Clan
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            Console.WriteLine($"use skip and take");
            var samuraiss = _context.Samurais.Skip(1)
                                             .Take(3)
                                             .ToList();
            samuraiss.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();// use these for update into Database
        }

        private static void QueryFiltersExtend()
        {
            var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name,"B%"))
                                            .ToList();

            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void QueryFilters()
        {

            //with out send parameter in SQL Syntax // This is hard code to SQL
            var samurais1 = _context.Samurais.Where(s => s.Name == "Harirak")
                                            .ToList();
            

            //with send parameter in SQL Syntax
            var name = "Somtavil";
            var samurais2 = _context.Samurais.Where(s => s.Name == name)
                                             .ToList();
            foreach (var samurai in samurais2)
            {
               
                Console.WriteLine($"use ToList() {samurai.Name}");
            }
            var samurais3 = _context.Samurais.FirstOrDefault(s => s.Name==name);

            Console.WriteLine($"use FirstOrDefault() {samurais3.Name}");

            var samurais4 = _context.Samurais.Find(2);
            Console.WriteLine($"use Find(key) {samurais4.Name}");

            var last = _context.Samurais.OrderBy(s => s.Name)
                                        .LastOrDefault();
            Console.WriteLine($"use LastOrDefault() and OrderBy Id {last.Name}");

        }

        private static void GetSamuraisSimpler()
        {
            //var samurais = context.Samurais.ToList();
            var query = _context.Samurais;
            

            foreach (var samurai in query) // database will stay open until finish foreach
            {
                Console.WriteLine(samurai.Name);
            }

            var query2 = _context.Samurais;
            var samurais2 = query.ToList(); //use ToList to get it into list
            //this is the best way fast performance
            foreach (var samurai2 in samurais2) 
            {
                Console.WriteLine(samurai2.Name);
            }
        }

        private static void InsertVariousTypes() //Use DbContext to Add value
        {
            //use 
            var samurai = new Samurai { Name = "KiroRabbit" };
            var clan = new Clan { ClanName = "Rabbito Imperial Clan"};
            //use dbcontext
            _context.AddRange(samurai, clan);//
            _context.SaveChanges();
        }

        private static void InsertMultipleSamurais() //use Dbset to Add value
        {
            var samurai1 = new Samurai { Name = "Bunnyto" };
            var samurai2 = new Samurai { Name = "Flamingo" };
            var samurai3 = new Samurai { Name = "Bearo" };
            var samurai4 = new Samurai { Name = "Buttero" };
            //use dbset
            _context.Samurais.AddRange(samurai1,samurai2,samurai3,samurai4);//
            _context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Harirak" };
            _context.Samurais.Add(samurai);//
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}
