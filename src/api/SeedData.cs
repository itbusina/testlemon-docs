namespace api
{
    public class SeedData
    {
        public static IEnumerable<RandomUser> SeedUsers(int count)
        {
            var users = Enumerable.Range(0, count).Select(x => new RandomUser()).ToList();
            return users;
        }
    }
}