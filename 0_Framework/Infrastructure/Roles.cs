namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "2";
        public const string User = "3";
        public const string Manager = "4";


        public static string GetRoleBy(int id)
        {
            switch (id)
            {
                case 2:
                    return "مدیر سیستم";
                case 3:
                    return "کاربر";
                case 4:
                    return "منیجر";
                default:
                     return "";
            }
        }
    }
}
