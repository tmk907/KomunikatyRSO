using System;

namespace KomunikatyRSO.Web.Infrastructure.Models
{
    public class User : EntityBase
    {
        public User(Guid userId, string passwordHash, string token)
        {
            UserId = userId;
            PasswordHash = passwordHash;

            Token = token;
            PushChannel = "!";
            

            Dolnoslaskie = false;
            KujawskoPomorskie = false;
            Lubelskie = false;
            Lubuskie = false;
            Lodzkie = false;
            Malopolskie = false;
            Mazowieckie = false;
            Opolskie = false;
            Podkarpackie = false;
            Podlaskie = false;
            Pomorskie = false;
            Slaskie = false;
            Swietokrzyskie = false;
            WarminskoMazuskie = false;
            Wielkopolskie = false;
            Zachodniopomorskie = false;
            Ogolne = false;
            Meteo = false;
            Drogowe = false;
            Hydro = false;
        }

        public Guid UserId { get; set; }
        public string PasswordHash { get; set; }

        public string Token { get; set; }

        public string PushChannel { get; set; }

        public DateTime ModificationDate { get; set; }

        public bool Dolnoslaskie { get; set; }
        public bool KujawskoPomorskie { get; set; }
        public bool Lubelskie { get; set; }
        public bool Lubuskie { get; set; }
        public bool Lodzkie { get; set; }
        public bool Malopolskie { get; set; }
        public bool Mazowieckie { get; set; }
        public bool Opolskie { get; set; }
        public bool Podkarpackie { get; set; }
        public bool Podlaskie { get; set; }
        public bool Pomorskie { get; set; }
        public bool Slaskie { get; set; }
        public bool Swietokrzyskie { get; set; }
        public bool WarminskoMazuskie { get; set; }
        public bool Wielkopolskie { get; set; }
        public bool Zachodniopomorskie { get; set; }

        public bool Ogolne { get; set; }
        public bool Meteo { get; set; }
        public bool Drogowe { get; set; }
        public bool Hydro { get; set; }
    }
}
