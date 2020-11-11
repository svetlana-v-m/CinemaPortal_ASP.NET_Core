using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaPortal_ASP.NET_Core.Models
{
    public class Cinema
    {
        //CinemaID. This is the primary key
        public int CinemaID { get; set; }

        //Title. The name of the cinema
        [Required]
        [DisplayName("Название:")]
        public string Name { get; set; }

        //Poster. This is a picture file
        [DisplayName("Постер:")]
        [MaxLength]
        public byte[] Poster { get; set; }

        //ImageMimeType, stores the MIME type for the Poster
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "Режиссёр:")]
        public string FilmMaker { get; set; }


        [Display(Name = "Описание:")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //CreatedDate
        [DisplayName("Год:")]
        public int Year { get; set; }

        //UserName. This is the name of the user who created the photo
        [DisplayName("Пользователь:")]
        public string UserName { get; set; }

    }
}
