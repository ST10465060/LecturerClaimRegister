namespace LecturerClaimRegister.Models
{
    /// <summary>
    /// Simple in-memory data store for claims.
    /// Static so the data survives across HTTP requests, since ASP.NET Core
    /// creates a new controller instance for every request.
    /// Replaced by a database in later phases of the CMCS project.
    /// </summary>
    public static class ClaimStore
    {
        // Seeded with two sample claims so the Index view is never empty on startup.
        private static readonly List<Claim> _claims = new()
        {
            new Claim
            {
                ClaimId = 1,
                LecturerName = "Dr Naledi Mokoena",
                ModuleCode = "PROG6212",
                HoursWorked = 42,
                HourlyRate = 385.00m,
                ClaimMonth = "August 2026",
                Status = "Approved"
            },
            new Claim
            {
                ClaimId = 2,
                LecturerName = "Mr Sipho Dlamini",
                ModuleCode = "WEDE5020",
                HoursWorked = 28.5,
                HourlyRate = 340.50m,
                ClaimMonth = "August 2026",
                Status = "Pending"
            }
        };

        /// <summary>
        /// Returns the claims as a read-only list so callers cannot bypass AddClaim.
        /// </summary>
        public static IReadOnlyList<Claim> GetAll() => _claims;

        /// <summary>
        /// Adds a claim and assigns the next sequential ClaimId.
        /// </summary>
        public static void AddClaim(Claim claim)
        {
            // Max() would throw on an empty list, so guard with a Count check.
            claim.ClaimId = _claims.Count == 0 ? 1 : _claims.Max(c => c.ClaimId) + 1;
            _claims.Add(claim);
        }
    }
}