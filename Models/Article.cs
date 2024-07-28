using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Auchan_WebAPI;

namespace Auchan_WebAPI.Models
{

    public class Art
    {
        private static int kontor = 0;
       
        public int? Id { get; }
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Text)]
        public string? Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(1000)]
        public string? Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedDate { get; set; }

        public Art() => Id = ++kontor;
        /// <summary>
        /// clone properties of current object
        /// </summary>
        /// <param name="a">a is clone of current object</param>
        public void CloneTo(Art a)
        {
            Type type = this.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (var p in props)
                if (p.CanWrite)
                    p.SetValue(a, p.GetValue(this));
        }
    }
}
