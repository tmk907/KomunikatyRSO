using KomunikatyRSO.Web.Infrastructure.EF;
using KomunikatyRSO.Web.Infrastructure.Models;
using KomunikatyRSO.Shared.Commands.Notifications;
using KomunikatyRSO.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class NotificationsService
    {
        private readonly NotificationsDbContext dbContext;

        public NotificationsService(NotificationsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task UpdatePushChannelAsync(Guid userId, string channel)
        {
            var user = await dbContext.Users.FindAsync(userId);
            user.PushChannel = channel;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePrefrencesAsync(UpdatePreferences command)
        {
            var user = await dbContext.Users.FindAsync(command.UserId);

            if (user.ModificationDate > command.ModificationDate) return;

            user.Dolnoslaskie = command.Preferences.Provinces.Dolnoslaskie;
            user.KujawskoPomorskie = command.Preferences.Provinces.KujawskoPomorskie;
            user.Lodzkie = command.Preferences.Provinces.Lodzkie;
            user.Lubelskie = command.Preferences.Provinces.Lubelskie;
            user.Lubuskie = command.Preferences.Provinces.Lubuskie;
            user.Malopolskie = command.Preferences.Provinces.Malopolskie;
            user.Mazowieckie = command.Preferences.Provinces.Mazowieckie;
            user.Opolskie = command.Preferences.Provinces.Opolskie;
            user.Podkarpackie = command.Preferences.Provinces.Podkarpackie;
            user.Podlaskie = command.Preferences.Provinces.Podlaskie;
            user.Pomorskie = command.Preferences.Provinces.Pomorskie;
            user.Slaskie = command.Preferences.Provinces.Slaskie;
            user.Swietokrzyskie = command.Preferences.Provinces.Swietokrzyskie;
            user.WarminskoMazuskie = command.Preferences.Provinces.WarminskoMazuskie;
            user.Wielkopolskie = command.Preferences.Provinces.Wielkopolskie;
            user.Zachodniopomorskie = command.Preferences.Provinces.Zachodniopomorskie;

            user.Drogowe = command.Preferences.Categories.Drogowe;
            user.Hydro = command.Preferences.Categories.Hydro;
            user.Meteo = command.Preferences.Categories.Meteo;
            user.Ogolne = command.Preferences.Categories.Ogolne;

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetSubsciberUrls(string category, string province)
        {
            return await dbContext.Users
                .Where(map[category])
                .Where(map[province])
                //.Where(ToLambda<User>(province))
                //.Where(ToLambda<User>(category))
                .Where(u => u.PushChannel != "!")
                .Select(u => u.PushChannel)
                .ToListAsync();
        }

        private static Expression<Func<T, bool>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(bool));

            return Expression.Lambda<Func<T, bool>>(propAsObject, parameter);
        }

        //private Func<User, bool> ToCategoryPredicate(string category)
        //{
        //    if (category == CategoriesInfo.Drogowe)
        //    {
        //        return u => u.Drogowe;
        //    }
        //    else if (category == CategoriesInfo.Hydro)
        //    {
        //        return u => u.Hydro;
        //    }
        //    else if (category == CategoriesInfo.Meteo)
        //    {
        //        return u => u.Meteo;
        //    }
        //    else if (category == CategoriesInfo.Ogolne)
        //    {
        //        return u => u.Ogolne;
        //    }
        //    throw new ArgumentException();
        //}

        private Dictionary<string, Expression<Func<User, bool>>> map = new Dictionary<string, Expression<Func<User, bool>>>()
        {
            { CategoriesInfo.Drogowe, u => u.Drogowe },
            { CategoriesInfo.Hydro, u => u.Hydro },
            { CategoriesInfo.Meteo, u => u.Meteo },
            { CategoriesInfo.Ogolne, u => u.Ogolne },
            { "dolnoslaskie" , u => u.Dolnoslaskie },
            { "kujawsko-pomorskie" , u => u.KujawskoPomorskie },
            { "lubelskie" , u => u.Lubelskie },
            { "lubuskie" , u => u.Lubuskie },
            { "lodzkie" , u => u.Lodzkie },
            { "malopolskie" , u => u.Malopolskie },
            { "mazowieckie" , u => u.Mazowieckie },
            { "opolskie" , u => u.Opolskie },
            { "podkarpackie" , u => u.Podkarpackie },
            { "podlaskie" , u => u.Podlaskie },
            { "pomorskie" , u => u.Pomorskie },
            { "slaskie" , u => u.Slaskie },
            { "swietokrzyskie", u => u.Swietokrzyskie },
            { "warminsko-mazurskie", u => u.WarminskoMazuskie },
            { "wielkopolskie" , u => u.Wielkopolskie },
            { "zachodniopomorskie" , u => u.Zachodniopomorskie }
        };
    }
}