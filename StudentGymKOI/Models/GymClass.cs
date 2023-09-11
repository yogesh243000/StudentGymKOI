namespace StudentGymKOI.Models
{
    public class GymClass
    {
        public int Id { get; set; } 

        public string ClassName  { get; set; }
        public int MaxMembers  { get; set; }

        public int CurrentMembers { get; set; } = 0;



    }
}
