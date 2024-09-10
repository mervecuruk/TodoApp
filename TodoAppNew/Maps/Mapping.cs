using AutoMapper;
using TodoAppNew.Models;
using TodoAppNew.Models.VMs;

namespace TodoAppNew.Maps
{
    public class Mapping:Profile
    {

        public Mapping()
        {
            CreateMap<ToDoItem, ToDoListVM>().ReverseMap();
            //CreateMap<ToDoItem, CompletedToDoItem>().ReverseMap();    //çalışmadı

        }
    }
}
