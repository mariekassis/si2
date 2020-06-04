using AutoMapper;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Requests.University;
using si2.bll.Dtos.Results.University;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.PagedList;
using si2.dal.Entities;
using si2.bll.Dtos.Requests.Book;
using si2.bll.Dtos.Results.Book;
using si2.bll.Dtos.Requests.Category;
using si2.bll.Dtos.Results.Category;

namespace si2.bll.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDataflowDto, Dataflow>();
            CreateMap<UpdateDataflowDto, Dataflow>();
            CreateMap<Dataflow, DataflowDto>();
            CreateMap<Dataflow, UpdateDataflowDto>();

            //Mapping University Object
            CreateMap<CreateUniversityDto, University>();
            CreateMap<UpdateUniversityDto, University>();
            CreateMap<University, UniversityDto>();
            CreateMap<University, UpdateUniversityDto>();

            //Mapping Institution Object
            CreateMap<CreateInstitutionDto, Institution>();
            CreateMap<UpdateInstitutionDto, Institution>();
            CreateMap<Institution, InstitutionDto>();
            CreateMap<Institution, UpdateInstitutionDto>();

            //Mapping Book Object
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<Book, UpdateBookDto>();

            //Mapping Category Object
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, UpdateCategoryDto>();
        }
    }
}
