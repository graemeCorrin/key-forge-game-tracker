using KeyForgeGameTracker.Util;
using System;
using System.ComponentModel.DataAnnotations;

namespace KeyForgeGameTracker.Models
{
    public abstract class KfgtTable
    {
        public int Id { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Constants.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }


        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Constants.DateFormat, ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }


        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}
