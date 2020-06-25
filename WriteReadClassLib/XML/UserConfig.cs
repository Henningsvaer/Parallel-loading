
using System.ComponentModel.DataAnnotations;

namespace WriteReadClassLib
{
    public struct UserConfig
    {
        [Range(0, int.MaxValue)]
        [Required]
        public int FileProcessingThreadsCount { get; set; }
    }
}
