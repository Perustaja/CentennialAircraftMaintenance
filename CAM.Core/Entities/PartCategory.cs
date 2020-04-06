using System.ComponentModel.DataAnnotations;

namespace CAM.Core.Entities
{
    /// <summary>
    /// Contains data relating to categories used for parts.
    /// </summary>
    public class PartCategory
    {
        public int Id { get; private set; }
        [Required]
        [StringLength(30)]
        public string Name { get; private set; }
        private PartCategory()
        {
            // Required by EF
        }
        public PartCategory(string name)
        {
            Name = name;
        }
        public void ChangeName(string name) => Name = name;
    }
}