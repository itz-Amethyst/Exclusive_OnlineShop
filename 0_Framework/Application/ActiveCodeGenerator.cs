namespace _0_Framework.Application
{
    public class ActiveCodeGenerator
    {
        public static string GenerateActiveCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
