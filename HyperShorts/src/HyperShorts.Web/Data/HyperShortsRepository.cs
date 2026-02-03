namespace HyperShorts.Web.Data
{
    public class HyperShortsRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<HyperShort> Save(HyperShort hyperShort) 
        {
            _context.Add(hyperShort);
            await _context.SaveChangesAsync();
            return hyperShort;
        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<HyperShort?> Get(string shortCode)
        {
            var result = await _context.HyperShorts.FindAsync(shortCode);
            return result;
        }
    }
}
