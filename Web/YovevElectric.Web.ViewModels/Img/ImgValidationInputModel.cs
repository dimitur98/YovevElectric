namespace YovevElectric.Web.ViewModels.Img
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ImgValidationInputModel
    {
        [Required]
        public string Format { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public string Type { get; set; }
    }

}
