using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Infrastructure.Data.Seed
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _context;

        public DatabaseInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            await SeedEntitiesAsync(new SeedDataProvider<Lookup, int>(LookupsSeed.GetData), _context.Lookups);
        }

        private async Task SeedEntitiesAsync<T, ET>(ISeedDataProvider<T, ET> provider, DbSet<T> dbSet, bool? isPermissionSeed = null) where T : Entity<ET>
        {
            var executionStrategy = _context.Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Lookups] ON");

                        var entities = provider.GetSeedData();
                        if (!entities.Any()) return;

                        var existingEntities = await dbSet.AsNoTracking().ToListAsync();
                        var dbIds = existingEntities.Select(e => e.Id).ToList();

                        var itemsToAdd = entities.Where(e => !dbIds.Contains(e.Id)).ToList();
                        var itemsToUpdate = entities.Where(e => dbIds.Contains(e.Id)).ToList();

                        if (itemsToAdd.Any())
                            await dbSet.AddRangeAsync(itemsToAdd);

                        // Update existing entities
                        if (itemsToUpdate.Any())
                        {
                            foreach (var item in itemsToUpdate)
                            {
                                var existing = existingEntities.FirstOrDefault(e => e.Id.Equals(item.Id));
                                if (existing != null)
                                {
                                    var entry = _context.Entry(existing);
                                    foreach (var property in entry.Properties)
                                    {
                                        if (property.Metadata.Name != "Id" && property.Metadata.Name != "Code" && property.Metadata.Name != "CreatedDate")
                                        {
                                            property.CurrentValue = _context.Entry(item).Property(property.Metadata.Name).CurrentValue;
                                            property.IsModified = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (itemsToAdd.Any() || itemsToUpdate.Any())
                            await _context.SaveChangesAsync();

                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Lookups] OFF");

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction in case of an error
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

    }
}