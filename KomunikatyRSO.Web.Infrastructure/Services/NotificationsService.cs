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
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            user.PushChannel = channel;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePrefrencesAsync(UpdatePreferences command)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == command.UserId);

            if (user.ModificationDate > command.ModificationDate) return;

            user.Dolnoslaskie = command.Preferences.Provinces[ProvinceSlug.Dolnoslaskie];
            user.KujawskoPomorskie = command.Preferences.Provinces[ProvinceSlug.KujawskoPomorskie];
            user.Lodzkie = command.Preferences.Provinces[ProvinceSlug.Lodzkie];
            user.Lubelskie = command.Preferences.Provinces[ProvinceSlug.Lubelskie];
            user.Lubuskie = command.Preferences.Provinces[ProvinceSlug.Lubuskie];
            user.Malopolskie = command.Preferences.Provinces[ProvinceSlug.Malopolskie];
            user.Mazowieckie = command.Preferences.Provinces[ProvinceSlug.Mazowieckie];
            user.Opolskie = command.Preferences.Provinces[ProvinceSlug.Opolskie];
            user.Podkarpackie = command.Preferences.Provinces[ProvinceSlug.Podkarpackie];
            user.Podlaskie = command.Preferences.Provinces[ProvinceSlug.Podlaskie];
            user.Pomorskie = command.Preferences.Provinces[ProvinceSlug.Pomorskie];
            user.Slaskie = command.Preferences.Provinces[ProvinceSlug.Slaskie];
            user.Swietokrzyskie = command.Preferences.Provinces[ProvinceSlug.Swietokrzyskie];
            user.WarminskoMazuskie = command.Preferences.Provinces[ProvinceSlug.WarminskoMazurskie];
            user.Wielkopolskie = command.Preferences.Provinces[ProvinceSlug.Wielkopolskie];
            user.Zachodniopomorskie = command.Preferences.Provinces[ProvinceSlug.Zachodniopomorskie];

            user.Drogowe = command.Preferences.Categories[CategoriesInfo.Drogowe];
            user.Hydro = command.Preferences.Categories[CategoriesInfo.Hydro];
            user.Meteo = command.Preferences.Categories[CategoriesInfo.Meteo];
            user.Ogolne = command.Preferences.Categories[CategoriesInfo.Ogolne];

            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetSubsciberUrlsAsync(string category, string province)
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
            { ProvinceSlug.Dolnoslaskie , u => u.Dolnoslaskie },
            { ProvinceSlug.KujawskoPomorskie, u => u.KujawskoPomorskie },
            { ProvinceSlug.Lubelskie , u => u.Lubelskie },
            { ProvinceSlug.Lubuskie , u => u.Lubuskie },
            { ProvinceSlug.Lodzkie , u => u.Lodzkie },
            { ProvinceSlug.Malopolskie , u => u.Malopolskie },
            { ProvinceSlug.Mazowieckie , u => u.Mazowieckie },
            { ProvinceSlug.Opolskie , u => u.Opolskie },
            { ProvinceSlug.Podkarpackie , u => u.Podkarpackie },
            { ProvinceSlug.Podlaskie , u => u.Podlaskie },
            { ProvinceSlug.Pomorskie , u => u.Pomorskie },
            { ProvinceSlug.Slaskie , u => u.Slaskie },
            { ProvinceSlug.Swietokrzyskie, u => u.Swietokrzyskie },
            { ProvinceSlug.WarminskoMazurskie, u => u.WarminskoMazuskie },
            { ProvinceSlug.Wielkopolskie , u => u.Wielkopolskie },
            { ProvinceSlug.Zachodniopomorskie , u => u.Zachodniopomorskie }
        };
    }
}