using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MVCArchitecture.Views.VLinq;

namespace MVCArchitecture.Controllers
{

    public class LinqController
    {
        private Linq _linqModel;
        private VLinq _vLinqView;

        public LinqController(Linq linqModel, VLinq vLinq)
        {
            _linqModel = linqModel;
            _vLinqView = vLinq;
        }

        public void GetAll()
        {


            var result = _linqModel.GetAll();
            if (result.Count == 0)
            {
                _vLinqView.EmployeeNotFound();
            }
            else
            {
                _vLinqView.GetAll(result);
            }


        }
    }
}
