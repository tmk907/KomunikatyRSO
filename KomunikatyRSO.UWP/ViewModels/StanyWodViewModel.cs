using System;
using System.Collections.Generic;
using KomunikatyRSO.Shared.Api.Models;
using KomunikatyRSO.Shared.Models;

namespace KomunikatyRSOUWP.ViewModels
{
    public class StanyWodViewModel : BasicViewModel
    {
        public StanyWodViewModel() : base()
        {
            catInfo = CategoriesInfo.GetBySlug("stany-wod");
        }

        protected override List<RSONews> ParseNewses(List<RSONews> list)
        {
            foreach (var news in list)
            {
                if (news.RiverName != "")
                {
                    string trend = (news.WaterLevelTrend == "-1") ? "↓ malejący" : (news.WaterLevelTrend == "1") ? "↑ rosnący" : "↔ bez zmian";
                    news.Title = "Rzeka: " + news.RiverName + ", wodowskaz: " + news.LocationName;
                    news.Shortcut = "stan wody: " + news.WaterLevelValue + "cm" + Environment.NewLine +
                        "stan ostrzegawczy: " + news.WaterLevelWarningStatusValue + "cm" + Environment.NewLine +
                        "stan alarmowy: " + news.WaterLevelAlarmStatusValue + "cm" + Environment.NewLine +
                        "trend: " + trend;
                }
            }
            return list;
        }
    }
}
