using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, ReCapProjectContext>, ICarImageDal
    {

        public bool IsExist(int id)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                return context.CarImages.Any(p => p.ImgId == id);
            }
        }
    }
}
