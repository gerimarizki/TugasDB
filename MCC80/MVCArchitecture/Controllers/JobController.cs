using MVCArchitecture.Models;
using MVCArchitecture.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCArchitecture.Controllers
{
    public class JobController
    {
        private Job _jobModel;
        private VJob _jobView;

        public JobController(Job jobModel, VJob jobView)
        {
            _jobModel = jobModel;
            _jobView = jobView;
        }

        public void GetAll()
        {
            var regionResult = _jobModel.GetAll();
            if (regionResult.Count() is 0)
            {
                _jobView.DataEmpty();
            }
            else
            {
                _jobView.GetAll(regionResult);
            }
        }

        public void Insert()
        {
            var region = _jobView.InsertMenu();

            var result = _jobModel.Insert(region);
            switch (result)
            {
                case -1:
                    _jobView.DataEmpty();
                    break;
                case 0:
                    _jobView.Fail();
                    break;
                default:
                    _jobView.Success();
                    break;
            }
        }

        public void Update()
        {
            var region = _jobView.UpdateMenu();
            var result = _jobModel.Update(region);

            switch (result)
            {
                case -1:
                    _jobView.DataEmpty();
                    break;
                case 0:
                    _jobView.Fail();
                    break;
                default:
                    _jobView.Success();
                    break;
            }
        }

        public void Delete()
        {
            var region = _jobView.DeleteMenu();
            var result = _jobModel.Delete(region);

            switch (result)
            {
                case -1:
                    _jobView.DataEmpty();
                    break;
                case 0:
                    _jobView.Fail();
                    break;
                default:
                    _jobView.Success();
                    break;
            }

        }
    }
}
