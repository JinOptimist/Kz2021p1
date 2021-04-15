using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.RestoBusiness
{

    public class BronRestoBusiness
    {
        private int number = 10000;
        private BronRestoRepository _bronRestoRepository;
        private RestoransRepository _restoransRepository;
        private IMapper MapResto { get; set; }
        public BronRestoBusiness(RestoransRepository restoransRepository, IMapper mapper, BronRestoRepository bronRestoRepository)
        {
            _restoransRepository = restoransRepository;
            MapResto = mapper;
            _bronRestoRepository = bronRestoRepository;
        }
        public BronResto BronR(BronViewModel model)
        {
            int num = _bronRestoRepository.GetAll().Count;
            var restmod = _restoransRepository.GetByName(model.Name);
            var bronrestModel = MapResto.Map<BronResto>(model);
            bronrestModel.ObjectResto = restmod;
            bronrestModel.BronRespNumber = NewBronRespNumber(num);
            return bronrestModel;
        }

        private int NewBronRespNumber(int numcount)
        {
            return number+ numcount;
        }
    }
}
