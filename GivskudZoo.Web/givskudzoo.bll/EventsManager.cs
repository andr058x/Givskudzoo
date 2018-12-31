using System;
using System.Collections.Generic;
using GivskudZoo.Dal;
using GivskudZoo.Model;
using System.Data.Entity;
using System.Linq;

namespace GivskudZoo.Bll
{
    public class EventsManager
    {
        public IEnumerable<EventsDto> GetEventsList(FilterBaseDto filter)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Events.Include(x => x.Images);

                ns = FilterQuery(filter, ns);

                var mapped = AutoMapper.Mapper.Map<IEnumerable<Events>, IEnumerable<EventsDto>>(ns);

                return mapped;
            }
        }

        public EventsDto GetEvents(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Events.Include(x => x.Images).FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                var mapped = AutoMapper.Mapper.Map<Events, EventsDto>(ns);

                return mapped;
            }
        }

        public bool AddEvents(EventsDto events)
        {
            var mapped = AutoMapper.Mapper.Map<EventsDto, Events>(events);

            using (var ctx = new Entities())
            {
                mapped.CreationDate = DateTime.Now;
                mapped.LastUpdateDate = DateTime.Now;

                ctx.Events.Add(mapped);

                return ctx.SaveChanges() > 0;
            }
        }

        public bool PutEvents(EventsDto events)
        {
            var mapped = AutoMapper.Mapper.Map<EventsDto, Events>(events);

            using (var ctx = new Entities())
            {
                var ns = ctx.Events.Include(x => x.Images).FirstOrDefault(x => x.Id == events.Id);

                if(ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {events.Id}");

                AutoMapper.Mapper.Map(mapped, ns);

                if (events.DeleteImage)
                {
                    ns.Images = null;
                    ns.ImageId = null;
                }

                if (events.Image != null)
                {
                    ctx.Images.Remove(ns.Images);
                    ctx.Images.Add(mapped.Images);
                    ns.ImageId = mapped.Images.Id;
                }

                ns.LastUpdateDate = DateTime.Now;

                ctx.Entry(ns).State = EntityState.Modified;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteEvents(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.Events.Include(x => x.Images).FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                ctx.Events.Remove(ns);

                return ctx.SaveChanges() > 0;
            }
        }

        private static IQueryable<Events> FilterQuery(FilterBaseDto filter, IQueryable<Events> ns)
        {
            if (!string.IsNullOrEmpty(filter.Query))
            {
                switch (filter.Field)
                {
                    case FieldsEnum.NoField:
                        ns = ns.Where(x => x.Title.Contains(filter.Query) || x.ShortDescription.Contains(filter.Query));
                        break;
                    case FieldsEnum.Title:
                        ns = ns.Where(x => x.Title.Contains(filter.Query));
                        break;
                    case FieldsEnum.ShortDescription:
                        ns = ns.Where(x => x.ShortDescription.Contains(filter.Query));
                        break;
                    case FieldsEnum.Description:
                        ns = ns.Where(x => x.Description.Contains(filter.Query));
                        break;
                    case FieldsEnum.Author:
                        ns = ns.Where(x => x.Author.Contains(filter.Query));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            switch (filter.OrderByField)
            {
                case OrderByFieldsEnum.NoField:
                    ns = filter.OrderByDescending == null || filter.OrderByDescending == true
                        ? ns.OrderByDescending(x => x.CreationDate)
                        : ns.OrderBy(x => x.CreationDate);
                    break;
                case OrderByFieldsEnum.Title:
                    ns = filter.OrderByDescending == true
                        ? ns.OrderByDescending(x => x.Title)
                        : ns.OrderBy(x => x.Title);
                    break;
                case OrderByFieldsEnum.CreationDate:
                    ns = filter.OrderByDescending == null || filter.OrderByDescending == true
                        ? ns.OrderByDescending(x => x.CreationDate)
                        : ns.OrderBy(x => x.CreationDate);
                    break;
                case OrderByFieldsEnum.LastUpdateDate:
                    ns = filter.OrderByDescending == null || filter.OrderByDescending == true
                        ? ns.OrderByDescending(x => x.LastUpdateDate)
                        : ns.OrderBy(x => x.LastUpdateDate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ns;
        }

    }
}
