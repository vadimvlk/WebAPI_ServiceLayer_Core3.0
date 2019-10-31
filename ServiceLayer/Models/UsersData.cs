using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayerApi.Models
{
    public class UsersData
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }

        [DefaultValue(true)]
        public bool IsComplete { get; set; }
    }
}