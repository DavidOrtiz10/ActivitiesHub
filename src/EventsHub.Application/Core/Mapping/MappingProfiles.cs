using EventsHub.Domain;

namespace EventsHub.Application.Core.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Event, Event>();
    }
}
