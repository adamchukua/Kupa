using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class EventCommentProfile : Profile
    {
        public EventCommentProfile()
        {
            CreateMap<CommentDto, EventComment>();
        }
    }
}
