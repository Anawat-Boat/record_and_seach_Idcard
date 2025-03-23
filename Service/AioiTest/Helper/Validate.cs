namespace AioiTest.Helper
{
    public class Validate
    {
        public static bool IsValidThaiCitizenId(string citizenId)
        {
            if (string.IsNullOrWhiteSpace(citizenId) || citizenId.Length != 13 || !citizenId.All(char.IsDigit))
            {
                return false; 
            }

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += (citizenId[i] - '0') * (13 - i);
            }

            int checkDigit = (11 - (sum % 11)) % 10; 
            return checkDigit == (citizenId[12] - '0'); 
        }
    }
}
