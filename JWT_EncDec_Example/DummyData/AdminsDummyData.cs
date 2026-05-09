using JWT_EncDec_Example.DTOs;

namespace JWT_EncDec_Example.DummyData
{
    public static class AdminsDummyData
    {
        public static List<Admin> Admins { get; } = new List<Admin>
        {
            new Admin { Id = 1,UserName = "admin", Password = "admin123", Role = "Admin" },
            new Admin { Id = 2 ,UserName = "admin", Password = "admin123", Role = "admin" },
        };
    }
}
