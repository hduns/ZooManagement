using ZooManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace ZooManagement;


public class ZooManagementContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<WorkAllocation> WorkAllocations { get; set; }
    public DbSet<Zookeeper> Zookeepers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db").UseSeeding((context, _) =>
        {
            if (!context.Set<Classification>().Any())
            {
                SeedClassification(context);
                context.SaveChanges();
            }

            if (!context.Set<Enclosure>().Any())
            {
                SeedEnclosure(context);
                context.SaveChanges();
            }

            if (!context.Set<Zookeeper>().Any())
            {
                SeedZookeeper(context);
                context.SaveChanges();
            }

            if (!context.Set<AnimalType>().Any())
            {
                SeedAnimalType(context);
                context.SaveChanges();
            }

            if (!context.Set<WorkAllocation>().Any())
            {
                SeedWorkAllocation(context);
                context.SaveChanges();
            }

            if (!context.Set<Animal>().Any())
            {
                SeedAnimal(context);
                context.SaveChanges();
            }
        })
        .UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            if (!context.Set<Classification>().Any())
            {
                SeedClassification(context);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (!context.Set<Enclosure>().Any())
            {
                SeedEnclosure(context);
                await context.SaveChangesAsync(cancellationToken);           
            }

            if (!context.Set<Zookeeper>().Any())
            {
                SeedEnclosure(context);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (!context.Set<AnimalType>().Any())
            {
                SeedAnimalType(context);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (!context.Set<WorkAllocation>().Any())
            {
                SeedWorkAllocation(context);
                await context.SaveChangesAsync(cancellationToken);
            }

            if (!context.Set<Animal>().Any())
            {
                SeedAnimal(context);
                await context.SaveChangesAsync(cancellationToken);
            }
        });
    }

    private static void SeedClassification(DbContext context)
    {
        context.Set<Classification>().AddRange(
            new Classification { Class = "Mammal" },
            new Classification { Class = "Bird" },
            new Classification { Class = "Reptile" },
            new Classification { Class = "Insect" },
            new Classification { Class = "Fish" },
            new Classification { Class = "Invertebrate" });
    }
    
    private static void SeedEnclosure(DbContext context)
    {
        context.Set<Enclosure>().AddRange(
            new Enclosure { Name = "Lions", MaxAnimals = 10 },
            new Enclosure { Name = "Aviary", MaxAnimals = 50 },
            new Enclosure { Name = "Reptile", MaxAnimals = 40 },
            new Enclosure { Name = "Giraffe", MaxAnimals = 6 },
            new Enclosure { Name = "Hippo", MaxAnimals = 10 });
    }

    private static void SeedZookeeper(DbContext context)
    {
        context.Set<Zookeeper>().AddRange(
            new Zookeeper { Name = "Holly" },
            new Zookeeper { Name = "Rachel" },
            new Zookeeper { Name = "Hannah" },
            new Zookeeper { Name = "Eleanor" },
            new Zookeeper { Name = "Sophia" },
            new Zookeeper { Name = "Erif" },
            new Zookeeper { Name = "Sasha" });
    }

    private static void SeedAnimalType(DbContext context) 
    {
        context.Set<AnimalType>().AddRange(
            new AnimalType { Species = "Lion", ClassificationId = 1 },
            new AnimalType { Species = "Parrot", ClassificationId = 2 },
            new AnimalType { Species = "Snake", ClassificationId = 3 },
            new AnimalType { Species = "Butterfly", ClassificationId = 4 },
            new AnimalType { Species = "Clownfish", ClassificationId = 5 },
            new AnimalType { Species = "Starfish", ClassificationId = 6 },
            new AnimalType { Species = "Giraffe", ClassificationId = 1 },
            new AnimalType { Species = "Hippo", ClassificationId = 1 }
        );
    }

    private static void SeedWorkAllocation(DbContext context) 
    {
        context.Set<WorkAllocation>().AddRange(
            new WorkAllocation { ZookeeperId = 1, EnclosureId = 1},
            new WorkAllocation { ZookeeperId = 2, EnclosureId = 1},
            new WorkAllocation { ZookeeperId = 2, EnclosureId = 2},
            new WorkAllocation { ZookeeperId = 3, EnclosureId = 3},
            new WorkAllocation { ZookeeperId = 4, EnclosureId = 3},
            new WorkAllocation { ZookeeperId = 5, EnclosureId = 4},
            new WorkAllocation { ZookeeperId = 6, EnclosureId = 4},
            new WorkAllocation { ZookeeperId = 6, EnclosureId = 5},
            new WorkAllocation { ZookeeperId = 7, EnclosureId = 4}
        );
    }

    private static void SeedAnimal(DbContext context)
    {
        var extraAnimals = new List<Animal>();
        Random random = new Random();

        for (int i = 7; i < 100; i++)
        {
            extraAnimals.Add(new Animal { AnimalTypeId = random.Next(1, 9), Name = $"Animal{i}", DateOfBirth = new DateOnly(2000, 10, 21), DateAcquired = new DateOnly(2001, 10, 22), EnclosureId = random.Next(1, 6) });

        }

        context.Set<Animal>().AddRange(
            new Animal { AnimalTypeId = 1, Name = "Lionel", Sex = "F", DateOfBirth = new DateOnly(2015, 10, 21), DateAcquired = new DateOnly(2015, 10, 21), EnclosureId = 1 },
            new Animal { AnimalTypeId = 2, Name = "Polly", Sex = "F", DateOfBirth = new DateOnly(2017, 9, 21), DateAcquired = new DateOnly(2015, 10, 21), EnclosureId = 2 },
            new Animal { AnimalTypeId = 3, Name = "Sally", Sex = "F", DateOfBirth = new DateOnly(2018, 1, 29), DateAcquired = new DateOnly(2015, 10, 21), EnclosureId = 3 },
            new Animal { AnimalTypeId = 4, Name = "Barry", Sex = "M", DateOfBirth = new DateOnly(2023, 5, 14), DateAcquired = new DateOnly(2023, 5, 14), EnclosureId = 2 },
            new Animal { AnimalTypeId = 7, Name = "Geri", Sex = "F", DateOfBirth = new DateOnly(2019, 12, 25), DateAcquired = new DateOnly(2020, 7, 31), EnclosureId = 4 },
            new Animal { AnimalTypeId = 8, Name = "Harry", Sex = "M", DateOfBirth = new DateOnly(2025, 6, 6), DateAcquired = new DateOnly(2025, 6, 6), EnclosureId = 5 }
        );
        context.Set<Animal>().AddRange(extraAnimals);
    }
}
