namespace api
{
    public class RandomUser
    {
        public RandomUser()
        {
            Id = Random.Shared.Next();
            Name = GenerateRandomName();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private static string GenerateRandomName()
        {
            string[] adjectives = ["Cool", "Fast", "Clever", "Brave", "Bright", "Sharp", "Quick", "Smart", "Bold", "Lucky"];
            string[] nouns = ["Tiger", "Eagle", "Lion", "Wolf", "Panther", "Fox", "Hawk", "Bear", "Shark", "Dragon"];

            Random random = new();

            string randomAdjective = adjectives[random.Next(adjectives.Length)];
            string randomNoun = nouns[random.Next(nouns.Length)];
            int randomNumber = random.Next(1000, 9999);  // Generates a random number between 1000 and 9999

            string randomUserName = randomAdjective + randomNoun + randomNumber;

            return randomUserName;
        }
    }
}