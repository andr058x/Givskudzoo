using System;
using System.Collections.Generic;
using GivskudZoo.Dal;
using GivskudZoo.Model;
using System.Data.Entity;
using System.Linq;

namespace GivskudZoo.Bll
{
    public class NewsManager
    {
        public IEnumerable<NewsDto> GetNewsList(FilterBaseDto filter)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.News.Include(x => x.Images); //Questo equivale a select * FROM News e poi aggiungerà JOIN a Images

                ns = FilterQuery(filter, ns);

                var mapped = AutoMapper.Mapper.Map<IEnumerable<News>, IEnumerable<NewsDto>>(ns);

                return mapped;
            }
        }

        public NewsDto GetNews(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.News.Include(x => x.Images).FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                var mapped = AutoMapper.Mapper.Map<News, NewsDto>(ns);

                return mapped;
            }
        }

        public bool AddNews(NewsDto news)
        {
            var mapped = AutoMapper.Mapper.Map<NewsDto, News>(news);

            using (var ctx = new Entities())
            {
                mapped.CreationDate = DateTime.Now;
                mapped.LastUpdateDate = DateTime.Now;

                ctx.News.Add(mapped);

                return ctx.SaveChanges() > 0;
            }
        }

        public bool PutNews(NewsDto news)
        {
            var mapped = AutoMapper.Mapper.Map<NewsDto, News>(news);

            using (var ctx = new Entities())
            {
                var ns = ctx.News.Include(x => x.Images).FirstOrDefault(x => x.Id == news.Id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {news.Id}");

                AutoMapper.Mapper.Map(mapped, ns);

                if (news.DeleteImage)
                {
                    ns.Images = null;
                    ns.ImageId = null;
                }

                if (news.Image != null)
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

        public bool DeleteNews(int id)
        {
            using (var ctx = new Entities())
            {
                var ns = ctx.News.Include(x => x.Images).FirstOrDefault(x => x.Id == id);

                if (ns == null)
                    throw new KeyNotFoundException($"Non trovata entità con id {id}");

                ctx.News.Remove(ns);

                return ctx.SaveChanges() > 0;
            }
        }

        private static IQueryable<News> FilterQuery(FilterBaseDto filter, IQueryable<News> ns)
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
