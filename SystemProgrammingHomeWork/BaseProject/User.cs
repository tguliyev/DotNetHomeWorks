namespace BaseProject
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }

        public User(string firstName, string lastName, int genderId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.GenderId = genderId;
        }

        public override string ToString()
        {
            return $"{Id}. {FirstName} {LastName}";
        }
    }
}