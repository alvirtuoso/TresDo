using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeDo.Models.DomainModel
{


    public class DataEventRecord
    {
        [Key]
        public long DataEventRecordId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("SourceInfoId")]
        public SourceInfo SourceInfo { get; set; }
        public long SourceInfoId { get; set; }
    }

}
