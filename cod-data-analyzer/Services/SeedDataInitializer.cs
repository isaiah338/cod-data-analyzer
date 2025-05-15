using cod_data_analyzer.Data;
using Microsoft.AspNetCore.Identity;

namespace cod_data_analyzer.Services
{
    public class SeedDataInitializer
    {
        private MultiplayerDataDatabaseContext _context;

        public SeedDataInitializer(MultiplayerDataDatabaseContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // This is where we can add data to the database
            // https://dotnettutorials.net/lesson/asp-net-core-identity-role-based-authorization/
            // https://dotnettutorials.net/lesson/how-to-add-or-remove-users-from-role-in-asp-net-core-identity/


        }
    }
}
