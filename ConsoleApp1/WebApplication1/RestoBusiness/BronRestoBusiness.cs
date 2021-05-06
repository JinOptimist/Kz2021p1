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
        Random random = new Random();
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
            var restmod = _restoransRepository.GetByName(model.Name);
            var bronrestModel = MapResto.Map<BronResto>(model);
            bronrestModel.Restoranses = restmod;
            bronrestModel.BronRespNumber = NewBronRespNumber();
            return bronrestModel;
        }

        private int NewBronRespNumber()
        {
            return random.Next(1000,1500);
        }
    }
}
