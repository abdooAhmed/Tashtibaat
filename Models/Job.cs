namespace Tashtibaat.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PersonalPic {  get; set; }
        public string JobType { get; set; }
        public string City {  get; set; }
        public string Address { get; set; }
        public int PhoneNum {  get; set; }
        public string IDUppperPic {  get; set; }
        public string IDLowerPic { get; set; }
        public string CriminalRecordPic { get; set; }
        public Users user { get; set; }
    }
    public class JobDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public IFormFile PersonalPic { get; set; }
        public string JobType { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PhoneNum { get; set; }
        public IFormFile IDUppperPic { get; set; }
        public IFormFile IDLowerPic { get; set; }
        public IFormFile CriminalRecordPic { get; set; }
    }
}
