using System.ComponentModel;

namespace CRUD_API.Models
{
    public class CountryModel
    {
        [DefaultValue("")]
        public int? CountryID { get; set; }
        [DefaultValue("")]
        public string CountryName { get; set; }
        [DefaultValue("")]
        public string CountryCode { get; set; }
    }

    public class CountryDropDown
    {
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
