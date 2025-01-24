using AutoMapper;
using RegistroDeHoras.Model;
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TarefaViewModel, Tarefa>();

        CreateMap<Tarefa, TarefaViewModel>();
    }
}