using System.Collections.Generic;
using System.Linq;

namespace KomunikatyRSO.Shared.Models
{
    public class ProvinceInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class ProvincesInfo
    {
        static ProvincesInfo()
        {
            AllProvinces = new List<ProvinceInfo>()
            {
                new ProvinceInfo() { Id=1, Name="Dolnośląskie", Slug="dolnoslaskie" },
                new ProvinceInfo() { Id=2, Name="Kujawsko-pomorskie", Slug="kujawsko-pomorskie" },
                new ProvinceInfo() { Id=3, Name="Lubelskie", Slug="lubelskie" },
                new ProvinceInfo() { Id=4, Name="Lubuskie", Slug="lubuskie" },
                new ProvinceInfo() { Id=5, Name="Łódzkie", Slug="lodzkie" },
                new ProvinceInfo() { Id=6, Name="Małopolskie", Slug="malopolskie" },
                new ProvinceInfo() { Id=7, Name="Mazowieckie", Slug="mazowieckie" },
                new ProvinceInfo() { Id=8, Name="Opolskie", Slug="opolskie" },
                new ProvinceInfo() { Id=9, Name="Podkarpackie", Slug="podkarpackie" },
                new ProvinceInfo() { Id=10, Name="Podlaskie", Slug="podlaskie" },
                new ProvinceInfo() { Id=11, Name="Pomorskie", Slug="pomorskie" },
                new ProvinceInfo() { Id=12, Name="Śląskie", Slug="slaskie" },
                new ProvinceInfo() { Id=13, Name="Świętokrzyskie", Slug="swietokrzyskie" },
                new ProvinceInfo() { Id=14, Name="Warmińsko-mazurskie", Slug="warminsko-mazurskie" },
                new ProvinceInfo() { Id=15, Name="Wielkopolskie", Slug="wielkopolskie" },
                new ProvinceInfo() { Id=16, Name="Zachodniopomorskie", Slug="zachodniopomorskie" }
            };

            All = new ProvinceInfo() { Id = 0, Name = "Wszystkie", Slug = "wszystkie" };
        }

        public static readonly List<ProvinceInfo> AllProvinces;

        public static readonly ProvinceInfo All;

        public static ProvinceInfo GetByName(string name)
        {
            return AllProvinces.Where(p => p.Name.Equals(name)).FirstOrDefault();
        }

        public static ProvinceInfo GetById(int id)
        {
            return AllProvinces.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public static ProvinceInfo GetBySlug(string slug)
        {
            return AllProvinces.Where(p => p.Slug.Equals(slug)).FirstOrDefault();
        }
    }
}
