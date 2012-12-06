using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories.Abstract;

namespace Varldsklass.Domain.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        List<Event> FindByBooker(int bookerId);
    }

    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository() : base() { }

        public List<Event> FindByBooker(int bookerId)
        {
            // Populate list
            List<Event> foundEvents = new List<Event>();
            var allEvents = FindAll().ToList();
            allEvents.ForEach(delegate(Event e)
            {
                e.Attendants.ToList().ForEach(delegate(Attendant a)
                {
                    if (a.BookerID == bookerId)
                    {
                        foundEvents.Add(e);
                    }
                });
            });

            // Return only one of each event
            return foundEvents.Distinct().OrderBy(e => e.StartDate).ToList();
        }
    }
}
