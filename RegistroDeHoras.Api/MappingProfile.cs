using AutoMapper;
using RegistroDeHoras.Model;
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamento de TarefaViewModel para Tarefa
        CreateMap<TarefaViewModel, Tarefa>()
            .ForMember(dest => dest.Pausas, opt => opt.MapFrom(src => src.Pausas));

        // Mapeamento de Tarefa para TarefaViewModel
        CreateMap<Tarefa, TarefaViewModel>()
            .ForMember(dest => dest.HorasUtilizadas, opt => opt.MapFrom(src => src.HorasUtilizadas))
            .ForMember(dest => dest.HorasDePausa, opt => opt.MapFrom(src => src.HorasDePausa)); 
    }
}