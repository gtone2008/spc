using SPI_PCC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPI_PCC.BLL
{
    public class Model
    {
        private ModelInfo _info { get; set; }
        private DAL.Model _dal = new DAL.Model();

        public Model(ModelInfo info)
        {
            this._info = info;
            _dal.Info = info;
        }

        public ModelInfo Get()
        {
            if (!_dal.IsExists())
            {
                throw new Exception("can not find model[" + _info.ModelName + "],BoardType[" + _info.BoardType + "]");
            }
            else
            {
                ModelInfo model = _dal.Get();
                if (model == null)
                {
                    _dal.Insert();
                    model = _dal.Get();
                }
                return model;
            }
        }

        public int Update()
        {
            return _dal.Update();
        }
    }
}
