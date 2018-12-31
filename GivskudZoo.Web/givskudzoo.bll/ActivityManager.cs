using System;
using System.Collections.Generic;
using GivskudZoo.Dal;
using GivskudZoo.Model;
using System.Data.Entity;
using System.Linq;

namespace GivskudZoo.Bll
{
    public class ActivityManager
    {
        public IEnumerable<ActivityDto> GetActivityList(DateTime dateFilter)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Activity.Where(x => DbFunctions.TruncateTime(x.BeginDate) == dateFilter.Date).OrderByDescending(x => x.BeginDate);

                var mapped = AutoMapper.Mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityDto>>(ns);

                return mapped;
            }
        }

        public ActivityDto GetActivity(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Activity.FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                var mapped = AutoMapper.Mapper.Map<Activity, ActivityDto>(ns);

                return mapped;
            }
        }

        public bool AddActivity(ActivityDto events)
        {
            var mapped = AutoMapper.Mapper.Map<ActivityDto, Activity>(events);

            using (var ctx = new Entities())
            {
                mapped.CreationDate = DateTime.Now;
                mapped.LastUpdateDate = DateTime.Now;

                ctx.Activity.Add(mapped);

                return ctx.SaveChanges() > 0;
            }
        }

        public bool PutActivity(ActivityDto events)
        {
            var mapped = AutoMapper.Mapper.Map<ActivityDto, Activity>(events);

            using (var ctx = new Entities())
            {
                var ns = ctx.Activity.FirstOrDefault(x => x.Id == events.Id);

                if(ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {events.Id}");

                AutoMapper.Mapper.Map(mapped, ns);

                ns.LastUpdateDate = DateTime.Now;

                ctx.Entry(ns).State = EntityState.Modified;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteActivity(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Activity.FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                ctx.Activity.Remove(ns);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
